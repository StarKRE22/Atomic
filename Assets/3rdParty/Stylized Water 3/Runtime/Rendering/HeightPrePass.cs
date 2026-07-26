// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

#if URP
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace StylizedWater3
{
    public class HeightPrePass : ScriptableRenderPass
    {
        private const string profilerTag = "Water Height Prepass";
        private static readonly ProfilingSampler profilerSampler = new ProfilingSampler(profilerTag);

        /// <summary>
        /// Using this as a value comparison in shader code to determine if not water is being hit
        /// </summary>
        public const float VOID_THRESHOLD = -1000f;
        private Color targetClearColor = new Color(VOID_THRESHOLD, 0, 0, 0);
        
        [Serializable]
        public class Settings
        {
            public bool enable = true;

            public float range = 500f;
            
            [Range(1f, 8f)]
            public int cellsPerUnit = 4;

            public int maxResolution = 4096;

            /// <summary>
            /// Returns the enabled state, either from the settings or if forced because it is required by other functionality
            /// </summary>
            public bool isEnabled => enable || StylizedWaterRenderFeature.RequireHeightPrePass || HeightQuerySystem.RequiresHeightPrepass;
        }
        
        //Render pass
        FilteringSettings m_FilteringSettings;
        RenderStateBlock m_RenderStateBlock;
        private readonly List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>()
        {
            new (ShaderParams.LightModes.WaterHeight)
        };

        public HeightPrePass()
        {
            m_FilteringSettings = new FilteringSettings(RenderQueueRange.all, LayerMask.GetMask("Water"));
            m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
        }
        
        public const string BufferName = "_WaterHeightBuffer";
        public static readonly int _WaterHeightBuffer = Shader.PropertyToID(BufferName);
        private const string CoordsName = "_WaterHeightCoords";
        private static readonly int _WaterHeightCoords = Shader.PropertyToID(CoordsName);
        public static readonly int _WaterHeightPrePassAvailable = Shader.PropertyToID("_WaterHeightPrePassAvailable");

        private int resolution;
        private int m_resolution;
        
        private Settings settings;
        
        private RendererListParams rendererListParams;
        private RendererList rendererList;

        public sealed class RenderTargetDebugContext : RenderTargetDebugger.RenderTarget
        {
            public RenderTargetDebugContext()
            {
                this.name = "Height Buffer";
                this.description = "Water geometry height (red channel). Height displacement from effects (green channel)." +
                                   "\n\nUsed by: Water decals, GPU-based buoyancy";
                this.textureName = BufferName;
                this.propertyID = _WaterHeightBuffer;
                this.order = 0;
            }
        }
        
        public void Setup(Settings settings)
        {
            this.settings = settings;
            
            resolution = PlanarProjection.CalculateResolution(settings.range, settings.cellsPerUnit, 16, settings.maxResolution);

        }

        public class FrameData : ContextItem
        {
            public TextureHandle _WaterHeightBuffer;

            public override void Reset()
            {
                _WaterHeightBuffer = TextureHandle.nullHandle;
            }
        }

        private class PassData
        {
            public RendererListHandle rendererListHandle;
            public TextureHandle renderTarget;

            public PlanarProjection planarProjection;
            public Vector4 rendererCoords;
        }
        
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameContext)
        {
            var renderingData = frameContext.Get<UniversalRenderingData>();
            var cameraData = frameContext.Get<UniversalCameraData>();
            var lightData = frameContext.Get<UniversalLightData>();
            
            DrawingSettings drawingSettings = CreateDrawingSettings(m_ShaderTagIdList, renderingData, cameraData, lightData, SortingCriteria.RenderQueue | SortingCriteria.SortingLayer | SortingCriteria.CommonTransparent);
            drawingSettings.perObjectData = PerObjectData.None;

            rendererListParams.cullingResults = renderingData.cullResults;
            rendererListParams.drawSettings = drawingSettings;
            rendererListParams.filteringSettings = m_FilteringSettings;
            
            using(var builder = renderGraph.AddRasterRenderPass<PassData>("Water Height Pre-pass", out var passData))
            {
                //Render target
                RenderTextureDescriptor renderTargetDescriptor = new RenderTextureDescriptor(resolution, resolution, GraphicsFormat.R16G16_SFloat, 0);
                passData.renderTarget = UniversalRenderer.CreateRenderGraphTexture(renderGraph, renderTargetDescriptor, BufferName, true, FilterMode.Bilinear, TextureWrapMode.Clamp);
                //Store render target in RG, so it can be retrieved in other passes
                FrameData frameData = frameContext.GetOrCreate<FrameData>();
                frameData._WaterHeightBuffer = passData.renderTarget;
                //Mark the texture as readable
                //builder.UseTexture(passData.renderTarget, AccessFlags.ReadWrite);
                
                #if UNITY_EDITOR || DEVELOPMENT_BUILD
                if (RenderTargetDebugger.InspectedProperty == _WaterHeightBuffer)
                {
                    StylizedWaterRenderFeature.DebugData debugData = frameContext.Get<StylizedWaterRenderFeature.DebugData>();
                    debugData.currentHandle = passData.renderTarget;
                }
                #endif
                
                passData.rendererListHandle = renderGraph.CreateRendererList(rendererListParams);

                passData.planarProjection = new PlanarProjection
                {
                    center = cameraData.camera.transform.position,
                    scale = settings.range,
                    offset = cameraData.camera.transform.forward * ((settings.range * 0.5f) - 5),
                    resolution = resolution
                };
                passData.planarProjection.Recalculate();
                
                passData.planarProjection.SetUV(ref passData.rendererCoords);

                //Set render target and bind to global property
                builder.SetRenderAttachment(passData.renderTarget, 0, AccessFlags.Write);
                //builder.CreateTransientTexture(passData.renderTarget);
                builder.SetGlobalTextureAfterPass(passData.renderTarget, _WaterHeightBuffer);
                
                builder.UseRendererList(passData.rendererListHandle);
                builder.AllowGlobalStateModification(true);
                builder.AllowPassCulling(false);

                builder.SetRenderFunc((PassData data, RasterGraphContext context) =>
                {
                    Execute(context, data);
                });
            }
        }

        private void Execute(RasterGraphContext context, PassData data)
        {
            var cmd = context.cmd;
            using (new ProfilingScope(cmd, profilerSampler))
            {
                cmd.ClearRenderTarget(true, true, targetClearColor);
                
                cmd.SetGlobalInt(_WaterHeightPrePassAvailable, 1);
                cmd.EnableShaderKeyword(ShaderParams.Keywords.WaterHeightPass);
                
                cmd.SetViewProjectionMatrices(data.planarProjection.view, data.planarProjection.projection);
                
                cmd.SetViewport(data.planarProjection.viewportRect);
                //Is this still needed?
                //cmd.SetGlobalMatrix("UNITY_MATRIX_V", data.view);

                cmd.SetGlobalVector(_WaterHeightCoords, data.rendererCoords);

                cmd.DrawRendererList(data.rendererListHandle);
                
                //Reset (breaks VR!)
                //cmd.SetViewProjectionMatrices(data.ViewMatrix, data.GPUProjectionMatrix);
            }
        }

        public void Dispose()
        {
            Shader.SetGlobalVector(_WaterHeightCoords, Vector4.zero);
            Shader.SetGlobalInt(_WaterHeightPrePassAvailable, 0);
            Shader.DisableKeyword(ShaderParams.Keywords.WaterHeightPass);
        }
        
#pragma warning disable CS0672
#pragma warning disable CS0618
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) { }
#pragma warning restore CS0672
#pragma warning restore CS0618
    }
}
#endif
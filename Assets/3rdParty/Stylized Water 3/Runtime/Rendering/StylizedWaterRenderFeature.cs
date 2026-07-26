// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define DEBUG_AVAILABLE
#endif

#if URP
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace StylizedWater3
{
    [DisallowMultipleRendererFeature("Stylized Water 3")]
    public partial class StylizedWaterRenderFeature : ScriptableRendererFeature
    {
        [Tooltip("Render all transparent materials NOT on the \"Water\" layer into a screen-space texture, and use that for refraction rendering in the water." +
                 "\n\n" +
                 "If enabled, transparent materials can be submerged and refracted correctly")]
        public bool transparencyRefraction;
        
        public static StylizedWaterRenderFeature GetDefault()
        {
            return (StylizedWaterRenderFeature)PipelineUtilities.GetRenderFeature<StylizedWaterRenderFeature>();
        }
        
        [Serializable]
        public class ScreenSpaceReflectionSettings
        {
            [FormerlySerializedAs("enable")]
            [Tooltip("Allow SSR to be rendered in water materials that have it enabled." +
                     "\n\nDisable as a global performance scaling measure")]
            public bool allow = true;
        }
        public ScreenSpaceReflectionSettings screenSpaceReflectionSettings = new ScreenSpaceReflectionSettings();
        
        [FormerlySerializedAs("directionalCaustics")]
        [Tooltip("Pass on the main directional light's projection onto the water shader. Allows caustics to project along its direction (rather than top-down)." +
                 "\n\nThis shading operation is relative expensive, as it involves analyzing the depth texture at 4 different points")]
        public bool allowDirectionalCaustics;
        
        public HeightPrePass.Settings heightPrePassSettings = new HeightPrePass.Settings();
        #if SWS_DEV
        public TerrainHeightPrePass.Settings terrainHeightPrePassSettings = new TerrainHeightPrePass.Settings();
        #endif
        
        private SetupConstants constantsSetup;
        private HeightPrePass heightPrePass;
        private HeightQuerySystem.RenderPass heightQueryPass;
        #if SWS_DEV
        private TerrainHeightPrePass terrainHeightPrePass;
        private DistanceFieldPass distanceFieldPass;
        private RenderTransparentTexture transparentTexturePass;
        #endif

        #if DEBUG_AVAILABLE
        private DebugInspectorPass debugInspectorPass;
        #endif
        
        [SerializeField]
        public ComputeShader heightReadbackCS;
        [SerializeField]
        public Shader heightProcessingShader;

        /// <summary>
        /// Set this to true from a render pass if it requires the displacement pre-pass, despite it being disabled in the render feature settings.
        /// </summary>
        public static bool RequireHeightPrePass;
        
        private void OnValidate()
        {
            VerifyReferences();
        }

        private void Reset()
        {
            VerifyReferences();
        }
        
        private void VerifyReferences()
        {
            if(!heightReadbackCS) heightReadbackCS = (ComputeShader)Resources.Load("HeightSampler");
            if(!heightProcessingShader) heightProcessingShader = Shader.Find(ShaderParams.ShaderNames.HeightProcessor);
            
            #if SWS_DEV
            if(!terrainHeightPrePassSettings.terrainHeightVisualizationShader) terrainHeightPrePassSettings.terrainHeightVisualizationShader = Shader.Find(ShaderParams.ShaderNames.TerrainHeight);
            #endif
        }
        
        public class DebugData : ContextItem
        {
            public TextureHandle currentHandle;
            
            public override void Reset()
            {
                currentHandle = TextureHandle.nullHandle;
            }
        }
        
        public override void Create()
        {
            constantsSetup = new SetupConstants
            {
                renderPassEvent = defaultInjectionPoint
            };

            heightPrePass = new HeightPrePass
            {
                renderPassEvent = defaultInjectionPoint
            };

            heightQueryPass = new HeightQuerySystem.RenderPass()
            {
                renderPassEvent = defaultInjectionPoint
            };

            #if SWS_DEV
            terrainHeightPrePass = new TerrainHeightPrePass()
            {
                renderPassEvent = defaultInjectionPoint
            };
            
            if (transparencyRefraction)
            {
                transparentTexturePass = new RenderTransparentTexture();
                transparentTexturePass.renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;
            }
            #endif
            
            CreateFlowMapPass();
            CreateDynamicEffectsPasses();
            CreateUnderwaterRenderingPasses();

            #if DEBUG_AVAILABLE
            debugInspectorPass = new DebugInspectorPass();
            debugInspectorPass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
            #endif
        }

        //Note: Actually prefer to render before transparents, but this creates a recursive RenderSingleCamera call
        //Restoring the view/projection to that of the camera also breaks VR. Required functions are internal URP code
        private readonly RenderPassEvent defaultInjectionPoint = RenderPassEvent.BeforeRendering;
        
        //Dynamic Effects
        partial void CreateDynamicEffectsPasses();
        partial void AddDynamicEffectsPasses(ScriptableRenderer renderer, ref RenderingData renderingData);
        partial void DisposeDynamicEffectsPasses();
        
        partial void CreateUnderwaterRenderingPasses();
        partial void AddUnderwaterRenderingPasses(ScriptableRenderer renderer, ref RenderingData renderingData);
        partial void DisposeUnderwaterRenderingPasses();
        
        partial void CreateFlowMapPass();
        partial void AddFlowMapPass(ScriptableRenderer renderer, ref RenderingData renderingData);
        partial void DisposeFlowMapPass();

        private bool IsInvalidContext(ref RenderingData renderingData)
        {
            var currentCam = renderingData.cameraData.camera;

            //Skip for any special use camera's (except scene view camera)
            if (currentCam.cameraType != CameraType.SceneView && (currentCam.cameraType == CameraType.Reflection || currentCam.cameraType == CameraType.Preview || currentCam.hideFlags != HideFlags.None))
            {
                return true;
            }

            //Skip overlay cameras
            if (renderingData.cameraData.renderType == CameraRenderType.Overlay)
            {
                return true;
            }

            return false;
        }
        
        private bool isMainCamera(Camera camera)
        {
            //Skip for any special use camera's (except scene view camera)
            return (camera.cameraType == CameraType.SceneView || camera.CompareTag("MainCamera"));
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if(IsInvalidContext(ref renderingData)) return;
            
            constantsSetup.Setup(this);
            renderer.EnqueuePass(constantsSetup);

            #if SWS_DEV
            if (terrainHeightPrePassSettings.enable)
            {
                terrainHeightPrePass.Setup(terrainHeightPrePassSettings);
                renderer.EnqueuePass(terrainHeightPrePass);
            }
            
            if (transparencyRefraction)
            {
                renderer.EnqueuePass(transparentTexturePass);
            }
            #endif
            
            AddFlowMapPass(renderer, ref renderingData);
            AddDynamicEffectsPasses(renderer, ref renderingData);
            
            if (RequireHeightPrePass || heightPrePassSettings.enable || HeightQuerySystem.RequiresHeightPrepass)
            {
                heightPrePass.Setup(heightPrePassSettings);
                renderer.EnqueuePass(heightPrePass);

                if (HeightQuerySystem.QueryCount > 0)
                {
                    heightQueryPass.Setup(heightReadbackCS);
                    renderer.EnqueuePass(heightQueryPass);
                }
            }
            else
            {
                Shader.SetGlobalInt(HeightPrePass._WaterHeightPrePassAvailable, 0);
            }
            
            AddUnderwaterRenderingPasses(renderer, ref renderingData);
            
            #if DEBUG_AVAILABLE
            if (RenderTargetDebugger.InspectedProperty > 0) renderer.EnqueuePass(debugInspectorPass);
            #endif
        }

        private void OnDisable()
        {
            constantsSetup.Dispose();
            heightPrePass.Dispose();
            heightQueryPass.Dispose();
            #if SWS_DEV
            terrainHeightPrePass.Dispose();
            #endif
            
            DisposeFlowMapPass();
            DisposeDynamicEffectsPasses();
            DisposeUnderwaterRenderingPasses();
        }

        #if DEBUG_AVAILABLE
        private class DebugInspectorPass : ScriptableRenderPass
        {
            public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
            {
                DebugData debugData = frameData.Get<DebugData>();
                
                //Whichever pass's render target PropertyID matches the selected one in the inspector window get assigned as the 'currentHandle'
                if (debugData.currentHandle.IsValid())
                {
                    var destinationDesc = renderGraph.GetTextureDesc(debugData.currentHandle);
                    destinationDesc.clearBuffer = false;

                    RenderTextureDescriptor rtDsc = new RenderTextureDescriptor
                    {
                        width = destinationDesc.width,
                        height = destinationDesc.height,
                        graphicsFormat = destinationDesc.format,
                        volumeDepth = 1,
                        dimension = destinationDesc.dimension,
                        useMipMap = destinationDesc.useMipMap,
                        msaaSamples = 1
                    };

                    TextureDesc textureDesc = debugData.currentHandle.GetDescriptor(renderGraph);
                    RenderingUtils.ReAllocateHandleIfNeeded(ref RenderTargetDebugger.CurrentRT, rtDsc, textureDesc.filterMode, textureDesc.wrapMode, textureDesc.anisoLevel, textureDesc.mipMapBias, textureDesc.name);

                    TextureHandle destination = renderGraph.ImportTexture(RenderTargetDebugger.CurrentRT);

                    //Copy TextureHandle into persistent RT
                    renderGraph.AddCopyPass(debugData.currentHandle, destination, passName: "Water Debug");
                }
                else
                {
                    RenderTargetDebugger.CurrentRT = null;
                }
            }

#pragma warning disable CS0672
#pragma warning disable CS0618
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) { }
#pragma warning restore CS0672
#pragma warning restore CS0618
        }
        #endif

        public static void VerifySetup(string requesterName = null)
        {
            #if UNITY_EDITOR && URP
            if (Application.isPlaying == false)
            {
                if (PipelineUtilities.RenderFeatureAdded<StylizedWaterRenderFeature>() == false)
                {
                    string requesterString = requesterName != null ? $" by \"{requesterName}\" " : " ";
                    
                    if (UnityEditor.EditorUtility.DisplayDialog("Stylized Water 3", $"The Stylized Water 3 render feature hasn't been added to the default renderer" +
                                                                                    $"\n\n" +
                                                                                    $"This is required{requesterString}for certain rendering to take effect.", "Setup", "Ignore for now"))
                    {
                        PipelineUtilities.SetupRenderFeature<StylizedWaterRenderFeature>(name:"Stylized Water 3");
                    }
                }
            }
            #endif
        }
    }
}
#endif
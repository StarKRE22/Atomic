// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using UnityEngine;
using UnityEngine.Rendering;
#if URP
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.Rendering.RenderGraphModule;

namespace StylizedWater3
{
    public class SetupConstants : ScriptableRenderPass
    {
        private ProfilingSampler m_ProfilingSampler;
        
        private static readonly int _CausticsProjectionAvailable = Shader.PropertyToID("_CausticsProjectionAvailable");
        private static readonly int CausticsProjection = Shader.PropertyToID("CausticsProjection");
        private static readonly int _WaterSSRAllowed = Shader.PropertyToID("_WaterSSRAllowed");

        private static VisibleLight mainLight;
        private Matrix4x4 causticsProjection;

        public SetupConstants()
        {
            //Force a unit scale, otherwise affects the projection tiling of the caustics
            causticsProjection = Matrix4x4.Scale(Vector3.one);
        }

        private StylizedWaterRenderFeature settings;
        
        public void Setup(StylizedWaterRenderFeature renderFeature)
        {
            this.settings = renderFeature;
        }
        
        private class PassData
        {
            public bool directionalCaustics;
            public Matrix4x4 causticsProjection;
            public bool ssr;
            public UniversalCameraData cameraData;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            //UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
            UniversalLightData lightData = frameData.Get<UniversalLightData>();
            UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            frameData.GetOrCreate<StylizedWaterRenderFeature.DebugData>();
            #endif
            
            using (var builder = renderGraph.AddRasterRenderPass<PassData>("Water Constants", out var passData, m_ProfilingSampler))
            {
                passData.ssr = settings.screenSpaceReflectionSettings.allow;
                passData.directionalCaustics = settings.allowDirectionalCaustics;
            
                if (passData.directionalCaustics)
                {
                    //When no lights are visible, main light will be set to -1.
                    if (lightData.mainLightIndex > -1)
                    {
                        mainLight = lightData.visibleLights[lightData.mainLightIndex];

                        if (mainLight.lightType == LightType.Directional)
                        {
                            causticsProjection = Matrix4x4.Rotate(mainLight.light.transform.rotation);

                            passData.causticsProjection = causticsProjection.inverse;
                            passData.cameraData = cameraData;
                        }
                        else
                        {
                            passData.directionalCaustics = false;
                        }
                    }
                    else
                    {
                        passData.directionalCaustics = false;
                    }
                }
                
                //Pass should always execute
                builder.AllowPassCulling(false);

                //if(passData.ssr) builder.UseTexture(resourceData.cameraOpaqueTexture, AccessFlags.Read);
                if (passData.ssr || passData.directionalCaustics)
                {
                    builder.UseTexture(resourceData.cameraDepthTexture, AccessFlags.Read);
                }

                builder.AllowGlobalStateModification(true);
                builder.SetRenderFunc((PassData data, RasterGraphContext rgContext) =>
                {
                    Execute(rgContext.cmd, data);
                });
            }
        }
        
        //RG
        static void Execute(RasterCommandBuffer cmd, PassData data)
        {
            //Debug.Log("ExecutePass");
            
            cmd.SetGlobalInt(_WaterSSRAllowed, data.ssr ? 1 : 0);
            cmd.SetGlobalInt(_CausticsProjectionAvailable, data.directionalCaustics ? 1 : 0);

            if (data.directionalCaustics && data.cameraData != null) //Remove camera data null check when removing non-RG fallback
            {
                cmd.SetGlobalMatrix(CausticsProjection, data.causticsProjection);
                
                //Sets up the required View- -> Clip-space matrices
                NormalReconstruction.SetupProperties(cmd, data.cameraData);
            }
        }
        
        //Non-RG fallback
        #pragma warning disable CS0672
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get();
            
            PassData passData = new PassData
            {
                ssr = settings.screenSpaceReflectionSettings.allow,
                directionalCaustics = settings.allowDirectionalCaustics
            };

            //UniversalLightData and LightData classes are identical, yet not interchangable. Simplified passing of sun-light projection as a fallback
            if (passData.directionalCaustics && RenderSettings.sun)
            {
                passData.causticsProjection = Matrix4x4.Rotate(RenderSettings.sun.transform.rotation).inverse;
            }
            
            using (new ProfilingScope(cmd, m_ProfilingSampler))
            {
                Execute(CommandBufferHelpers.GetRasterCommandBuffer(cmd), passData);
            }
            
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
        #pragma warning restore CS0672

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.SetGlobalInt(_CausticsProjectionAvailable, 0);
            cmd.SetGlobalInt(_WaterSSRAllowed, 0);
        }

        public void Dispose()
        {
            
        }
    }
}
#endif
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace StylizedWater3
{
    public static partial class HeightQuerySystem
    {
        public class RenderPass : ScriptableRenderPass
        {
            public const int THREAD_GROUPS = 64; //Value must be mirrored in compute shader
            
            private const string PROFILER_PREFIX = "[GPU] Water Height Query:";
            
            private static readonly ProfilingSampler computeProfilerSampler = new ProfilingSampler( $"{PROFILER_PREFIX} Dispatch");
            private static readonly ProfilingSampler readbackAsyncProfilerSampler = new ProfilingSampler( $"{PROFILER_PREFIX} Readback Async");

            private ComputeShader cs;
            private int kernel;
            
            public void Setup(ComputeShader heightReadbackCs)
            {
                if (!heightReadbackCs)
                {
                    throw new Exception("[Stylized Water 3] Height query render pass initialized with an empty compute shader reference. Was it deleted from the project? Or not referenced on the render feature?");
                }
                
                this.cs = heightReadbackCs;
                this.kernel = cs.FindKernel("SampleOffsets");
            }
            
            private class PassData
            {
                // Compute shader.
                public ComputeShader cs;
                public int kernel;
                
                // Buffer handles for the compute buffers.
                public GraphicsBuffer[] input;
                public GraphicsBuffer[] output;
            }

            public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameContext)
            {
                if (HeightQuerySystem.IsSupported() == false)
                {
                    return;
                }
                
                HeightPrePass.FrameData heightPrePassData = frameContext.Get<HeightPrePass.FrameData>();
                
                bool cull = HeightQuerySystem.QueryCount == 0;
                
                using(var builder = renderGraph.AddComputePass($"Water Height Query Sampling", out PassData passData))
                {
                    passData.cs = this.cs;
                    passData.kernel = this.kernel;

                    passData.input = new GraphicsBuffer[HeightQuerySystem.QueryCount];
                    passData.output = new GraphicsBuffer[HeightQuerySystem.QueryCount];
                    
                    for (int i = 0; i < HeightQuerySystem.QueryCount; i++)
                    {
                        passData.input[i] = HeightQuerySystem.queries[i].inputPositionBuffer;
                        BufferHandle inputBufferHandle = renderGraph.ImportBuffer(passData.input[i]);
                        builder.UseBuffer(inputBufferHandle);
                        
                        passData.output[i] = HeightQuerySystem.queries[i].outputOffsetsBuffer;
                        BufferHandle outputBufferHandle = renderGraph.ImportBuffer(passData.output[i]);
                        builder.UseBuffer(outputBufferHandle);
                    }

                    //Input
                    builder.UseTexture(heightPrePassData._WaterHeightBuffer);

                    builder.AllowPassCulling(cull);
                    builder.SetRenderFunc((PassData data, ComputeGraphContext cgContext) => ExecuteSampling(data, cgContext));
                }

                using(var builder = renderGraph.AddUnsafePass("Water Height Query: Async Readback", out PassData passData))
                {
                    builder.EnableAsyncCompute(true);
                    //builder.UseTexture(heightPrePassData._WaterHeightBuffer);
                    
                    builder.AllowPassCulling(cull);
                    builder.SetRenderFunc((PassData data, UnsafeGraphContext cgContext) => ExecuteReadback(data, cgContext));
                }
            }
            
            private void ExecuteSampling(PassData data, ComputeGraphContext context)
            {
                var cmd = context.cmd;
                using (new ProfilingScope(cmd, computeProfilerSampler))
                {
                    foreach (var q in HeightQuerySystem.queries)
                    {
                        q.Dispatch(cmd, data.cs, data.kernel);
                    }
                }
            }
            
            //Pass using "cmd.RequestAsyncReadbackIntoNativeArray"
            private void ExecuteReadback(PassData data, UnsafeGraphContext context)
            {
                var cmd = context.cmd;
                
                using (new ProfilingScope(cmd, readbackAsyncProfilerSampler))
                {
                    foreach (var q in HeightQuerySystem.queries)
                    {
                        q.Readback(cmd);
                    }
                }
            }

            #pragma warning disable CS0672
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) { }
            #pragma warning restore CS0672
   
            public void Dispose()
            {
               
            }
        }
    }
}
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using Unity.Mathematics;

namespace StylizedWater3
{
    public static partial class HeightQuerySystem
    {
        public class Query
        {
            /// <summary>
            /// Maximum allowed number of sample positions allowed within a single height query
            /// A value too high may result in decreased performance, as the data payload needed to be retrieved from the GPU becomes too large.
            /// </summary>
            public const int MAX_SIZE = 128;
            
            //Input
            private NativeArray<float3> samplePositions;
            public readonly GraphicsBuffer inputPositionBuffer;
            
            //Output
            public NativeArray<float> outputOffsets;
            public readonly GraphicsBuffer outputOffsetsBuffer;

            private int currentBufferIndex = 0;
            //Need to keep a buffer alive so it can be used the next frame
            private readonly NativeArray<float>[] readbackBuffers = new NativeArray<float>[2];
            
            //Every request is issued with an ID
            public readonly Dictionary<int, AsyncRequest> requests = new Dictionary<int, AsyncRequest>();

            //Index of the last request.
            public int sampleCount;
            private bool hasPendingRequest;

            public List<int> availableIndices;
            
            public Query()
            {
                RecreateIndexPool();
                
                //CPU input
                samplePositions = new NativeArray<float3>(MAX_SIZE, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                //GPU input
                inputPositionBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, MAX_SIZE, 3 * sizeof(float));
                inputPositionBuffer.name = "Water Height Query: Sample Positions";
                
                //GPU output
                outputOffsetsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, MAX_SIZE, sizeof(float));
                inputPositionBuffer.name = "Water Height Query: Sampled Heights";
            }
            
            public int GetNextAvailableIndex()
            {
                var index = availableIndices[0];
                
                //Remove as it is now no longer available
                availableIndices.Remove(index);
                
                return index;
            }

            public void ReleaseIndex(int index)
            {
                availableIndices.Add(index);
                
                //Sorting optimizes the array occupancy
                availableIndices.Sort();
            }
            
            private void RecreateIndexPool()
            {
                //Populate pool of available indices
                availableIndices = new List<int>();
                for (int i = 0; i < MAX_SIZE; i++)
                {
                    availableIndices.Add(i);
                }
            }

            //Combine all the positions from the requests into one list
            private void PopulateSampleList()
            {
                //Copy all the sample positions in the queue to the summed array
                foreach (KeyValuePair<int, AsyncRequest> request in requests)
                {
                    for (int i = 0; i < request.Value.indices.Count; i++)
                    {
                        int index = request.Value.indices[i];
                        
                        //NOTE: Slow! Setting items of a NativeArray is slow
                        samplePositions[index] = request.Value.sampler.positions[i];
                    }
                }
            }

            //Dispatch the compute shader. The "offsets" array will be populated based on the current GPU buffer contents.
            public void Dispatch(ComputeCommandBuffer cmd, ComputeShader cs, int kernel)
            {
                Profiler.BeginSample($"{PROFILER_PREFIX} Setup and dispatch");

                PopulateSampleList();
                
                cmd.SetBufferData(inputPositionBuffer, samplePositions);

                cmd.SetComputeBufferParam(cs, kernel, "positions", inputPositionBuffer);
                
                //Output
                cmd.SetComputeBufferParam(cs, kernel, "offsets", outputOffsetsBuffer);

                cmd.DispatchCompute(cs, kernel, RenderPass.THREAD_GROUPS, 1, 1);
                
                Profiler.EndSample();
            }
            
            void ValidateNativeBuffer(ref NativeArray<float> buffer)
            {
                if (!buffer.IsCreated || buffer.Length != MAX_SIZE)
                {
                    if (buffer.IsCreated) buffer.Dispose();
                    
                    buffer = new NativeArray<float>(MAX_SIZE, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
                }
            }
            
            public void Readback(UnsafeCommandBuffer cmd)
            {
                //Array was disposed of after readback request. Forced to recreate it
                //https://forum.unity.com/threads/asyncgpureadback-requestintonativearray-causes-invalidoperationexception-on-nativearray.1011955
                //AllocateReadbackBuffer();
                
                //Query may have been disposed, but an async readback was still pending...
                if (hasPendingRequest)
                {
                    return;
                }
                hasPendingRequest = true;
                
                //After the readback request is complete Unity will dispose of this array automatically. Possibly when 'GetData' is called.
                //Hence a swap-buffer method is employed
                ValidateNativeBuffer(ref readbackBuffers[0]);
                ValidateNativeBuffer(ref readbackBuffers[1]);
                
                NativeArray<float> nextBuffer = readbackBuffers[NextBufferIndex()];
                
#if UNITY_EDITOR
                //Unity dev: Remove when bug is fixed
                AtomicSafetyHandle ash = NativeArrayUnsafeUtility.GetAtomicSafetyHandle(nextBuffer);
                AtomicSafetyHandle.CheckReadAndThrow(ash);
                AtomicSafetyHandle.CheckDeallocateAndThrow(ash);
#endif
                
                cmd.RequestAsyncReadbackIntoNativeArray(ref nextBuffer, outputOffsetsBuffer, MAX_SIZE * outputOffsetsBuffer.stride, 0, OnCompleteReadback);
            }

            private void SwapCurrentBuffer()
            {
                currentBufferIndex = (currentBufferIndex + 1) % 2;
            }
            
            int NextBufferIndex()
            {
                //return 0;
                return (currentBufferIndex + 1) % 2;
            }

            private void OnCompleteReadback(AsyncGPUReadbackRequest asyncGPUReadbackRequest)
            {
                if (asyncGPUReadbackRequest.hasError)
                {
                    throw new Exception("Error reading GPU water height with AsyncGPUReadbackRequest.");
                }
                
                Profiler.BeginSample($"{PROFILER_PREFIX} Readback data");
                
                outputOffsets = asyncGPUReadbackRequest.GetData<float>();

                foreach (KeyValuePair<int, AsyncRequest> m_request in requests)
                {
                    AsyncRequest request = m_request.Value;
                    
                    int queryLength = request.indices.Count;

                    for (int i = 0; i < queryLength; i++)
                    {
                        //List of indices this request occupies in the query
                        int index = request.indices[i];

                        //Height value equals a void do not assign it
                        if (request.invalidateMisses && EqualsVoid(outputOffsets[index]))
                        {
                            continue;
                        }
                        
                        request.sampler.heightValues[i] = outputOffsets[index];

                        //Issue a callback event for the external scripts that issued the request
                        request.InvokeCallback();
                    }
                    
                }
                outputOffsets.Dispose();

                SwapCurrentBuffer();
                
                hasPendingRequest = false;
                
                Profiler.EndSample();
            }

            public void Clear()
            {
                foreach (KeyValuePair<int, AsyncRequest> request in requests)
                {
                    request.Value.sampler.Dispose();
                }
                requests.Clear();
                
                RecreateIndexPool();
            }
            
            public void Dispose()
            {
                Clear();
                
                //Remove itself from the list of queries
                queries.Remove(this);
                QueryCount--;
                
                //Wait before we freeing the resources
                if (hasPendingRequest) AsyncGPUReadback.WaitAllRequests();
                
                samplePositions.Dispose();
                inputPositionBuffer.Dispose();
                
                //Dispose any allocated arrays
                outputOffsetsBuffer.Dispose();
                
                if (readbackBuffers[0].IsCreated)
                    readbackBuffers[0].Dispose();

                if (readbackBuffers[1].IsCreated)
                    readbackBuffers[1].Dispose();
                
                //If the query is being disposed, whilst no readback request was pending then this array would not be allocated
                if(outputOffsets.IsCreated) outputOffsets.Dispose();
            }
        }
    }
}
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using Unity.Collections;
using Unity.Mathematics;

namespace StylizedWater3
{
    public static partial class HeightQuerySystem
    {
        /// <summary>
        /// Holds a list of sampling positions, and the returned water height values relative to them
        /// </summary>
        public class Sampler
        {
            /// <summary>
            /// Input sample positions in world-space
            /// </summary>
            public NativeArray<float3> positions;
            /// <summary>
            /// Output height values at each sampling <see cref="positions"/>
            /// </summary>
            public float[] heightValues = Array.Empty<float>();

            private int currentSampleCount;

            public void Initialize(int sampleCount, bool cpu = false)
            {
                if (cpu == false && sampleCount > Query.MAX_SIZE)
                {
                    Dispose();

                    throw new Exception($"The number of sample positions ({sampleCount}) exceeds the maximum capacity ({Query.MAX_SIZE}) of a single sampler." +
                                        $" Decrease the number of input positions, or issue multiple smaller requests");
                }

                if (positions.IsCreated)
                {
                    positions.Dispose();
                }

                //Input data
                positions = new NativeArray<float3>(sampleCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);

                //Output data
                Array.Resize(ref heightValues, sampleCount);

                currentSampleCount = sampleCount;
            }

            public void SetSamplePosition(int index, float3 value)
            {
                if (index > currentSampleCount)
                {
                    throw new Exception($"Index out of range. This sampler was initialized with {currentSampleCount} number of samples. Dispose() and Initialize() the sampler to increase the number of samples!");
                }

                positions[index] = value;
            }

            public void Dispose()
            {
                positions.Dispose();
            }
        }
    }
}
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Profiling;
using Unity.Mathematics;

namespace StylizedWater3
{
    public static partial class HeightQuerySystem
    {
        private const string PROFILER_PREFIX = "[GPU] Water Height Query:";

        public static bool IsSupported()
        {
            return SystemInfo.supportsComputeShaders;
        }
        
        /// <summary>
        /// Verifies if the returned height value is valid. If not, the sampling position fell outside of the camera frustum or was not above any water surface
        /// If false, do not incorporate this value in any processing!
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsVoid(float value)
        {
            return value <= HeightPrePass.VOID_THRESHOLD;
        }
        
        /// <summary>
        /// Given 4 height values (each representing the points of a +sign) a normal vector can be derived
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="down"></param>
        /// <param name="up"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Vector3 DeriveNormal(float left, float right, float down, float up, float strength = 1f)
        {
            float xDelta = (left - right) * strength;
            float zDelta = (down - up) * strength;

            return Vector3.Normalize(new Vector3(xDelta, 1.0f, zDelta));
        }

        /// <summary>
        /// A generic front-end to determine the method used to sampling the water height
        /// </summary>
        [Serializable]
        public class Interface
        {
            public enum Method
            {
                [InspectorName("GPU (Async height readback)")]
                GPU,
                [InspectorName("CPU (Wave pattern replication)")]
                CPU
            }
            [Tooltip("Two completely different methods can be used to reproduce the wave height." +
                     "\n\n" +
                     "[GPU] Requires the \"Height Pre-pass\" feature to be enabled on the render feature. This queues up height samples and processes them in a compute shader, the result will be read back from the GPU asynchronously. " +
                     "\n\n" +
                     "Height values will represent the water surface height as it literally appears in the world, including any and all displacement effects. Slowest method, but ultimately more flexible." +
                     "\n\n" +
                     "[CPU] Given a water level, and water material, the same wave pattern can be 1:1 replicated through script. Does not include displacement effects and supports flat water geometry only. Fastest method")]
            public Method method = Method.GPU;

            [Tooltip("This reference is required to grab the wave distance and height values")]
            public WaterObject waterObject;
            [Tooltip("Try to find the Water Object below or above the Transform's position. This is slower than assigning a specific Water Object directly!")]
            public bool autoFind = true;
            public WaveProfile waveProfile;
            
            public enum WaterLevelSource
            {
                FixedValue,
                [InspectorName("Water Object Y-position")]
                WaterObject
            }
            [Tooltip("Configure what should be used to set the base water level. Relative wave height is added to this value")]
            public WaterLevelSource waterLevelSource = WaterLevelSource.WaterObject;
            public float waterLevel;

            /// <summary>
            /// Based on the current configuration, retrieve the water level height value
            /// </summary>
            /// <returns></returns>
            public float GetWaterLevel()
            {
                return waterLevelSource == WaterLevelSource.WaterObject && waterObject ? waterObject.transform.position.y : waterLevel;
            }

            public bool IsRiverMaterial()
            {
                return waterObject.material.IsKeywordEnabled("_RIVER");
            }
        }

        /// <summary>
        /// List of queries being submitted the next frame
        /// </summary>
        public static readonly List<Query> queries = new List<Query>();

        public static int QueryCount { get; private set; }
        
        /// <summary>
        /// If there are any height queries present, the displacement pre-pass must execute to ensure data is being returned
        /// </summary>
        public static bool RequiresHeightPrepass => QueryCount > 0;

        private static void AddRequest(AsyncRequest request)
        {
            //Find the next available query with enough space for the positions
            //-If not, create a new query

            Profiler.BeginSample($"{PROFILER_PREFIX} Add Request");

            foreach (Query q in queries)
            {
                if (q.requests.ContainsKey(request.hashCode))
                {
                    throw new Exception($"A request with the ID {request.hashCode} has already been issued. Use the \"onReadbackCompleted\" callback to receive the data." +
                                        $"Use the DisposeRequest function only when you are sure you no longer need the data.");
                }
            }

            int queryIndex = queries.Count;
            int sampleCount = request.sampler.positions.Length;

            if (sampleCount == 0)
            {
                return;
            }
            
            void CreateNewQuery()
            {
                queries.Add(new Query());
                QueryCount++;
                queryIndex++;
            }
            
            //Initial query needs to be created
            if (queryIndex == 0)
            {
                CreateNewQuery();
            }
            
            int occupiedIndices = HeightQuerySystem.Query.MAX_SIZE - queries[queryIndex - 1].availableIndices.Count;
            //Not enough space in the latest query for this many samples
            if (occupiedIndices + sampleCount >= HeightQuerySystem.Query.MAX_SIZE)
            {
                CreateNewQuery();

                //Debug.Log($"Query #{queryIndex-1} contains {occupiedIndices}, requires {occupiedIndices + sampleCount}. Created a new query (#{queryIndex}).");
            }
            
            var query = queries[queryIndex-1];

            //Assign the indices available in the query to the request
            for (int i = 0; i < sampleCount; i++)
            {
                request.indices.Add(query.GetNextAvailableIndex());
            }

            query.sampleCount += sampleCount;
            query.requests.Add(request.hashCode, request);
            
            Profiler.EndSample();
        }

        private static void WithdrawRequest(AsyncRequest request)
        {
            Profiler.BeginSample($"{PROFILER_PREFIX} Withdraw Request");

            for (int i = 0; i < queries.Count; i++)
            {
                var query = queries[i];
                
                if (query.requests.TryGetValue(request.hashCode, out _))
                {
                    //Remove request from query
                    query.requests.Remove(request.hashCode);

                    int indexCount = request.indices.Count;
                    
                    //Return the occupied indices to the pool
                    for (int j = 0; j < indexCount; j++)
                    {
                        query.ReleaseIndex(request.indices[j]);
                    }

                    //Update the current sample count
                    query.sampleCount -= indexCount;
                    
                    //Clear the list of occupied indices, these will be repopulate should the request be issued again
                    request.indices.Clear();
                    
                    //If the query is now completely empty, yeet it
                    if (query.requests.Count == 0)
                    {
                        //Debug.Log($"Query #{i} is now empty and was disposed");
                        
                        query.Dispose();
                    }
                }
            }
            
            Profiler.EndSample();
        }
        
        /// <summary>
        /// Clears all queries from the system
        /// </summary>
        //Need to force a clean start when entering/exiting play mode. Otherwise certain arrays will get de-allocated.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Clear()
        {
            for (int i = 0; i < queries.Count; i++)
            {
                queries[i].Dispose();
            }
        }
    }
}
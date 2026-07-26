// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using Unity.Mathematics;
using UnityEngine;

namespace StylizedWater3
{
    public static class Gerstner
    {
        private const float TWO_PI = Mathf.PI * 2f;
        private const float GRAVITY = 9.8f;
        private const float MAX_AMPLITUDE = 5.0f;
        
        private static readonly int TimeParametersID = Shader.PropertyToID("_TimeParameters");
        
        //Returns the same value as _TimeParameters.x
        private static float _TimeParameters
        {
            get
            {
                if (WaterObject.CustomTime > 0) return WaterObject.CustomTime;
                
#if UNITY_EDITOR
                return Application.isPlaying ? Time.time : Shader.GetGlobalVector(TimeParametersID).x;
#else
                return Time.time;
#endif
            }
        }

        public static void ComputeHeight(HeightQuerySystem.Sampler sampler, HeightQuerySystem.Interface heightInterface)
        {
            ComputeHeight(sampler, heightInterface.waveProfile, heightInterface.GetWaterLevel(), heightInterface.waterObject.material);
        }
        
        public static void ComputeHeight(HeightQuerySystem.Sampler sampler, WaveProfile profile, float waterLevel, Material waterMaterial)
        {
            if (waterMaterial.IsKeywordEnabled(ShaderParams.Keywords.Waves) == false)
            {
                return;
            }
            
            //Get the material's wave-related parameters as these greatly influence the wave pattern
            Vector4 m_dir = waterMaterial.GetVector(ShaderParams.Properties._Direction);
            float2 direction = new float2(m_dir.x, m_dir.y);
            
            float speed = waterMaterial.GetFloat(ShaderParams.Properties._Speed) * waterMaterial.GetFloat(ShaderParams.Properties._WaveSpeed);
            float frequency = waterMaterial.GetFloat(ShaderParams.Properties._WaveFrequency);
            int layerCount = waterMaterial.GetInt(ShaderParams.Properties._WaveMaxLayers);
            float waveHeight = waterMaterial.GetFloat(ShaderParams.Properties._WaveHeight);
            float3 scale = new float3(1f, waveHeight, 1f);

            ComputeHeight(profile, sampler, waterLevel, speed, frequency, direction, scale, layerCount);
        }
        
        public static void ComputeHeight(WaveProfile profile, HeightQuerySystem.Sampler sampler, float waterLevel, in float speed, in float frequency, in float2 baseDirection, float3 scale, in int count)
        {
            int layerCount = profile.layers.Length - 1;
            int m_count = min(count, layerCount);

            for (int i = 0; i < sampler.positions.Length; i++)
            {
                //Account for world position offset
                float2 worldPosition = new float2(sampler.positions[i].x + WaterObject.PositionOffset.x, sampler.positions[i].z + WaterObject.PositionOffset.z);
                
                sampler.heightValues[i] = ComputeHeight(profile, waterLevel, worldPosition, frequency, speed * _TimeParameters, baseDirection, scale, m_count);
            }
        }

        //Keep in sync with the HLSL function in the 'Gerstner' shader library
        public static float ComputeHeight(WaveProfile profile, float waterLevel, float2 position, in float frequency, in float time, in float2 baseDirection, in float3 scale, int count)
        {
            float3 offset = 0f;
            //float3 tangent = new float3(1,0,0);
            //float3 bitangent = new float3(0,0,1);

            //float normalStrength = 1f;
            
            uint waveCount = 0;
            for (uint i = 0; i <= count; i++)
            {
                WaveProfile.WaveParameters parameters = profile.CreateWaveParameters(profile.layers[i], (float)i / (float)count, 1f / profile.averageSteepness);
                
                if (parameters.enabled > 0)
                {
                    waveCount += 1;
                    
                    float w = TWO_PI / (parameters.waveLength * frequency);
                    float freq = sqrt(GRAVITY * w);
                    //As amplitude scales down, so should the steepness
                    float ampRCP = (parameters.amplitude/MAX_AMPLITUDE);
                    //Both divide and scale by amplitude
                    float steepness = (parameters.steepness / parameters.amplitude) * ampRCP;

                    //Rotation already pre-converted into radians
                    float2 direction = new float2(sin(parameters.direction), cos(parameters.direction)) * baseDirection;

                    //Radial mode		
                    if (parameters.mode == 1)
                    {
                        position -= parameters.origin;

                        direction += (position - parameters.origin);
                        direction = normalize(direction);
                    }

                    float dir = dot(direction, position - (parameters.origin * parameters.mode));

                    float t = dir * w + (freq * -time);

                    float proximalSine = sin(t); //Y
                    float lateralSine = cos(t); //XZ

                    //Relative XYZ offsets
                    offset.x += direction.x * parameters.amplitude * lateralSine * steepness;
                    offset.y += proximalSine * parameters.amplitude;
                    offset.z += direction.y * parameters.amplitude * lateralSine * steepness;

                    /*
                    tangent += new float3(
                        -direction.x * direction.x * (steepness * proximalSine),
                        offset.x,
                        -direction.x * direction.y * (steepness * proximalSine)
                        );

                    bitangent += new float3(
                        -direction.x * direction.y * (steepness * proximalSine),
                        offset.z,
                        -direction.y * -direction.y * (steepness * proximalSine)
                        );
                    */
                }
            }
            waveCount = max(waveCount, 1);
	
            //tangent = lerp(new float3(1,0,0), tangent, normalStrength / waveCount);
            //bitangent = lerp(new float3(0,0,1), bitangent, normalStrength / waveCount);

            offset *= scale;
            
            return waterLevel + offset.y;
        }
        
        #region Maths
        //Mirroring the syntax of HLSL
        private static float sqrt(float value)
        {
            return math.sqrt(value);
        }
        
        private static float sin(float value)
        {
            return math.sin(value);
        }
        
        private static float cos(float value)
        {
            return math.cos(value);
        }        
        
        private static float2 normalize(float2 value)
        {
            return math.normalize(value);
        }
        
        private static float dot(float2 a, float2 b)
        {
            return math.dot(a, b);
        }
        
        private static uint max(uint a, uint b)
        {
            return math.max(a, b);
        }
        
        private static int min(int a, int b)
        {
            return math.min(a, b);
        }
        
        private static float3 lerp(float3 a, float3 b, float t)
        {
            return math.lerp(a, b, t);
        }
        #endregion
    }
}
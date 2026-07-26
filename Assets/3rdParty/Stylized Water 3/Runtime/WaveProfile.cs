// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StylizedWater3
{
    [Serializable]
    [HelpURL("https://staggart.xyz/unity/stylized-water-3/sw3-docs/?section=waves-2")]
    public class WaveProfile : ScriptableObject
    {
        const float MAX_AMPLITUDE = 5f;
        public const int MAX_LAYERS = 64;
        
        [Min(0.01f)]
        [InspectorName("Wave length")]
        public float waveLengthMultiplier = 1f;
        [Min(0.01f)]
        public float amplitudeMultiplier = 1f;
        [Min(0.01f)]
        public float steepnessMultiplier = 1f;
        
        [Range(0f, 1f)]
        [Tooltip("A steepness value too high can result in wave crest \"looping\" the geometry.\n\n" +
                 "To avoid this from happening, the steepness value for each Layer can be clamped to an average.")]
        public float steepnessClamping = 1f;

        [Space]
    
        [Header(("Curves (value over layer index)"))]
        [Tooltip("Scales the wave length over each layer." +
                 "\n\n" +
                 "Left=first layer, Right=last layer")]
        public AnimationCurve waveLengthCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
        public AnimationCurve amplitudeCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
        public AnimationCurve steepnessCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

        [Serializable]
        public class ProceduralSettings
        {
            public int seed = 0;
            [Range(1, MAX_LAYERS)]
            [Tooltip("Number of individual wave layers. Aim for the lowest amount as possible")]
            public int numLayers = 4;
            
            [Space]
            
            [Tooltip("The wave length represents the distance between two wave peaks. Use a high maximum value to create stormy ocean swells.")]
            public Vector2 minMaxWaveLength = new Vector2(8f, 50f);
            [Tooltip("Height of the wave, from its base to its peak")]
            public Vector2 minMaxAmplitude = new Vector2(0.1f, 1f);
            [Range(0f,1f)]
            [Tooltip("Scale the amplitude by the wavelength. If at 1, short waves becomes very short as well")]
            public float amplitudeByLength;
            [Tooltip("Steepness is the amount of horizontal movement a wave creates")]
            public Vector2 minMaxSteepness = new Vector2(0.1f, 1f);
            [Range(0f,1f)]
            [Tooltip("Scale up the steepness value by the wave length, making large waves displace the water more horizontally")]
            public float steepnessByLength;
            
            [Range(0f, 360)]
            [Tooltip("If at 0, all waves move in a single direction, If at 360, all of them go in a random direction")]
            public float directionAngleVariation = 180f;

            public void Apply(WaveProfile waveProfile)
            {
                Array.Resize(ref waveProfile.layers, numLayers);
                
                float ScaleByLength(float value, float min, float max, float length, float amount)
                {
                    value *= Mathf.Lerp(1f, value / length, amount);
                    value = Mathf.Max(value, min);

                    return value;
                }
                
                int layerCount = waveProfile.layers.Length;
                for (int i = 0; i < layerCount; i++)
                {
                    WaveProfile.Wave layer = new WaveProfile.Wave();
                    
                    Random.InitState(seed + i);

                    float t = (float)i / (float)layerCount;

                    layer.direction = (Random.Range(-directionAngleVariation, directionAngleVariation));
                    
                    layer.waveLength = Random.Range(minMaxWaveLength.x, minMaxWaveLength.y);
                    
                    layer.amplitude = Random.Range(minMaxAmplitude.x, minMaxAmplitude.y);
                    layer.amplitude = ScaleByLength(layer.amplitude, minMaxAmplitude.x, minMaxAmplitude.y, layer.waveLength, amplitudeByLength);
                    
                    layer.steepness = Random.Range(minMaxSteepness.x, minMaxSteepness.y);
                    layer.steepness = ScaleByLength(layer.steepness, minMaxSteepness.x, minMaxSteepness.y, layer.waveLength, steepnessByLength);
                    
                    waveProfile.layers[i] = layer;
                }
                
                waveProfile.UpdateShaderParameters();
            }
        }
        
        /// <summary>
        /// House various parameter values used for randomize wave profile creation. Call the <see cref="Apply">Apply</see> function to use the settings to generated randomized wave layers.
        /// </summary>
        public ProceduralSettings proceduralSettings;
        
        /// <summary>
        /// Class to describe a single Gerstner Wave
        /// </summary>
        [Serializable]
        public class Wave
        {
            [Tooltip("Directional: Waves move in a specific direction (angle)" +
                     "\n\nRadial: Wave originates from the position defined below")]
            public enum Mode
            {
                Directional,
                Radial
            }
            public bool enabled = true;

            [Space]

            [Tooltip("Distance between each crest")]
            [Range(0.1f, 64f)]
            public float waveLength = 10f;
            
            [Tooltip("Height of the wave in units(m)")]
            [Range(0.001f, MAX_AMPLITUDE)]
            public float amplitude = 1f;

            [Tooltip("Amount of horizontal movement. Values too high can cause the crest of a wave to \"loop\"")]
            [Range(0.001f, 1f)]
            public float steepness = 0.5f;

            public Mode mode;
            
            [Tooltip("Direction the wave travels forward in degrees (on the Y-axis)")]
            [Range(0f, 360f)]
            public float direction;

            [Tooltip("Position in world-space")]
            public Vector2 origin;
        }

        [Space]
    
        public Wave[] layers = new Wave[8];

        public Texture2D shaderParametersLUT;
        
        public float averageSteepness
        {
            get;
            private set;
        }
        public float averageAmplitude
        {
            get;
            private set;
        }
        
        private void Reset()
        {
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Wave();
                
                float t = (float)i / (float)layers.Length;
                
                if (i == 0)
                {
                    layers[i].enabled = true;
                }
                else
                {
                    layers[i].enabled = false;
                    
                    layers[i].direction = t * 360f + Random.Range(-15f, 15f);
                    layers[i].waveLength = layers.Length - (t * Random.value);
                }
            }
            
            UpdateShaderParameters();
        }
        
        public void UpdateShaderParameters()
        {
            if (layers.Length == 0) return;
                
            shaderParametersLUT = CreateLookUpTable();
            shaderParametersLUT.hideFlags = HideFlags.NotEditable;
            shaderParametersLUT.name = this.name + " Shader Parameters";
            
            #if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(this);

            if (path == string.Empty) return;
            
            Texture2D file = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));

            if (file == null)
            {
                Object mainAsset = (WaveProfile)AssetDatabase.LoadAssetAtPath(path, typeof(WaveProfile));
                
                AssetDatabase.AddObjectToAsset(shaderParametersLUT, mainAsset);
                AssetDatabase.SaveAssets();
                
                //Import
                path = AssetDatabase.GetAssetPath(shaderParametersLUT);
                
                //Reference serialized texture asset
                shaderParametersLUT = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
            }
            else
            {
                EditorUtility.CopySerialized(shaderParametersLUT, file);
                file.name = shaderParametersLUT.name;
            }
 
            //Reference serialized texture asset on disk
            shaderParametersLUT = file;
            #endif
        }

        //Having a total of 8 float4 components, 2 rows are required to store them.
        private const int LUT_ROWS = 2;

        //Settings for a wave layer, converted for GPU use
        public struct WaveParameters
        {
            public float waveLength;
            public float amplitude;
            public float direction;
            public float steepness;
            public float2 origin;
            public uint mode;
            public uint enabled;
        };
        
        private Texture2D CreateLookUpTable()
        {
            int layerCount = layers.Length;

            if (layers.Length == 0)
            {
                throw new Exception("Cannot create a wave profile LUT from 0 wave layers!");
            }
            
            //16-bit precision since values can exceed 1
            Texture2D texture = new Texture2D(layerCount, LUT_ROWS, TextureFormat.RGBAHalf, false, true)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp,
            };

            //The summed steepness must never exceed a value of 1 in the final calculations
            //This would otherwise incur visible loops in the wave crests
            //To avoid this, divide the steepness parameter value by the average
            RecalculateAverages();

            float steepnessRCP = 1f / averageSteepness;
            float amplitudeRCP = 1f / averageSteepness;
            
            for (int x = 0; x < layerCount; x++)
            {
                //Normalized value of the layer (0-1)
                float t = (float)x / (float)layerCount;
                
                WaveParameters parameters = CreateWaveParameters(layers[x], t, steepnessRCP);

                Color row0 = new Color(parameters.amplitude, parameters.waveLength, parameters.direction, parameters.enabled);
                texture.SetPixel(x, 0, row0);

                Color row1 = new Color(parameters.origin.x, parameters.origin.y, parameters.mode, parameters.steepness);
                texture.SetPixel(x, 1, row1);
            }

            texture.Apply();

            return texture;
        }

        public WaveParameters CreateWaveParameters(Wave wave, float t, float steepnessRCP)
        {
            WaveParameters parameters = new WaveParameters
            {
                enabled = (uint)(wave.enabled ? 1 : 0),
                waveLength = Mathf.Max(0.01f, wave.waveLength * waveLengthMultiplier * waveLengthCurve.Evaluate(t)),
                amplitude = (wave.amplitude * amplitudeMultiplier * amplitudeCurve.Evaluate(t)),
                direction = wave.direction * Mathf.Deg2Rad,
            };

            parameters.origin.x = wave.origin.x;
            parameters.origin.y = wave.origin.y;
            parameters.mode = (uint)wave.mode;
            parameters.steepness = wave.steepness * steepnessMultiplier * steepnessCurve.Evaluate(t) * Mathf.Lerp(1f, steepnessRCP, steepnessClamping);

            return parameters;
        }

        public void RecalculateAverages()
        {
            averageSteepness = 0f;
            averageAmplitude = 0f;
            int activeLayers = 0;
            
            for (int x = 0; x < layers.Length; x++)
            {
                //Normalized value of the layer (0-1)
                float t = (float)x / (float)layers.Length;

                if (layers[x].enabled)
                {
                    activeLayers++;
                    averageSteepness += layers[x].steepness * steepnessMultiplier * steepnessCurve.Evaluate(t);
                    averageAmplitude += layers[x].amplitude * amplitudeMultiplier * amplitudeCurve.Evaluate(t);
                }
            }

            if (activeLayers > 0)
            {
                averageSteepness = activeLayers;
                averageAmplitude /= activeLayers;
            }
        }
    }
}
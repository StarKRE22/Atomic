// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using UnityEngine;

namespace StylizedWater3
{
    public static class ShaderParams
    {
        public static class Properties
        {
            public static readonly int _Direction = Shader.PropertyToID("_Direction");
            public static readonly int _Speed = Shader.PropertyToID("_Speed");
        
            public static readonly int _WaveSpeed = Shader.PropertyToID("_WaveSpeed");
            public static readonly int _WaveFrequency = Shader.PropertyToID("_WaveFrequency");
            public static readonly int _WaveMaxLayers = Shader.PropertyToID("_WaveMaxLayers");
            public static readonly int _WaveHeight = Shader.PropertyToID("_WaveHeight");
        }
        
        public static class Keywords
        {
            public const string Waves = "_WAVES";
            public const string Translucency = "_TRANSLUCENCY";
            public const string Caustics = "_CAUSTICS";
            public const string Refraction = "_REFRACTION";
            
            public const string UnderwaterRendering = "UNDERWATER_ENABLED";
            public const string DynamicEffects = "DYNAMIC_EFFECTS_ENABLED";
            public const string WaterHeightPass = "WATER_HEIGHT_PASS";
        }

        public static class LightModes
        {
            public const string WaterHeight = "WaterHeight";
        }

        public static class ShaderNames
        {
            public const string TESSELLATION_NAME_SUFFIX = " (Tessellation)";
            
            public const string HeightProcessor = "Hidden/StylizedWater3/HeightProcessor";
            public const string TerrainHeight = "Hidden/StylizedWater3/TerrainHeight";
        }
        public static class Passes
        {
            //Keep in sync with shader!
            public const string HeightPrePass = "Height";
        }
    }
}
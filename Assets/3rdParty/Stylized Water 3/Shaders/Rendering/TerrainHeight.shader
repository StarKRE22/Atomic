Shader "Hidden/StylizedWater3/TerrainHeight"
{
    Properties
    {
        [MainTexture] _BaseMap("Texture", 2D) = "black" {}
        [MainColor] _BaseColor("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "IgnoreProjector" = "True"
            "UniversalMaterialType" = "Unlit"
            "RenderPipeline" = "UniversalPipeline"
        }
        LOD 100

        ZWrite Off
        Cull Off

        Pass
        {
            Name "Output terrain height"

            HLSLPROGRAM
            #pragma target 2.0
            
            #pragma vertex UnlitPassVertex
            #pragma fragment UnlitPassFragment

            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
            
            #include "Packages/com.unity.render-pipelines.universal/Shaders/UnlitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl" //UnpackHeightmap
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl" //UnpackHeightmap
                        
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 positionCS : SV_POSITION;
      
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            Varyings UnlitPassVertex(Attributes input)
            {
                Varyings output = (Varyings)0;
            
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
               
                return output;
            }
            
            TEXTURE2D_FLOAT(_TerrainHeightmap);
            float4 _TerrainHeightRange;
            //X: Bottom y-position
            //Y: Max height (heightmap scale)

            void UnlitPassFragment(Varyings input, out half4 outColor : SV_Target0)
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
            
                half2 uv = input.uv;

                float scale = (_TerrainHeightRange.y);
                float heightMap = UnpackHeightmap(SAMPLE_TEXTURE2D(_TerrainHeightmap, sampler_LinearRepeat, uv).r);
                heightMap *= scale * 1;
                
                float worldHeight = (_TerrainHeightRange.x + heightMap);

                //worldHeight = PackHeightmap(worldHeight);
                
                outColor = float4(worldHeight.xxx, 1.0);
            }
            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}

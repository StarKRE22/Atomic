Shader "Stylized Water 3/Flowmap/Default"
{
    Properties
    {
        _BaseMap ("Texture", 2D) = "black" {}
    	_Strength ("Strength", Range(0,1)) = 1
    }
	
    SubShader
    {
        Tags 
		{ 
			"LightMode" = "WaterFlowVectors"
			//"LightMode" = "UniversalForward" //Uncomment to enable regular rendering
			"RenderType" = "Transparent"
			"RenderQueue" = "Transparent"
		}

        Blend SrcAlpha OneMinusSrcAlpha
		//BlendOp Max
        ZWrite Off
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

            struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
            	float4 tangentOS : TANGENT;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float2 uv : TEXCOORD0;
				float4 positionCS : SV_POSITION;
				float4 color : TEXCOORD1;
				half3 tangentWS : TEXCOORD2;

				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

            TEXTURE2D(_BaseMap);
            half _Strength;

            Varyings vert (Attributes input)
			{
				Varyings output;
				
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
				output.uv = input.uv;
				output.color = input.color;

                half3 wTangent = TransformObjectToWorldDir(float3(0,0,1));
				output.tangentWS = wTangent;
				//output.tangentWS = cross(float3(0,1,0), wTangent);

				return output;
			}

            float2 Encode(float2 direction)
            {
	            return direction * 0.5 + 0.5;
            }
            
            float4 frag (Varyings i) : SV_Target
            {
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

            	float2 flowMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_LinearClamp, i.uv).xy;
            	flowMap = LinearToSRGB(flowMap);

            	i.tangentWS.xy = i.tangentWS.xz * 0.5 + 0.5;
            	
            	//return float4(i.tangentWS.x, i.tangentWS.y, 0, 1);

            	float2 flowVector = flowMap * 2.0 - 1.0;
            	float2 worldNormal = mul(float2x2(flowVector.x, flowVector.y, i.tangentWS.x, i.tangentWS.y), flowVector).xy;

            	//worldNormal.x = dot(i.tangentWS.y, flowMap.x);
            	//worldNormal.y = dot(i.tangentWS.x, flowMap.y);
            	
            	float2 dir = worldNormal.xy * 0.5 + 0.5;

            	//No rotation
            	dir = flowVector * _Strength;
            	
            	float velocity = length(dir) * 4.0;

            	//dir = dir * 0.5 + 0.5;
                return float4(Encode(dir), 0, velocity);
            }
            ENDHLSL
        }
    }
}

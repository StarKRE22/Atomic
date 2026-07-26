Shader "Custom/ScreenSpaceGradient" {
    Properties {
        _TopColor ("Top Color", Color) = (0.4, 0.6, 0.9, 1)
        _BottomColor ("Bottom Color", Color) = (0.0, 0.2, 0.4, 1)
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Background" "PreviewType"="Skybox" }
        LOD 100
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f {
                float4 position : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };
            
            float4 _TopColor;
            float4 _BottomColor;
            
            v2f vert(appdata v) {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.position);
                return o;
            }
            
            float4 frag(v2f i) : SV_Target {
                // Calculate normalized screen position (0-1 on Y axis)
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                
                // Use Y coordinate to interpolate between top and bottom colors
                float4 gradientColor = lerp(_BottomColor, _TopColor, screenUV.y);
                
                return gradientColor;
            }
            ENDCG
        }
    }
    
    FallBack "Unlit/Color"
}
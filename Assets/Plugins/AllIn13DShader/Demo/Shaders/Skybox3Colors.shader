Shader "Custom/Skybox3Colors"
{
  Properties
  {
    _TopColor ("Top Color", Color) = (0, 1, 1, 1)
    _HorizonColor ("Horizon Color", Color) = (0, 0, 0, 1)
    _BottomColor ("Bottom Color", Color) = (1, 0, 1, 1)
    _TopExponent ("Top Exponent", Float) = 0.5
    _BottomExponent ("Bottom Exponent", Float) = 0.5
    _AmplFactor ("Amplification", Float) = 1.0
  
  }
  SubShader
  {
    Tags{"RenderType" ="Background" "Queue" = "Background"}
    ZWrite Off Cull Off 
    Fog { Mode Off }
    LOD 100

    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      

      struct vertIn
      {
        float4 vertex : POSITION;
        float3 uv : TEXCOORD0;
      };

      struct vertOut
      {
        float4 vertex : SV_POSITION;
        float3 uv: TEXCOORD0;
      };
      
      vertOut vert (vertIn v)
      {
        vertOut o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }

      half _TopExponent, _BottomExponent, _AmplFactor;
      half4 _TopColor, _HorizonColor, _BottomColor;
      
      half4 frag (vertOut i) : SV_Target
      {
        half interpUv = normalize (i.uv).y;
        half topLerp = 1.0f - pow (min (1.0f, 1.0f - interpUv), _TopExponent);
        half bottomLerp = 1.0f - pow (min (1.0f, 1.0f + interpUv), _BottomExponent);
        half horizonLerp = 1.0f - topLerp - bottomLerp;
        half4 horizonCol = (_TopColor * topLerp + _HorizonColor * horizonLerp + _BottomColor * bottomLerp) * _AmplFactor;
        
        return horizonCol;
      }

      ENDCG
    }
  }
}
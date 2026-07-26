Shader "AllIn13DShader/Noises/AllIn13DShaderFractalNoise"
{
    Properties
    {
        _ScaleX("Scale X", Range(0.1, 100)) = 4
        _ScaleY("Scale Y", Range(0.1, 100)) = 4
        _StartBand("Start Band", Range(0.1, 10)) = 1
        _EndBand("End Band", Range(0.1, 10)) = 8
        _Offset("Offset", Range(-100, 100)) = 1
        _Contrast("Contrast", Range (0, 10)) = 1
        _Brightness("Brightness", Range (-1, 1)) = 0
        [MaterialToggle] _Invert("Invert?", Float) = 0
        [MaterialToggle] _Fractal("Fractal?", Float) = 0
    }
    SubShader
    {
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 Mod289(float4 x)
            {
                return x - floor(x * (1.0 / 289.0)) * 289.0;
            }

            float4 PermuteFloat4(float4 x)
            {
                return Mod289(((x * 34.0) + 1.0) * x);
            }

            float4 TaylorInvSqrt(float4 r)
            {
                return 1.79284291400159 - 0.85373472095314 * r;
            }

            float2 FadeFloat2(float2 t)
            {
                return t * t * t * (t * (t * 6.0 - 15.0) + 10.0);
            }

            float3 FadeFloat3(float3 t)
            {
                return t * t * t * (t * (t * 6.0 - 15.0) + 10.0);
            }

            float PerlinNoise(float2 P, float2 rep)
            {
                float4 Pi = floor(P.xyxy) + float4(0.0, 0.0, 1.0, 1.0);
                float4 Pf = frac(P.xyxy) - float4(0.0, 0.0, 1.0, 1.0);
                Pi = fmod(Pi, rep.xyxy);
                Pi = Mod289(Pi);
                float4 ix = Pi.xzxz;
                float4 iy = Pi.yyww;
                float4 fx = Pf.xzxz;
                float4 fy = Pf.yyww;

                float4 i = PermuteFloat4(PermuteFloat4(ix) + iy);

                float4 gx = frac(i * (1.0 / 41.0)) * 2.0 - 1.0;
                float4 gy = abs(gx) - 0.5;
                float4 tx = floor(gx + 0.5);
                gx = gx - tx;

                float2 g00 = float2(gx.x, gy.x);
                float2 g10 = float2(gx.y, gy.y);
                float2 g01 = float2(gx.z, gy.z);
                float2 g11 = float2(gx.w, gy.w);

                float4 norm = TaylorInvSqrt(float4(dot(g00, g00), dot(g01, g01), dot(g10, g10), dot(g11, g11)));
                g00 *= norm.x;
                g01 *= norm.y;
                g10 *= norm.z;
                g11 *= norm.w;

                float n00 = dot(g00, float2(fx.x, fy.x));
                float n10 = dot(g10, float2(fx.y, fy.y));
                float n01 = dot(g01, float2(fx.z, fy.z));
                float n11 = dot(g11, float2(fx.w, fy.w));

                float2 fade_xy = FadeFloat2(Pf.xy);
                float2 n_x = lerp(float2(n00, n01), float2(n10, n11), fade_xy.x);
                float n_xy = lerp(n_x.x, n_x.y, fade_xy.y);
                return 2.3 * n_xy;
            }

            float NoiseManger(float2 st, int scale_x, int scale_y, float seed, float scale_value, int start_band, int end_band, float persistance, int type)
            {
                float accum = 0.0;
                float sx = scale_x;
                float sy = scale_y;
                float sv = scale_value;
                for (int i = 1; i <= 16; i += 1)
                {
                    if (i >= start_band && i <= end_band)
                    {
                        if (type == 0)
                            accum += (PerlinNoise(float3(st.x * sx, st.y * sy, seed), float3(sx, sy, 1000.)) * 0.5 + 0.5) * sv;
                        else
                            accum += abs(PerlinNoise(float3(st.x * sx, st.y * sy, seed), float3(sx, sy, 1000.))) * sv;
                        sv *= persistance;
                        seed += 1.;
                    }
                    sx *= 2.0;
                    sy *= 2.0;
                }

                if (type == 2)
                    accum = 1 - accum;

                return accum;
            }

            half _ScaleX, _ScaleY, _Offset, _StartBand, _EndBand, _Fractal, _Invert, _Contrast, _Brightness;

            fixed4 frag(v2f i) : SV_Target
            {
                i.uv.x += _Offset * 1.3234;
                i.uv.y += _Offset * 0.8734;

                float result = NoiseManger(i.uv, _ScaleX, _ScaleY, _Offset * 9745, 0.7, _StartBand, _EndBand, 0.5, 1 - _Fractal);
                if(_Invert) result = 1 - result;
                result = saturate((result - 0.5) * _Contrast + 0.5 + _Brightness);

                return half4(result, result, result, 1);
            }
            ENDCG
        }
    }
}
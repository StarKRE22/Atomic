Shader "AllIn13DShader/Noises/AllIn13DShaderWorleyNoise"
{
    Properties
    {
        _ScaleX("Scale X", Range(0.1, 100)) = 10
        _ScaleY("Scale Y", Range(0.1, 100)) = 10
        _Jitter("Jitter", Range(0.0, 2.0)) = 1
        _NoiseType("Noise Type", Range(0.0, 4.0)) = 0
        _Offset("Offset", Range(-100, 100)) = 1
        _Contrast("Contrast", Range (0, 10)) = 1
        _Brightness("Brightness", Range (-1, 1)) = 0
        [MaterialToggle] _Invert("Invert?", Float) = 0
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

            float3 Mod(float3 x, float y)
            {
                return x - y * floor(x / y);
            }

            float3 Mod3(float3 x, float3 y)
            {
                return x - y * floor(x / y);
            }

            float3 Permute(float3 x)
            {
                return Mod((34.0 * x + 1.0) * x, 289.0);
            }

            float3 Distance(float3 x, float3 y, float3 z, bool manhattanDistance)
            {
                return manhattanDistance ? abs(x) + abs(y) + abs(z) : (x * x + y * y + z * z);
            }

            float2 WorleyNoise(float3 P, float3 rep, float jitter, bool manhattanDistance)
            {
                float K = 0.142857142857;
                float Ko = 0.428571428571;
                float K2 = 0.020408163265306;
                float Kz = 0.166666666667;
                float Kzo = 0.416666666667;

                float3 Pi = Mod3(floor(P), rep);
                float3 Pf = frac(P) - 0.5;

                float3 Pfx = Pf.x + float3(1.0, 0.0, -1.0);
                float3 Pfy = Pf.y + float3(1.0, 0.0, -1.0);
                float3 Pfz = Pf.z + float3(1.0, 0.0, -1.0);

                float3 p = Permute(Mod(Pi.x + float3(-1.0, 0.0, 1.0), rep.x));
                float3 p1 = Permute(Mod(p + Pi.y - 1.0, rep.y));
                float3 p2 = Permute(Mod(p + Pi.y, rep.y));
                float3 p3 = Permute(Mod(p + Pi.y + 1.0, rep.y));

                float3 p11 = Permute(p1 + Pi.z - 1.0);
                float3 p12 = Permute(p1 + Pi.z);
                float3 p13 = Permute(p1 + Pi.z + 1.0);

                float3 p21 = Permute(p2 + Pi.z - 1.0);
                float3 p22 = Permute(p2 + Pi.z);
                float3 p23 = Permute(p2 + Pi.z + 1.0);

                float3 p31 = Permute(p3 + Pi.z - 1.0);
                float3 p32 = Permute(p3 + Pi.z);
                float3 p33 = Permute(p3 + Pi.z + 1.0);

                float3 ox11 = frac(p11 * K) - Ko;
                float3 oy11 = Mod(floor(p11 * K), 7.0) * K - Ko;
                float3 oz11 = floor(p11 * K2) * Kz - Kzo;

                float3 ox12 = frac(p12 * K) - Ko;
                float3 oy12 = Mod(floor(p12 * K), 7.0) * K - Ko;
                float3 oz12 = floor(p12 * K2) * Kz - Kzo;

                float3 ox13 = frac(p13 * K) - Ko;
                float3 oy13 = Mod(floor(p13 * K), 7.0) * K - Ko;
                float3 oz13 = floor(p13 * K2) * Kz - Kzo;

                float3 ox21 = frac(p21 * K) - Ko;
                float3 oy21 = Mod(floor(p21 * K), 7.0) * K - Ko;
                float3 oz21 = floor(p21 * K2) * Kz - Kzo;

                float3 ox22 = frac(p22 * K) - Ko;
                float3 oy22 = Mod(floor(p22 * K), 7.0) * K - Ko;
                float3 oz22 = floor(p22 * K2) * Kz - Kzo;

                float3 ox23 = frac(p23 * K) - Ko;
                float3 oy23 = Mod(floor(p23 * K), 7.0) * K - Ko;
                float3 oz23 = floor(p23 * K2) * Kz - Kzo;

                float3 ox31 = frac(p31 * K) - Ko;
                float3 oy31 = Mod(floor(p31 * K), 7.0) * K - Ko;
                float3 oz31 = floor(p31 * K2) * Kz - Kzo;

                float3 ox32 = frac(p32 * K) - Ko;
                float3 oy32 = Mod(floor(p32 * K), 7.0) * K - Ko;
                float3 oz32 = floor(p32 * K2) * Kz - Kzo;

                float3 ox33 = frac(p33 * K) - Ko;
                float3 oy33 = Mod(floor(p33 * K), 7.0) * K - Ko;
                float3 oz33 = floor(p33 * K2) * Kz - Kzo;

                float3 dx11 = Pfx + jitter * ox11;
                float3 dy11 = Pfy.x + jitter * oy11;
                float3 dz11 = Pfz.x + jitter * oz11;

                float3 dx12 = Pfx + jitter * ox12;
                float3 dy12 = Pfy.x + jitter * oy12;
                float3 dz12 = Pfz.y + jitter * oz12;

                float3 dx13 = Pfx + jitter * ox13;
                float3 dy13 = Pfy.x + jitter * oy13;
                float3 dz13 = Pfz.z + jitter * oz13;

                float3 dx21 = Pfx + jitter * ox21;
                float3 dy21 = Pfy.y + jitter * oy21;
                float3 dz21 = Pfz.x + jitter * oz21;

                float3 dx22 = Pfx + jitter * ox22;
                float3 dy22 = Pfy.y + jitter * oy22;
                float3 dz22 = Pfz.y + jitter * oz22;

                float3 dx23 = Pfx + jitter * ox23;
                float3 dy23 = Pfy.y + jitter * oy23;
                float3 dz23 = Pfz.z + jitter * oz23;

                float3 dx31 = Pfx + jitter * ox31;
                float3 dy31 = Pfy.z + jitter * oy31;
                float3 dz31 = Pfz.x + jitter * oz31;

                float3 dx32 = Pfx + jitter * ox32;
                float3 dy32 = Pfy.z + jitter * oy32;
                float3 dz32 = Pfz.y + jitter * oz32;

                float3 dx33 = Pfx + jitter * ox33;
                float3 dy33 = Pfy.z + jitter * oy33;
                float3 dz33 = Pfz.z + jitter * oz33;

                float3 d11 = Distance(dx11, dy11, dz11, manhattanDistance);
                float3 d12 = Distance(dx12, dy12, dz12, manhattanDistance);
                float3 d13 = Distance(dx13, dy13, dz13, manhattanDistance);
                float3 d21 = Distance(dx21, dy21, dz21, manhattanDistance);
                float3 d22 = Distance(dx22, dy22, dz22, manhattanDistance);
                float3 d23 = Distance(dx23, dy23, dz23, manhattanDistance);
                float3 d31 = Distance(dx31, dy31, dz31, manhattanDistance);
                float3 d32 = Distance(dx32, dy32, dz32, manhattanDistance);
                float3 d33 = Distance(dx33, dy33, dz33, manhattanDistance);

                float3 d1a = min(d11, d12);
                d12 = max(d11, d12);
                d11 = min(d1a, d13);
                d13 = max(d1a, d13);
                d12 = min(d12, d13);
                float3 d2a = min(d21, d22);
                d22 = max(d21, d22);
                d21 = min(d2a, d23);
                d23 = max(d2a, d23);
                d22 = min(d22, d23);
                float3 d3a = min(d31, d32);
                d32 = max(d31, d32);
                d31 = min(d3a, d33);
                d33 = max(d3a, d33);
                d32 = min(d32, d33);
                float3 da = min(d11, d21);
                d21 = max(d11, d21);
                d11 = min(da, d31);
                d31 = max(da, d31);
                d11.xy = (d11.x < d11.y) ? d11.xy : d11.yx;
                d11.xz = (d11.x < d11.z) ? d11.xz : d11.zx;
                d12 = min(d12, d21);
                d12 = min(d12, d22);
                d12 = min(d12, d31);
                d12 = min(d12, d32);
                d11.yz = min(d11.yz, d12.xy);
                d11.y = min(d11.y, d12.z);
                d11.y = min(d11.y, d11.z);
                return sqrt(d11.xy);
            }

            half _ScaleX, _ScaleY, _Jitter, _NoiseType, _Offset, _Fractal, _Invert, _Contrast, _Brightness;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 s = float2(_ScaleX, _ScaleY);
                float2 f = WorleyNoise(float3(i.uv * s, _Offset), float3(s, 1000), _Jitter, false) * 0.5;

                //Add if for each Type
                float result = 0;
                if (_NoiseType < 0.1) result = f.x; //F1 - Voronoi
                else if (_NoiseType < 1.1) result = f.y; //F2 - Cells 1
                else if (_NoiseType < 2.1) result = f.y - f.x; //Distance Sub - Cells 2
                else if (_NoiseType < 3.1) result = f.x * f.y; //Distance Mul - Water Voronoi
                else if (_NoiseType < 4.1) result = 1 - f.x; //One Minus F - Cellular Noise

                if (_Invert) result = 1 - result;
                result = saturate((result - 0.5) * _Contrast + 0.5 + _Brightness);
                
                return half4(result, result, result, 1);
            }
            ENDCG
        }
    }
}
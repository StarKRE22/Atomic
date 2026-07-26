Shader "Custom/SkyStylized"
{
    Properties
    {
        [Header(Features)]
        [Toggle] _EnableGradient("Enable Gradient", Float) = 1
        [Toggle] _EnableClouds("Enable Clouds", Float) = 1
        [Toggle] _EnableSun("Enable Sun", Float) = 1

        [Header(Sky Colors)]
        _SkyColor ("Sky Color", Color) = (0.5, 0.7, 1.0, 1)
        [AllIn13DShaderGradientDrawer]_GradientRamp ("Gradient Ramp", 2D) = "white" {} 
        _RampTiling ("Ramp Tiling", Range(0.1, 4)) = 1.0
        _HorizonOffset ("Horizon Offset", Range(-0.5, 0.5)) = 0.15

        [Header(Cloud Shape)]
        _CloudColor ("Cloud Color", Color) = (1,1,1,1)
        _CloudThreshold ("Cloud Cutoff", Range(0, 1)) = 0.5
        _CloudContrast ("Cloud Contrast", Range(1, 10)) = 2
        _CloudDensity ("Cloud Coverage", Range(0, 1)) = 0.5
        
        [Header(Cloud Movement)]
        _CloudSpeed ("Cloud Speed", Vector) = (0.1, 0, 0.05, 0)
        _WindStretch ("Wind Stretch", Range(0, 2)) = 0.5
        
        [Header(Cloud Pattern)]
        _CloudScale1 ("Large Pattern Scale", Range(1, 20)) = 8
        _CloudScale2 ("Detail Scale", Range(1, 20)) = 12
        _WarpStrength ("Pattern Distortion", Range(0, 1)) = 0.3
        _CloudHeight ("Cloud Height", Range(0, 1)) = 0.3
        _CloudThickness ("Cloud Thickness", Range(0, 1)) = 0.1

        [Header(Sun)]
        _SunColor ("Sun Color", Color) = (1, 0.54, 0.17, 1)
        _SunSize ("Sun Size", Range(0.0001, 1.0)) = 0.05
        [Header(Sun Glow)]
        _GlowColor ("Glow Color", Color) = (1, 0.95, 0.8, 1)
        _GlowSize ("Glow Size", Range(1, 10)) = 4
        _GlowPower ("Glow Falloff", Range(1, 10)) = 3
    }

    SubShader
    {
        Tags
        {
            "Queue"="Background"
            "RenderType"="Background"
            "PreviewType"="Skybox"
        }
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #pragma shader_feature_local _ENABLEGRADIENT_ON
            #pragma shader_feature_local _ENABLECLOUDS_ON
            #pragma shader_feature_local _ENABLESUN_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float2 rampUV : TEXCOORD1;
            };

            // Toggle properties
            float _EnableGradient;
            float _EnableClouds;
            float _EnableSun;
            float4 _SkyColor;

            // Sky properties
            sampler2D _GradientRamp;
            float _RampTiling;
            float _HorizonOffset;

            // Cloud properties
            float4 _CloudColor;
            float _CloudThreshold, _CloudContrast, _CloudDensity;
            float4 _CloudSpeed;
            float _WindStretch;
            float _CloudScale1, _CloudScale2, _WarpStrength;
            float _CloudHeight, _CloudThickness;

            // Sun properties
            float4 _SunColor;
            float _SunSize;
            float4 _GlowColor;
            float _GlowSize, _GlowPower;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = normalize(mul(unity_ObjectToWorld, v.vertex).xyz);
                
                // Map world height to UV coordinates for gradient
                float height = o.worldPos.y + _HorizonOffset;
                o.rampUV = float2(height * _RampTiling + 0.5, 0.5);
                
                return o;
            }

            float hash(float3 p)
            {
                p = frac(p * 0.3183099 + 0.1);
                p *= 17.0;
                return frac(p.x * p.y * p.z * (p.x + p.y + p.z));
            }

            float noise(float3 x)
            {
                float3 i = floor(x);
                float3 f = frac(x);
                f = f * f * (3.0 - 2.0 * f);
                
                return lerp(lerp(lerp(hash(i + float3(0,0,0)), 
                                    hash(i + float3(1,0,0)), f.x),
                               lerp(hash(i + float3(0,1,0)), 
                                    hash(i + float3(1,1,0)), f.x), f.y),
                           lerp(lerp(hash(i + float3(0,0,1)), 
                                    hash(i + float3(1,0,1)), f.x),
                               lerp(hash(i + float3(0,1,1)), 
                                    hash(i + float3(1,1,1)), f.x), f.y), f.z);
            }

            float fbm(float3 p)
            {
                float value = 0.0;
                float amplitude = 0.5;
                float frequency = 1.0;
                
                for(int i = 0; i < 4; i++)
                {
                    value += amplitude * noise(p * frequency);
                    amplitude *= 0.5;
                    frequency *= 2.0;
                }
                
                return value;
            }

            float3 warp(float3 pos, float strength)
            {
                float3 q = float3(
                    fbm(pos + float3(0.0, 1.0, 0.5)),
                    fbm(pos + float3(2.0, -2.0, -0.5)),
                    fbm(pos + float3(-2.0, -1.0, 1.0))
                );
                
                return pos + q * strength;
            }

            float calculateCloudShape(float3 worldPos)
            {
                // Apply wind stretching along movement direction
                float3 stretching = worldPos;
                stretching.xz += worldPos.y * _WindStretch * normalize(_CloudSpeed.xz);
                
                // First layer of warping for large cloud shapes
                float3 warpedPos = warp(stretching * _CloudScale1 + _Time.y * _CloudSpeed, _WarpStrength);
                float largePattern = fbm(warpedPos);
                
                // Second layer for detail and erosion
                float3 detailPos = stretching * _CloudScale2 + _Time.y * _CloudSpeed * 1.5;
                float detail = fbm(detailPos + largePattern * 0.5) * 0.5;
                
                // Combine patterns and apply threshold
                float clouds = largePattern + detail;
                clouds = saturate((clouds - _CloudThreshold) * _CloudContrast);
                
                return clouds;
            }

            float calculateClouds(float3 worldPos)
            {
                // Height-based cloud layer mask
                float heightMask = smoothstep(_CloudHeight - _CloudThickness, 
                                           _CloudHeight + _CloudThickness, 
                                           worldPos.y);
                
                // Calculate cloud pattern
                float cloudPattern = calculateCloudShape(worldPos);
                
                // Apply coverage control
                float coverage = cloudPattern * _CloudDensity;
                
                // Combine with height mask
                return coverage * heightMask;
            }

            float3 calculateSun(float3 rayDir)
            {
                float3 sunDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 delta = rayDir - sunDir;
                float distToSun = length(delta);

                // Sharp sun disk
                float sunDisk = distToSun < _SunSize ? 1.0 : 0.0;

                // Smooth glow falloff
                float glow = 1.0 - saturate(distToSun / (_SunSize * _GlowSize));
                glow = pow(glow, _GlowPower);

                // Combine sun disk and glow
                return _SunColor.rgb * sunDisk + _GlowColor.rgb * glow;
            }

            float4 frag(v2f i) : SV_Target
            {
                // Sample sky color or gradient
                float4 skyColor;
                #if _ENABLEGRADIENT_ON
                    skyColor = tex2D(_GradientRamp, i.rampUV);
                #else
                    skyColor = _SkyColor;
                #endif

                // Calculate cloud coverage
                #if _ENABLECLOUDS_ON
                    float clouds = calculateClouds(i.worldPos);
                    skyColor.rgb = lerp(skyColor.rgb, _CloudColor.rgb, clouds);
                #endif

                // Add sun contribution
                #if _ENABLESUN_ON
                    float3 sunContribution = calculateSun(i.worldPos);
                    skyColor.rgb += sunContribution;
                #endif

                return skyColor;
            }
            ENDCG
        }
    }
}
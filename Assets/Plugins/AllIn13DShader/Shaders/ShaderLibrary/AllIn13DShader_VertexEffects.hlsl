#ifndef ALLIN13DSHADER_VERTEX_EFFECTS
#define ALLIN13DSHADER_VERTEX_EFFECTS

#ifdef _VERTEX_SHAKE_ON
float3 VertexShake(float3 vertexPos, float3 shaderTime)
{
	float3 res = vertexPos;
    
	float3 speedOffset = float3(1.0f, 1.13f, 1.07f) * ACCESS_PROP(_ShakeSpeedMult);
	float3 displacement = sin(shaderTime.y * ACCESS_PROP(_ShakeSpeed) * speedOffset) * ACCESS_PROP(_ShakeMaxDisplacement);
	displacement *= ACCESS_PROP(_ShakeBlend);
    
	res += displacement;

	return res;
}
#endif

#ifdef _VERTEX_INFLATE_ON
float3 VertexInflate(float3 vertexPos, float3 normalOS, float3 shaderTime)
{
	float3 res = vertexPos;

	float inflateValue = lerp(ACCESS_PROP(_MinInflate), ACCESS_PROP(_MaxInflate), ACCESS_PROP(_InflateBlend));
	res += normalOS * inflateValue;
	
	return res;
}
#endif

#ifdef _VERTEX_DISTORTION_ON
float3 VertexDistortion(float3 vertexPos, float3 normalOS, float3 shaderTime)
{
	float3 res = vertexPos;
	
	float noisePower = 1.0;

	float2 noiseUV = SIMPLE_CUSTOM_TRANSFORM_TEX(vertexPos.xy, _VertexDistortionNoiseTex);
	float4 correctedNoiseUV = float4(noiseUV.x, noiseUV.y, 0, 0);
	
	correctedNoiseUV.x += frac(shaderTime.x * ACCESS_PROP(_VertexDistortionNoiseSpeed.x));
	correctedNoiseUV.y += frac(shaderTime.x * ACCESS_PROP(_VertexDistortionNoiseSpeed.y));

	noisePower = SAMPLE_TEX2D_LOD(_VertexDistortionNoiseTex, correctedNoiseUV).r;

	res += normalOS * noisePower * ACCESS_PROP(_VertexDistortionAmount);
	
	return res;
}
#endif

#ifdef _VOXELIZE_ON
float3 VertexVoxel(float3 vertexPos)
{
	float3 voxelizedPosition = round(vertexPos * ACCESS_PROP(_VoxelSize)) / ACCESS_PROP(_VoxelSize);
	return lerp(vertexPos, voxelizedPosition, ACCESS_PROP(_VoxelBlend));
}
#endif

#ifdef _GLITCH_ON
float3 Glitch(float3 vertexPos, float3 shaderTime)
{
	float3 res = vertexPos;

	float3 glitchDir = mul((float3x3)unity_WorldToObject, ACCESS_PROP(_GlitchOffset));
	float3 scale = float3(length(unity_ObjectToWorld[0].xyz),
					length(unity_ObjectToWorld[1].xyz),
					length(unity_ObjectToWorld[2].xyz));
	float pos = ACCESS_PROP(_GlitchWorldSpace) ? mul(unity_ObjectToWorld, float4(vertexPos, 1.0)).y : vertexPos.y;
	float time = shaderTime.y * ACCESS_PROP(_GlitchSpeed);
	
	// Add high frequency noise to the main UV
    float2 glitchUV = float2(pos * ACCESS_PROP(_GlitchTiling) + time, time * 0.89);
    float mainNoise = noise2D(glitchUV);
    float fastNoise = noise2D(glitchUV * 2.5 + float2(time * 3.7, 0));
    mainNoise = mainNoise * 0.6 + fastNoise * 0.4;

	float2 periodicUV = float2(time * 0.5, time * 0.14);
    float periodicNoise = saturate(noise2D(periodicUV) + 0.1);
                
    float detailNoise = noise2D(float2(20.0 * glitchUV.x, glitchUV.y));
                
    float glitchValue = (2.0 * mainNoise - 1.0) * periodicNoise;
    glitchValue += glitchValue * lerp(0, saturate(2.0 * detailNoise - 1.0), 2.0);

	res += (glitchDir / scale) * glitchValue * ACCESS_PROP(_GlitchAmount);

	return res;
}
#endif

#ifdef _RECALCULATE_NORMALS_ON
float3 RecalculateNormal(float3 originalVertex, float3 normal, float3 tangent)
{
	return normal;
}
#endif

#endif
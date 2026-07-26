// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

TEXTURE2D(_BumpMap);
SAMPLER(sampler_BumpMap);
TEXTURE2D(_BumpMapLarge);
TEXTURE2D(_BumpMapSlope);

float3 BlendTangentNormals(float3 a, float3 b)
{
	#if _ADVANCED_SHADING
	return BlendNormalRNM(a, b);
	#else
	return BlendNormal(a, b);
	#endif
}

float3 SampleNormals(float2 uv, float2 tiling, float subTiling, float3 wPos, float2 time, float speed, float subSpeed, float slope, int vFace) 
{
	float4 uvs = PackedUV(uv, tiling, time, speed, subTiling, subSpeed);
	float3 n1 = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, uvs.xy));
	float3 n2 = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, uvs.zw));

	float3 blendedNormals = BlendTangentNormals(n1, n2);

	#ifdef QUAD_NORMAL_SAMPLES
	uvs = PackedUV(uv, tiling, time.yx, speed, subTiling, subSpeed);
	float3 n4 = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, uvs.xy * 2.0));
	float3 n5 = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, uvs.zw * 2.0));

	blendedNormals = BlendTangentNormals(blendedNormals, BlendTangentNormals(n4, n5));
	#endif

#if _DISTANCE_NORMALS
	float fadeFactor = DistanceFadeMask(wPos, _DistanceNormalsFadeDist.x, _DistanceNormalsFadeDist.y, vFace);

	float3 largeBlendedNormals;
	
	uvs = PackedUV(uv, _DistanceNormalsTiling.xx, time, speed * 2.0, 2.0, -0.9);
	float3 n1b = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMapLarge, sampler_BumpMap, uvs.xy));
	
	#if _ADVANCED_SHADING //Use 2nd texture sample
	float3 n2b = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMapLarge, sampler_BumpMap, uvs.zw));
	largeBlendedNormals = BlendTangentNormals(n1b, n2b);
	#else
	largeBlendedNormals = n1b;
	#endif
	
	blendedNormals = lerp(largeBlendedNormals, blendedNormals, fadeFactor);
#endif
	
#if _RIVER
	uvs = PackedUV(uv, tiling, time, speed * _SlopeSpeed, subTiling, subSpeed * _SlopeSpeed);
	uvs.xy = uvs.xy * float2(1, 1-_SlopeStretching);
	float3 n3 = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMapSlope, sampler_BumpMap, uvs.xy));

	#if _ADVANCED_SHADING
	n3 = BlendTangentNormals(n3, UnpackNormal(SAMPLE_TEXTURE2D(_BumpMapSlope, sampler_BumpMap, uvs.zw)));
	#endif
	
	blendedNormals = lerp(blendedNormals, n3, slope);
#endif

	#if WAVE_SIMULATION
	BlendWaveSimulation(wPos, blendedNormals);
	#endif
	
	return blendedNormals;
}
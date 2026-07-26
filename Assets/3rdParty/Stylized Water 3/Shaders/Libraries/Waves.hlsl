// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#ifndef WATER_WAVES_INCLUDED
#define WATER_WAVES_INCLUDED

#if !_FLAT_SHADING && (!defined(SHADER_STAGE_VERTEX) || !defined(SHADER_STAGE_DOMAIN)) //Skip normal calculations, would be calculated in pixel shader
#define CALCULATE_NORMALS
#endif

#include "Gerstner.hlsl"
TEXTURE2D(_WaveProfile);

void CalculateWaves(in Texture2D<float4> lutTex, in uint layerCount, in uint maxCount, float2 uv, float frequency, float3 positionWS, float2 baseDir, float3 normalWS, float time, float mask, float3 scale,
	in float normalStrength, float fadeStart, float fadeEnd, out float3 waveOffset, out float3 waveNormalWS)
{
	waveOffset = float3(0,0,0);
	float3 waveTangent = float3(1,0,0);
	float3 waveBiTangent = float3(0,0,1);

	float2 waveDir = baseDir;

	#if _RIVER
	waveDir.x = 1;
	waveDir.y = -1;
	#endif
	
	CalculateGerstnerWaves_float(lutTex, layerCount, uv, frequency, time, normalStrength, waveDir, maxCount,
	//Out
	waveOffset, waveTangent, waveBiTangent);

	waveNormalWS = cross(waveBiTangent, waveTangent);

	//waveNormal = float3(0,0,1);
	//Tangent- to world-space
	//half3x3 waveTangentToWorldMatrix = half3x3(waveTangent, waveBiTangent, normal);
	//waveNormalWS = TransformTangentToWorld(waveNormalWS, waveTangentToWorldMatrix);

	//Flatten by blue vertex color weight
	float waveMask = lerp(1.0, 0.0, mask);

	//Distance based scalar
	float fadeFactor = DistanceFadeMask(positionWS, fadeStart, fadeEnd, 1.0);

	waveMask *= fadeFactor;
	waveMask = saturate(max(0.0001, waveMask));
	//return float4(waveMask.xxx, 1.0);

	//Scaling
	waveOffset.y *= scale.y * waveMask;
	waveOffset.xz *= scale.xz * waveMask;

	//Fading
	waveNormalWS = lerp(normalWS, waveNormalWS, waveMask * scale.y);
	waveNormalWS = normalize(waveNormalWS);

	//water.offset.xyz += waveOffset;
	//water.waveNormal = waveNormalWS;
}
#endif
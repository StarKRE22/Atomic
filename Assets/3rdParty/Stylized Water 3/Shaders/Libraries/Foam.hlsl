// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#ifndef WATER_FOAM_INCLUDED
#define WATER_FOAM_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Common.hlsl" //PackedUV

TEXTURE2D(_FoamTex);
SAMPLER(sampler_FoamTex);

#define FOAM_CHANNEL 0
#define BUBBLES_CHANNEL 1

//WIP
//#define DISTANCE_FOAM 1

float CalculateFoamWeight(float gradient, float input)
{
	gradient = saturate(1.0 - gradient);
	return smoothstep(gradient, gradient + 1.0, input);
}

float CalculateCrestFoam(float minHeight, float maxHeight, float waveHeight)
{
	return smoothstep(minHeight, maxHeight, waveHeight);
}

float2 SampleFoamLayer(TEXTURE2D_PARAM(tex, samplerName), float2 uv, float2 tiling, float2 time, float speed, float subTiling, float subSpeed)
{
	float4 uvs = PackedUV(uv, tiling, time, speed, subTiling, subSpeed);

	float2 f1 = SAMPLE_TEXTURE2D(tex, samplerName, uvs.xy).rg;
	float2 f2 = SAMPLE_TEXTURE2D(tex, samplerName, uvs.zw).rg;

	#if UNITY_COLORSPACE_GAMMA
	f1 = SRGBToLinear(f1);
	f2 = SRGBToLinear(f2);
	#endif

	float2 foam = saturate(f1 + f2);

	return foam;
}

float2 SampleFoamTexture(TEXTURE2D_PARAM(tex, samplerName), float3 positionWS, float2 uv, float2 tiling, float subTiling, float2 time, float speed, float subSpeed, float slopeMask, float slopeSpeed, float slopeStretch,
	bool slopeFoamOn, bool distanceFoamOn)
{
	float2 foam = SampleFoamLayer(TEXTURE2D_ARGS(tex, samplerName), uv, tiling, time, speed, subTiling, subSpeed);

	#if DISTANCE_FOAM
	if(distanceFoamOn)
	{
		float fadeFactor = DistanceFadeMask(positionWS, 100, 350);
		float distanceTiling = 0.2;
		float distanceSpeed = 0.1;
		
		float4 distanceUV = PackedUV(uv, tiling * distanceTiling, time, speed * distanceSpeed, subTiling * distanceTiling, subSpeed * distanceSpeed);

		#if _ADVANCED_SHADING
		float2 distanceFoam = SampleFoamLayer(TEXTURE2D_ARGS(tex, samplerName), uv, tiling * distanceTiling, time, speed * distanceSpeed, subTiling * distanceTiling, subSpeed * distanceSpeed);
		#else
		float2 distanceFoam = SAMPLE_TEXTURE2D(tex, samplerName, distanceUV.xy).rg;
		distanceFoam *= 2.0;
		#endif

		
		foam = lerp(distanceFoam, foam, fadeFactor);
		//foam = distanceFoam;
	}
	#endif
	
	if(slopeFoamOn)
	{
		float2 slopeUV = uv;
		//Stretch UV vertically on slope
		slopeUV.y *= 1-slopeStretch;
		
		const half2 slopeFoam = SampleFoamLayer(TEXTURE2D_ARGS(_FoamTex, sampler_FoamTex), slopeUV, tiling, time, speed * slopeSpeed, subTiling, subSpeed * slopeSpeed);
	
		foam = lerp(foam, slopeFoam, slopeMask);
	}

	return foam;
}

float2 SampleFoamTexture(float3 positionWS, float2 uv, float2 tiling, float subTiling, float2 time, float speed, float subSpeed, float slopeMask, float slopeSpeed, half slopeStretch, bool slopeFoamOn, bool distanceFoamOn)
{
	return SampleFoamTexture(TEXTURE2D_ARGS(_FoamTex, sampler_FoamTex), positionWS, uv, tiling, subTiling, time, speed, subSpeed, slopeMask, slopeSpeed, slopeStretch, slopeFoamOn, distanceFoamOn);
}

TEXTURE2D(_IntersectionNoise);
SAMPLER(sampler_IntersectionNoise);

float SampleIntersection(float2 uv, float2 time, float tiling, float gradient, float falloff, float speed, half rippleDistance, float rippleStrength, float rippleSpeed, float clipping, bool sharp)
{
	float intersection = 0;
	float dist = saturate(gradient / falloff);
	
	float2 nUV = uv * tiling;
	half noise1 = SAMPLE_TEXTURE2D(_IntersectionNoise, sampler_IntersectionNoise, nUV + (time.xy * speed)).r;

	half noise2 = 0;
	#if _ADVANCED_SHADING
	noise2 = SAMPLE_TEXTURE2D(_IntersectionNoise, sampler_IntersectionNoise, (nUV * 0.8) - (time.xy * speed)).r;
	#endif
	
	#if UNITY_COLORSPACE_GAMMA
	noise1 = SRGBToLinear(noise1);
	noise2 = SRGBToLinear(noise2);
	#endif
	
	float sine = sin((time.y * rippleSpeed) - (gradient * rippleDistance)) * rippleStrength;

	half noise = saturate((max(noise1, noise2) + sine) * dist);

	UNITY_BRANCH
	if(sharp)
	{
		noise += dist;
		intersection = step(clipping, noise);
	}
	else
	{
		intersection = saturate(noise + dist) * dist;
	}

	return intersection;
}
#endif
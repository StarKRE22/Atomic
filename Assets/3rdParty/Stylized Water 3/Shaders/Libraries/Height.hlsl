// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#include "Projection.hlsl"

uniform bool _WaterHeightPrePassAvailable;
#define VOID_THRESHOLD -1000 //Same value as in HeightPrePass class

uniform float3 _WaterHeightCoords;
//XY: Bounds min
//Z: Bounds size
uniform Texture2D _WaterHeightBuffer;
//RED: Geometry world height
//GREEN: Relative world height (displacement effects)

#ifndef UNITY_CORE_SAMPLERS_INCLUDED
SamplerState sampler_LinearClamp;
#endif

//Position, relative to rendering bounds (normalized 0-1)
float2 WorldToHeightUV(float3 positionWS)
{
	return WorldToProjectionUV(positionWS, _WaterHeightCoords.xy, _WaterHeightCoords.z);
}

//May be used to validate if the sampled (summed) height is actually from a water surface
bool HasHitWaterSurface(float height)
{
	return height > VOID_THRESHOLD;
}

float2 SampleHeightBuffer(float2 uv)
{
	//if(_WaterHeightPrePassAvailable == false) return VOID_THRESHOLD;

	float2 heightData = _WaterHeightBuffer.SampleLevel(sampler_LinearClamp, uv, 0).rg;

	return heightData;
}

//Main function
float2 SampleWaterHeight(float3 positionWS)
{
	return SampleHeightBuffer(WorldToHeightUV(positionWS));
}

//Alternative version
void SampleWaterHeights(float3 positionWS, out float geometryHeight, out float displacement)
{
	float2 heights = SampleHeightBuffer(WorldToHeightUV(positionWS));

	geometryHeight = heights.r;
	displacement = heights.g;
}

//Derive a world-space normal from the height data
float3 CalculateWaterNormal(float3 positionWS, float strength)
{
	if(_WaterHeightPrePassAvailable == false) return float3(0,1,0);
	
	//Note: not using the buffer's texel size so that the sampled result remains consistent across different resolutions.
	const float radius = 1.0 / _WaterHeightCoords.z;
	
	float2 uv = WorldToHeightUV(positionWS);

	const float2 xMinSample = SampleHeightBuffer(float2(uv.x - radius, uv.y)).rg;
	const float xLeft = xMinSample.r + xMinSample.g;
	const float2 xMaxSample = SampleHeightBuffer(float2(uv.x + radius, uv.y)).rg;
	const float xRight = xMaxSample.r + xMaxSample.g;

	const float2 yMaxSample = SampleHeightBuffer(float2(uv.x, uv.y + radius)).rg;
	const float yUp = yMaxSample.r + yMaxSample.g;
	const float2 yMinSample = SampleHeightBuffer(float2(uv.x, uv.y - radius)).rg;
	const float yDown = yMinSample.r + yMinSample.g;

	float xDelta = (xLeft - xRight) * strength;
	float zDelta = (yDown - yUp) * strength;

	float3 normal = float3(xDelta, 1.0, zDelta);

	//return float3(0,xLeft,0);

	return normalize(normal.xyz);
}

//Shader Graph
void SampleWaterHeight_float(float3 positionWS, out float geometryHeight, out float displacement)
{
	#if defined(SHADERGRAPH_PREVIEW)
	geometryHeight = positionWS.y;
	displacement = 0.0;
	#else
	float2 heights = SampleWaterHeight(positionWS);
	geometryHeight = heights.r;
	displacement = heights.g;
#endif
}

//Shader Graph
void CalculateWaterNormal_float(float3 positionWS, float strength, out float3 normal)
{
	#if defined(SHADERGRAPH_PREVIEW)
	normal = float3(0,1,0);
	#else
	normal = CalculateWaterNormal(positionWS, strength);
#endif
}
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#if !defined(SHADERGRAPH_PREVIEW)
//#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Core.hlsl"
#else
SamplerState sampler_LinearClamp;
#endif

#include "Projection.hlsl"

uniform Texture2D _TerrainHeightBuffer;
float4 _TerrainHeightBuffer_TexelSize;
uniform float3 _TerrainHeightRenderCoords;
//XY: Bounds min
//Z: Bounds size
bool _TerrainHeightPrePassAvailable;

uniform Texture2D _WaterTerrainDistanceField;


//Position, relative to rendering bounds (normalized 0-1)
float2 WorldToTerrainUV(float3 positionWS)
{
	return WorldToProjectionUV(positionWS, _TerrainHeightRenderCoords.xy, _TerrainHeightRenderCoords.z);
}

float SampleTerrainHeightBuffer(float2 uv)
{
	if(_TerrainHeightPrePassAvailable == false) return 0;
	return UnpackHeightmap(_TerrainHeightBuffer.SampleLevel(sampler_LinearClamp, uv, 0).r);
}

//Main function
float SampleTerrainHeightBuffer(float3 positionWS)
{
	return SampleTerrainHeightBuffer(WorldToTerrainUV(positionWS));
}

float SampleTerrainHeight(float3 positionWS)
{
	return SampleTerrainHeightBuffer(positionWS);
}

//Shader Graph
void SampleTerrainHeight_float(float3 positionWS, out float height)
{
	height = SampleTerrainHeightBuffer(positionWS);
}

TEXTURE2D(_WaterTerrainIntersectionMask);
float SampleTerrainIntersection(float3 positionWS)
{
	return _WaterTerrainIntersectionMask.SampleLevel(sampler_LinearClamp, WorldToTerrainUV(positionWS), 0).r;
}

float SampleTerrainDepth(float3 positionWS, float falloff)
{
	float terrainHeight = SampleTerrainHeightBuffer(positionWS);

	//Sampling position will correspond to the actual terrain position on the XZ plane
	float3 terrainPosition = float3(positionWS.x, terrainHeight, positionWS.z);

	//if(terrainPosition.y >= positionWS.y) return 0;

	float dist = abs(distance(positionWS, terrainPosition));

	float attenuation = 1-saturate(dist / falloff);

	//attenuation = saturate(exp(-(dist * falloff)));

	return attenuation;
}


//Shader Graph
void SampleTerrainDepth_float(float3 positionWS, float falloff, out float attenuation)
{
	attenuation = SampleTerrainDepth(positionWS, falloff);
}

float SampleTerrainSDF(float3 positionWS)
{
	float sdfSample = _WaterTerrainDistanceField.SampleLevel(sampler_LinearClamp, WorldToTerrainUV(positionWS), 0).r;

	float edgeMask = ProjectionEdgeMask(positionWS, _TerrainHeightRenderCoords.xy, _TerrainHeightRenderCoords.z, 15);

	return sdfSample * edgeMask;
}

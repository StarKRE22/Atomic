// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#include "Common.hlsl"

bool _CausticsProjectionAvailable;
float4x4 CausticsProjection;

TEXTURE2D(_CausticsTex);
SAMPLER(sampler_CausticsTex);

float2 CalculateTriPlanarProjection(in float3 positionWS, in float3 normalWS)
{
	float3 faceNormal = round(abs(normalWS));
	
	float2 compA = lerp(positionWS.yz, positionWS.xz, faceNormal.y);
	float2 compB = lerp(compA, positionWS.xy, faceNormal.z);

	return compB;
}

void CalculateTriPlanarProjection_float(in float3 positionWS, in float3 normalWS, out float2 uv)
{
	uv = CalculateTriPlanarProjection(positionWS, normalWS);
}

//Normal is expected to be that of the geometry not the water surface
float2 GetCausticsProjection(in float4 positionCS, in float3 lightDir, float3 positionWS, float3 sceneWorldNormal, bool directional, inout half attenuation)
{
	//return CalculateTriPlanarProjection(positionWS, sceneWorldNormal);
	
	#if !_DISABLE_DEPTH_TEX
	if(directional && _CausticsProjectionAvailable)
	{
		const half NdotL = saturate(dot(sceneWorldNormal, lightDir));
		attenuation *= NdotL;
		
		//CausticsProjection matrix set up through scripting
		return mul(CausticsProjection, float4(positionWS, 1.0)).xy;
	}
	#endif


	return positionWS.xz;
}

float3 SampleCaustics(float2 uv, float2 time, float tiling, float chromance)
{
	//return SAMPLE_TEXTURE2D(_CausticsTex, sampler_CausticsTex, uv * tiling).rgb;
	
	float3 caustics1 = SAMPLE_TEXTURE2D_LOD(_CausticsTex, sampler_CausticsTex, uv * tiling + (time.xy), 0).rgb;
	float3 caustics2 = SAMPLE_TEXTURE2D_LOD(_CausticsTex, sampler_CausticsTex, (uv * tiling * 0.8) - (time.xy), 0).rgb;
	
	#if UNITY_COLORSPACE_GAMMA
	caustics1 = SRGBToLinear(caustics1);
	caustics2 = SRGBToLinear(caustics2);
	#endif

	float3 caustics = min(caustics1, caustics2) * 2.0;
	
	return lerp(caustics.rrr, caustics.rgb, chromance);
}

//Backwards compatibility for Underwater Rendering
float3 SampleCaustics(float2 uv, float2 time, float tiling)
{
	return SampleCaustics(uv, time, tiling, 1.0);
}
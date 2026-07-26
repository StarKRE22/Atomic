// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

TEXTURE2D(_WaterFlowVectors);
float4 _WaterFlowVectorsCoords;

#ifndef UNITY_CORE_SAMPLERS_INCLUDED
SamplerState sampler_LinearClamp;
#endif

struct FlowAnimation
{
	float2 uv1;
	float2 uv2;
	float2 flowVector;
	float cycle1;
	float cycle2;
	float t;
};

FlowAnimation CreateAnimation(float2 uv, float2 flowVector, float time, float phaseOffset, float duration)
{
	FlowAnimation anim;

	float t = time + phaseOffset;
	float progress = frac(time + phaseOffset);
	
	anim.flowVector = flowVector;
	anim.cycle1 = frac(t / duration) * 2.0 - 1.0;
	anim.cycle2 = frac(t / duration + (duration * 0.5)) * 2.0 - 1.0;

	anim.uv1 = uv + flowVector * anim.cycle1;
	anim.uv1 += (time - progress) * 0.5;
	anim.uv2 = uv + flowVector * anim.cycle2;
	//anim.uv2 += (time - progress) * 0.2568;
	
	anim.t = abs(1.0 - 2.0 * frac(t / duration));
	
	return anim;
}

//Position, relative to rendering bounds (normalized 0-1)
float2 WorldToFlowVectorUV(float3 positionWS)
{
	return (positionWS.xz - _WaterFlowVectorsCoords.xy) / _WaterFlowVectorsCoords.z;
}

float2 SampleFlowMapTexture(float2 uv, float strength)
{
	float4 flowMap = _WaterFlowVectors.SampleLevel(sampler_LinearClamp, uv, 0);

	return (flowMap.xy * 2.0 - 1.0) * strength;
}

float2 SampleFlowMap(float3 positionWS)
{
	return SampleFlowMapTexture(WorldToFlowVectorUV(positionWS), 1.0);
}

void SampleFlowMap_float(float3 positionWS, out float2 flowVector)
{
	flowVector = SampleFlowMap(positionWS);
}

float4 SampleTextureFlow(TEXTURE2D_PARAM(tex, samplerName), FlowAnimation anim)
{
	float4 tex1 = SAMPLE_TEXTURE2D(tex, samplerName, anim.uv1);
	float4 tex2 = SAMPLE_TEXTURE2D(tex, samplerName, anim.uv2);

	//return (tex1 * anim.cycle1) + (tex2 * anim.cycle2);
	return lerp(tex1, tex2, anim.t);
}
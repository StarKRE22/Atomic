#ifndef ALLIN13DSHADER_HELPER_URP_INCLUDED
#define ALLIN13DSHADER_HELPER_URP_INCLUDED

#define NUM_ADDITIONAL_LIGHTS GetNumAdditionalLights();

#ifdef REQUIRE_SCENE_DEPTH

	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
	
	float GetRawDepth(float4 projPos)
	{
		float res = SampleSceneDepth(projPos.xy / projPos.w);
		return res;
	}

	float GetLinearDepth01(float4 projPos)
	{
		float rawDepth = GetRawDepth(projPos);
		float res = Linear01Depth (rawDepth, _ZBufferParams);

		return res;
	}
	
	float GetSceneDepthDiff(float4 projPos)
	{
		float rawDepth = GetRawDepth(projPos);
		float res = LinearEyeDepth(rawDepth, _ZBufferParams) - projPos.z;

		return res;
	}

#endif

float3 GetNormalWS(float3 normalOS)
{
	float3 normalWS = TransformObjectToWorldNormal(normalOS);
	return normalWS;
}

float3 GetViewDirWS(float3 vertexWS)
{
	float3 res = GetWorldSpaceViewDir(vertexWS);
	return res;
}

float3 GetPositionVS(float3 positionOS)
{
	float3 res = TransformWorldToView(positionOS);
	return res;
}

float3 GetMainLightDir(float3 vertexWS)
{
#ifdef _LIGHTMODEL_FASTLIGHTING
	float3 res = global_lightDirection;
#else
	float3 res = GetMainLight().direction;
#endif
	
	return res;
}

float3 GetMainLightColorRGB()
{
#ifdef _LIGHTMODEL_FASTLIGHTING
	float3 res = global_lightColor.rgb;
#else
	float3 res = GetMainLight().color;
#endif

	return res;
}

//normalWS needed for the equivalent method in BIRP
//effectsData needed for the equivalent method in BIRP
AllIn1LightData GetPointLightData(int index, float3 vertexWS, float3 normalWS, EffectsData effectsData)
{
	AllIn1LightData lightData;

	Light additionalLight = GetAdditionalLight(index, vertexWS); 
	lightData.lightColor = additionalLight.color;
	lightData.lightDir = additionalLight.direction;

#ifdef _FORWARD_PLUS 
	int lightIndex = index;
#else
	int lightIndex = GetPerObjectLightIndex(index);
#endif


#ifdef _CAST_SHADOWS_ON
	//lightData.realtimeShadow = AdditionalLightRealtimeShadow(perObjectIndex, vertexWS, additionalLight.direction);
	lightData.realtimeShadow = AdditionalLightShadow(lightIndex, vertexWS, additionalLight.direction, 1.0, 0);
#else
	lightData.realtimeShadow = 1.0;
#endif
	
	lightData.distanceAttenuation = additionalLight.distanceAttenuation;
	lightData.shadowColor = lightData.realtimeShadow;
	
	return lightData;
}

AllIn1LightData GetMainLightData(float3 vertexWS, EffectsData effectsData)
{
	AllIn1LightData lightData;
	
	Light mainLight = GetMainLight(); 
	lightData.lightColor = mainLight.color;
	lightData.lightDir = mainLight.direction;

	float4 shadowCoords = TransformWorldToShadowCoord(vertexWS);
	
#ifdef _CAST_SHADOWS_ON
	lightData.realtimeShadow = MainLightRealtimeShadow(shadowCoords);
#else
	lightData.realtimeShadow = 1.0;
#endif
	
	lightData.distanceAttenuation = mainLight.distanceAttenuation;

	lightData.shadowColor = lightData.realtimeShadow;
	
	return lightData;
}

int GetNumAdditionalLights()
{
	return GetAdditionalLightsCount();
}


/*Needed for URP shadow caster pass*/
float3 _LightDirection;
float3 _LightPosition;
/************/
FragmentDataShadowCaster GetClipPosShadowCaster(VertexData v, FragmentDataShadowCaster input)
{
    float3 positionWS = TransformObjectToWorld(v.vertex.xyz);
    float3 normalWS = TransformObjectToWorldNormal(v.normal);

#if _CASTING_PUNCTUAL_LIGHT_SHADOW
    float3 lightDirectionWS = normalize(_LightPosition - positionWS);
#else
    float3 lightDirectionWS = _LightDirection;
#endif

    float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));

#if UNITY_REVERSED_Z
    positionCS.z = min(positionCS.z, UNITY_NEAR_CLIP_VALUE);
#else
    positionCS.z = max(positionCS.z, UNITY_NEAR_CLIP_VALUE);
#endif

	input.pos = positionCS;
    return input;
}

ShadowCoordStruct GetShadowCoords(VertexData v, float4 clipPos, float3 vertexWS)
{
	ShadowCoordStruct res;

	res._ShadowCoord = 0;
	res.pos = clipPos;
	
	return res;
}

float GetShadowAttenuation(EffectsData data)
{
	float4 shadowCoords = TransformWorldToShadowCoord(data.vertexWS);

	float mainLightShadow = MainLightRealtimeShadow(shadowCoords);
	
	//int numAdditionalLights = NUM_ADDITIONAL_LIGHTS;
	//float additionalLightsShadows = 1.0;
	float res = mainLightShadow;
	//for(int lightIndex = 0; lightIndex < numAdditionalLights; lightIndex++)
	//{
	//	Light additionalLight = GetAdditionalLight(lightIndex, vertexWS);
	//	float additionalLightShadow = AdditionalLightRealtimeShadow(lightIndex, vertexWS/*, additionalLight.direction*/) * additionalLight.distanceAttenuation;
		
	//	res *= additionalLightShadow;
	//}
	

	res = mainLightShadow;
	return res;
}

//float GetShadowAttenuation(float3 vertexWS)
//{
//	float4 shadowCoords = TransformWorldToShadowCoord(vertexWS);
//	float mainLightShadow = MainLightRealtimeShadow(shadowCoords);

//	float res = mainLightShadow;
//	int numAdditionalLights = NUM_ADDITIONAL_LIGHTS;
//	for(int lightIndex = 0; lightIndex < numAdditionalLights; lightIndex++)
//	{
//		Light additionalLight = GetAdditionalLight(lightIndex, vertexWS);
//		float additionalLightShadow = AdditionalLightRealtimeShadow(lightIndex, vertexWS) * additionalLight.distanceAttenuation;
		
//		res += additionalLightShadow;
//	}
	
//	res = 1.0;
//	return res;
//}

float3 GetLightmap(float2 uvLightmap, EffectsData data)
{
	float3 res = 0.0;

#if defined(_AFFECTED_BY_LIGHTMAPS) && defined(LIGHTMAP_ON)
	res = SAMPLE_TEXTURE2D(unity_Lightmap, samplerunity_Lightmap, uvLightmap).xyz;
	#ifdef SUBTRACTIVE_LIGHTING
		AllIn1LightData mainLight = GetMainLightData(data.vertexWS, data);
		float attenuation = mainLight.realtimeShadow;
		float ndotl = saturate(dot(data.normalWS, mainLight.lightDir));
		float3 shadowedLightEstimate =
				ndotl * (1 - attenuation) * mainLight.lightColor.rgb;
		float3 subtractedLight = res - shadowedLightEstimate;
		subtractedLight = max(subtractedLight, _SubtractiveShadowColor.xyz);
		subtractedLight = lerp(subtractedLight, res, attenuation);
	
		res = subtractedLight;
	#endif
#endif

	return res;
}

float3 GetAmbientColor(float4 normalWS)
{
	return SampleSH(normalWS.xyz);
}

float GetFogFactor(float4 clipPos)
{
	float res = 0;

#if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
	res = ComputeFogFactor(clipPos.z);
#endif

	return res;
}

float4 CustomMixFog(float fogFactor, float4 col)
{
	float4 res = col;
	res.rgb = MixFog(res.rgb, fogFactor);
	return res;
}

#ifdef REFLECTIONS_ON
float3 GetSkyColor(float3 normalWS, float3 viewDirWS, float cubeLod)
{
	float3 worldRefl = normalize(reflect(-viewDirWS, normalWS));
	float4 skyData = SAMPLE_TEXTURECUBE_LOD(unity_SpecCube0, samplerunity_SpecCube0, worldRefl, cubeLod);
	float3 res = DecodeHDREnvironment (skyData, unity_SpecCube0_HDR);

#ifdef _REFLECTIONS_TOON
	float posterizationLevel = lerp(2, 20, ACCESS_PROP(_ToonFactor));
	res = floor(res * posterizationLevel) / posterizationLevel;
#endif

	res *= ACCESS_PROP(_ReflectionsAtten);

	return res;
}
#endif

float3 ShadeSH(float4 normalWS)
{
	float3 res = SampleSH(normalWS.xyz);
	return res;
}

#define OBJECT_TO_CLIP_SPACE(v) TransformObjectToHClip(v.vertex.xyz)
#define OBJECT_TO_CLIP_SPACE_FLOAT4(pos) TransformObjectToHClip(pos.xyz)

#endif
#ifndef ALLIN13DSHADER_LIGHT_INCLUDED
#define ALLIN13DSHADER_LIGHT_INCLUDED

#if USE_FORWARD_PLUS
    #define LIGHT_LOOP_BEGIN_ALLIN13D(lightCount, data) { \
    uint lightIndex; \
    ClusterIterator _urp_internal_clusterIterator = ClusterInit(data.normalizedScreenSpaceUV, data.vertexWS, 0); \
    [loop] while (ClusterNext(_urp_internal_clusterIterator, lightIndex)) { \
        lightIndex += URP_FP_DIRECTIONAL_LIGHTS_COUNT; \
        FORWARD_PLUS_SUBTRACTIVE_LIGHT_CHECK
    #define LIGHT_LOOP_END_ALLIN13D } }
#else
    #define LIGHT_LOOP_BEGIN_ALLIN13D(lightCount, data) \
    for (uint lightIndex = 0u; lightIndex < lightCount; ++lightIndex) {
    #define LIGHT_LOOP_END_ALLIN13D }
#endif

float GetMainLightIntensity()
{
	return length(GetMainLightColorRGB());
}

#ifdef _AOMAP_ON

float3 GetAOMapTerm(float2 uv)
{
	//float aoTex = tex2D(_AOMap, uv).r;

	//TODO: Fix this
	float aoTex = SAMPLE_TEX2D(_AOMap, uv).r;
	//--------------------------------------------------
	//float ao = smoothstep(ACCESS_PROP(_AOContrast), 1 - ACCESS_PROP(_AOContrast), aoTex);
	float3 ao = max(0, (aoTex - float3(0.5, 0.5, 0.5)) * ACCESS_PROP(_AOContrast) + float3(0.5, 0.5, 0.5));

	ao = saturate(ao + 1 - ACCESS_PROP(_AOMapStrength));
	float3 res = lerp(ACCESS_PROP(_AOColor).rgb, 1, ao);
	return res;
}

#endif

#if defined(_SHADINGMODEL_PBR) || defined(_SPECULARMODEL_ANISOTROPIC) || defined(_SPECULARMODEL_ANISOTROPICTOON)
	#include "ShaderLibrary/AllIn13DShader_BRDF.hlsl"
#endif

float3 GetBitangentWS(float4 tangentOS, float3 tangentWS, float3 normalWS)
{
    float tangentSign = tangentOS.w * unity_WorldTransformParams.w;
    float3 res = cross(normalWS, tangentWS) * tangentSign;
    return res;
}

float3 DiffuseTerm(float3 lightColor, float3 lightDir, float3 normal)
{
	float rawNdotL = dot(normal, lightDir);
	float3 lightModel = 0;

#ifdef _LIGHTMODEL_CLASSIC
	float NdotL = saturate(rawNdotL);
	lightModel = NdotL;

#elif _LIGHTMODEL_TOON
	float NdotL = saturate(rawNdotL);
	lightModel = smoothstep(ACCESS_PROP(_ToonCutoff), ACCESS_PROP(_ToonCutoff) + ACCESS_PROP(_ToonSmoothness), NdotL);

#elif _LIGHTMODEL_TOONRAMP
	float NdotL = (rawNdotL * 0.5) + 0.5;
	lightModel = SAMPLE_TEX2D(_ToonRamp, float2(NdotL, NdotL)).rgb;
	
#elif _LIGHTMODEL_HALFLAMBERT
	float NdotL = saturate(rawNdotL);
	float halfLambertTerm = (NdotL * ACCESS_PROP(_HalfLambertWrap)) + (1 - ACCESS_PROP(_HalfLambertWrap));
	lightModel = halfLambertTerm * halfLambertTerm;

#elif _LIGHTMODEL_FAKEGI
	lightModel = (saturate(rawNdotL) * ACCESS_PROP(_HardnessFakeGI)) + 1.0 - ACCESS_PROP(_HardnessFakeGI);

#elif _LIGHTMODEL_FASTLIGHTING
	lightModel = saturate(rawNdotL);
#endif
	
	float3 res = lightModel * lightColor;
    return res;
}

#ifdef SPECULAR_ON

float3 SpecularTerm(float3 objectColor, float3 lightColor, float3 lightDir, 
		float3 normalWS, float3 tangentWS, float3 bitangentWS,
		float3 viewDirWS, float glossiness, float2 mainUV)
{
    float3 reflectionDir = reflect(-lightDir, normalWS);
    float rawVdotL = dot(viewDirWS, reflectionDir);
	
    float3 specularModel = 0;
	

#if defined(_SPECULARMODEL_CLASSIC) || defined(_SPECULARMODEL_TOON)
	float3 halfVector = normalize(viewDirWS + lightDir);
	float NdotH = saturate(dot(normalWS, halfVector));
	float specularIntensity = pow(NdotH, glossiness);
	//#ifdef _SPECULARMODEL_TOON
	//	float specularSmoothness = max(ACCESS_PROP(_SpecularToonSmoothness), 0.001);
	//	specularIntensity = smoothstep(ACCESS_PROP(_SpecularToonCutoff), ACCESS_PROP(_SpecularToonCutoff) + specularSmoothness, specularIntensity);
	//#endif
#elif defined(_SPECULARMODEL_ANISOTROPIC) || defined(_SPECULARMODEL_ANISOTROPICTOON)
	float3 H = normalize(viewDirWS + lightDir);

	float TdotH = saturate(dot(tangentWS, H));
	float BdotH = saturate(dot(bitangentWS, H));

	float TdotV = saturate(dot(tangentWS, viewDirWS));
	float BdotV = saturate(dot(bitangentWS, viewDirWS));

	float TdotL = saturate(dot(tangentWS, lightDir));
	float BdotL = saturate(dot(bitangentWS, lightDir));

	float NdotH = saturate(dot(normalWS, H));
	float NdotL = saturate(dot(normalWS, lightDir));
	float NdotV = saturate(dot(normalWS, viewDirWS));

	float VdotH = saturate(dot(viewDirWS, H));

	float anisotropy = ACCESS_PROP(_Anisotropy);
	float anisoShininess = ACCESS_PROP(_AnisoShininess);
	float at = max((1 - anisoShininess) * (1.0 + anisotropy), 0.001);
	float ab = max((1 - anisoShininess) * (1.0 - anisotropy), 0.001);

	float specularIntensity = SpecularAnisoTerm(
		at, ab, 
		1.0, 
		NdotH, NdotV, NdotL,
		TdotV, BdotV, TdotL,
		BdotL, TdotH, BdotH,
		H, tangentWS, bitangentWS) * NdotL;

	specularIntensity = saturate(specularIntensity);
#endif

#if defined(_SPECULARMODEL_ANISOTROPICTOON) || defined(_SPECULARMODEL_TOON)
	float specularSmoothness = max(ACCESS_PROP(_SpecularToonSmoothness), 0.001);
	specularIntensity = smoothstep(ACCESS_PROP(_SpecularToonCutoff), ACCESS_PROP(_SpecularToonCutoff) + specularSmoothness, specularIntensity);
#endif

	specularModel = specularIntensity * lightColor;
	
	float4 specularTex = SAMPLE_TEX2D(_SpecularMap, mainUV);
    float3 res = specularModel * ACCESS_PROP(_SpecularAtten) * specularTex.r;
	
	return res;
}

float3 SpecularTerm_Basic(float3 objectColor, float3 lightColor, float3 lightDir, 
		float3 normalWS, float3 tangentWS, float3 bitangentWS, float3 viewDirWS, float2 mainUV)
{
	float3 res = SpecularTerm(objectColor, lightColor, lightDir,
		normalWS, tangentWS, bitangentWS,
		viewDirWS, ACCESS_PROP(_Shininess) * ACCESS_PROP(_Shininess), mainUV);
	return res;
}

#endif

float3 RimTermOperation(float3 normalWS, float3 viewDirWS, float minRim, float maxRim, float rimAttenuation, float3 rimColor)
{
	float NdotV = saturate(dot(normalWS, viewDirWS));
	float rimIntensity = (1 - NdotV);
	rimIntensity = smoothstep(minRim, maxRim, rimIntensity) * rimAttenuation;

	float3 res = rimIntensity * rimColor;
	return res;
}

float3 DirectLighting(float3 objectColor, EffectsData effectsData, AllIn1LightData lightData)
{
	float3 res = 0;

	float3 lightColor = lightData.lightColor.rgb * lightData.distanceAttenuation * lightData.shadowColor.rgb;
	float3 diffuseTerm = DiffuseTerm(lightColor, lightData.lightDir, effectsData.normalWS) * objectColor;
	
#if defined(_CUSTOM_SHADOW_COLOR_ON) && !defined(FORWARD_ADD_PASS)
	float shadowT = saturate(lightData.realtimeShadow + 1.0 - global_shadowColor.a);
	diffuseTerm = lerp(global_shadowColor, diffuseTerm, shadowT);
#endif
	
	float3 specularTerm = 0;
#ifdef SPECULAR_ON
	
	float NdotL = saturate(dot(effectsData.normalWS, lightData.lightDir));

    specularTerm = SpecularTerm_Basic(objectColor, lightColor, lightData.lightDir,
		effectsData.normalWS, effectsData.tangentWS, effectsData.bitangentWS, 
		effectsData.viewDirWS, effectsData.mainUV) * NdotL;
#endif
	
	res = diffuseTerm + specularTerm;
	
	return res;
}

float3 DirectLighting(float3 objectColor, EffectsData effectsData)
{
	AllIn1LightData mainLightData = GetMainLightData(effectsData.vertexWS, effectsData); 

	float3 res = DirectLighting(objectColor, effectsData, mainLightData);

#ifdef ADDITIONAL_LIGHT_LOOP
	uint numAdditionalLights = NUM_ADDITIONAL_LIGHTS;
	LIGHT_LOOP_BEGIN_ALLIN13D(numAdditionalLights, effectsData)
		AllIn1LightData additionalLightData = GetPointLightData(lightIndex, effectsData.vertexWS, effectsData.normalWS, effectsData);
		res += DirectLighting(objectColor, effectsData, additionalLightData);
	LIGHT_LOOP_END_ALLIN13D
#endif

	return res;
}

float3 CalculateLighting_Basic(float3 objectColor, float3 lightmap, EffectsData effectsData)
{
	float3 directLighting = DirectLighting(objectColor, effectsData);

	float3 ambientColor = GetAmbientColor(float4(effectsData.normalWS, 1));

	float3 ao = 1.0;
#ifdef _AOMAP_ON
	ao = GetAOMapTerm(effectsData.mainUV);
	ambientColor *= ao;
#endif

	float3 reflections = 0;
#ifdef REFLECTIONS_ON
	reflections = GetSkyColor(effectsData.normalWS, effectsData.viewDirWS, 1.0);
#endif
	
	float3 indirectLighting = (ambientColor + lightmap) * objectColor;
	indirectLighting += reflections;
	float3 res = directLighting + (indirectLighting * ao);

	return res;
}

#ifdef _SHADINGMODEL_PBR
float3 CalculateLighting_MetallicWorkflow(
	float3 vertexWS, 
	float3 normalWS, float3 tangentWS, float3 bitangentWS, 
	float3 objectColor, 
	float3 shadows, float3 lightmap, float3 ambientCol, float3 viewDirWS, 
	float2 mainUV, FragmentData fragmentData, EffectsData effectsData)
{
	float3 res = CalculateLighting_PBR(objectColor, lightmap, effectsData);

	return res;
}

#endif

float3 CalculateLighting(float3 vertexWS, 
	float3 normalWS, float3 tangentWS, float3 bitangentWS, 
	float3 objectColor,
	float shadows, float3 lightmap, float3 ambientCol, float3 viewDirWS, 
	float2 mainUV,
	float3 lightColor, float3 lightDir,
	FragmentData fragmentData, float lightAtten, EffectsData effectsData)
{
#ifdef _CUSTOM_SHADOW_COLOR_ON
	float3 shadowColor = lerp(1.0, global_shadowColor, 1 - shadows);
#else
	float3 shadowColor = shadows;
#endif
	
	float3 res = 0;
#ifdef _LIGHTMODEL_FASTLIGHTING
	res = CalculateLighting_Basic(objectColor, lightmap, effectsData);
#else
	#ifdef _SHADINGMODEL_PBR
		res = CalculateLighting_MetallicWorkflow(vertexWS, normalWS, tangentWS, 
			bitangentWS, objectColor, shadowColor, 
			lightmap, ambientCol, 
			viewDirWS, mainUV, 
			fragmentData, effectsData);
	#else
		res = CalculateLighting_Basic(objectColor, lightmap, effectsData);
	#endif
#endif
	
	return res;
}

#endif
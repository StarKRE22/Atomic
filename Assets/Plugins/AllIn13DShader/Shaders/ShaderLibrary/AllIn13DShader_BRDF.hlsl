#ifndef ALLIN13DSHADER_BRDF
#define ALLIN13DSHADER_BRDF

/************/
#define MEDIUMP_FLT_MAX    65504.0
#define saturateMediump(x) min(x, MEDIUMP_FLT_MAX)


struct BDRFPerLightData
{
	float3 H;
	float3 L;

	float3 lightColor;
	
	float rawNdotL;
	float NdotL;
	float TdotL;
	float BdotL;

	float NdotH;
	float TdotH;
	float BdotH;

	float VdotH;
	float LdotH;
	float LdotH_2;

	float LdotV;

	float3 F;
	float3 kS;
	float3 kD;
};

struct BDRFCommonData
{
	float3 N;
	float3 T;
	float3 B;
	float3 V;

	float NdotV;
	float TdotV;
	float BdotV;

	float smoothness;
	float roughness;
	float roughness_2;
	float cubeLod;

	float3 F0;

	float2 mainUV;
};

float D_GGX_Anisotropic(float NoH, const float3 h,
        const float3 t, const float3 b, float at, float ab) {
   
	//TODO: Pass TdotH and BdotH through parameters
	float ToH = dot(t, h);
    float BoH = dot(b, h);
    float a2 = at * ab;
    float3 v = float3(ab * ToH, at * BoH, a2 * NoH);
    float v2 = dot(v, v);
    float w2 = a2 / v2;
    return a2 * w2 * w2 * (1.0 / ALLIN13DSHADER_PI);
}

float V_SmithGGXCorrelated_Anisotropic(float at, float ab, float ToV, float BoV,
        float ToL, float BoL, float NoV, float NoL) {
    float lambdaV = NoL * length(float3(at * ToV, ab * BoV, NoV));
    float lambdaL = NoV * length(float3(at * ToL, ab * BoL, NoL));
    float v = 0.5 / (lambdaV + lambdaL);
    return saturateMediump(v);
}
/************/

float DistributionGGX(float a, float NdotH)
{
	float a2     = a*a;
	float NdotH2 = NdotH*NdotH;
	
	float num   = a2;
	float denom = (NdotH2 * (a2 - 1.0) + 1.0);
	denom = ALLIN13DSHADER_PI * denom * denom;
	
	return num / denom;
}

float GeometrySchlickGGX(float NdotV, float roughness)
{
	float r = (roughness + 1.0);
	float k = (r*r) / 8.0;

	float num   = NdotV;
	float denom = NdotV * (1.0 - k) + k;
	
	return num / denom;
}

float GeometrySmith(float NdotV, float NdotL, float roughness)
{
	float ggx2  = GeometrySchlickGGX(NdotV, roughness);
	float ggx1  = GeometrySchlickGGX(NdotL, roughness);

	return ggx1 * ggx2;
}

float3 fresnelSchlick(float3 F0, float VdotH)
{
	float OneMinusVdotH = 1 - VdotH;
	float OneMinusVdotH_5 = OneMinusVdotH * OneMinusVdotH * OneMinusVdotH * OneMinusVdotH * OneMinusVdotH;
	return F0 + (1.0 - F0) * OneMinusVdotH_5;
}

float3 fresnelSchlickRoughness(float cosTheta, float3 F0, float roughness)
{
	float oneMinusRoughness = 1.0 - roughness;
	float oneMinusCosTheta = 1.0 - cosTheta;

	return F0 + (max(oneMinusRoughness, F0) - F0) * pow(clamp(oneMinusCosTheta, 0.0, 1.0), 5.0);
}

inline float3 FresnelLerp (float3 F0, float3 F90, float cosA)
{
    float t = Pow_5 (1 - cosA);   // ala Schlick interpoliation
    return lerp (F0, F90, t);
}

//Cook Torrance
float3 SpecularTerm(float a, float roughness, float3 F, float NdotH, float NdotV, float NdotL, float VdotH)
{
	float D		= DistributionGGX(a, NdotH);
	float G		= GeometrySmith(NdotV, NdotL, roughness);
	float3 numerator = 4.0 * D * G * F;

	float denominator = 4 * NdotV * NdotL;
	denominator = max(denominator, 0.0001);

	float3 res = numerator / denominator;
	return res;
}


float3 SpecularAnisoTerm(
	float at, float ab, 
	float3 F, 
	float NdotH, float NdotV, float NdotL, 
	float TdotV, float BdotV, 
	float TdotL, float BdotL, 
	float TdotH, float BdotH, 
	float3 H, float3 T, float3 B)
{
	float D		= D_GGX_Anisotropic(NdotH, H, T, B, at, ab);
	float V		= V_SmithGGXCorrelated_Anisotropic(
					at, ab, 
					TdotV, BdotV, 
					TdotL, BdotL,
					NdotV, NdotL); 

	float3 res = D * V * F;

	return res;
}

float3 SpecularIBL(float3 normalWS, float3 viewDirWS, float cubeLod)
{
	float3 res = 0;
	
#ifdef REFLECTIONS_ON
	res = GetSkyColor(normalWS, viewDirWS, cubeLod);
#endif
	return res;
}

float3 DiffuseTerm(float LdotH, float LdotV, float NdotL, float roughness, float3 colorDiffuse)
{
	float LdotH_2 = LdotH * LdotH;

	float f0 = 1.0;
	float f90 = 0.5 + 2*(roughness * LdotH_2);

	float3 fDiffuse = lerp(f0, f90, NdotL) * lerp(f0, f90, LdotV);

	float3 res = (colorDiffuse / ALLIN13DSHADER_PI) * fDiffuse;
	return res;
}

float3 DiffuseTerm02(float NdotV, float NdotL, float LdotH, float perceptualRoughness, float3 albedo)
{
    float fd90 = 0.5 + 2 * LdotH * LdotH * perceptualRoughness;
    // Two schlick fresnel term
    float lightScatter   = (1 + (fd90 - 1) * Pow_5(1 - NdotL));
    float viewScatter    = (1 + (fd90 - 1) * Pow_5(1 - NdotV));

	float3 res = (albedo /*/ ALLIN13DSHADER_PI*/) * lightScatter * viewScatter;
	return res;
}

float3 IndirectLighting(float3 albedo, float3 specularColor, float3 lightmap, BDRFCommonData commonData)
{
	float3 N = commonData.N;
	float3 V = commonData.V;

	float3 diffuse = (ShadeSH(float4(N,1)) + lightmap) * albedo;

	float3 F = fresnelSchlickRoughness(commonData.NdotV, commonData.F0, commonData.roughness);
	float3 kS = F;
	float3 specular = 0;
#ifdef REFLECTIONS_ON
	float3 specularIBL = SpecularIBL(N, V, commonData.cubeLod);
	
	float reflectionLuminance = GetLuminance(float4(specularIBL, 1.0));
	specularIBL = lerp(diffuse, specularIBL, smoothstep(0.0, 0.1, reflectionLuminance * (1 - commonData.roughness)));
	
	//TODO: Intermediate _Metallic values looks weird
	float F_factor = 1 - ACCESS_PROP(_Metallic);
	specular = specularIBL * lerp(1.0, F, F_factor);
#else
	specular = lerp(diffuse * 0.25, diffuse, (1 - commonData.roughness));
#endif

	float3 kD = 1.0 - kS;
	kD *= 1.0 - ACCESS_PROP(_Metallic);

	float3 indirectDiffuse = kD * diffuse;
	
	//TODO: Some problems with specularColor
	float3 indirectSpecular = specular * specularColor;

	
	float3 res = indirectDiffuse + indirectSpecular;

	return res;
}


float3 DirectDiffuse_PBR(float3 albedo, BDRFCommonData commonData, BDRFPerLightData perLightData, AllIn1LightData lightData)
{	
	float3 diffuseTerm = DiffuseTerm02(commonData.NdotV, perLightData.NdotL, perLightData.LdotH, commonData.roughness, albedo);

	float3 directDiffuse = 0;
#ifdef _LIGHTMODEL_CLASSIC
	directDiffuse = diffuseTerm * perLightData.kD * perLightData.lightColor * perLightData.NdotL;
#elif _LIGHTMODEL_HALFLAMBERT
	float halfLambertTerm = (perLightData.NdotL * ACCESS_PROP(_HalfLambertWrap)) + (1 - ACCESS_PROP(_HalfLambertWrap));
	float halfLambertTerm_2 = halfLambertTerm * halfLambertTerm;
	directDiffuse = diffuseTerm * halfLambertTerm_2 * perLightData.lightColor;
#elif _LIGHTMODEL_FAKEGI
	float fakeGI = (perLightData.NdotL * ACCESS_PROP(_HardnessFakeGI)) + 1.0 - ACCESS_PROP(_HardnessFakeGI);
	directDiffuse = diffuseTerm * fakeGI * perLightData.lightColor;
#elif _LIGHTMODEL_TOON
	directDiffuse = diffuseTerm * perLightData.kD * perLightData.lightColor * perLightData.NdotL;
	float toonFactor = smoothstep(ACCESS_PROP(_ToonCutoff), ACCESS_PROP(_ToonCutoff) + ACCESS_PROP(_ToonSmoothness), perLightData.NdotL);
	directDiffuse *= toonFactor;
#elif _LIGHTMODEL_TOONRAMP
	directDiffuse = diffuseTerm * perLightData.kD * perLightData.lightColor * perLightData.NdotL;
	float diffuseTermLuminance = GetLuminance(directDiffuse);
	directDiffuse = SAMPLE_TEX2D(_ToonRamp, float2(diffuseTermLuminance, diffuseTermLuminance)).rgb;
#endif

	return directDiffuse;
}

#ifdef SPECULAR_ON
float3 DirectSpecular_PBR(float3 specularColor, BDRFCommonData commonData, BDRFPerLightData perLightData, AllIn1LightData lightData)
{
	float3 specularTerm = 0;

	float4 specularMap = SAMPLE_TEX2D(_SpecularMap, commonData.mainUV);

	#ifdef _SPECULARMODEL_CLASSIC
		specularTerm = SpecularTerm(commonData.roughness_2, commonData.roughness, perLightData.F, perLightData.NdotH, commonData.NdotV, perLightData.NdotL, perLightData.VdotH);
	#elif _SPECULARMODEL_TOON
		specularTerm = SpecularTerm(commonData.roughness_2, commonData.roughness, perLightData.F, perLightData.NdotH, commonData.NdotV, perLightData.NdotL, perLightData.VdotH);
		specularTerm = smoothstep(ACCESS_PROP(_SpecularToonCutoff), ACCESS_PROP(_SpecularToonCutoff) + ACCESS_PROP(_SpecularToonSmoothness), specularTerm);
	#elif defined(_SPECULARMODEL_ANISOTROPIC) || defined(_SPECULARMODEL_ANISOTROPICTOON)
		//https://google.github.io/filament/Filament.html?utm_source=chatgpt.com#materialsystem/anisotropicmodel
		float aniso = ACCESS_PROP(_Anisotropy);
		float at = max((1 - ACCESS_PROP(_AnisoShininess)) * (1.0 + aniso), 0.001);
		float ab = max((1 - ACCESS_PROP(_AnisoShininess)) * (1.0 - aniso), 0.001);

		specularTerm = SpecularAnisoTerm(at, ab, perLightData.F, 
											perLightData.NdotH, commonData.NdotV, perLightData.NdotL, 
											commonData.TdotV, commonData.BdotV, perLightData.TdotL, 
											perLightData.BdotL, perLightData.TdotH, perLightData.BdotH, 
											perLightData.H, commonData.T, commonData.B);
		specularTerm = saturate(specularTerm);

		#if defined(_SPECULARMODEL_ANISOTROPICTOON)
			float specularSmoothness = max(ACCESS_PROP(_SpecularToonSmoothness), 0.001);
			specularTerm = smoothstep(ACCESS_PROP(_SpecularToonCutoff), ACCESS_PROP(_SpecularToonCutoff) + specularSmoothness, specularTerm);
		#endif
	#endif

	specularTerm *= specularMap.r;

	float3 directSpecular = specularTerm * specularColor * ACCESS_PROP(_SpecularAtten) * perLightData.lightColor * perLightData.NdotL;
	return directSpecular;
}
#endif

float3 DirectLighting_PBR(float3 albedo, BDRFCommonData commonData, BDRFPerLightData perLightData, AllIn1LightData lightData)
{
	float3 diffuseTerm = DirectDiffuse_PBR(albedo, commonData, perLightData, lightData);

	float3 specularColor = lerp(1.0, albedo, ACCESS_PROP(_Metallic));

	float3 specularTerm = 0;
#ifdef SPECULAR_ON
	specularTerm = DirectSpecular_PBR(specularColor, commonData, perLightData, lightData);
#endif

	float3 directLight = diffuseTerm + specularTerm;
	return directLight;
}

BDRFCommonData CreateCommonBDRFData(float3 albedo, EffectsData effectsData)
{
	BDRFCommonData res;

	res.N = effectsData.normalWS;
	res.T = effectsData.tangentWS;
	res.B = effectsData.bitangentWS;
	res.V =  normalize(_WorldSpaceCameraPos.xyz - effectsData.vertexWS);

	res.smoothness = lerp(0, 0.95, ACCESS_PROP(_Smoothness));
	res.roughness = (1 - res.smoothness);
	res.roughness_2 = res.roughness * res.roughness;
	res.cubeLod = res.roughness * 8;

	res.NdotV = max(dot(res.N, res.V), 0.0);
	res.TdotV = max(dot(res.T, res.V), 0.0);
	res.BdotV = max(dot(res.B, res.V), 0.0);


	float3 f0 = 0.04;
	f0 = lerp(f0, albedo, ACCESS_PROP(_Metallic));

	res.F0 = f0;

	res.mainUV = effectsData.mainUV;

	return res;
}

BDRFPerLightData CreatePerLightData(BDRFCommonData commonData, AllIn1LightData lightData)
{
	BDRFPerLightData res;

	float oneMinusMetallic = 1 - ACCESS_PROP(_Metallic);

	res.L = lightData.lightDir;
	res.lightColor = lightData.lightColor.rgb * lightData.distanceAttenuation * lightData.shadowColor.rgb;
	res.H = normalize(commonData.V + res.L);

	res.NdotH = max(dot(commonData.N, res.H), 0.0);
	res.rawNdotL = dot(commonData.N, res.L);
	res.NdotL = max(res.rawNdotL, 0.0);
	res.TdotL = max(dot(commonData.T, res.L), 0.0);
	res.BdotL = max(dot(commonData.B, res.L), 0.0);

	res.VdotH = max(dot(commonData.V, res.H), 0.0);
	res.TdotH = max(dot(commonData.T, res.H), 0.0);
	res.BdotH = max(dot(commonData.B, res.H), 0.0);

	res.LdotV = max(dot(res.L, commonData.V), 0.0);
	res.LdotH = max(dot(res.L, res.H), 0.0);

	res.LdotH_2 = res.LdotH * res.LdotH;

	res.F = fresnelSchlick(commonData.F0, res.VdotH);
	res.kS = res.F;
	res.kD = (1.0 - res.kS) * oneMinusMetallic;

	return res;
}

float3 CalculateLighting_PBR(float3 albedo, float3 lightmap, EffectsData effectsData)
{
	BDRFCommonData commonData = CreateCommonBDRFData(albedo, effectsData);


	float3 specularColor = lerp(1.0, albedo, ACCESS_PROP(_Metallic));

	AllIn1LightData mainLightData = GetMainLightData(effectsData.vertexWS, effectsData);
	BDRFPerLightData perLightData_mainLight = CreatePerLightData(commonData, mainLightData);

	float3 directLighting = DirectLighting_PBR(albedo, commonData, perLightData_mainLight, mainLightData);

#ifdef ADDITIONAL_LIGHT_LOOP
	uint numAdditionalLights = NUM_ADDITIONAL_LIGHTS;
	LIGHT_LOOP_BEGIN_ALLIN13D(numAdditionalLights, effectsData)
		AllIn1LightData additionalLightData = GetPointLightData(lightIndex, effectsData.vertexWS, effectsData.normalWS, effectsData);
		BDRFPerLightData perLightData_additionalLight = CreatePerLightData(commonData, additionalLightData);
		directLighting += DirectLighting_PBR(albedo, commonData, perLightData_additionalLight, additionalLightData);
	LIGHT_LOOP_END_ALLIN13D
#endif
	
	float3 res = directLighting;

	
	
	//We add IndirectLighting only once
#ifndef FORWARD_ADD_PASS
	#if defined(_CUSTOM_SHADOW_COLOR_ON)
		float shadowT = saturate(mainLightData.realtimeShadow + 1.0 - global_shadowColor.a);
		res = lerp(global_shadowColor, res, shadowT);
	#endif

	float3 ao = 1.0;
	#ifdef _AOMAP_ON
		ao = GetAOMapTerm(effectsData.mainUV);
	#endif

	float3 indirectLighting = IndirectLighting(albedo, specularColor, lightmap, commonData);
	res += (indirectLighting) * ao;
#endif

	return res;
}

#endif
#ifndef ALLIN13DSHADER_LIGHT_ADD_PASS_INCLUDED
#define ALLIN13DSHADER_LIGHT_ADD_PASS_INCLUDED

float4 CalculateLightingAdd(float3 vertexWS, float3 normalWS, float3 viewDirWS, 
	float3 objectColor, float shadows, float2 mainUV, 
	FragmentData fragmentData, EffectsData effectsData)
{
	float4 col = float4(0, 0, 0, 1);
	col.rgb = CalculateLighting(vertexWS, normalWS, 0, 0, objectColor,
		0, 0, 0, viewDirWS, mainUV, 0, 1.0, fragmentData, 1.0, effectsData);

	return col;
}

FragmentData BasicVertexAdd(VertexData v)
{
	FragmentData o;

#ifdef INSTANCING_ON
	UNITY_SETUP_INSTANCE_ID(v);
    UNITY_TRANSFER_INSTANCE_ID(v, o);
#endif
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); 
	
	v.vertex = ApplyVertexEffects(v.vertex, v.normal, 0);

	
	POSITION_WS(o) = mul(unity_ObjectToWorld, v.vertex);

	o.normalWS = GetNormalWS(v.normal);

	VIEWDIR_WS(o) = GetViewDirWS(POSITION_WS(o));
	
	SCALED_MAIN_UV(o) = TRANSFORM_TEX(v.uv, _MainTex);
	RAW_MAIN_UV(o) = v.uv;
	
	o.pos = OBJECT_TO_CLIP_SPACE(v);

	ShadowCoordStruct shadowCoordStruct = GetShadowCoords(v, o.pos, POSITION_WS(o));
	FOGCOORD(o) = GetFogFactor(o.pos);

#ifdef REQUIRE_TANGENT_WS
	float3 tangentWS = mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0));
	float3 bitangentWS = GetBitangentWS(v.tangent, tangentWS, o.normalWS);
	INIT_T_SPACE(o.normalWS)
#endif

	return o;
}

float4 BasicFragmentAdd(FragmentData i) : SV_Target
{
#ifdef INSTANCING_ON
	UNITY_SETUP_INSTANCE_ID(i);
#endif
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

	EffectsData data = CalculateEffectsData(i);

	data = ApplyUVEffects_FragmentStage(data);
	data.normalWS = GetNormalWS(data, i);

	float4 objectColor = GetBaseColor(data);

	float3 normalOS = data.normalOS;
	float3 normalWS = data.normalWS;
	float3 viewDirWS = data.viewDirWS;
	
	objectColor *= ACCESS_PROP(_Color);
	objectColor = ApplyColorEffectsBeforeLighting(objectColor, data);

	float4 col = objectColor;

	col = CalculateLightingAdd(POSITION_WS(i), normalWS, VIEWDIR_WS(i), objectColor, 1.0, i.mainUV, i, data);
	
	col = CustomMixFog(FOGCOORD(i), col); 

	col = ApplyColorEffectsAfterLighting(col, data);
	col.a = ApplyAlphaEffects(col.a, i.mainUV, 0);
	
	col.a *= ACCESS_PROP(_GeneralAlpha);

	float4 additiveRes = col * col.a;

	return additiveRes;
}

#endif
#ifndef ALLIN13DSHADER_SHADOW_CASTER_PASS_INCLUDED
#define ALLIN13DSHADER_SHADOW_CASTER_PASS_INCLUDED

FragmentDataShadowCaster BasicVertexShadowCaster(VertexData v)
{
#ifdef INSTANCING_ON
	UNITY_SETUP_INSTANCE_ID(v);
    UNITY_TRANSFER_INSTANCE_ID(v, o);
#endif
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); 

#ifdef _USE_CUSTOM_TIME
	float3 shaderTime = allIn13DShader_globalTime.xyz + ACCESS_PROP(_TimingSeed);
#else
	float3 shaderTime = _Time.xyz + ACCESS_PROP(_TimingSeed);
#endif

	v.vertex = ApplyVertexEffects(v.vertex, v.normal, shaderTime);
	
	FragmentDataShadowCaster o;

	o.mainUV.xy = SIMPLE_CUSTOM_TRANSFORM_TEX(v.uv, _MainTex);
	o.positionOS = v.vertex;

	o = GetClipPosShadowCaster(v, o);

	return o;
}

float4 BasicFragmentShadowCaster(FragmentDataShadowCaster i) : SV_Target
{
#ifdef INSTANCING_ON
	UNITY_SETUP_INSTANCE_ID(i);
#endif
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

	float4 col = SAMPLE_TEX2D(_MainTex, i.mainUV);
	
	col = ApplyAlphaEffects(col, i.mainUV, 1.0);

#ifdef _ALPHA_CUTOFF_ON
	clip((col.a - ACCESS_PROP(_AlphaCutoffValue)) - 0.001);
#endif	
	
	return 0;
}

#endif
#ifndef ALLIN13DSHADER_ALPHA_EFFECTS
#define ALLIN13DSHADER_ALPHA_EFFECTS

#ifdef _FADE_ON

float4 Fade(float4 inputColor, float2 uv)
{
	float4 res = inputColor;

	float2 fadeUV = SIMPLE_CUSTOM_TRANSFORM_TEX(uv, _FadeTex);
	float fadeSample = SAMPLE_TEX2D(_FadeTex, fadeUV).r;
	
	fadeSample = pow(saturate(fadeSample), ACCESS_PROP(_FadePower));

#ifdef _FADE_BURN_ON
	float fadeAmount = lerp(ACCESS_PROP(_FadeAmount) - ACCESS_PROP(_FadeTransition) - ACCESS_PROP(_FadeBurnWidth), 1.0, ACCESS_PROP(_FadeAmount));
	float fade = smoothstep(fadeAmount, fadeAmount + ACCESS_PROP(_FadeTransition), fadeSample);

	float fadePlusBurn = smoothstep(fadeAmount + ACCESS_PROP(_FadeBurnWidth), fadeAmount + ACCESS_PROP(_FadeBurnWidth) + ACCESS_PROP(_FadeTransition), fadeSample);
	
	float diff = saturate(fade - fadePlusBurn);
	
	float3 burnColor = diff * ACCESS_PROP(_FadeBurnColor).rgb;

	res.rgb += burnColor;
#else
	float fadeAmount = lerp(ACCESS_PROP(_FadeAmount) - ACCESS_PROP(_FadeTransition), 1.0, ACCESS_PROP(_FadeAmount));
	float fade = smoothstep(fadeAmount, fadeAmount + ACCESS_PROP(_FadeTransition), fadeSample);
#endif
	
	res.a = fade;

	return res;
}

#endif

#ifdef _INTERSECTION_FADE_ON
float4 IntersectionFade(float4 inputColor, float sceneDepthDiff)
{
	float4 res = inputColor;

	res.a *= saturate(ACCESS_PROP(_IntersectionFadeFactor) * sceneDepthDiff);

	return res;
}
#endif

#endif
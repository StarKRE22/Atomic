// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"

#define CHROMASHIFT_SIZE 0.05
#define REFRACTION_IOR_RCP 0.7501875 //=1f/1.333f

float2 RefractionOffset(float2 screenPos, float3 viewDir, float3 normalWS, float strength)
{
	//Normalized to match the more accurate method
	float2 offset = normalWS.xz * 0.5;

	#if PHYSICAL_REFRACTION	
	//Light direction as traveling towards the eye, through the water surface
	float3 rayDir = refract(-viewDir, normalWS, REFRACTION_IOR_RCP);
	//Convert to view-space, because the coordinates are used to sample a screen-space texture
	float3 viewSpaceRefraction = TransformWorldToViewDir(rayDir);

	//Prevent streaking at the edges, by lerping to non-screenspace coordinates at the screen edges
	half edgeMask = ScreenEdgeMask(screenPos, length(viewSpaceRefraction.xy));
	//edgeMask = 1.0; //Test, disable
	
	offset.xy = lerp(normalWS.xz * 0.5, viewSpaceRefraction.xy, edgeMask);
	#endif

	return offset * strength;
}

//#define TRANSPARENCY_REFRACTION

#ifdef TRANSPARENCY_REFRACTION
TEXTURE2D_X(_CameraTransparentTexture);
#endif

float3 SampleUnderwaterColor(float2 uv)
{
	#ifdef TRANSPARENCY_REFRACTION
	float4 transparents = SAMPLE_TEXTURE2D_X(_CameraTransparentTexture, sampler_LinearClamp, uv.xy).rgba;
	//return transparents.rgb;
	#endif
	
	float3 opaque = SampleSceneColor(uv.xy).rgb;

	#ifdef TRANSPARENCY_REFRACTION
	opaque = opaque + (transparents.rgb * transparents.a);
	#endif
	
	return opaque;
}

float3 SampleOpaqueTexture(float4 screenPos, float2 offset, float dispersion)
{
	//Normalize for perspective projection
	screenPos.xy += offset;
	screenPos.xy /= screenPos.w;
	
	float3 sceneColor = SampleUnderwaterColor(screenPos.xy).rgb;

	#if PHYSICAL_REFRACTION //Chromatic part
	if(dispersion > 0)
	{
		float chromaShift = (length(offset) * dispersion) / screenPos.w;
		//Note: screen buffer texelsize purposely not used, this way the effect is actually consistent across all resolutions
		float texelOffset = chromaShift * CHROMASHIFT_SIZE;
	
		sceneColor.r = SampleUnderwaterColor(screenPos.xy + float2(texelOffset, 0)).r;
		sceneColor.b = SampleUnderwaterColor(screenPos.xy - float2(texelOffset, 0)).b;
	}
	#endif

	return sceneColor;
}
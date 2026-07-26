Shader "AllIn13DShader/AllIn13DShader_NoShadowCaster"
{
	Properties
	{
		_RenderPreset("Render Preset", Float) = 1 
		[AdvancedProperty]_AdvancedConfigurationEnabled("Show Advanced Configuration", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)]_BlendSrc ("Blend mode Source", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)]_BlendDst ("Blend mode Destination", Float) = 0
		[AdvancedProperty][Enum(UnityEngine.Rendering.CullMode)]_CullingMode("Culling Mode", Float) = 2
		[AdvancedProperty][Enum(Off, 0, On, 1)]_ZWrite("Depth Write", float) = 1.0
        [AdvancedProperty][Enum(UnityEngine.Rendering.CompareFunction)]_ZTestMode("Z Test Mode", float) = 4
		[AdvancedProperty][Enum(UnityEngine.Rendering.ColorWriteMask)]_ColorMask("Color Write Mask", float) = 15
		[AdvancedProperty][Toggle(_FOG_ON)] _FogOn("Fog On", Float) = 0
		[AdvancedProperty][Toggle(_SPHERIZE_NORMALS_ON)]_SpherizeNormals("Spherize Normals", Float) = 0.0
		[AdvancedProperty][Toggle(_USE_CUSTOM_TIME)] _UseCustomTime("Use Custom Time", Float) = 0

		[SingleProperty]_MainTex ("Main Texture", 2D) = "white" {}
		[SingleProperty]_Color("Color", Color) = (1, 1, 1, 1)
		[SingleProperty]_GeneralAlpha("General Alpha", Range(0, 1)) = 1.0 
		

		//Color Ramp
		[Effect(EffectID# COLOR_RAMP, GroupID# ColorEffects, CustomDrawer# COLOR_RAMP_EFFECT_DRAWER)][Toggle(_COLOR_RAMP_ON)]_ColorRampOn("Color Ramp", Float) = 0
		[EffectProperty(ParentEffect# COLOR_RAMP, Keywords(_COLOR_RAMP_ON), AllowReset# False)][KeywordEnum(BeforeLighting, AfterLighting)]_ColorRampLightingStage("Stage", Float) = 0.0
		[EffectProperty(COLOR_RAMP)][AllIn13DShaderGradientDrawer]_ColorRampTex("Color Ramp Tex", 2D) = "white" {}
		[EffectProperty(COLOR_RAMP)]_ColorRampLuminosity("Color Ramp Luminosity", Range(0, 1)) = 0
		[EffectProperty(COLOR_RAMP)]_ColorRampBlend("Color Ramp Blend", Range(0, 1)) = 1
		[EffectProperty(COLOR_RAMP)]_ColorRampTiling("Tiling", Range(0.01, 10)) = 1.0 
		[EffectProperty(COLOR_RAMP)]_ColorRampScrollSpeed("Scroll Speed", Float) = 0.0

		//Lighting
		[Effect(EffectID# LIGHTMODEL, GroupID# Lighting)][KeywordEnum(None, Classic, Toon, ToonRamp, HalfLambert, FakeGI, FastLighting)] _LightModel ("Light Model", Float) = 1
		[EffectProperty(LIGHTMODEL, TOON)]_ToonCutoff("Toon Cutoff", Range(0, 1)) = 0.5
		[EffectProperty(LIGHTMODEL, TOON)]_ToonSmoothness("Toon Smoothness", Range(0, 1)) = 0.5
		[EffectProperty(LIGHTMODEL, TOONRAMP)][AllIn13DShaderGradientDrawer]_ToonRamp("Toon Ramp", 2D) = "white" {}
		[EffectProperty(LIGHTMODEL, HALFLAMBERT)]_HalfLambertWrap("Half Lambert", Range(0, 1)) = 1
		[EffectProperty(LIGHTMODEL, FAKEGI)]_HardnessFakeGI("Fake GI Hardness", Range(0, 1)) = 0.75

		//Shading Model
		[Effect(EffectID# SHADINGMODEL, GroupID# Lighting, DependentOn# LIGHTMODEL)][KeywordEnum(Basic, PBR)] _ShadingModel("Shading Model", Float) = 0
		[EffectProperty(SHADINGMODEL, PBR)]_Metallic("Metallic", Range(0, 1)) = 0
		[EffectProperty(SHADINGMODEL, PBR)]_Smoothness("Smoothness", Range(0, 1)) = 0.5

    	//Specular
		[Effect(EffectID# SPECULARMODEL, GroupID# Lighting, DependentOn# LIGHTMODEL)][KeywordEnum(None, Classic, Toon, Anisotropic, AnisotropicToon)]_SpecularModel ("Specular Model", Float) = 0
		[EffectProperty(SPECULARMODEL, CLASSIC, TOON, ANISOTROPIC, ANISOTROPICTOON)]_SpecularAtten("Specular Attenuation", Range(0, 1)) = 0.5
		[EffectProperty(ParentEffect# SPECULARMODEL, IncompatibleWithKws(_SHADINGMODEL_PBR), Keywords(_SPECULARMODEL_CLASSIC, _SPECULARMODEL_TOON), AllowReset# True)]_Shininess("Shininess", Range(0.01, 25)) = 16.0
		[EffectProperty(ParentEffect# SPECULARMODEL, IncompatibleWithKws(_SHADINGMODEL_PBR), Keywords(_SPECULARMODEL_ANISOTROPIC, _SPECULARMODEL_ANISOTROPICTOON), AllowReset# True)]_AnisoShininess("Aniso Shininess", Range(0, 1)) = 0.85
		[EffectProperty(SPECULARMODEL, TOON, ANISOTROPICTOON)]_SpecularToonCutoff("Specular Toon Cutoff", Range(0, 1)) = 0.35
		[EffectProperty(SPECULARMODEL, TOON, ANISOTROPICTOON)]_SpecularToonSmoothness("Specular Toon Smoothness", Range(0, 1)) = 0.0
		[EffectProperty(SPECULARMODEL, CLASSIC, TOON, ANISOTROPIC, ANISOTROPICTOON)]_SpecularMap("Specular Map", 2D) = "white" {}
		[EffectProperty(SPECULARMODEL, ANISOTROPIC, ANISOTROPICTOON)]_Anisotropy("Anisotropy", Range(-1, 1)) = 0.45

		//Reflections 
		[Effect(EffectID# REFLECTIONS, GroupID# Lighting, DependentOn# LIGHTMODEL)][KeywordEnum(None, Classic, Toon)]_Reflections("Reflections", Float) = 0.0
		[EffectProperty(REFLECTIONS, TOON)]_ToonFactor("Toon Factor", Range(0, 1)) = 0
		[EffectProperty(REFLECTIONS, CLASSIC, TOON)]_ReflectionsAtten("Attenuation", Range(0, 1)) = 0.5

		//Normal Map
		[Effect(EffectID# NORMAL_MAP, GroupID# Lighting, DependentOn# LIGHTMODEL)][Toggle(_NORMAL_MAP_ON)]_NormalMapEnabled("Normal Map", Float) = 0
		[NoScaleOffset][EffectProperty(NORMAL_MAP)]_NormalMap("Normal Map", 2D) = "bump" {}
		[EffectProperty(NORMAL_MAP)]_NormalStrength("Normal Strength", Range(0.0, 10.0)) = 1.0

		//Custom Shadow Color
		[Effect(EffectID# CUSTOM_SHADOW_COLOR, GroupID# Lighting)][Toggle(_CUSTOM_SHADOW_COLOR_ON)]_CustomShadowColorOn("Custom Shadow Color", Float) = 0

		//Lightmaps
		[Effect(EffectID# AFFECTED_BY_LIGHTMAPS, GroupID# Lighting)][Toggle(_AFFECTED_BY_LIGHTMAPS)]_AffectedByLightmaps("Affected by lightmaps", Float) = 0

		//Cast Shadows Enabled
		[Effect(EffectID# CAST_SHADOWS_ON, GroupID# Lighting)][Toggle(_CAST_SHADOWS_ON)]_CastShadowsOn("Enable Shadows (Casted and Received)", Float) = 1.0

		//Scroll Texture
		[Effect(EffectID# SCROLL_TEXTURE, GroupID# UVEffects)][Toggle(_SCROLL_TEXTURE_ON)]_ScrollTextureOn("Scroll Texture", Float) = 0
		[EffectProperty(SCROLL_TEXTURE)]_ScrollTextureX("Scroll X", Float) = 1.0
		[EffectProperty(SCROLL_TEXTURE)]_ScrollTextureY("Scroll Y", Float) = 1.0
    	
    	//Screen Space UVs
		[Effect(EffectID# SCREEN_SPACE_UV, GroupID# UVEffects, IncompatibleWith# TRIPLANAR_MAPPING)][Toggle(_SCREEN_SPACE_UV_ON)]_ScreenSpaceUVOn("Screen Space UV", Float) = 0.0
		[EffectProperty(SCREEN_SPACE_UV)]_ScaleWithCameraDistance("Scale with camera distance", Range(0, 1)) = 0.0
    	
		//Pixelate
		[Effect(EffectID# PIXELATE, GroupID# UVEffects)][Toggle(_PIXELATE_ON)]_Pixelate("Pixelate", Float) = 0
		[EffectProperty(PIXELATE)]_PixelateSize("Pixelate Size", Range(4, 512)) = 32
    	
		//Stochastic Texture Sampling
		[Effect(EffectID# STOCHASTIC_SAMPLING, GroupID# UVEffects)][Toggle(_STOCHASTIC_SAMPLING_ON)]_StochasticSampling("Stochastic Sampling", Float) = 0
		[EffectProperty(STOCHASTIC_SAMPLING)]_StochasticScale("Grid Scale", Range(0, 10)) = 3.464
		[EffectProperty(STOCHASTIC_SAMPLING)]_StochasticSkew("Grid Skew", Range(0, 3)) = 0.57735027

		//Wave UV
		[Effect(EffectID# WAVE_UV, GroupID# UVEffects)][Toggle(_WAVE_UV_ON)]_WaveUVOn("Wave UV On", Float) = 0
		[EffectProperty(WAVE_UV)]_WaveAmount("Wave Amount", Range(0, 25)) = 7
		[EffectProperty(WAVE_UV)]_WaveSpeed("Wave Speed", Range(0, 25)) = 10
		[EffectProperty(WAVE_UV)]_WaveStrength("Wave Strength", Range(0, 25)) = 7.5
		[EffectProperty(WAVE_UV)]_WaveX("Wave X Axis", Range(0, 1)) = 0
		[EffectProperty(WAVE_UV)]_WaveY("Wave Y Axis", Range(0, 1)) = 0.5

		//Triplanar Mapping
		[Effect(EffectID# TRIPLANAR_MAPPING, GroupID# ColorEffects, IncompatibleWith# SCREEN_SPACE_UV, CustomDrawer# TRIPLANAR_EFFECT_DRAWER)][Toggle(_TRIPLANAR_MAPPING_ON)]_TriplanarMappingOn("Triplanar Mapping", Float) = 0
		[EffectProperty(TRIPLANAR_MAPPING)][KeywordEnum(Local, World)] _TriplanarNormalSpace("UV Space", Float) = 1.0
		[EffectProperty(TRIPLANAR_MAPPING)]_TriplanarTopTex("Top Texture", 2D) = "white" {}
		[EffectProperty(TRIPLANAR_MAPPING)]_TriplanarTopNormalMap("Top Normal Map", 2D) = "bump" {}
		[EffectProperty(TRIPLANAR_MAPPING)]_TopNormalStrength("Top Normal Map Strength", Range(0.0, 10.0)) = 1.0
		[EffectProperty(TRIPLANAR_MAPPING)]_FaceDownCutoff("Face Down Cutoff", Range(-1, 1)) = 0.25
		[EffectProperty(TRIPLANAR_MAPPING)]_TriplanarSharpness("Sharpness", Range(1, 64)) = 15.0

		//AO Map
		[Effect(EffectID# AOMAP, GroupID# ColorEffects)][Toggle(_AOMAP_ON)]_AOMapEnabled("AO Map", Float) = 0
		[EffectProperty(AOMAP)][NoScaleOffset]_AOMap("AO Map", 2D) = "white" {}
		[EffectProperty(AOMAP)]_AOMapStrength("AO Strength", Range(0, 1)) = 1.0
		[EffectProperty(AOMAP)]_AOContrast("AO Contrast", Range(0, 1)) = 1.0
		[EffectProperty(AOMAP)]_AOColor("AO Color", Color) = (0, 0, 0, 0)

		//Highlights
		[Effect(EffectID# HIGHLIGHTS, GroupID# ColorEffects)][Toggle(_HIGHLIGHTS_ON)]_Highlights("Highlights", Float) = 0
		[EffectProperty(HIGHLIGHTS)][HDR]_HighlightsColor("Highlights Color", Color) = (2, 2, 2, 1)
		[EffectProperty(HIGHLIGHTS)]_HighlightsStrength("Highlights Strength", Range(0, 1)) = 1
		[EffectProperty(HIGHLIGHTS)]_HighlightCutoff("Highlight Cutoff", Range(0, 1)) = 0.5
		[EffectProperty(HIGHLIGHTS)]_HighlightSmoothness("Highlight Smoothness", Range(0, 1)) = 0.5
		[EffectProperty(HIGHLIGHTS)][Vector3]_HighlightOffset("Highlight Offset", Vector) = (0, 0, 0, 0)

		//Rim
		[Effect(EffectID# RIM_LIGHTING, GroupID# ColorEffects)][Toggle(_RIM_LIGHTING_ON)]_RimLighting("Rim or Fresnel", Float) = 0
		[EffectProperty(ParentEffect# RIM_LIGHTING, Keywords(_RIM_LIGHTING_ON), AllowReset# False)][KeywordEnum(BeforeLighting, BeforeLightingLast, AfterLighting)]_RimLightingStage("Stage", Float) = 0.0
		[EffectProperty(RIM_LIGHTING)][HDR]_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		[EffectProperty(RIM_LIGHTING)]_RimAttenuation("Rim Attenuation", Range(0, 1)) = 1.0
		[EffectProperty(RIM_LIGHTING)]_MinRim("Min Rim", Range(0, 1)) = 0
		[EffectProperty(RIM_LIGHTING)]_MaxRim("Max Rim", Range(0, 1)) = 1.0
    	[EffectProperty(RIM_LIGHTING)][Vector3]_RimOffset("Rim Offset", Vector) = (0, 0, 0, 0)

		//Greyscale
		[Effect(EffectID# GREYSCALE, GroupID# ColorEffects)][Toggle(_GREYSCALE_ON)]_Greyscale("Greyscale", Float) = 0
		[EffectProperty(GREYSCALE)][KeywordEnum(BeforeLighting, AfterLighting)] _GreyScaleStage("Stage", Float) = 0
		[EffectProperty(GREYSCALE)] _GreyscaleLuminosity("Luminosity", Range(-1, 1)) = 0 
		[EffectProperty(GREYSCALE)] _GreyscaleTintColor("Greyscale Tint", Color) = (1.0, 1.0, 1.0, 1.0)
		[EffectProperty(GREYSCALE)] _GreyscaleBlend("Blend", Range(0, 1)) = 1
    	
    	//Posterize
    	[Effect(EffectID# POSTERIZE, GroupID# ColorEffects)][Toggle(_POSTERIZE_ON)]_Posterize("Posterize", Float) = 0
    	[EffectProperty(POSTERIZE)] _PosterizeNumColors("Number of Colors",  Range(0,200)) = 8
		[EffectProperty(POSTERIZE)] _PosterizeGamma("Posterize Gamma",  Range(0.1,10)) = 0.75

		//Hand Drawn 
		[Effect(EffectID# HAND_DRAWN, GroupID# UVEffects)][Toggle(_HAND_DRAWN_ON)]_HandDrawn("Hand Drawn", Float) = 0
		[EffectProperty(HAND_DRAWN)]_HandDrawnAmount("Hand Drawn Amount", Range(0, 50)) = 10
		[EffectProperty(HAND_DRAWN)]_HandDrawnSpeed("Hand Drawn Speed", Range(1, 15)) = 5

		//Distortion
		[Effect(EffectID# UV_DISTORTION, GroupID# UVEffects)][Toggle(_UV_DISTORTION_ON)]_UVDistortion("Distortion", Float) = 0
		[EffectProperty(UV_DISTORTION)]_DistortTex("Distortion Texture", 2D) = "white" {}
		[EffectProperty(UV_DISTORTION)]_DistortAmount("Distortion Amount", Range(0,4)) = 0.3
		[EffectProperty(UV_DISTORTION)]_DistortTexXSpeed("Scroll speed X", Range(-10,10)) = 2
		[EffectProperty(UV_DISTORTION)]_DistortTexYSpeed("Scroll speed Y", Range(-10,10)) = 2

		//Vertex Shake
		[Effect(EffectID# VERTEX_SHAKE, GroupID# MeshEffects)][Toggle(_VERTEX_SHAKE_ON)] _VertexShakeOn("Vertex Shake", Float) = 0
		[EffectProperty(VERTEX_SHAKE)][Vector3] _ShakeSpeed("Speed", Vector) = (41, 49, 45, 0)
    	[EffectProperty(VERTEX_SHAKE)] _ShakeSpeedMult("Shake Mult", Float) = 1.0
		[EffectProperty(VERTEX_SHAKE)][Vector3] _ShakeMaxDisplacement("Shake Max Displacement", Vector) = (0.1, 0.1, 0.1, 0)
		[EffectProperty(VERTEX_SHAKE)] _ShakeBlend("Shake Blend", Range(0, 1)) = 1.0

		//Vertex Inflate
		[Effect(EffectID# VERTEX_INFLATE, GroupID# MeshEffects)][Toggle(_VERTEX_INFLATE_ON)] _VertexInflate("Vertex Inflate", Float) = 0
		[EffectProperty(VERTEX_INFLATE)]_MinInflate("Min Inflate", Float) = 0.0
		[EffectProperty(VERTEX_INFLATE)]_MaxInflate("Max Inflate", Float) = 0.2
		[EffectProperty(VERTEX_INFLATE)]_InflateBlend("Inflate blend", Range(0, 1)) = 1.0

		//Vertex Distortion
		[Effect(EffectID# VERTEX_DISTORTION, GroupID# MeshEffects)][Toggle(_VERTEX_DISTORTION_ON)]_VertexDistortionOn("Vertex Distortion", Float) = 0
		[EffectProperty(VERTEX_DISTORTION)]_VertexDistortionNoiseTex("Noise Tex", 2D) = "white" {}
		[EffectProperty(VERTEX_DISTORTION)]_VertexDistortionAmount("Distortion Amount", Range(0, 2)) = 0
		[EffectProperty(VERTEX_DISTORTION)][Vector2]_VertexDistortionNoiseSpeed("Scroll Speed", Vector) = (4.0, 4.0, 0, 0)
    	
    	//Voxelize
    	[Effect(EffectID# VOXELIZE, GroupID# MeshEffects)][Toggle(_VOXELIZE_ON)] _Voxelize("Voxelize", Float) = 0
		[EffectProperty(VOXELIZE)]_VoxelSize("Voxel Size", Range(0.1, 500)) = 100
		[EffectProperty(VOXELIZE)]_VoxelBlend("Blend Amount", Range(0, 1)) = 1

		//Glitch
		[Effect(EffectID# GLITCH, GroupID# MeshEffects)][Toggle(_GLITCH_ON)]_Glitch("Glitch", Float) = 0
		[EffectProperty(GLITCH)]_GlitchTiling ("Glitch Tiling", Float) = 5
        [EffectProperty(GLITCH)]_GlitchAmount ("Glitch Amount", Range(0, 1)) = 0.5
        [EffectProperty(GLITCH)]_GlitchOffset ("Glitch Offset", Vector) = (-0.5, 0, 0, 0)
        [EffectProperty(GLITCH)]_GlitchSpeed ("Glitch Speed", Float) = 2.5
        [EffectProperty(GLITCH)][Toggle]_GlitchWorldSpace ("Use World Space", Float) = 1

		//Recalculate Normals
		[Effect(EffectID# RECALCULATE_NORMALS, GroupID# MeshEffects)][Toggle(_RECALCULATE_NORMALS_ON)]_RecalculateNormals("Recalculate Normals", Float) = 0
		
		//Hue Shift
		[Effect(EffectID# HUE_SHIFT, GroupID# ColorEffects)][Toggle(_HUE_SHIFT_ON)] _HueShiftEnabled("Hue Shift", Float) = 0
		[EffectProperty(HUE_SHIFT)]_HueShift("Hue Shift", Range(0, 360)) = 0
		[EffectProperty(HUE_SHIFT)]_HueSaturation("Saturation", Range(0, 4)) = 1
		[EffectProperty(HUE_SHIFT)]_HueBrightness("Brightness", Range(0, 2)) = 1

		//Emission
		[Effect(EffectID# EMISSION, GroupID# ColorEffects)][Toggle(_EMISSION_ON)] _EmissionEnabled("Emission", Float) = 0
    	[EffectProperty(EMISSION)]_EmissionSelfGlow("Emission Self Glow", Range(0, 20)) = 1
		[EffectProperty(EMISSION)][HDR]_EmissionColor("Emission Color", Color) = (1, 1, 1, 1)
		[EffectProperty(EMISSION)]_EmissionMap("Emission Map", 2D) = "white" {}
    
		//Hologram
		[Effect(EffectID# HOLOGRAM, GroupID# ColorEffects)][Toggle(_HOLOGRAM_ON)] _Hologram("Hologram", Float) = 0
        [EffectProperty(HOLOGRAM)][HDR]_HologramColor("Hologram Color", Color) = (1.25,2.8,6.8,1)
        [EffectProperty(HOLOGRAM)]_HologramLineDirection("Line Direction", Vector) = (0,1,0,0)
        [EffectProperty(HOLOGRAM)]_HologramBaseAlpha("Hologram Base Alpha", Range(0, 1)) = 0.1
        
        [EffectProperty(HOLOGRAM)]_HologramScrollSpeed("Hologram Scroll Speed", Float) = 2
        [EffectProperty(HOLOGRAM)]_HologramFrequency("Hologram Frequency", Float) = 20
        [EffectProperty(HOLOGRAM)]_HologramAlpha("Hologram Alpha", Range(0, 1)) = 1
        
        [EffectProperty(HOLOGRAM)]_HologramAccentSpeed("Hologram Accent Speed", Float) = 1
        [EffectProperty(HOLOGRAM)]_HologramAccentFrequency("Hologram Accent Frequency", Float) = 2
        [EffectProperty(HOLOGRAM)]_HologramAccentAlpha("Hologram Accent Alpha", Range(0, 1)) = 0.5
        
        [EffectProperty(HOLOGRAM)]_HologramLineCenter("Hologram Line Center", Range(0, 1)) = 0.5
        [EffectProperty(HOLOGRAM)]_HologramLineSpacing("Hologram Line Spacing", Range(0.001, 5)) = 2.0
        [EffectProperty(HOLOGRAM)]_HologramLineSmoothness("Hologram Line Smoothness", Range(0.01, 5)) = 2.0

    	//Matcap
		[Effect(EffectID# MATCAP, GroupID# ColorEffects)][Toggle(_MATCAP_ON)]_Matcap("Matcap", Float) = 0
		[EffectProperty(MATCAP)][KeywordEnum(Multiply, Replace)]_MatcapBlendMode("Blend Mode", Float) = 0
		[EffectProperty(MATCAP)][NoScaleOffset]_MatcapTex("Matcap Tex", 2D) = "white" {}
		[EffectProperty(MATCAP)]_MatcapIntensity("Matcap Intensity", Range(0, 10)) = 1.0
		[EffectProperty(MATCAP)]_MatcapBlend("Matcap Blend", Range(0, 1)) = 1.0

		//Hit
		[Effect(EffectID# HIT, GroupID# ColorEffects)][Toggle(_HIT_ON)] _Hit("Hit", Float) = 0
		[EffectProperty(HIT)]_HitColor("Hit Color", Color) = (1, 1, 1, 1)
		[EffectProperty(HIT)]_HitGlow("Hit Glow", Range(0, 100)) = 5
		[EffectProperty(HIT)]_HitBlend("Hit Blend", Range(0, 1)) = 1.0

		//Contrast and Brightness
		[Effect(EffectID# CONTRAST_BRIGHTNESS, GroupID# ColorEffects)][Toggle(_CONTRAST_BRIGHTNESS_ON)]_ContrastBrightnessOn("Contrast and Brightness", Float) = 0
		[EffectProperty(CONTRAST_BRIGHTNESS)]_Contrast("Contrast", Range(0, 10)) = 1.0
		[EffectProperty(CONTRAST_BRIGHTNESS)]_Brightness("Brightness", Range(-1, 1)) = 0.0

		//Height Gradient
		[Effect(EffectID# HEIGHT_GRADIENT, GroupID# ColorEffects)][Toggle(_HEIGHT_GRADIENT_ON)]_HeightGradientOn("Height Gradient", Float) = 0
		[EffectProperty(HEIGHT_GRADIENT)][KeywordEnum(Local, World)]_HeightGradientPositionSpace("Position Space", Float) = 0
		[EffectProperty(HEIGHT_GRADIENT)]_MinGradientHeight("Min Height", Float) = 0.0
		[EffectProperty(HEIGHT_GRADIENT)]_MaxGradientHeight("Max Height", Float) = 0.75
		[EffectProperty(HEIGHT_GRADIENT)][HDR]_GradientHeightColor01("Gradient Color 01", Color) = (0.2, 0.2, 0.2, 1)
		[EffectProperty(HEIGHT_GRADIENT)][HDR]_GradientHeightColor02("Gradient Color 02", Color) = (1, 1, 1, 1)

		//Intersection Glow
		[Effect(EffectID# INTERSECTION_GLOW, GroupID# ColorEffects)][Toggle(_INTERSECTION_GLOW_ON)]_IntersectionGlowOn("Intersection Glow", Float) = 0
		[EffectProperty(INTERSECTION_GLOW)]_DepthGlowDist("Depth Distance", Range(0.01, 10)) = 0.2
		[EffectProperty(INTERSECTION_GLOW)]_DepthGlowPower("Depth Power", Float) = 25.0
		[EffectProperty(INTERSECTION_GLOW)]_DepthGlowColor("Depth Glow Color", Color) = (1.0, 0.987, 0.6, 1.0)
		[EffectProperty(INTERSECTION_GLOW)]_DepthGlowColorIntensity("Color Intensity", Float) = 25.0
		[EffectProperty(INTERSECTION_GLOW)]_DepthGlowGlobalIntensity("Global Intensity", Float) = 2.0
		
		//Albedo from Vertex Color
		[Effect(EffectID# ALBEDO_VERTEX_COLOR, GroupID# ColorEffects)][Toggle(_ALBEDO_VERTEX_COLOR_ON)]_AlbedoVertexColorOn("Albedo From Vertex Color", Float) = 0
		[EffectProperty(ALBEDO_VERTEX_COLOR)][KeywordEnum(Multiply, Replace)]_AlbedoVertexColorMode("Mode", Float) = 1
		[EffectProperty(ALBEDO_VERTEX_COLOR)]_VertexColorBlending("Blending", Range(0, 1)) = 1.0
		  
		//Texture Blending
		[Effect(EffectID# TEXTURE_BLENDING, GroupID# ColorEffects, CustomDrawer# TEXTURE_BLENDING_EFFECT_DRAWER)][Toggle(_TEXTURE_BLENDING_ON)]_TextureBlending ("Texture Blending", Float) = 0
		[EffectProperty(TEXTURE_BLENDING)][KeywordEnum(VertexColor, Texture)]_TextureBlendingSource("Source", Float) = 0
		[EffectProperty(ParentEffect# TEXTURE_BLENDING, Keywords(_TEXTUREBLENDINGSOURCE_TEXTURE), AllowReset# True)]_TexBlendingMask("Texture Blending Mask", 2D) = "white" {}
		[EffectProperty(ParentEffect# TEXTURE_BLENDING, Keywords(_TEXTUREBLENDINGSOURCE_TEXTURE), AllowReset# True)]_BlendingMaskCutoffG("Cutoff (G)", Range(0, 1)) = 0.1
		[EffectProperty(ParentEffect# TEXTURE_BLENDING, Keywords(_TEXTUREBLENDINGSOURCE_TEXTURE), AllowReset# True)]_BlendingMaskSmoothnessG("Smoothness (G)", Range(0, 1)) = 0.4
		[EffectProperty(ParentEffect# TEXTURE_BLENDING, Keywords(_TEXTUREBLENDINGSOURCE_TEXTURE), AllowReset# True)]_BlendingMaskCutoffB("Cutoff (B)", Range(0, 1)) = 0.1
		[EffectProperty(ParentEffect# TEXTURE_BLENDING, Keywords(_TEXTUREBLENDINGSOURCE_TEXTURE), AllowReset# True)]_BlendingMaskSmoothnessB("Smoothness (B)", Range(0, 1)) = 0.4
		[EffectProperty(ParentEffect# TEXTURE_BLENDING, Keywords(_TEXTUREBLENDINGSOURCE_TEXTURE), AllowReset# True)]_BlendingMaskCutoffWhite("Cutoff (White)", Range(0, 1)) = 0.15
		[EffectProperty(ParentEffect# TEXTURE_BLENDING, Keywords(_TEXTUREBLENDINGSOURCE_TEXTURE), AllowReset# True)]_BlendingMaskSmoothnessWhite("Smoothness (White)", Range(0, 1)) = 0.4
		[EffectProperty(TEXTURE_BLENDING)][KeywordEnum(RGB, BlackAndWhite)]_TextureBlendingMode("Blending Mode", Float) = 0
		[EffectProperty(TEXTURE_BLENDING)]_BlendingTextureG("Blending Texture (G)", 2D) = "white" {}
		[EffectProperty(TEXTURE_BLENDING)]_BlendingTextureB("Blending Texture (B)", 2D) = "white" {}
		[EffectProperty(TEXTURE_BLENDING)]_BlendingTextureWhite("Blending Texture (White)", 2D) = "white" {}
		[NoScaleOffset][EffectProperty(TEXTURE_BLENDING)]_BlendingNormalMapG("Blending Normal Map (G)", 2D) = "bump" {}
		[NoScaleOffset][EffectProperty(TEXTURE_BLENDING)]_BlendingNormalMapB("Blending Normal Map (B)", 2D) = "bump" {}
		[NoScaleOffset][EffectProperty(TEXTURE_BLENDING)]_BlendingNormalMapWhite("Blending Normal Map (White)", 2D) = "bump" {}

		//Depth Coloring
		[Effect(EffectID# DEPTH_COLORING, GroupID# ColorEffects)][Toggle(_DEPTH_COLORING_ON)]_DepthColoringOn("Depth Coloring", Float) = 0
		
		//Sub surface scattering
		[Effect(EffectID# SUBSURFACE_SCATTERING, GroupID# ColorEffects)][Toggle(_SUBSURFACE_SCATTERING_ON)]_SubsurfaceScattering("Fake Subsurface Scattering", Float) = 0.0
		[EffectProperty(SUBSURFACE_SCATTERING)]_NormalInfluence("Normal Influence", Range(0, 2.5)) = 0.5
		[EffectProperty(SUBSURFACE_SCATTERING)]_SSSPower("SSS Power", Range(0.01, 20)) = 1.0
		[EffectProperty(SUBSURFACE_SCATTERING)]_SSSFrontPower("SSS Front Power", Range(0.2, 20)) = 3.0
		[EffectProperty(SUBSURFACE_SCATTERING)]_SSSFrontAtten("SSS Front Atten", Range(0, 1)) = 0.3
		[EffectProperty(SUBSURFACE_SCATTERING)]_SSSAtten("SSS General Atten", Range(0, 1)) = 1.0
    	[EffectProperty(SUBSURFACE_SCATTERING)][HDR]_SSSColor("SSS Color", Color) = (1.0, 1.0, 1.0, 1.0)
		[EffectProperty(SUBSURFACE_SCATTERING)]_SSSMap("SSS Map", 2D) = "white" {}
    	
		//Alpha Cutoff
		[Effect(EffectID# ALPHA_CUTOFF, GroupID# AlphaEffects)][Toggle(_ALPHA_CUTOFF_ON)]_AlphaCutoffOn("Alpha Cutoff", Float) =  1.0
		[EffectProperty(ALPHA_CUTOFF)]_AlphaCutoffValue("Cutoff Value", Range(0, 1)) = 0.25

		//Fade
		[Effect(EffectID# FADE, GroupID# AlphaEffects)][Toggle(_FADE_ON)]_FadeOn("Fade", Float) = 0
		[EffectProperty(FADE)]_FadeTex("Fade Tex", 2D) = "white" {}
		[EffectProperty(FADE)]_FadeAmount("Fade Amount", Range(0, 1)) = 0.0
		[EffectProperty(FADE)]_FadePower("Fade Power", Range(0.25, 4.0)) = 1.0
		[EffectProperty(FADE)]_FadeTransition("Fade Transition", Range(0, 0.4)) = 0.2
		[EffectProperty(FADE)][Toggle(_FADE_BURN_ON)]_FadeBurnOn("Use Fade Burn Color?", Float) = 0.0
		[EffectProperty(ParentEffect# FADE, Keywords(_FADE_BURN_ON), AllowReset# True)][HDR]_FadeBurnColor("Fade Burn Color", Color) = (1,1,0,1) 
		[EffectProperty(ParentEffect# FADE, Keywords(_FADE_BURN_ON), AllowReset# True)]_FadeBurnWidth("Fade Burn Width", Range(0, 0.2)) = 0.01

		//Intersection Fade
		[Effect(EffectID# INTERSECTION_FADE, GroupID# AlphaEffects)][Toggle(_INTERSECTION_FADE_ON)]_IntersectionFadeOn("Intersection Fade", Float) = 0.0
		[EffectProperty(INTERSECTION_FADE)]_IntersectionFadeFactor("Intersection Fade Factor", Range(0.1, 3.0)) = 1.0

		//Alpha Round
		[Effect(EffectID# ALPHA_ROUND, GroupID# AlphaEffects)][Toggle(_ALPHA_ROUND_ON)]_AlphaRoundOn("Alpha Round", Float) = 0

		//
		[Effect(EffectID# OUTLINETYPE, GroupID# OtherEffects)][KeywordEnum(None, Simple, Constant, FadeWithDistance)]_OutlineType("Outline Type", Float) = 0

		//
		[PerRendererData]_TimingSeed("Timing Seed", Float) = 0
	}

	SubShader
	{
		PackageRequirements
		{
			"com.unity.render-pipelines.universal" : "12.0"
		}

		Pass
        {
			Name "AllIn13D_MainPass_URP"
			Tags
			{
				"RenderType" = "Opaque"
				"RenderPipeline" = "UniversalPipeline"
			}

			Blend [_BlendSrc] [_BlendDst]
			Cull [_CullingMode]
			ZWrite [_ZWrite]
			ZTest [_ZTestMode]
			ColorMask [_ColorMask]

			/*<STENCIL_BLOCK_START>*/
			Stencil
            {
                 Ref [_StencilRef]
                 Comp Always
				 Pass Replace
			}
			/*<STENCIL_BLOCK_END>*/

            HLSLPROGRAM
            #pragma vertex BasicVertex
            #pragma fragment BasicFragment
            
			#define URP_PASS

			#pragma multi_compile_fog

			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			// #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS

			// Universal Pipeline keywords
			
			#pragma multi_compile _ _FORWARD_PLUS
			#pragma multi_compile _ _SHADOWS_SOFT
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile _ EVALUATE_SH_MIXED EVALUATE_SH_VERTEX
			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING // v10+ only, renamed from "_MIXED_LIGHTING_SUBTRACTIVE"
			#pragma multi_compile _ _LIGHT_LAYERS
			#pragma multi_compile _ SHADOWS_SHADOWMASK // v10+ only

			// Baked GI
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			//#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			//#pragma multi_compile _ SHADOWS_SHADOWMASK


			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include_with_pragmas  "ShaderLibrary/AllIn13DShader_Features.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "ShaderLibrary/AllIn13DShader_CommonStructs.hlsl"
			#include "ShaderLibrary/AllIn13DShader_CommonFunctions.hlsl"
			#include "ShaderLibrary/AllIn13DShaderHelper_URP.hlsl"
			#include "ShaderLibrary/AllIn13DShaderLight.hlsl"
			#include "ShaderLibrary/AllIn13DShader_UVEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_VertexEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_FragmentEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_AlphaEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShaderCore.hlsl"
			#include "ShaderLibrary/AllIn13DShader_BasePass.hlsl"

            ENDHLSL 
        }

		Pass
		{
			Name "DepthNormals"
			Tags{ "Lightmode" = "DepthNormals" }
			
			ZWrite On
			Cull [_CullingMode]

			HLSLPROGRAM
			#pragma only_renderers gles gles3 glcore d3d11
            #pragma target 2.0

            #pragma vertex DepthNormalsVertex
            #pragma fragment DepthNormalsFragment

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include_with_pragmas  "ShaderLibrary/AllIn13DShader_Features.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "ShaderLibrary/AllIn13DShader_CommonStructs.hlsl"
			#include "ShaderLibrary/AllIn13DShader_CommonFunctions.hlsl"
			#include "ShaderLibrary/AllIn13DShaderHelper_URP.hlsl"
			#include "ShaderLibrary/AllIn13DShaderLight.hlsl"
			#include "ShaderLibrary/AllIn13DShader_UVEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_VertexEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_FragmentEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_AlphaEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShaderCore.hlsl"
			#include "ShaderLibrary/AllIn13DShader_URP_DepthNormalsPass.hlsl"
			
			ENDHLSL
		}
	}

	SubShader
	{
		Pass
        {
			Name "AllIn13D_Forward_BIRP"
			Tags { "LightMode" = "ForwardBase" }
			
			/*<STENCIL_BLOCK_START>*/
			Stencil
            {
                 Ref [_StencilRef]
                 Comp Always
				 Pass Replace
			}
			/*<STENCIL_BLOCK_END>*/

			Blend [_BlendSrc] [_BlendDst]
			Cull [_CullingMode]
			ZWrite [_ZWrite]
			ZTest [_ZTestMode]
			ColorMask [_ColorMask]

            HLSLPROGRAM
			#pragma target 3.0
			#pragma multi_compile_fwdbase
			#pragma multi_compile_fog
			#pragma multi_compile_instancing

			#define BIRP_PASS
			#define BUILTIN_MAIN_PASS

            #pragma vertex BasicVertex
            #pragma fragment BasicFragment

            #include "UnityCG.cginc"
			#include_with_pragmas "ShaderLibrary/AllIn13DShader_Features.hlsl"
			#include "AutoLight.cginc"
			#include "UnityLightingCommon.cginc"
			#include "ShaderLibrary/AllIn13DShader_CommonStructs.hlsl"
			#include "ShaderLibrary/AllIn13DShader_CommonFunctions.hlsl"
			#include "ShaderLibrary/AllIn13DShaderHelper_BIRP.hlsl"
			#include "ShaderLibrary/AllIn13DShaderLight.hlsl"
			#include "ShaderLibrary/AllIn13DShader_UVEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_VertexEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_FragmentEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_AlphaEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShaderCore.hlsl"
			#include "ShaderLibrary/AllIn13DShader_BasePass.hlsl"

            ENDHLSL
        }

		Pass
        {
			Name "FORWARD_DELTA"
			Tags { "LightMode" = "ForwardAdd"}

			Blend One One
			ZWrite Off
			ZTest LEqual
			Cull [_CullingMode]

            HLSLPROGRAM
			#pragma target 3.0

			#define BIRP_PASS
			#define FORWARD_ADD_PASS

			#pragma multi_compile_fwdadd_fullshadows
			#pragma multi_compile_fog
			
            #pragma vertex BasicVertex
            #pragma fragment BasicFragmentAdd

            #include "UnityCG.cginc"
			#include_with_pragmas  "ShaderLibrary/AllIn13DShader_Features.hlsl"
			#include "AutoLight.cginc"
			#include "UnityLightingCommon.cginc"
			#include "ShaderLibrary/AllIn13DShader_CommonStructs.hlsl"
			#include "ShaderLibrary/AllIn13DShader_CommonFunctions.hlsl"
			#include "ShaderLibrary/AllIn13DShaderHelper_BIRP.hlsl"
			#include "ShaderLibrary/AllIn13DShaderLight.hlsl"
			#include "ShaderLibrary/AllIn13DShader_UVEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_VertexEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_FragmentEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShader_AlphaEffects.hlsl"
			#include "ShaderLibrary/AllIn13DShaderCore.hlsl"
			#include "ShaderLibrary/AllIn13DShader_BasePass.hlsl"
			#include "ShaderLibrary/AllIn13DShaderLightAddPass.hlsl"

            ENDHLSL
        }
	}

	CustomEditor "AllIn13DShader.AllIn13DShaderMaterialInspector"
}
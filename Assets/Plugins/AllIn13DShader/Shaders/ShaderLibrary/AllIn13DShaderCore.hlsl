#ifndef ALLIN13DSHADER_CORE_INCLUDED
#define ALLIN13DSHADER_CORE_INCLUDED

float4 GetBaseColor(EffectsData data)
{
	float4 res = float4(0, 0, 0, 1);

#ifdef _TRIPLANAR_MAPPING_ON
	float4 colFront = 0;
	float4 colSide = 0;
	float4 colTop = 0;
	float4 colDown = 0;

	#ifdef _STOCHASTIC_SAMPLING_ON
		float2 dx = 0;
		float2 dy = 0;

		float stochasticScale = ACCESS_PROP(_StochasticScale);
		float stochasticSkew = ACCESS_PROP(_StochasticSkew);
		float4x3 stochasticOffsets_front = getStochasticOffsets(UV_FRONT(data), stochasticScale, stochasticSkew);
		STOCHASTIC_SAMPLING_NO_DEF_DD(_MainTex, UV_FRONT(data), stochasticOffsets_front, colFront)
	
		float4x3 stochasticOffsets_side = getStochasticOffsets(UV_SIDE(data), stochasticScale, stochasticSkew);
		STOCHASTIC_SAMPLING_NO_DEF_DD(_MainTex, UV_SIDE(data), stochasticOffsets_side, colSide)
	
		float4x3 stochasticOffsets_top = getStochasticOffsets(UV_TOP(data), stochasticScale, stochasticSkew);
		STOCHASTIC_SAMPLING_NO_DEF_DD(_TriplanarTopTex, UV_TOP(data), stochasticOffsets_top, colTop)
		
		float4x3 stochasticOffsets_down = getStochasticOffsets(UV_DOWN(data), stochasticScale, stochasticSkew);
		STOCHASTIC_SAMPLING_NO_DEF_DD(_MainTex, UV_DOWN(data), stochasticOffsets_down, colDown)
	#else
		colFront = SAMPLE_TEX2D(_MainTex, UV_FRONT(data));
		colSide = SAMPLE_TEX2D(_MainTex, UV_SIDE(data));
	
		colTop = SAMPLE_TEX2D(_TriplanarTopTex, UV_TOP(data));
		colDown = SAMPLE_TEX2D(_MainTex, UV_DOWN(data));
	#endif
	
	float faceDown = smoothstep(ACCESS_PROP(_FaceDownCutoff), 1.0, UV_DOWN_WEIGHT(data));
	colTop = lerp(colTop, colDown, faceDown);

	colFront *= UV_FRONT_WEIGHT(data);
	colSide *= UV_SIDE_WEIGHT(data);
	colTop *= UV_TOP_WEIGHT(data);
	
	res = colFront + colSide + colTop;
#else
	#ifdef _STOCHASTIC_SAMPLING_ON
		float4x3 stochasticOffsets = getStochasticOffsets(MAIN_UV(data), ACCESS_PROP(_StochasticScale), ACCESS_PROP(_StochasticSkew));
		STOCHASTIC_SAMPLING(_MainTex, MAIN_UV(data), stochasticOffsets, res)
	#else
		res = SAMPLE_TEX2D(_MainTex, MAIN_UV(data));
	#endif
#endif

	return res;
}

float3 GetNormalWS(EffectsData data, FragmentData i)
{
	float3 res = data.normalWS;
	
#ifdef _STOCHASTIC_SAMPLING_ON
	float4x3 stochasticOffset = 0;
	float2 dx = 0;
	float2 dy = 0;
#endif

#ifdef _NORMAL_MAP_ON
	#ifdef _TRIPLANAR_MAPPING_ON	
		#ifdef _TRIPLANARNORMALSPACE_LOCAL
			float3 normalReference = data.normalOS;
		#else 
			float3 normalReference = data.normalWS;
		#endif

		float4 sampledNormal_side = 0;
		float4 sampledNormal_top = 0;
		float4 sampledNormal_front = 0;
		
		#ifdef _STOCHASTIC_SAMPLING_ON
			float stochasticScale = ACCESS_PROP(_StochasticScale);
			float stochasticSkew = ACCESS_PROP(_StochasticSkew);	
			float4x3 stochasticOffsets_side = getStochasticOffsets(NORMAL_UV_SIDE(data), stochasticScale, stochasticSkew);
			STOCHASTIC_SAMPLING_NO_DEF_DD(_NormalMap, NORMAL_UV_SIDE(data), stochasticOffsets_side, sampledNormal_side)
			
			float4x3 stochasticOffsets_top = getStochasticOffsets(NORMAL_UV_TOP(data), stochasticScale, stochasticSkew);
			STOCHASTIC_SAMPLING_NO_DEF_DD(_TriplanarTopNormalMap, NORMAL_UV_TOP(data), stochasticOffsets_top, sampledNormal_top)
		
			float4x3 stochasticOffsets_front = getStochasticOffsets(NORMAL_UV_FRONT(data), stochasticScale, stochasticSkew);
			STOCHASTIC_SAMPLING_NO_DEF_DD(_NormalMap, NORMAL_UV_FRONT(data), stochasticOffsets_front, sampledNormal_front)
		#else
			sampledNormal_side	= SAMPLE_TEX2D(_NormalMap, NORMAL_UV_SIDE(data));
			sampledNormal_top	= SAMPLE_TEX2D(_TriplanarTopNormalMap, NORMAL_UV_TOP(data));
			sampledNormal_front = SAMPLE_TEX2D(_NormalMap, NORMAL_UV_FRONT(data));
		#endif
		
		float3 tnormalX = UnpackNormal(sampledNormal_side);
		tnormalX.xy *= ACCESS_PROP(_NormalStrength);

		float3 tnormalY = UnpackNormal(sampledNormal_top);
		tnormalY.xy *= ACCESS_PROP(_TopNormalStrength);

		float3 tnormalZ = UnpackNormal(sampledNormal_front);
		tnormalZ.xy *= ACCESS_PROP(_NormalStrength);

		tnormalX = float3(tnormalX.xy + normalReference.zy, normalReference.x);
		tnormalY = float3(tnormalY.xy + normalReference.xz, normalReference.y);
		tnormalZ = float3(tnormalZ.xy + normalReference.xy, normalReference.z);
	
		res = normalize(
			tnormalX.zyx * UV_SIDE_WEIGHT(data) +
			tnormalY.xzy * UV_TOP_WEIGHT(data) +
			tnormalZ.xyz * UV_FRONT_WEIGHT(data)
		);
	#else
	
		float4 sampledNormal = 0;
		#ifdef _STOCHASTIC_SAMPLING_ON
			float4x3 stochasticOffsets = getStochasticOffsets(MAIN_UV(data), ACCESS_PROP(_StochasticScale), ACCESS_PROP(_StochasticSkew));
			STOCHASTIC_SAMPLING_NO_DEF_DD(_NormalMap, MAIN_UV(data), stochasticOffsets, sampledNormal)
		#else
			sampledNormal = SAMPLE_TEX2D(_NormalMap, MAIN_UV(data));
		#endif
		
		float3 tnormal = UnpackNormal(sampledNormal);
		tnormal.xy *= ACCESS_PROP(_NormalStrength); 

		res.x = dot(i.tspace0, tnormal);
		res.y = dot(i.tspace1, tnormal);
		res.z = dot(i.tspace2, tnormal);
    
		res = normalize(res);
	#endif

	#if defined(_TEXTURE_BLENDING_ON)
		
		#ifdef _TEXTUREBLENDINGSOURCE_TEXTURE
			float3 maskColor = SAMPLE_TEX2D(_TexBlendingMask, /*data.mainUV*/MAIN_UV(data)).rgb;
		#else
			float3 maskColor = data.vertexColor.rgb;
		#endif

		#ifdef _TEXTUREBLENDINGMODE_RGB
			float2 texGUV = SIMPLE_CUSTOM_TRANSFORM_TEX(data.rawMainUV, _BlendingTextureG);
			float2 texBUV = SIMPLE_CUSTOM_TRANSFORM_TEX(data.rawMainUV, _BlendingTextureB);
			
			float4 sampledNormalG = 0;
			float4 sampledNormalB = 0;
			#ifdef _STOCHASTIC_SAMPLING_ON
				STOCHASTIC_SAMPLING_COMPLETE_NO_DEF_DD(_BlendingNormalMapG, texGUV, stochasticOffset, sampledNormalG)
				STOCHASTIC_SAMPLING_COMPLETE_NO_DEF_DD(_BlendingNormalMapB, texBUV, stochasticOffset, sampledNormalB)
			#else
				sampledNormalG = SAMPLE_TEX2D(_BlendingNormalMapG, texGUV);
				sampledNormalB = SAMPLE_TEX2D(_BlendingNormalMapB, texBUV);
			#endif
			
			float3 tnormalG = UnpackNormal(sampledNormalG);
			float3 tnormalB = UnpackNormal(sampledNormalB);
			
			float3 normalG = GetNormalWSFromNormalMap(
				tnormalG, ACCESS_PROP(_NormalStrength), 
				i.tspace0, i.tspace1, i.tspace2);

			float3 normalB = GetNormalWSFromNormalMap(
				tnormalB, ACCESS_PROP(_NormalStrength), 
				i.tspace0, i.tspace1, i.tspace2);

			res = normalize(lerp(res, normalG, maskColor.g));
			res = normalize(lerp(res, normalB, maskColor.b));
		#elif _TEXTUREBLENDINGMODE_BLACKANDWHITE
			float2 texWhiteUV = SIMPLE_CUSTOM_TRANSFORM_TEX(data.rawMainUV, _BlendingTextureWhite);
			
			float4 sampledNormalWhite = 0;
			#ifdef _STOCHASTIC_SAMPLING_ON
				STOCHASTIC_SAMPLING_COMPLETE_NO_DEF_DD(_BlendingNormalMapWhite, texWhiteUV, stochasticOffset, sampledNormalWhite)
			#else
				sampledNormalWhite = SAMPLE_TEX2D(_BlendingNormalMapWhite, texWhiteUV);
			#endif
			
			float3 tnormalWhite = UnpackNormal(sampledNormalWhite);
			float3 normalWhite = GetNormalWSFromNormalMap(
				tnormalWhite, ACCESS_PROP(_NormalStrength), 
				i.tspace0, i.tspace1, i.tspace2);
			float maskLuminosity = GetLuminanceRaw(float4(maskColor.rgb, 1.0));
			maskLuminosity = saturate(maskLuminosity);
			res = lerp(res, normalWhite, maskLuminosity);
		#endif
	#endif
#endif

	return res;
}

EffectsData CalculateEffectsData(FragmentData i)
{
	EffectsData res;
	
	res.vertexColor = float4(
		VERTEX_COLOR_R(i), 
		VERTEX_COLOR_G(i), 
		VERTEX_COLOR_B(i), 
		VERTEX_COLOR_A(i));

#ifdef URP_PASS
	res.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(i.pos);
#endif

	res.vertexColorLuminosity = GetLuminanceRaw(float4(res.vertexColor.r, res.vertexColor.g, res.vertexColor.b, 1.0));
	res.vertexColorLuminosity = saturate(res.vertexColorLuminosity);

	res.mainUV = SCALED_MAIN_UV(i);
	res.rawMainUV = RAW_MAIN_UV(i);
	
	res.vertexOS = POSITION_OS(i);
	res.vertexWS = POSITION_WS(i);
	res.normalOS = NORMAL_OS(i);
	res.normalWS = normalize(i.normalWS);
	res.viewDirWS = VIEWDIR_WS(i);

	res.tangentWS = 0;
	res.bitangentWS = 0;
#ifdef REQUIRE_TANGENT_WS
	res.tangentWS = normalize(TANGENT_WS(i));
	res.bitangentWS = normalize(cross(res.normalWS.xyz, res.tangentWS.xyz));
#endif

#ifdef _NORMAL_MAP_ON
	res.uv_normalMap = UV_NORMAL_MAP(i);
#endif
	
	res.lightColor = GetMainLightColorRGB();
	res.lightDir = GetMainLightDir(POSITION_WS(i));
	
	res.projPos = 0;
	res.sceneDepthDiff = 0;
	res.normalizedDepth = 0;
#ifdef REQUIRE_SCENE_DEPTH
	res.projPos = i.projPos;
	res.sceneDepthDiff = GetSceneDepthDiff(i.projPos);
	res.normalizedDepth = GetNormalizedDepth(i.projPos);
#endif

	res.camDistance = 0;
#ifdef REQUIRE_CAM_DISTANCE
	float3 positionVS = mul(UNITY_MATRIX_V, float4(POSITION_WS(i), 1.0));
	res.camDistanceViewSpace = -positionVS.z;
	res.camDistance = distance(POSITION_WS(i), _WorldSpaceCameraPos);
#endif

#ifdef _UV_DISTORTION_ON
	res.uv_dist = SIMPLE_CUSTOM_TRANSFORM_TEX(i.mainUV.xy, _DistortTex);
#endif

	res.shaderTime = SHADER_TIME(i);
	
	res.uvMatrix = 0;
	res.uvMatrix._m00_m01 = i.mainUV.xy;

#ifdef _NORMAL_MAP_ON
	res.uv_matrix_normalMap = 0;
	MAIN_NORMAL_UV(res) = UV_NORMAL_MAP(i);
#endif

	res.uvDiff = UV_DIFF(i);
	
	res._ShadowCoord = i._ShadowCoord;

	return res;
}


float2 ApplyUVEffects_VertexStage(float2 inputUV, float3 vertexWS, float4 projPos, float3 shaderTime)
{
	float2 res = inputUV;

#ifdef _SCREEN_SPACE_UV_ON
	res = ScreenSpaceUV(res, vertexWS, projPos);
#endif
	
#ifdef _SCROLL_TEXTURE_ON
	res = ScrollTexture(res, shaderTime);
#endif

#ifdef _WAVE_UV_ON
	res = WaveUV(res, shaderTime);
#endif

#ifdef _HAND_DRAWN_ON
	res = HandDrawn(res, shaderTime);
#endif

	return res;
}

EffectsData ApplyUVEffects_FragmentStage(EffectsData data)
{
	EffectsData res = data;

#ifdef _TRIPLANAR_MAPPING_ON
	res = TriplanarMapping(res);
#endif

#ifdef _UV_DISTORTION_ON
	res = UVDistortion(res);
#endif

#ifdef _PIXELATE_ON
	res = Pixelate(res);
#endif

	return res;
}

float4 ApplyVertexEffects(float4 vertexOS, float3 normalOS, float3 shaderTime)
{
	float4 res = vertexOS;
	
#ifdef _VERTEX_SHAKE_ON
	res.xyz = VertexShake(res.xyz, shaderTime);
#endif

#ifdef _VERTEX_INFLATE_ON
	res.xyz = VertexInflate(res.xyz, normalOS, shaderTime);
#endif

#ifdef _VERTEX_DISTORTION_ON
	res.xyz = VertexDistortion(res.xyz, normalOS, shaderTime);
#endif

#ifdef _VOXELIZE_ON
	res.xyz = VertexVoxel(res.xyz);
#endif

#ifdef _GLITCH_ON
	res.xyz = Glitch(res.xyz, shaderTime);
#endif

	return res;
}

float4 ApplyColorEffectsBeforeLighting(float4 inputColor, EffectsData data)
{
	float4 res = inputColor;

#ifdef _ALBEDO_VERTEX_COLOR_ON
	res = AlbedoVertexColor(res, data);
#endif

#ifdef _TEXTURE_BLENDING_ON
	res = TextureBlending(res, data);
#endif

#ifdef _HOLOGRAM_ON
	res = Hologram(res, data);
#endif

#ifdef _HEIGHT_GRADIENT_ON
	res.rgb = HeightGradient(res.rgb, data);
#endif

#ifdef _HUE_SHIFT_ON
	res = HueShift(res);
#endif

#ifdef _MATCAP_ON
	float3 matcap = Matcap(data);
	
	#ifdef _MATCAPBLENDMODE_MULTIPLY
		float3 colorWithMatcapApplied = res.rgb * matcap;
	#elif _MATCAPBLENDMODE_REPLACE
		float3 colorWithMatcapApplied = matcap;
	#endif

	res.rgb = lerp(res.rgb, colorWithMatcapApplied, ACCESS_PROP(_MatcapBlend));
#endif

#ifdef _POSTERIZE_ON
	res.rgb = Posterize(res.rgb);
#endif

#ifdef _CONTRAST_BRIGHTNESS_ON
	res.rgb = ContrastBrightness(res.rgb);
#endif

#if defined(_GREYSCALE_ON) && defined(_GREYSCALESTAGE_BEFORELIGHTING)
	res.rgb = Greyscale(res.rgb);
#endif

#if defined(_RIM_LIGHTING_ON) && defined(_RIMLIGHTINGSTAGE_BEFORELIGHTING)
	res.rgb = Rim(res.rgb, data);
#endif

#if defined(_COLOR_RAMP_ON) && defined(_COLORRAMPLIGHTINGSTAGE_BEFORELIGHTING)
	res = ColorRamp(res, data);
#endif

#if defined(_RIM_LIGHTING_ON) && defined(_RIMLIGHTINGSTAGE_BEFORELIGHTINGLAST)
	res.rgb = Rim(res.rgb, data);
#endif
	
	return res;
}

float4 ApplyColorEffectsAfterLighting(float4 inputColor, EffectsData data)
{
	float4 res = inputColor;
	
#ifdef _SUBSURFACE_SCATTERING_ON
	res = SubsurfaceScattering(inputColor, data);
#endif

#ifdef _HIT_ON
	res = Hit(res);
#endif

#ifdef _HIGHLIGHTS_ON
	res.rgb = Highlights(res.rgb, data);
#endif

#ifdef _DEPTH_COLORING_ON
	res = DepthColoring(res, data);
#endif

#if defined(_RIM_LIGHTING_ON) && defined(_RIMLIGHTINGSTAGE_AFTERLIGHTING)
	res.rgb = Rim(res.rgb, data);
#endif

#if defined(_GREYSCALE_ON) && defined(_GREYSCALESTAGE_AFTERLIGHTING)
	res.rgb = Greyscale(res.rgb);
#endif

#ifdef _INTERSECTION_GLOW_ON
	res.rgb = IntersectionGlow(res, data);
#endif

#if defined(_COLOR_RAMP_ON) && defined(_COLORRAMPLIGHTINGSTAGE_AFTERLIGHTING)
	res = ColorRamp(res, data);
#endif

	return res;
}

float4 ApplyAlphaEffects(float4 inputColor, float2 uv, float sceneDepthDiff)
{
	float4 res = inputColor;
#ifdef _FADE_ON
	res = Fade(res, uv);
#endif

#ifdef _INTERSECTION_FADE_ON
	res = IntersectionFade(res, sceneDepthDiff);
#endif

#ifdef _ALPHA_ROUND_ON
	res.a = round(res.a);
#endif

	return res;
}

#endif
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

CBUFFER_START(UnityPerMaterial)
	float4 _ShallowColor;
	float4 _BaseColor;
	half _ColorAbsorption;

	//float _Smoothness;
	//float _Metallic;

	float4 _IntersectionColor;
	float _DepthVertical;
	float _DepthHorizontal;
	float _WorldSpaceUV;
	float2 _NormalTiling;
	float _NormalSubTiling;
	float _NormalSpeed;
	float _NormalSubSpeed;
	half _NormalStrength;

	half2 _DistanceNormalsFadeDist;
	half _DistanceNormalsTiling;

	half _TranslucencyStrength;
	half _TranslucencyStrengthDirect;
	half _TranslucencyExp;
	half _TranslucencyCurvatureMask;
	half _EdgeFade;
	float4 _HorizonColor;
	half _HorizonDistance;
	float _SparkleIntensity;
	half _SparkleSize;
	half _SunReflectionDistortion;
	half _SunReflectionSize;
	float _SunReflectionStrength;
	float _PointSpotLightReflectionStrength;
	half _PointSpotLightReflectionSize;
	half _PointSpotLightReflectionDistortion;
	float _ReflectionDistortion;
	float _ReflectionBlur;
	float _ReflectionFresnel;
	float _ReflectionStrength;
	half _ReflectionLighting;
	bool _PlanarReflectionsEnabled;
	bool _ScreenSpaceReflectionsEnabled;
	half _ShadowStrength;
	float2 _Direction;
	float _Speed;

	half _SlopeStretching;
	half _SlopeSpeed;
	half _SlopeAngleThreshold;
	half _SlopeAngleFalloff;
	half _SlopeFoam;

	//Foam
	float4 _FoamColor;
	float _FoamSpeed;
	float _FoamSubSpeed;
	float2 _FoamTiling;
	float _FoamSubTiling;
	half _FoamBaseAmount;
	half _FoamStrength;
	half _FoamClipping;
	half2 _FoamCrestMinMaxHeight;
	half _FoamBubblesSpread;
	half _FoamBubblesStrength;
	half _FoamDistortion;

	float _FoamTilingDynamic;
	float _FoamSubTilingDynamic;
	float _FoamSpeedDynamic;
	float _FoamSubSpeedDynamic;

	//Intersection
	half _IntersectionSource;
	half _IntersectionLength;
	half _IntersectionFalloff;
	half _IntersectionTiling;
	half _IntersectionDistortion;
	half _IntersectionRippleDist;
	half _IntersectionRippleStrength;
	float _IntersectionRippleSpeed;
	half _IntersectionClipping;
	bool _IntersectionSharp;
	float _IntersectionSpeed;

	//Waves
	half _WaveHeight;
	float _WaveFrequency;
	half _WaveNormalStr;
	float _WaveDistance;
	half2 _WaveFadeDistance;
	float _WaveSteepness;
	uint _WaveMaxLayers;
	half4 _WaveDirection;
	float _WaveSpeed;

	half _ShoreLineWaveStr;
	half _ShoreLineWaveDistance;
	half _ShoreLineLength;

	//Underwater
	half _CausticsBrightness;
	half _CausticsChromance;
	float _CausticsTiling;
	half _CausticsSpeed;
	half _RefractionStrength;
	half _RefractionChromaticAberration;
	half _CausticsDistortion;
	bool _EnableDirectionalCaustics;

	half _UnderwaterSurfaceSmoothness;
	half _UnderwaterRefractionOffset;

	half _VertexColorDepth;
	half _VertexColorWaveFlattening;
	half _VertexColorFoam;

	bool _ReceiveDynamicEffects;

	half _WaveTint;
	float4 _WaveProfile_TexelSize;
//#ifdef TESSELLATION_ON	
	float _TessValue;
	float _TessMin;
	float _TessMax;
//#endif
CBUFFER_END
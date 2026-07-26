#ifndef ALLIN13DSHADER_COMMON_STRUCTS
#define ALLIN13DSHADER_COMMON_STRUCTS

#define T_SPACE_PROPERTIES(n1, n2, n3) float3 tspace0 : TEXCOORD##n1; float3 tspace1 : TEXCOORD##n2; float3 tspace2 : TEXCOORD##n3;
#define INIT_T_SPACE(normalWS) \
	o.tspace0 = float3(tangentWS.x, bitangentWS.x, normalWS.x); \
	o.tspace1 = float3(tangentWS.y, bitangentWS.y, normalWS.y); \
	o.tspace2 = float3(tangentWS.z, bitangentWS.z, normalWS.z);


#define UV_FRONT(data) data.uvMatrix._m00_m01
#define UV_FRONT_WEIGHT(data) data.uvMatrix._m02

#define UV_SIDE(data) data.uvMatrix._m10_m11
#define UV_SIDE_WEIGHT(data) data.uvMatrix._m12

#define UV_TOP(data) data.uvMatrix._m20_m21
#define UV_TOP_WEIGHT(data) data.uvMatrix._m22

#define UV_DOWN(data) data.uvMatrix._m30_m31
#define UV_DOWN_WEIGHT(data) data.uvMatrix._m32

#define MAIN_UV(data) data.uvMatrix._m00_m01

//Main UV
#define SCALED_MAIN_UV(input)	input.mainUV.xy
#define RAW_MAIN_UV(input)		input.mainUV.zw

//Tangent WS
#define TANGENT_WS(input) float3(input.tspace0.x, input.tspace1.x, input.tspace2.x)

//Interpolator 01
#define UV_LIGHTMAP(input) input.interpolator_01.xy
#define UV_DIFF(input) input.interpolator_01.zw

//Interpolator 02
#define UV_NORMAL_MAP(input) input.interpolator_02.xy
#define UV_EMISSION_MAP(input) input.interpolator_02.zw

//Interpolator 03
#define SHADER_TIME(input) input.interpolator_03.xyz
#define FOGCOORD(input) input.interpolator_03.w

//Interpolator 04
#define NORMAL_OS(input) input.interpolator_04.xyz
#define VERTEX_COLOR_R(input) input.interpolator_04.w

//Interpolator 05
#define POSITION_OS(input) input.interpolator_05.xyz
#define VERTEX_COLOR_G(input) input.interpolator_05.w

//Interpolator 06
#define POSITION_WS(input) input.interpolator_06.xyz
#define VERTEX_COLOR_B(input) input.interpolator_06.w

//Interpolator 07
#define VIEWDIR_WS(input) input.interpolator_07.xyz
#define VERTEX_COLOR_A(input) input.interpolator_07.w


#ifdef _NORMAL_MAP_ON
	#define NORMAL_UV_FRONT(data) data.uv_matrix_normalMap._m00_m01
	#define NORMAL_UV_SIDE(data) data.uv_matrix_normalMap._m10_m11
	#define NORMAL_UV_TOP(data) data.uv_matrix_normalMap._m20_m21

	#define MAIN_NORMAL_UV(data) data.uv_matrix_normalMap._m00_m01
#endif


#ifdef _NORMAL_MAP_ON
	#ifdef _TRIPLANAR_MAPPING_ON
		#define DISPLACE_ALL_UVS(data, displacementAmount) \
			UV_FRONT(data) += displacementAmount; \
			UV_SIDE(data) += displacementAmount; \
			UV_TOP(data) += displacementAmount; \
			UV_DOWN(data) += displacementAmount; \
			NORMAL_UV_FRONT(data) += displacementAmount; \
			NORMAL_UV_SIDE(data) += displacementAmount; \
			NORMAL_UV_TOP(data) += displacementAmount; \
			data.rawMainUV += displacementAmount;
			
		#define QUANTIZE_ALL_UVS(data, quantizeFactor) \
			UV_FRONT(data)	= floor(UV_FRONT(data)	* quantizeFactor) / quantizeFactor; \
			UV_SIDE(data)	= floor(UV_SIDE(data)	* quantizeFactor) / quantizeFactor; \
			UV_TOP(data)	= floor(UV_TOP(data)	* quantizeFactor) / quantizeFactor; \
			UV_DOWN(data)	= floor(UV_DOWN(data)	* quantizeFactor) / quantizeFactor; \
			NORMAL_UV_FRONT(data)	= floor(NORMAL_UV_FRONT(data)	* quantizeFactor) / quantizeFactor; \
			NORMAL_UV_SIDE(data)	= floor(NORMAL_UV_SIDE(data)	* quantizeFactor) / quantizeFactor; \
			NORMAL_UV_TOP(data)		= floor(NORMAL_UV_TOP(data)		* quantizeFactor) / quantizeFactor; \
			data.rawMainUV = floor(data.rawMainUV * quantizeFactor) / quantizeFactor;
		
		#define FLOOR_ALL_UVS(data) \
			UV_FRONT(data)	= floor(UV_FRONT(data)); \
			UV_SIDE(data)	= floor(UV_SIDE(data)); \
			UV_TOP(data)	= floor(UV_TOP(data)); \
			UV_DOWN(data)	= floor(UV_DOWN(data)); \
			NORMAL_UV_FRONT(data)	= floor(NORMAL_UV_FRONT(data)); \
			NORMAL_UV_SIDE(data)	= floor(NORMAL_UV_SIDE(data)); \
			NORMAL_UV_TOP(data)		= floor(NORMAL_UV_TOP(data)); \
			data.rawMainUV = floor(data.rawMainUV);
	#else
		#define QUANTIZE_ALL_UVS(data, quantizeFactor) \
			MAIN_UV(data) = floor(MAIN_UV(data) * quantizeFactor) / quantizeFactor; \
			MAIN_NORMAL_UV(data) = floor(MAIN_NORMAL_UV(data) * quantizeFactor) / quantizeFactor; \
			data.rawMainUV = floor(data.rawMainUV * quantizeFactor) / quantizeFactor;
			
		#define DISPLACE_ALL_UVS(data, displacementAmount) \
			MAIN_UV(data) += displacementAmount; \
			MAIN_NORMAL_UV(data) += displacementAmount; \
			data.rawMainUV += displacementAmount;
	#endif
#else
	#ifdef _TRIPLANAR_MAPPING_ON
		#define DISPLACE_ALL_UVS(data, displacementAmount) \
			data.uvMatrix._m00_m01 += displacementAmount; \
			data.uvMatrix._m10_m11 += displacementAmount; \
			data.uvMatrix._m20_m21 += displacementAmount; \
			data.uvMatrix._m30_m31 += displacementAmount; \
			data.rawMainUV += displacementAmount;
			
		#define QUANTIZE_ALL_UVS(data, quantizeFactor) \
			data.uvMatrix._m00_m01 = floor(data.uvMatrix._m00_m01 * quantizeFactor) / quantizeFactor; \
			data.uvMatrix._m10_m11 = floor(data.uvMatrix._m10_m11 * quantizeFactor) / quantizeFactor; \
			data.uvMatrix._m20_m21 = floor(data.uvMatrix._m20_m21 * quantizeFactor) / quantizeFactor; \
			data.uvMatrix._m30_m31 = floor(data.uvMatrix._m30_m31 * quantizeFactor) / quantizeFactor; \
			data.rawMainUV = floor(data.rawMainUV * quantizeFactor) / quantizeFactor;
	#else
		#define DISPLACE_ALL_UVS(data, displacementAmount) \
			MAIN_UV(data)	+= displacementAmount; \
			data.rawMainUV	+= displacementAmount;
			
		#define QUANTIZE_ALL_UVS(data, quantizeFactor) \
			MAIN_UV(data) = floor(MAIN_UV(data) * quantizeFactor) / quantizeFactor; \
			data.rawMainUV = floor(data.rawMainUV * quantizeFactor) / quantizeFactor;
	#endif
#endif

#define RECALCULATE_NORMAL_OFFSET 0.01

struct AllIn1LightData
{
	float3 lightColor;
	float3 lightDir;
	float realtimeShadow;
	float4 shadowColor;
	float distanceAttenuation;
};

struct ShadowCoordStruct
{
	float4 _ShadowCoord : TEXCOORD0;
	float4 pos : TEXCOORD1;
};

struct FogStruct
{
	float fogCoord : TEXCOORD0;
};

struct VertexData
{
	float4 vertex		: POSITION;
	float2 uv			: TEXCOORD0;
	float2 uvLightmap	: TEXCOORD1;
	float3 normal		: NORMAL;
	float4 tangent		: TANGENT;
	float4 vertexColor	: COLOR;

#ifdef INSTANCING_ON
	UNITY_VERTEX_INPUT_INSTANCE_ID
#endif
};

struct FragmentDataOutline
{
	float4 pos	: SV_POSITION;
	float2 mainUV	: TEXCOORD0;
	float3 normalWS : TEXCOORD1;
	
	float4 interpolator_01 : TEXCOORD2;
	float4 interpolator_02 : TEXCOORD3;
	float4 interpolator_03 : TEXCOORD4;

#ifdef INSTANCING_ON
	UNITY_VERTEX_INPUT_INSTANCE_ID
#endif
	
	UNITY_VERTEX_OUTPUT_STEREO 
};

struct FragmentData
{
	float4 pos	: SV_POSITION;
	float4 mainUV	: TEXCOORD0;
	
	float3 normalWS : TEXCOORD1;

	float4 interpolator_01 : TEXCOORD2;
	float4 interpolator_02 : TEXCOORD3;
	float4 interpolator_03 : TEXCOORD4;
	float4 interpolator_04 : TEXCOORD5;
	float4 interpolator_05 : TEXCOORD6;
	float4 interpolator_06 : TEXCOORD7;
	float4 interpolator_07 : TEXCOORD8;

#ifdef REQUIRE_TANGENT_WS
	T_SPACE_PROPERTIES(9, 10, 11)
#endif

#ifdef REQUIRE_SCENE_DEPTH
	float4 projPos : TEXCOORD12;
#endif
	
	float4 _ShadowCoord : TEXCOORD13;

#ifdef INSTANCING_ON
	UNITY_VERTEX_INPUT_INSTANCE_ID
#endif
	
	UNITY_VERTEX_OUTPUT_STEREO 
};

struct FragmentDataShadowCaster
{
	float4 pos	: SV_POSITION;
	float4 positionOS : TEXCOORD1;
	float2 mainUV : TEXCOORD2;

#ifdef INSTANCING_ON
	UNITY_VERTEX_INPUT_INSTANCE_ID
#endif
	
	UNITY_VERTEX_OUTPUT_STEREO 
};

struct TriplanarData
{
	float2 uv_triplanar_front;
	float2 uv_triplanar_side;
	float2 uv_triplanar_top;
};

struct EffectsData
{
	float2 mainUV;
	float2 rawMainUV;

#ifdef URP_PASS
	float2 normalizedScreenSpaceUV;
#endif

	float4 vertexColor;
	float vertexColorLuminosity;

	float3 vertexOS;
	float3 vertexWS;
	float3 normalOS;
	float3 normalWS;
	float3 viewDirWS;

	float3 tangentWS;
	float3 bitangentWS;

	float4 projPos;
	
	float3 lightColor;
	float3 lightDir;
	
	float sceneDepthDiff;
	float normalizedDepth;
	float camDistance;
	float camDistanceViewSpace;
	float3 shaderTime;

#ifdef _UV_DISTORTION_ON
	float2 uv_dist;
#endif
	
	float4x3 uvMatrix;
#ifdef _NORMAL_MAP_ON
	float4x3 uv_matrix_normalMap;
	float2 uv_normalMap;
#endif

	float2 uvDiff;

	float4 _ShadowCoord;
};

#ifdef _TRIPLANAR_MAPPING_ON
float3 GetTriplanarWeights(float3 normal)
{
	float3 weights = abs(normal);
	weights = pow(weights, ACCESS_PROP(_TriplanarSharpness));
	weights = weights / (weights.x + weights.y + weights.z);

	return weights;
}
#endif

#endif
#ifndef ALLIN13DSHADER_COMMON_FUNCTIONS
#define ALLIN13DSHADER_COMMON_FUNCTIONS

#define ALLIN13DSHADER_PI            3.14159265359f
#define ALLIN13DSHADER_TWO_PI        6.28318530718f
#define ALLIN13DSHADER_FOUR_PI       12.56637061436f
#define ALLIN13DSHADER_INV_PI        0.31830988618f
#define ALLIN13DSHADER_INV_TWO_PI    0.15915494309f
#define ALLIN13DSHADER_INV_FOUR_PI   0.07957747155f
#define ALLIN13DSHADER_HALF_PI       1.57079632679f
#define ALLIN13DSHADER_INV_HALF_PI   0.636619772367f

#define CUSTOM_TRANSFORM_TEX(uv, increment, name) ((uv.xy + increment.xy) * ACCESS_PROP(name##_ST).xy + ACCESS_PROP(name##_ST).zw)
#define SIMPLE_CUSTOM_TRANSFORM_TEX(uv, name) uv.xy * ACCESS_PROP(name##_ST).xy + ACCESS_PROP(name##_ST).zw

#ifdef _CUSTOM_SHADOW_COLOR_ON
	#define SHADOW_COLOR global_shadowColor
#else
	#define SHADOW_COLOR 0
#endif

float Pow_5(float x)
{
	float x2 = x * x;
	float res = x2 * x2 * x;
	return res;
}

float EaseOutQuint(float x) 
{
	return 1 - Pow_5(1 - x);
}

float RemapFloat(float inValue, float inMin, float inMax, float outMin, float outMax)
{
	return outMin + (inValue - inMin) * (outMax - outMin) / (inMax - inMin);
}

float GetLuminanceRaw(float4 col)
{
	float res = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
	return res;
}

float GetLuminance(float4 col)
{
	return GetLuminanceRaw(col);
}

float GetLuminance(float3 col)
{
	return GetLuminance(float4(col, 1.0));
}

float noise(float2 p)
{
    return frac(sin(dot(p, float2(12.9898, 78.233))) * 43758.5453);
}

float noise2D(float2 p)
{
    float2 ip = floor(p);
    float2 fp = frac(p);
    fp = fp * fp * (3 - 2 * fp);
                
    float n00 = noise(ip);
    float n01 = noise(ip + float2(0, 1));
    float n10 = noise(ip + float2(1, 0));
    float n11 = noise(ip + float2(1, 1));
                
    return lerp(lerp(n00, n01, fp.y), lerp(n10, n11, fp.y), fp.x);
}

//hash for randomness
float2 hash2D2D(float2 s)
{
	//magic numbers
	return frac(sin(fmod(float2(dot(s, float2(127.1, 311.7)), dot(s, float2(269.5, 183.3))), 3.14159)) * 43758.5453);
}

//float4x3 getStochasticOffsets(float2 uv, float scale = 3.464, float skewAmount = 0.57735027)
float4x3 getStochasticOffsets(float2 uv, float scale, float skewAmount)
{
	//triangle vertices and blend weights
	//BW_vx[0...2].xyz = triangle verts
	//BW_vx[3].xy = blend weights (z is unused)
	float4x3 BW_vx;
    
	//uv transformed into triangular grid space with UV scaled by approximation of 2*sqrt(3)
	float2 skewUV = mul(float2x2(1.0, 0.0, -skewAmount, 1.15470054), uv * scale);
    
	//vertex IDs and barycentric coords
	float2 vxID = float2(floor(skewUV));
	float3 barry = float3(frac(skewUV), 0);
	barry.z = 1.0 - barry.x - barry.y;
    
	BW_vx = ((barry.z > 0) ?
		float4x3(float3(vxID, 0), float3(vxID + float2(0, 1), 0), float3(vxID + float2(1, 0), 0), barry.zyx) :
		float4x3(float3(vxID + float2(1, 1), 0), float3(vxID + float2(1, 0), 0), float3(vxID + float2(0, 1), 0), float3(-barry.z, 1.0 - barry.y, 1.0 - barry.x)));

	return BW_vx;
}

#ifdef _NORMAL_MAP_ON
float3 GetNormalWSFromNormalMap(float3 tnormal, float normalStrength, 
	float3 tspace0, float3 tspace1, float3 tspace2)
{
	tnormal.xy *= normalStrength; 

	float3 res = 0;
	res.x = dot(tspace0, tnormal);
	res.y = dot(tspace1, tnormal);
	res.z = dot(tspace2, tnormal);
    
	res = normalize(res);

	return res;
}

//float3 GetNormalWSFromNormalMap(sampler2D normalMap, float2 uv, float normalStrength, 
//	float3 tspace0, float3 tspace1, float3 tspace2)
//{
//	float3 tnormal = UnpackNormal(SAMPLE_TEX2D(normalMap, uv));
	
//	return GetNormalWSFromNormalMap(tnormal, normalStrength, tspace0, tspace1, tspace2);
	
//	//tnormal.xy *= normalStrength; 

//	//float3 res = 0;
//	//res.x = dot(tspace0, tnormal);
//	//res.y = dot(tspace1, tnormal);
//	//res.z = dot(tspace2, tnormal);
    
//	//res = normalize(res);

//	//return res;
//}
#endif


#ifdef REQUIRE_SCENE_DEPTH
float ComputeEyeDepth(float3 vertexWS)
{
	float3 positionVS = mul(UNITY_MATRIX_V, float4(vertexWS, 1.0)).xyz;
	float res = -positionVS.z;
	
	return res;
}

float GetNormalizedDepth(float4 projPos)
{
	float nearClip = _ProjectionParams.y + global_MinDepth;
	float farClip = nearClip + global_DepthZoneLength;

	float cameraRange = farClip - nearClip;
		
	float distanceToNearClip = projPos.z - global_MinDepth;
		
	float res = distanceToNearClip / cameraRange;
	
	res = saturate(res);
	
	return res;
}

#endif

#endif
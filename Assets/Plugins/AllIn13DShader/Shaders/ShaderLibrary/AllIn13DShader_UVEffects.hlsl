#ifndef ALLIN13DSHADER_UV_EFFECTS
#define ALLIN13DSHADER_UV_EFFECTS

#ifdef _SCROLL_TEXTURE_ON
float2 ScrollTexture(float2 inputUV, float3 shaderTime)
{
	float2 res = inputUV;

	res.x += frac(shaderTime.x * ACCESS_PROP(_ScrollTextureX)); 
	res.y += frac(shaderTime.x * ACCESS_PROP(_ScrollTextureY));

	return res;
}
#endif

#ifdef _WAVE_UV_ON
float2 WaveUV(float2 inputUV, float3 shaderTime)
{
	float2 res = inputUV;

	float2 uvWave = float2(ACCESS_PROP(_WaveX) * ACCESS_PROP(_MainTex_ST).x, ACCESS_PROP(_WaveY) * ACCESS_PROP(_MainTex_ST).y) - res;
	uvWave = frac(uvWave);
	
	uvWave.x *= _ScreenParams.x / _ScreenParams.y;
    float waveTime = shaderTime.y;
	float angWave = (sqrt(dot(uvWave, uvWave)) * ACCESS_PROP(_WaveAmount)) - ((waveTime *  ACCESS_PROP(_WaveSpeed)));
	res = res + uvWave * sin(angWave) * (ACCESS_PROP(_WaveStrength) / 1000.0);

	return res;
}
#endif

#ifdef _SCREEN_SPACE_UV_ON
float2 ScreenSpaceUV(float2 inputUV, float3 vertexWS, float4 projPos)
{
	float2 res = inputUV;

	float aspect = _ScreenParams.x / _ScreenParams.y;
	
	float4 pivotCS = OBJECT_TO_CLIP_SPACE_FLOAT4(float4(0, 0, 0, 1)); 
	pivotCS.xy /= pivotCS.w;
	pivotCS.y *= -1;
	pivotCS.xy += 1.0;
	pivotCS.xy *= 0.5;
	
	float3 positionVS = mul(UNITY_MATRIX_V, float4(vertexWS, 1.0)).xyz;
	
	float2 screenUV = (projPos.xy / projPos.w);
	screenUV -= 0.5;
	screenUV.x *= aspect;
	
	
	float2 screenUVMinusPivot = screenUV - pivotCS.xy;
	float2 stableUVs = (screenUV - pivotCS.xy + 0.5) * -positionVS.z;
	stableUVs *= 0.1;
	
	res = lerp(screenUV, stableUVs, ACCESS_PROP(_ScaleWithCameraDistance));
	return res;
}
#endif

#ifdef _HAND_DRAWN_ON
float2 HandDrawn(float2 inputUV, float3 shaderTime)
{
	float2 uvCopy = inputUV;
	float2 res = inputUV;


	float drawnSpeed = (floor(frac(shaderTime.x) * 20 * ACCESS_PROP(_HandDrawnSpeed)) / ACCESS_PROP(_HandDrawnSpeed)) * ACCESS_PROP(_HandDrawnSpeed);
	uvCopy.x = sin((uvCopy.x * ACCESS_PROP(_HandDrawnAmount) + drawnSpeed) * 4);
	uvCopy.y = cos((uvCopy.y * ACCESS_PROP(_HandDrawnAmount) + drawnSpeed) * 4);
	res = lerp(res, res + uvCopy, 0.0005 * ACCESS_PROP(_HandDrawnAmount));
	
	return res;
}
#endif


#ifdef _TRIPLANAR_MAPPING_ON
EffectsData TriplanarMapping(EffectsData input)
{
	EffectsData res = input;

	float2 uvDiff = input.uvDiff;

	#ifdef _TRIPLANARNORMALSPACE_LOCAL
		float3 normal = res.normalOS;
		float3 position = res.vertexOS;
	#else
		float3 normal = res.normalWS;
		float3 position = res.vertexWS;

		uvDiff *= 10.0;
	#endif

	float3 triplanarWeights = GetTriplanarWeights(normal);

	res.uvMatrix._m00_m01 = CUSTOM_TRANSFORM_TEX(position.xy, uvDiff, _MainTex); //Front
	res.uvMatrix._m02 = triplanarWeights.z;

	res.uvMatrix._m10_m11 = CUSTOM_TRANSFORM_TEX(position.zy, uvDiff, _MainTex); //Side
	res.uvMatrix._m12 = triplanarWeights.x;

	res.uvMatrix._m20_m21 = CUSTOM_TRANSFORM_TEX(position.xz, uvDiff, _TriplanarTopTex); //Top
	res.uvMatrix._m22 = triplanarWeights.y;

	res.uvMatrix._m30_m31 = CUSTOM_TRANSFORM_TEX(position.xz, uvDiff, _MainTex); //Down
	res.uvMatrix._m32 = 1 - normal.y;

#ifdef _NORMAL_MAP_ON
	float2 frontUV_normal_Z = CUSTOM_TRANSFORM_TEX(position.xy, uvDiff, _NormalMap); //Front - Z facing plane
	float2 sideUV_normal_X = CUSTOM_TRANSFORM_TEX(position.zy, uvDiff, _NormalMap); //Side - X facing plane
	float2 topUV_normal_Y = CUSTOM_TRANSFORM_TEX(position.xz, uvDiff, _TriplanarTopNormalMap); //Top - Y facing plane

	res.uv_matrix_normalMap._m00_m01 = frontUV_normal_Z; //Front
	res.uv_matrix_normalMap._m02 = triplanarWeights.z;

	res.uv_matrix_normalMap._m10_m11 = sideUV_normal_X; //Side
	res.uv_matrix_normalMap._m12 = triplanarWeights.x;

	res.uv_matrix_normalMap._m20_m21 = topUV_normal_Y; //Top
	res.uv_matrix_normalMap._m22 = triplanarWeights.y;
#endif

	return res;
}
#endif

#ifdef _UV_DISTORTION_ON
EffectsData UVDistortion(EffectsData data)
{
	EffectsData res = data;
	
	float2 distortTexUV = data.uv_dist;
	
	distortTexUV.x += frac((data.shaderTime.x) * ACCESS_PROP(_DistortTexXSpeed));
	distortTexUV.y += frac((data.shaderTime.x) * ACCESS_PROP(_DistortTexYSpeed));
	
	float4 distortTexCol = SAMPLE_TEX2D_LOD(_DistortTex, float4(distortTexUV.x, distortTexUV.y, 0, 0));
	float distortAmnt = (distortTexCol.r - 0.5) * 0.2 * ACCESS_PROP(_DistortAmount);
	
	DISPLACE_ALL_UVS(res, distortAmnt);

	return res;
}
#endif

#ifdef _PIXELATE_ON
EffectsData Pixelate(EffectsData data)
{
	EffectsData res = data;

	half aspectRatio = ACCESS_PROP(_MainTex_TexelSize).x / ACCESS_PROP(_MainTex_TexelSize).y;
	half2 pixelSize = float2(ACCESS_PROP(_PixelateSize), ACCESS_PROP(_PixelateSize) * aspectRatio);
	
	QUANTIZE_ALL_UVS(res, pixelSize)
	return res;
}
#endif

//#ifdef _STOCHASTIC_SAMPLING_ON
//EffectsData StochasticSamplingOn(EffectsData data)
//{
//	EffectsData res = data;
	
//	float2 UV = MAIN_UV(data);
	
//	//triangle vertices and blend weights
//	//BW_vx[0...2].xyz = triangle verts
//	//BW_vx[3].xy = blend weights (z is unused)
//	float4x3 BW_vx;
 
//	//uv transformed into triangular grid space with UV scaled by approximation of 2*sqrt(3)
//	float2 skewUV = mul(float2x2 (1.0 , 0.0 , -0.57735027 , 1.15470054), UV * 3.464);
 
//	//vertex IDs and barycentric coords
//	float2 vxID = float2 (floor(skewUV));
//	float3 barry = float3 (frac(skewUV), 0);
//	barry.z = 1.0-barry.x-barry.y;
 
//	BW_vx = ((barry.z>0) ? 
//		float4x3(float3(vxID, 0), float3(vxID + float2(0, 1), 0), float3(vxID + float2(1, 0), 0), barry.zyx) :
//		float4x3(float3(vxID + float2 (1, 1), 0), float3(vxID + float2 (1, 0), 0), float3(vxID + float2 (0, 1), 0), float3(-barry.z, 1.0-barry.y, 1.0-barry.x)));
 
//	//calculate derivatives to avoid triangular grid artifacts
//	float2 dx = ddx(UV);
//	float2 dy = ddy(UV);
 
//	float4 newUVs = mul(tex2D(tex, UV + hash2D2D(BW_vx[0].xy), dx, dy), BW_vx[3].x) +
//			mul(tex2D(tex, UV + hash2D2D(BW_vx[1].xy), dx, dy), BW_vx[3].y) + 
//			mul(tex2D(tex, UV + hash2D2D(BW_vx[2].xy), dx, dy), BW_vx[3].z);

//	return res;
//}
//#endif

#endif
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "../Libraries/Common.hlsl"
#include "../Libraries/Waves.hlsl"
#include "../Libraries/Projection.hlsl"
#include "../Libraries/Height.hlsl"

struct HeightPassAttributes
{
	float4 positionOS 	: POSITION;
	float4 uv 			: TEXCOORD0;
	float4 color 		: COLOR0;
	
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct HeightPassVaryings
{	
	float4 positionWS 	: TEXCOORD0;
	//XYZ: World Position
	//W: Displacement offset
	float4 positionCS 	: SV_POSITION;
	float4 uv : TEXCOORD1; //Needs to be defined for Common.hlsl
	
	UNITY_VERTEX_INPUT_INSTANCE_ID
	UNITY_VERTEX_OUTPUT_STEREO
};

HeightPassVaryings HeightPassVertex(HeightPassAttributes input)
{
	HeightPassVaryings output = (HeightPassVaryings)0;

	UNITY_SETUP_INSTANCE_ID(input);
	UNITY_TRANSFER_INSTANCE_ID(input, output);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

#if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON) 
    CURVEDWORLD_TRANSFORM_VERTEX(input.positionOS)
#endif
	
	float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
	float3 offset = 0;

	output.uv = float4(input.uv.xy, _TimeParameters.x, 0);
	
#if _WAVES
	float2 uv = GetSourceUV(input.uv.xy, positionWS.xz, _WorldSpaceUV);

	float3 waveOffset = float3(0,0,0);
	float3 waveNormal = float3(0,1,0);
	
	CalculateWaves(_WaveProfile, _WaveProfile_TexelSize.z, _WaveMaxLayers, uv.xy, _WaveFrequency, positionWS.xyz, _Direction.xy, float3(0,1,0), (TIME_VERTEX * _Speed) * _WaveSpeed, input.color.b * _VertexColorWaveFlattening, float3(_WaveSteepness, _WaveHeight, _WaveSteepness),
	_WaveNormalStr, _WaveFadeDistance.x, _WaveFadeDistance.y,
	//Out
	waveOffset, waveNormal);
	
	offset.xyz += waveOffset.xyz;
#endif
	
	#if DYNAMIC_EFFECTS_ENABLED
	if(_ReceiveDynamicEffects)
	{
		float4 effectsData = SampleDynamicEffectsData(positionWS.xyz);

		half falloff = 1.0;
		#if defined(TESSELLATION_ON)
		//falloff = saturate(1.0 - (distance(positionWS.xyz, GetCurrentViewPosition() - _TessMin)) / (_TessMax - _TessMin));
		#endif
	
		offset.y += effectsData[DE_HEIGHT_CHANNEL] * falloff;
	}
	#endif
	
	output.positionCS = TransformWorldToHClip(positionWS);
	output.positionWS.xyz = positionWS.xyz;
	output.positionWS.w = offset.y;

	return output;
}

float4 HeightFragment(HeightPassVaryings input) : SV_TARGET
{
	UNITY_SETUP_INSTANCE_ID(input);
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

	float3 positionWS = input.positionWS.xyz;
	float height = input.positionWS.w;

	//float projectionEdgeMask = ProjectionEdgeMask(positionWS, _WaterHeightCoords.xy, _WaterHeightCoords.z, 15);

	//positionWS.y = lerp(VOID_THRESHOLD, positionWS.y, projectionEdgeMask);
	
	return float4(positionWS.y, height, 0.0, 1.0);
}
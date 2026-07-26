// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

Shader "Hidden/StylizedWater3/HeightProcessor"
{
	SubShader
	{
		Cull Off ZWrite Off ZTest Always
		
		HLSLINCLUDE
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
		ENDHLSL
		
		Pass
		{
			Name "Height To Normal"
			HLSLPROGRAM
			
			#pragma vertex Vert
			#pragma fragment frag
			
			float4 _HeightToNormalParams;
			//X: Strength
			//Y: Channel
			//Z: Miplevel
			
			float4 frag (Varyings input) : SV_Target
			{
				float2 uv = input.texcoord;
				float radius = _BlitTexture_TexelSize.x; //1f/width

				float strength = _HeightToNormalParams.x * 2.0;
				int channel = _HeightToNormalParams.y;
				uint mip = _HeightToNormalParams.z;
				
				if(uv.x >= (1-radius) || uv.y >= (1-radius)
					|| uv.x <= (radius) || uv.y <= (radius)
					) return float4(1,1,1,1);

				const float xLeft = (SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, float2(uv.xy - float2(radius, 0.0)), mip)[channel]) * strength;
				const float xRight = (SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, float2(uv.xy + float2(radius, 0.0)), mip)[channel]) * strength;

				const float yUp = (SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, float2(uv.xy - float2(0.0, radius)), mip)[channel]) * strength;
				const float yDown = (SAMPLE_TEXTURE2D_X_LOD(_BlitTexture, sampler_LinearClamp, float2(uv.xy + float2(0.0, radius)), mip)[channel]) * strength;

				float xDelta = ((xLeft - xRight) + 1.0) * 0.5f;
				float yDelta = ((yUp - yDown) + 1.0) * 0.5f;

				float4 normals = float4(xDelta, yDelta, 0.0, 0);

				return normals;
			}
			ENDHLSL
		}
		
		Pass
		{
			Name "Terrain Intersection Mask"
			HLSLPROGRAM
			
			#pragma vertex Vert
			#pragma fragment frag
			
			#include "../Libraries/Height.hlsl"
			#include "../Libraries/Terrain.hlsl"

			float _TerrainIntersectionMaskOffset;
			
			float4 frag (Varyings input) : SV_Target
			{
				float2 uv = input.texcoord;

				//Snap texels to terrain height buffer grid to avoid texel swimming
				float cellSize = 2;

				//uv = floor(uv / cellSize) * cellSize;
				float3 positionWS = float3(
					_TerrainHeightRenderCoords.x + (uv.x * _TerrainHeightRenderCoords.z),
					0,
					_TerrainHeightRenderCoords.y + (uv.y * _TerrainHeightRenderCoords.z));


				float3 terrainHeightSamplePos = positionWS;
				terrainHeightSamplePos.x = floor(positionWS.x / cellSize) * cellSize;
				terrainHeightSamplePos.z = floor(positionWS.z / cellSize) * cellSize;
				terrainHeightSamplePos = floor(positionWS / cellSize) * (cellSize) + (cellSize * 0.5f);
				
				/*
				int resolution = 256.0f;
				float3 origin = float3(_TerrainHeightRenderCoords.x, 0, _TerrainHeightRenderCoords.y);
				origin = origin * resolution / 2.0f;
				float3 roundedOrigin = round(origin);
				float3 roundOffset = roundedOrigin - origin;

				//Need to fix the pixel swimming. SDF mask renders at a lower resolution than the terrain height prepass
				roundOffset = roundOffset * 2.0f / resolution;
				positionWS += roundOffset;
				*/
				
				float2 waterHeights = SampleWaterHeight(positionWS);
				float waterHeight = waterHeights.x;

				//Factor in displacement effects?
				//waterHeight += waterHeights.g;
				
				if(HasHitWaterSurface(waterHeight) == false) return 0;

				//TODO: Implement padding to shift the SDF inwards a bit
				waterHeight += _TerrainIntersectionMaskOffset;
				
				const float terrainHeight = SampleTerrainHeight(terrainHeightSamplePos);
				
				float delta = waterHeight - terrainHeight;

				//Soft
				//float mask = 1-saturate(delta * 32);
				//Boolean
				float mask = waterHeight < terrainHeight ? 1 : 0;
				
				return float4(mask, 0.0, 0.0, 1.0);
			}
			ENDHLSL
		}
	}
}
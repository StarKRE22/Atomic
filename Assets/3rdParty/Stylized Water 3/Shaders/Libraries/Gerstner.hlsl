// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#ifndef WATER_GERSTNER_INCLUDED
#define WATER_GERSTNER_INCLUDED

#define MAX_AMPLITUDE 5.0
#define GRAVITY 9.8f

#if !defined(UNITY_CORE_SAMPLERS_INCLUDED)
//Do not want linear interpolation between texels, forcing the usage of a point sampler
SamplerState sampler_PointClamp;
#endif

struct WaveParameters
{
	uint enabled;
	float amplitude;
	float waveLength;
	float steepness;
	uint mode;
	float direction;
	float2 origin;
};

//Sample parameters from LUT, stored in two horizontal rows
void SampleWaveParameters(inout WaveParameters data, uint index, Texture2D tex, uint columns)
{
	//Each layer's data is stored in a texel, from left to right. The horizontal position corresponds to the array element, so that's the UV
	const float2 lutUV = float2((float)index / (float)columns, 0);

	const float4 row0 = SAMPLE_TEXTURE2D_LOD(tex, sampler_PointClamp, lutUV, 0);
	data.amplitude = row0.x;
	data.waveLength = row0.y;
	data.direction = row0.z; //Rotation angle in radians
	data.enabled = row0.w;

	const float4 row1 = SAMPLE_TEXTURE2D_LOD(tex, sampler_PointClamp, lutUV + float2(0, 0.5), 0);
	data.origin = row1.xy;
	data.mode = row1.z;
	data.steepness  = row1.w;
}

//LUT texture is passed in as a parameter, since it may differ on a per-material basis
void CalculateGerstnerWaves_float(in Texture2D<float4> lutTex, in uint layerCount, in float2 position, in float frequency, in float time, in float normalStrength, in float2 baseDirection, in uint count, out float3 offset, out float3 tangent, out float3 bitangent)
{
	//Defaults
	offset = float3(0,0,0);
	tangent = float3(1,0,0);
	bitangent = float3(0,0,1);

	//Clamp to maximum number of layers
	count = min(count - 1, layerCount);

	WaveParameters layer = (WaveParameters)0;

	uint waveCount = 0;
	for(uint i = 0; i <= count; i++)
	{
		SampleWaveParameters(layer, i, lutTex, layerCount);

		if(layer.enabled > 0)
		{
			waveCount += 1;
			
			const float w = TWO_PI / (layer.waveLength * frequency);
			const float freq = sqrt(GRAVITY * w);
			//As amplitude scales down, so should the steepness
			half ampRCP = (layer.amplitude/MAX_AMPLITUDE);
			//Both divide and scale by amplitude
			float steepness = (layer.steepness / layer.amplitude) * ampRCP;
		
			//Rotation already pre-converted into radians
			float2 direction = float2(sin(layer.direction), cos(layer.direction)) * baseDirection;
			
			//Radial mode		
			if(layer.mode == 1)
			{
				position -= layer.origin;

				direction += (position - layer.origin);
				direction = normalize(direction);
			}
			
			const float dir = dot(direction, position - (layer.origin * layer.mode));

			const float t = dir * w + (freq * -time);
			
			float proximalSine = sin(t); //Y
			float lateralSine = cos(t); //XZ

			//Relative XYZ offsets
			offset.x += direction.x * layer.amplitude * lateralSine * steepness;
			offset.y += proximalSine * layer.amplitude;
			offset.z += direction.y * layer.amplitude * lateralSine * steepness;
			
			tangent += float3(
				-direction.x * direction.x * (steepness * proximalSine),
				offset.x,
				-direction.x * direction.y * (steepness * proximalSine)
			);

			bitangent += float3(
				-direction.x * direction.y * (steepness * proximalSine),
				offset.z,
				-direction.y * -direction.y * (steepness * proximalSine)
			);
		}
	}
	waveCount = max(waveCount, 1);
	
	tangent = lerp(float3(1,0,0), tangent, normalStrength / waveCount);
	bitangent = lerp(float3(0,0,1), bitangent, normalStrength / waveCount);
}
#endif
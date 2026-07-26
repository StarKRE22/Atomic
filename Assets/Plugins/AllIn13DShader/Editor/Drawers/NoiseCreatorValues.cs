using UnityEngine;

namespace AllIn13DShader
{
	public class NoiseCreatorValues
	{
		public Texture2D noisePreview = null;
		public Texture2D finalNoiseTex = null;

		public RenderTexture noiseRenderTarget = null;
		public Material noiseMaterial;

		public float noiseScaleX = 10f;
		public float noiseScaleY = 10f;
		public float noiseContrast = 1f;
		public float noiseBrightness = 0f;

		public float noiseFractalAmount = 1f;
		public float noiseJitter = 1f;
		public int noiseSeed = 0;

		public bool noiseSquareScale = false;
		public bool noiseInverted = false;
		public bool isFractalNoise = false;

		public NoiseCreatorTool.NoiseTypes noiseType;

		public TextureSizes noiseSize;
		public FilterMode noiseFiltering;

		public NoiseCreatorValues()
		{
			SetDefault();
		}

		public void SetDefault()
		{
			noisePreview = null;
			noiseRenderTarget = null;
			finalNoiseTex = null;

			noiseMaterial = null;
			noiseScaleX = 10f;
			noiseScaleY = 10f;
			noiseContrast = 1f;
			noiseBrightness = 0f;

			noiseFractalAmount = 1f;
			noiseJitter = 1f;
			noiseSeed = 0;

			noiseSquareScale = false;
			noiseInverted = false;
			isFractalNoise = false;

			noiseType = NoiseCreatorTool.NoiseTypes.Voronoi;

			noiseSize = TextureSizes._512;
			noiseFiltering = FilterMode.Bilinear;
		}
	}
}
using UnityEngine;

namespace AllIn13DShader
{
	public class NoiseCreatorTool
	{
		private const string SHADER_NAME_FRACTAL_NOISE = "AllIn13DShaderFractalNoise";
		private const string SHADER_NAME_WORLEY_NOISE = "AllIn13DShaderWorleyNoise";

		public enum NoiseTypes
		{
			Fractal,
			Perlin,
			Billow,
			Voronoi,
			Water,
			Cellular,
			Cells1,
			Cells2
		}

		public NoiseCreatorValues values;

		public NoiseCreatorTool()
		{
			values = new NoiseCreatorValues();

			NoiseSetMaterial();
			CheckCreationNoiseTextures();
			UpdateNoiseMatAndRender();
			CreateNoiseTex();
		}

		public void CreateNoiseTex()
		{
			int texSize = (int)values.noiseSize;
			values.finalNoiseTex = new Texture2D(texSize, texSize);
			RenderTexture finalRenderTarget = new RenderTexture(values.finalNoiseTex.width, values.finalNoiseTex.height, 0, RenderTextureFormat.ARGB32);
			Graphics.Blit(values.finalNoiseTex, finalRenderTarget, values.noiseMaterial);
			values.finalNoiseTex.ReadPixels(new Rect(0, 0, finalRenderTarget.width, finalRenderTarget.height), 0, 0);
			values.finalNoiseTex.Apply();
		}

		public void CheckCreationNoiseTextures()
		{
			if (values.noisePreview == null)
			{
				values.noisePreview = new Texture2D(256, 256);
			}

			if (values.noiseRenderTarget == null)
			{
				values.noiseRenderTarget = new RenderTexture(values.noisePreview.width, values.noisePreview.height, 0, RenderTextureFormat.ARGB32);
			}
		}

		public void UpdateNoiseMatAndRender()
		{
			if (values.noiseType == NoiseTypes.Fractal || values.noiseType == NoiseTypes.Perlin || values.noiseType == NoiseTypes.Billow)
			{
				values.noiseMaterial.SetFloat("_EndBand", values.noiseFractalAmount);
			}
			else values.noiseMaterial.SetFloat("_Jitter", values.noiseJitter);

			values.noiseMaterial.SetFloat("_ScaleX", values.noiseScaleX);
			values.noiseMaterial.SetFloat("_ScaleY", values.noiseScaleY);
			values.noiseMaterial.SetFloat("_Offset", (float)values.noiseSeed);
			values.noiseMaterial.SetFloat("_Contrast", values.noiseContrast);
			values.noiseMaterial.SetFloat("_Brightness", values.noiseBrightness);
			values.noiseMaterial.SetFloat("_Invert", values.noiseInverted ? 1f : 0f);

			Graphics.Blit(values.noisePreview, values.noiseRenderTarget, values.noiseMaterial);
			values.noisePreview.ReadPixels(new Rect(0, 0, values.noiseRenderTarget.width, values.noiseRenderTarget.height), 0, 0);
			values.noisePreview.Apply();
		}

		public void NoiseSetMaterial()
		{
			if (values.noiseType == NoiseTypes.Fractal || values.noiseType == NoiseTypes.Perlin || values.noiseType == NoiseTypes.Billow)
			{
				values.isFractalNoise = true;
				values.noiseMaterial = new Material(EditorUtils.FindShader(SHADER_NAME_FRACTAL_NOISE));
				values.noiseScaleX = 4f;
				values.noiseScaleY = 4f;
			}
			else
			{
				values.isFractalNoise = false;
				values.noiseMaterial = new Material(EditorUtils.FindShader(SHADER_NAME_WORLEY_NOISE));
				values.noiseScaleX = 10f;
				values.noiseScaleY = 10f;
			}

			switch (values.noiseType)
			{
				case NoiseTypes.Fractal:
					values.noiseFractalAmount = 8f;
					values.noiseMaterial.SetFloat("_Fractal", 1);
					break;
				case NoiseTypes.Perlin:
					values.noiseFractalAmount = 1f;
					values.noiseMaterial.SetFloat("_Fractal", 1);
					break;
				case NoiseTypes.Billow:
					values.noiseFractalAmount = 4f;
					values.noiseMaterial.SetFloat("_Fractal", 0);
					break;
				case NoiseTypes.Voronoi:
					values.noiseMaterial.SetFloat("_NoiseType", 0f);
					break;
				case NoiseTypes.Water:
					values.noiseMaterial.SetFloat("_NoiseType", 3f);
					break;
				case NoiseTypes.Cellular:
					values.noiseMaterial.SetFloat("_NoiseType", 4f);
					break;
				case NoiseTypes.Cells1:
					values.noiseMaterial.SetFloat("_NoiseType", 1f);
					break;
				case NoiseTypes.Cells2:
					values.noiseMaterial.SetFloat("_NoiseType", 2f);
					break;
			}
		}
	}
}
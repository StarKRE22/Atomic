using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class TextureEditorTool
	{
		public Texture2D editorTexInput;
		public Texture2D editorTex;
		public Texture2D cleanEditorTex;

		public TextureEditorValues values;

		public void Setup()
		{
			EditorUtils.SetTextureReadWrite(AssetDatabase.GetAssetPath(editorTexInput), true);

			editorTex = new Texture2D(editorTexInput.width, editorTexInput.height);
			editorTex.SetPixels(editorTexInput.GetPixels());
			editorTex.Apply();

			float aspectRatio = (float)editorTex.width / (float)editorTex.height;
			int width = Mathf.Min(editorTex.width, 256);
			editorTex = ScaleTexture(editorTex, width, (int)(width / aspectRatio));

			cleanEditorTex = new Texture2D(editorTex.width, editorTex.height);
			cleanEditorTex.SetPixels(editorTex.GetPixels());
			cleanEditorTex.Apply();

			values = new TextureEditorValues();

			RecalculateEditorTexture();
		}

		//private void SetTextureReadWrite(string assetPath, bool enable)
		//{
		//	TextureImporter tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
		//	if (tImporter != null)
		//	{
		//		tImporter.isReadable = enable;
		//		tImporter.SaveAndReimport();
		//	}
		//}

		private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
		{
			targetWidth = Mathf.ClosestPowerOfTwo(targetWidth);
			targetHeight = Mathf.ClosestPowerOfTwo(targetHeight);

			Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
			Color[] scaledPixels = result.GetPixels(0);
			float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
			float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);
			for (int px = 0; px < scaledPixels.Length; px++) scaledPixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * (float)Mathf.Floor(px / targetWidth));

			result.SetPixels(scaledPixels, 0);
			result.Apply();
			return result;
		}

		public void RecalculateEditorTexture()
		{
			Color[] pixels = cleanEditorTex.GetPixels();
			int texWidth = cleanEditorTex.width;
			int texHeight = cleanEditorTex.height;

			ComputeImageColorFilters(pixels);

			editorTex = new Texture2D(texWidth, texHeight);
			editorTex.SetPixels(pixels);
			editorTex.Apply();
		}

		private void ComputeImageColorFilters(Color[] pixels)
		{
			float cosHsv = values.saturation * Mathf.Cos(values.hue * 3.14159265f / 180f);
			float sinHsv = values.saturation * Mathf.Sin(values.hue * 3.14159265f / 180f);

			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i].r = Mathf.Clamp01(((pixels[i].r - 0.5f) * values.contrast) + 0.5f);
				pixels[i].g = Mathf.Clamp01(((pixels[i].g - 0.5f) * values.contrast) + 0.5f);
				pixels[i].b = Mathf.Clamp01(((pixels[i].b - 0.5f) * values.contrast) + 0.5f);

				pixels[i] = new Color(Mathf.Clamp01(pixels[i].r * (1 + values.brightness)), Mathf.Clamp01(pixels[i].g * (1 + values.brightness)), Mathf.Clamp01(pixels[i].b * (1 + values.brightness)), pixels[i].a);

				pixels[i].r = Mathf.Pow(Mathf.Abs(pixels[i].r), values.gamma);
				pixels[i].g = Mathf.Pow(Mathf.Abs(pixels[i].g), values.gamma);
				pixels[i].b = Mathf.Pow(Mathf.Abs(pixels[i].b), values.gamma);

				pixels[i].r = Mathf.Clamp01(pixels[i].r * Mathf.Pow(2, values.exposure));
				pixels[i].g = Mathf.Clamp01(pixels[i].g * Mathf.Pow(2, values.exposure));
				pixels[i].b = Mathf.Clamp01(pixels[i].b * Mathf.Pow(2, values.exposure));

				pixels[i] *= values.editorColorTint;

				Color hueShiftColor = pixels[i];
				hueShiftColor.r = Mathf.Clamp01((.299f + .701f * cosHsv + .168f * sinHsv) * pixels[i].r + (.587f - .587f * cosHsv + .330f * sinHsv) * pixels[i].g + (.114f - .114f * cosHsv - .497f * sinHsv) * pixels[i].b);
				hueShiftColor.g = Mathf.Clamp01((.299f - .299f * cosHsv - .328f * sinHsv) * pixels[i].r + (.587f + .413f * cosHsv + .035f * sinHsv) * pixels[i].g + (.114f - .114f * cosHsv + .292f * sinHsv) * pixels[i].b);
				hueShiftColor.b = Mathf.Clamp01((.299f - .3f * cosHsv + 1.25f * sinHsv) * pixels[i].r + (.587f - .588f * cosHsv - 1.05f * sinHsv) * pixels[i].g + (.114f + .886f * cosHsv - .203f * sinHsv) * pixels[i].b);
				pixels[i] = hueShiftColor;

				if (values.invert) pixels[i] = new Color(1 - pixels[i].r, 1 - pixels[i].g, 1 - pixels[i].b, pixels[i].a);

				if (values.greyscale || values.fullWhite || values.alphaGreyscale)
				{
					float greyScale = pixels[i].r * 0.59f + pixels[i].g * 0.3f + pixels[i].b * 0.11f;

					if (values.fullWhite) pixels[i] = new Color(1, 1, 1, greyScale);
					else if (values.greyscale) pixels[i] = new Color(greyScale, greyScale, greyScale, pixels[i].a);

					if (values.alphaGreyscale) pixels[i] = new Color(pixels[i].r, pixels[i].g, pixels[i].b, greyScale);
				}
				
				if (values.alphaIsOne) pixels[i] = new Color(pixels[i].r, pixels[i].g, pixels[i].b, 1f);

				if (values.blackBackground)
				{
					if (pixels[i].a < 0.05f) pixels[i] = new Color(pixels[i].a, pixels[i].a, pixels[i].a, 1);
					else pixels[i] = new Color(pixels[i].r, pixels[i].g, pixels[i].b, 1);
				}
			}
		}

		public void FlipEditorTexture(bool isHorizontal)
		{
			Color[] pixels = editorTex.GetPixels();
			Color[] pixelsClean = cleanEditorTex.GetPixels();
			int texWidth = editorTex.width;
			int texHeight = editorTex.height;

			if (isHorizontal)
			{
				pixels = FlipHorizontal(pixels, texWidth, texHeight);
				pixelsClean = FlipHorizontal(pixelsClean, texWidth, texHeight);
				values.isFlipHorizontal = !values.isFlipHorizontal;
			}
			else
			{
				pixels = FlipVertical(pixels, texWidth, texHeight);
				pixelsClean = FlipVertical(pixelsClean, texWidth, texHeight);
				values.isFlipVertical = !values.isFlipVertical;
			}

			editorTex = new Texture2D(texWidth, texHeight);
			editorTex.SetPixels(pixels);
			editorTex.Apply();
			cleanEditorTex = new Texture2D(texWidth, texHeight);
			cleanEditorTex.SetPixels(pixelsClean);
			cleanEditorTex.Apply();
		}

		private Color[] FlipHorizontal(Color[] pixels, int width, int height)
		{
			Color[] outputPixels = new Color[pixels.Length];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					int i1 = GetPixelIndex(x, y, width);
					int i2 = GetPixelIndex(width - 1 - x, y, width);
					outputPixels[i1] = pixels[i2];
				}
			}

			return outputPixels;
		}

		private Color[] FlipVertical(Color[] pixels, int width, int height)
		{
			Color[] outputPixels = new Color[pixels.Length];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					int i1 = GetPixelIndex(x, y, width);
					int i2 = GetPixelIndex(x, height - 1 - y, width);
					outputPixels[i1] = pixels[i2];
				}
			}

			return outputPixels;
		}

		private int GetPixelIndex(int x, int y, int width)
		{
			return y * width + x;
		}

		public void RotateEditorTextureLeft()
		{
			Color[] pixels = editorTex.GetPixels();
			Color[] pixelsClean = cleanEditorTex.GetPixels();
			int texWidth = editorTex.width;
			int texHeight = editorTex.height;

			pixels = RotateClockWise(pixels, texWidth, texHeight);
			pixelsClean = RotateClockWise(pixelsClean, texWidth, texHeight);

			editorTex = new Texture2D(texHeight, texWidth); //Width and Height get swapped to account for rotation
			editorTex.SetPixels(pixels);
			editorTex.Apply();
			cleanEditorTex = new Texture2D(texHeight, texWidth); //Width and Height get swapped to account for rotation
			cleanEditorTex.SetPixels(pixelsClean);
			cleanEditorTex.Apply();

			values.rotationAmount = (values.rotationAmount + 1) % 4;
		}

		public Color[] RotateClockWise(Color[] pixels, int width, int height)
		{
			Color[] outputPixels = new Color[pixels.Length];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					int i1 = GetPixelIndex(x, height - y - 1, width);
					int i2 = GetPixelIndex(y, x, height);
					outputPixels[i2] = pixels[i1];
				}
			}

			return outputPixels;
		}

		private void ComputeFinalTexture()
		{
			Color[] pixels;
			int texWidth, texHeight;

			for (int i = 0; i < values.rotationAmount; i++)
			{
				texWidth = editorTexInput.width;
				texHeight = editorTexInput.height;
				pixels = editorTexInput.GetPixels();
				pixels = RotateClockWise(pixels, texWidth, texHeight);
				editorTexInput = new Texture2D(texHeight, texWidth);
				editorTexInput.SetPixels(pixels);
				editorTexInput.Apply();
			}

			pixels = editorTexInput.GetPixels();
			texWidth = editorTexInput.width;
			texHeight = editorTexInput.height;
			if (values.isFlipHorizontal) 
			{
				pixels = FlipHorizontal(pixels, texWidth, texHeight);
			}
			if (values.isFlipVertical)
			{
				pixels = FlipVertical(pixels, texWidth, texHeight);
			}

			ComputeImageColorFilters(pixels);
			editorTexInput = new Texture2D(texWidth, texHeight);
			editorTexInput.SetPixels(pixels);
			editorTexInput.Apply();

			if (Math.Abs(values.exportScale - 1f) > 0.05f)
			{
				editorTexInput = ScaleTexture(editorTexInput, (int)(texWidth * values.exportScale), (int)(texHeight * values.exportScale));
			}
		}

		public void SaveAsPNG()
		{
			string fullPath = AssetDatabase.GetAssetPath(editorTexInput);
			string path = fullPath.Replace(Path.GetFileName(fullPath), "");

			fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);

			string fileName = fullPath.Replace(path, "");
			fileName = fileName.Replace(".png", "");
			fullPath = EditorUtility.SaveFilePanel("Save Image", path, fileName, "png");
			if (fullPath.Length == 0) 
			{
				return;
			}

			string pingPath = fullPath;

			ComputeFinalTexture();

			byte[] bytes = editorTexInput.EncodeToPNG();
			File.WriteAllBytes(pingPath, bytes);
			AssetDatabase.ImportAsset(pingPath);
			AssetDatabase.Refresh();
			EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(pingPath, typeof(Texture)));

			EditorUtils.ShowNotification("Edited Image saved to: " + fullPath);

			editorTexInput = null;
			editorTex = null;
			cleanEditorTex = null;

			SetTextureEditorDefaultValues();
		}

		private void SetTextureEditorDefaultValues()
		{
			values.SetDefault();
		}
	}
}
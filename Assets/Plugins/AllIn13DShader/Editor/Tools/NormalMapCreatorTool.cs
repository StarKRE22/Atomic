using UnityEngine;

namespace AllIn13DShader
{
	public class NormalMapCreatorTool
	{
		public Texture2D targetNormalImage;
		public int normalSmoothing;
		public float normalStrength;
		public int isComputingNormals;

		public Texture2D CreateNormalMap()
		{
			int width = targetNormalImage.width;
			int height = targetNormalImage.height;
			Color[] sourcePixels = targetNormalImage.GetPixels();
			Color[] resultPixels = new Color[width * height];
			Vector3 vScale = new Vector3(0.3333f, 0.3333f, 0.3333f);

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					int index = x + y * width;
					Vector3 cSampleNegXNegY = GetPixelClamped(sourcePixels, x - 1, y - 1, width, height);
					Vector3 cSampleZerXNegY = GetPixelClamped(sourcePixels, x, y - 1, width, height);
					Vector3 cSamplePosXNegY = GetPixelClamped(sourcePixels, x + 1, y - 1, width, height);
					Vector3 cSampleNegXZerY = GetPixelClamped(sourcePixels, x - 1, y, width, height);
					Vector3 cSamplePosXZerY = GetPixelClamped(sourcePixels, x + 1, y, width, height);
					Vector3 cSampleNegXPosY = GetPixelClamped(sourcePixels, x - 1, y + 1, width, height);
					Vector3 cSampleZerXPosY = GetPixelClamped(sourcePixels, x, y + 1, width, height);
					Vector3 cSamplePosXPosY = GetPixelClamped(sourcePixels, x + 1, y + 1, width, height);

					float fSampleNegXNegY = Vector3.Dot(cSampleNegXNegY, vScale);
					float fSampleZerXNegY = Vector3.Dot(cSampleZerXNegY, vScale);
					float fSamplePosXNegY = Vector3.Dot(cSamplePosXNegY, vScale);
					float fSampleNegXZerY = Vector3.Dot(cSampleNegXZerY, vScale);
					float fSamplePosXZerY = Vector3.Dot(cSamplePosXZerY, vScale);
					float fSampleNegXPosY = Vector3.Dot(cSampleNegXPosY, vScale);
					float fSampleZerXPosY = Vector3.Dot(cSampleZerXPosY, vScale);
					float fSamplePosXPosY = Vector3.Dot(cSamplePosXPosY, vScale);

					float edgeX = (fSampleNegXNegY - fSamplePosXNegY) * 0.25f + (fSampleNegXZerY - fSamplePosXZerY) * 0.5f + (fSampleNegXPosY - fSamplePosXPosY) * 0.25f;
					float edgeY = (fSampleNegXNegY - fSampleNegXPosY) * 0.25f + (fSampleZerXNegY - fSampleZerXPosY) * 0.5f + (fSamplePosXNegY - fSamplePosXPosY) * 0.25f;

					Vector2 vEdge = new Vector2(edgeX, edgeY) * normalStrength;
					Vector3 norm = new Vector3(vEdge.x, vEdge.y, 1.0f).normalized;
					resultPixels[index] = new Color(norm.x * 0.5f + 0.5f, norm.y * 0.5f + 0.5f, norm.z * 0.5f + 0.5f, 1);
				}
			}

			if (normalSmoothing > 0)
			{
				resultPixels = SmoothNormals(resultPixels, width, height, normalSmoothing);
			}

			Texture2D texNormal = new Texture2D(width, height, TextureFormat.RGB24, false, false);
			texNormal.SetPixels(resultPixels);
			texNormal.Apply();
			return texNormal;
		}

		private Vector3 GetPixelClamped(Color[] pixels, int x, int y, int width, int height)
		{
			x = Mathf.Clamp(x, 0, width - 1);
			y = Mathf.Clamp(y, 0, height - 1);
			Color c = pixels[x + y * width];
			return new Vector3(c.r, c.g, c.b);
		}

		private Color[] SmoothNormals(Color[] pixels, int width, int height, int normalSmooth)
		{
			Color[] smoothedPixels = new Color[pixels.Length];
			float step = 0.00390625f * normalSmooth;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					float pixelsToAverage = 0.0f;
					Color c = pixels[x + y * width];
					pixelsToAverage++;

					for (int offsetY = -normalSmooth; offsetY <= normalSmooth; offsetY++)
					{
						for (int offsetX = -normalSmooth; offsetX <= normalSmooth; offsetX++)
						{
							if (offsetX == 0 && offsetY == 0) continue;

							int sampleX = Mathf.Clamp(x + offsetX, 0, width - 1);
							int sampleY = Mathf.Clamp(y + offsetY, 0, height - 1);

							c += pixels[sampleX + sampleY * width];
							pixelsToAverage++;
						}
					}

					smoothedPixels[x + y * width] = c / pixelsToAverage;
				}
			}

			return smoothedPixels;
		}
	}
}
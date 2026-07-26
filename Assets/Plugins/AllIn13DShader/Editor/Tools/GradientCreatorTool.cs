using System.IO;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class GradientCreatorTool
	{
		public const int HEIGHT_GRADIENT = 16;

		public Texture2D gradientTex;

		public TextureSizes gradientSizes;
		public FilterMode gradientFiltering;
		public Gradient gradient;

		public GradientCreatorTool()
		{
			gradientSizes = TextureSizes._128;
			gradientFiltering = FilterMode.Bilinear;
			gradient = new Gradient();

			int size = (int)gradientSizes;
			gradientTex = new Texture2D(size, size);
		}

		public Texture2D CreateGradientTexture(TextureSizes gradientSizes, FilterMode gradientFiltering, Gradient gradient)
		{
			this.gradientSizes = gradientSizes;
			this.gradientFiltering = gradientFiltering;
			this.gradient = gradient;

			return CreateGradientTexture();
		}

		public Texture2D CreateGradientTexture(Gradient gradient)
		{
			this.gradientSizes = TextureSizes._256;
			this.gradientFiltering = FilterMode.Bilinear;
			this.gradient = gradient;

			return CreateGradientTexture();
		}

		public Texture2D CreateGradientTexture()
		{
			int textureSize = (int)gradientSizes;
			this.gradientTex = new Texture2D(textureSize, HEIGHT_GRADIENT, TextureFormat.RGBA32, false);
			this.gradientTex.wrapMode = TextureWrapMode.Clamp;

			for (int i = 0; i < textureSize; i++)
			{
				Color col = gradient.Evaluate((float)i / (float)textureSize);
				for (int j = 0; j < HEIGHT_GRADIENT; j++)
				{
					gradientTex.SetPixel(i, j, gradient.Evaluate((float)i / (float)textureSize));
				}
			}

			gradientTex.Apply();
			return gradientTex;
		}

		public Texture SaveGradientTexture(GradientTexture gradientTextureAsset, bool generateUniqueAssetPath)
		{
			string texName = "RampTexture";
			if(gradientTextureAsset != null)
			{
				texName = gradientTextureAsset.texture.name;
			}

			Texture tex = EditorUtils.SaveTextureAsPNG(GlobalConfiguration.instance.GradientSavePath, texName, "Ramp Texture", gradientTex,
					FilterMode.Bilinear, TextureImporterType.Default, TextureWrapMode.Clamp, false, generateUniqueAssetPath);

			if (generateUniqueAssetPath)
			{
				GradientTexture gradientTexture = ScriptableObject.CreateInstance<GradientTexture>();
				gradientTexture.texture = tex;
				gradientTexture.gradient = new Gradient();
				CopyGradient(gradient, gradientTexture.gradient);

				string gradientTextureAssetPath = Path.Combine(GlobalConfiguration.instance.GradientSavePath, $"GradientAsset_{tex.name}.asset");
				gradientTextureAssetPath = AssetDatabase.GenerateUniqueAssetPath(gradientTextureAssetPath);

				AssetDatabase.CreateAsset(gradientTexture, gradientTextureAssetPath);
				AssetDatabase.Refresh();
			}
			else
			{
				gradientTextureAsset.texture = tex;

				gradientTextureAsset.gradient = new Gradient();
				CopyGradient(gradient, gradientTextureAsset.gradient);

				EditorUtility.SetDirty(gradientTextureAsset);
			}

			return tex;
		}

		public static void CopyGradient(Gradient from, Gradient to)
		{
			GradientColorKey[] newColorKeys = new GradientColorKey[from.colorKeys.Length];
			GradientAlphaKey[] newAlphaKeys = new GradientAlphaKey[from.alphaKeys.Length];

			for (int i = 0; i < from.colorKeys.Length; i++)
			{
				newColorKeys[i] = from.colorKeys[i];
			}

			for (int i = 0; i < from.alphaKeys.Length; i++)
			{
				newAlphaKeys[i] = from.alphaKeys[i];
			}

			to.colorKeys = newColorKeys;
			to.alphaKeys = newAlphaKeys;
			to.mode = from.mode;
		}

		public static GradientTexture FindGradientTexureByTex(Texture selectedTex)
		{
			GradientTexture res = null;
			string[] guids = AssetDatabase.FindAssets("t:GradientTexture");

			for (int i = 0; i < guids.Length; i++)
			{
				string path = AssetDatabase.GUIDToAssetPath(guids[i]);
				GradientTexture gradientTexture = AssetDatabase.LoadAssetAtPath<GradientTexture>(path);

				if (gradientTexture.name.StartsWith("GradientAsset_"))
				{
					if (gradientTexture.texture == selectedTex)
					{
						res = gradientTexture;
						break;
					}
				}
			}

			return res;
		}
	}
}
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class AtlasPackerTool
	{
		public AtlasPackerValues values;

		//public int atlasXCount;
		//public int atlasYCount;
		
		//public TextureSizes atlasSizesX;
		//public TextureSizes atlasSizesY;

		//public FilterMode atlasFiltering;

		//public bool squareAtlas;

		//public Texture2D[] atlas;

		//public Texture2D createdAtlas;

		public AtlasPackerTool()
		{
			ResetValues();

			values.atlasXCount = 1;
			values.atlasYCount = 1;

			values.atlasSizesX = TextureSizes._1024;
			values.atlasSizesY = TextureSizes._1024;

			values.squareAtlas = true;

			values.atlasFiltering = FilterMode.Bilinear;
		}

		public void ResetValues()
		{
			values = ScriptableObject.CreateInstance<AtlasPackerValues>();
			values.atlas = new Texture2D[0];
		}

		public void CreateAtlas()
		{
			int atlasElements = values.atlasXCount * values.atlasYCount;
			int atlasWidth = (int)values.atlasSizesX;
			int atlasHeight = (int)values.atlasSizesY;

			Texture2D[] AtlasCopy = (Texture2D[])values.atlas.Clone();
			int textureXTargetWidth = atlasWidth / values.atlasXCount;
			int textureYTargetHeight = atlasHeight / values.atlasYCount;
			values.createdAtlas = new Texture2D(atlasWidth, atlasHeight);
			for (int i = 0; i < values.atlasYCount; i++)
			{
				for (int j = 0; j < values.atlasXCount; j++)
				{
					int currIndex = (i * values.atlasXCount) + j;
					bool hasImageForThisIndex = currIndex < AtlasCopy.Length && AtlasCopy[currIndex] != null;
					if (hasImageForThisIndex)
					{
						EditorUtils.SetTextureReadWrite(AssetDatabase.GetAssetPath(AtlasCopy[currIndex]), true);
						Texture2D copyTexture = new Texture2D(AtlasCopy[currIndex].width, AtlasCopy[currIndex].height);
						copyTexture.SetPixels(AtlasCopy[currIndex].GetPixels());
						copyTexture.Apply();
						AtlasCopy[currIndex] = copyTexture;
						AtlasCopy[currIndex] = EditorUtils.ScaleTexture(AtlasCopy[currIndex], textureXTargetWidth, textureYTargetHeight);
						AtlasCopy[currIndex].Apply();
					}

					for (int y = 0; y < textureYTargetHeight; y++)
					{
						for (int x = 0; x < textureXTargetWidth; x++)
						{
							if (hasImageForThisIndex) values.createdAtlas.SetPixel((j * textureXTargetWidth) + x, (i * textureYTargetHeight) + y, AtlasCopy[currIndex].GetPixel(x, y));
							else values.createdAtlas.SetPixel((j * textureXTargetWidth) + x, (i * textureYTargetHeight) + y, new Color(0, 0, 0, 1));
						}
					}
				}
			}

			values.createdAtlas.Apply();
		}

		public int GetAtlasElements()
		{
			int res = values.atlasXCount * values.atlasYCount;
			return res;
		}
	}
}
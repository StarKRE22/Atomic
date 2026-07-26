using UnityEngine;

namespace AllIn13DShader
{
	public class AtlasPackerValues : ScriptableObject
	{
		public int atlasXCount;
		public int atlasYCount;

		public TextureSizes atlasSizesX;
		public TextureSizes atlasSizesY;

		public FilterMode atlasFiltering;

		public bool squareAtlas;

		public Texture2D[] atlas;

		public Texture2D createdAtlas;
	}
}
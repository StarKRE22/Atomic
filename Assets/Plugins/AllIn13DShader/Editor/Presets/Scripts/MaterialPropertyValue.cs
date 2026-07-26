using UnityEngine;

namespace AllIn13DShader
{
	[System.Serializable]
	public class MaterialPropertyValue
	{
		public enum ValueType
		{
			NONE,
			INT,
			FLOAT,
			COLOR,
			TEXTURE,
		}

		public string propertyName;

		public ValueType valueType;

		public int intValue;
		public float floatValue;
		public Color colorValue;
		public Texture2D textureValue;
	}
}
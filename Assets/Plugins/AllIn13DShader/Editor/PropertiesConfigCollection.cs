using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class PropertiesConfigCollection : ScriptableObject
	{
		public PropertiesConfig[] shaderPropertiesConfig;

		public void AddConfig(PropertiesConfig config)
		{
			if (shaderPropertiesConfig == null)
			{
				shaderPropertiesConfig = new PropertiesConfig[0];
			}

			ArrayUtility.Add(ref shaderPropertiesConfig, config);
		}

		public PropertiesConfig FindPropertiesConfigByShader(Shader shader)
		{
			PropertiesConfig res = null;

			for (int i = 0; i < shaderPropertiesConfig.Length; i++)
			{
				if (shaderPropertiesConfig[i].shader == shader)
				{
					res = shaderPropertiesConfig[i];
					break;
				}
			}

			return res;
		}

		public bool IsAllIn3DShaderMaterial(Material mat)
		{
			bool res = false;

			for (int i = 0; i < shaderPropertiesConfig.Length; i++)
			{
				if (shaderPropertiesConfig[i].shader == mat.shader)
				{
					res = true;
				}
			}

			return res;
		}
	}
}
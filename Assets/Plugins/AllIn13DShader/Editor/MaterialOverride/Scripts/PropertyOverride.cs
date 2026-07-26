using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace AllIn13DShader
{
	public class PropertyOverride
	{
		public string propertyName
		{
			get; private set;
		}

		public string displayName
		{
			get; private set;
		}

		private AbstractEffectOverride effectOverride;

		public ShaderPropertyType shaderPropertyType;

		public bool isKeywordsProperty;

		public float floatValue;
		public int intValue;
		public Color colorValue;
		public Vector2 rangeLimits;
		public Vector4 vectorValue;
		public Texture texValue;

		public string[] keywordsEnumOptions;


		public PropertyOverride(AbstractEffectOverride effectOverride, int propertyIndex, Shader shader)
		{
			this.effectOverride = effectOverride;
			this.isKeywordsProperty = false;

			Initialize(propertyIndex, shader);
		}

		public PropertyOverride(AbstractEffectOverride effectOverride, EffectProperty effectProperty, Shader shader) 
		{
			this.effectOverride = effectOverride;
			this.isKeywordsProperty = effectProperty.propertyKeywords.Count > 0;

			if (isKeywordsProperty)
			{
				keywordsEnumOptions = new string[effectProperty.propertyKeywords.Count];
				for(int i = 0; i < effectProperty.propertyKeywords.Count; i++)
				{
					keywordsEnumOptions[i] = effectProperty.propertyKeywords[i];
				}
			}

			Initialize(effectProperty.propertyIndex, shader);
		}

		private void Initialize(int propertyIndex, Shader shader)
		{
			this.propertyName = shader.GetPropertyName(propertyIndex);
			this.displayName = shader.GetPropertyDescription(propertyIndex);

			this.shaderPropertyType = shader.GetPropertyType(propertyIndex);

			if (this.shaderPropertyType == ShaderPropertyType.Range)
			{
				this.rangeLimits = shader.GetPropertyRangeLimits(propertyIndex);
			}

			InitializeDefaultValues(propertyIndex, shader);
		}

		private void InitializeDefaultValues(int propertyIndex, Shader shader)
		{
			switch (shaderPropertyType)
			{
				case ShaderPropertyType.Range:
				case ShaderPropertyType.Float:
					this.floatValue = shader.GetPropertyDefaultFloatValue(propertyIndex);
					break;
				case ShaderPropertyType.Vector:
					this.vectorValue = shader.GetPropertyDefaultVectorValue(propertyIndex);
					break;
				case ShaderPropertyType.Color:
					this.colorValue = shader.GetPropertyDefaultVectorValue(propertyIndex);
					break;
				case ShaderPropertyType.Int:
					this.intValue = (int)shader.GetPropertyDefaultFloatValue(propertyIndex);
					break;
			}
		}

		public override bool Equals(object obj)
		{
			bool res = false;

			if (obj is PropertyOverride)
			{
				PropertyOverride propertyOverride = (PropertyOverride)obj;
				res = (propertyName == propertyOverride.propertyName);
			}

			return res;
		}

		public override int GetHashCode()
		{
			int res = propertyName.GetHashCode();

			return res;
		}

		public void ApplyChangesToMaterial(Material mat)
		{
			if(mat == null) { return; }

			if (isKeywordsProperty)
			{
				//TODO: Make this cleaner
				for(int i = 0; i < keywordsEnumOptions.Length; i++)
				{
					string kw = (propertyName + "_" + keywordsEnumOptions[i]).ToUpper();
					mat.DisableKeyword(kw);

					if(i == floatValue)
					{
						mat.EnableKeyword(kw);
					}
				}
			}

			switch (shaderPropertyType)
			{
				case ShaderPropertyType.Float:
				case ShaderPropertyType.Range:
					mat.SetFloat(propertyName, floatValue);
					break;
				case ShaderPropertyType.Color:
					mat.SetColor(propertyName, colorValue);
					break;
				case ShaderPropertyType.Texture:
					mat.SetTexture(propertyName, texValue);
					break;
				case ShaderPropertyType.Vector:
					mat.SetVector(propertyName, vectorValue);
					break;
				case ShaderPropertyType.Int:
					mat.SetInt(propertyName, intValue);
					break;
			}
		}

		public bool Remove()
		{
			bool res = false;

			if(effectOverride != null)
			{
				res = effectOverride.RemovePropertyOverride(this);
			}

			return res;
		}
	}
}
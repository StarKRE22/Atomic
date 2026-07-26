using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace AllIn13DShader
{
	public class ConverterGeneral
	{
		protected Material from;
		protected Material target;

		protected ConversionConfig conversionConfig;
		protected PropertiesConfigCollection propertiesConfigCollection;
		protected PropertiesConfig propertiesConfig;

		public virtual void ApplyConversion(Material from, Material target)
		{
			this.from = from;
			this.target = target;

			this.conversionConfig = EditorUtils.FindAssetByName<ConversionConfig>("ConversionConfig");
			this.propertiesConfigCollection = EditorUtils.FindAssetByName<PropertiesConfigCollection>("PropertiesConfigCollection");
			this.propertiesConfig = propertiesConfigCollection.FindPropertiesConfigByShader(target.shader);

			for (int i = 0; i < conversionConfig.conversionProperties.Length; i++)
			{
				ConversionProperty conversionProperty = conversionConfig.conversionProperties[i];

				bool propertiveActive = false;
				ApplyConversionProperty(conversionProperty, from, target, ref propertiveActive);

				if (!string.IsNullOrEmpty(conversionProperty.belongingToEffect) && conversionProperty.requiredProperty && propertiveActive)
				{
					AllIn13DEffectConfig effectConfig = propertiesConfig.FindEffectConfigByID(conversionProperty.belongingToEffect);
					EnableEffect(effectConfig);
				}
			}

			target.renderQueue = from.renderQueue;
		}

		protected void SetEnableEffect(string effectID, bool enabled)
		{
			AllIn13DEffectConfig effectConfig = propertiesConfig.FindEffectConfigByID(effectID);

			if (enabled)
			{
				EnableEffect(effectConfig);
			}
			else
			{
				DisableEffect(effectConfig);
			}
		}

		protected void EnableEffect(AllIn13DEffectConfig effectConfig)
		{
			if (effectConfig.keywords.Count == 1)
			{
				target.EnableKeyword(effectConfig.keywords[0].keyword);
				target.SetFloat(effectConfig.keywordPropertyName, 1f);
			}
		}

		protected void DisableEffect(string effectID)
		{
			AllIn13DEffectConfig effectConfig = propertiesConfig.FindEffectConfigByID(effectID);
			DisableEffect(effectConfig);
		}

		protected void DisableEffect(AllIn13DEffectConfig effectConfig)
		{
			for (int i = 0; i < effectConfig.keywords.Count; i++)
			{
				target.DisableKeyword(effectConfig.keywords[i].keyword);
			}

			target.SetFloat(effectConfig.keywordPropertyName, 0f);
		}

		protected void EnablePBR()
		{
			AllIn13DEffectConfig shadingModelEffect = propertiesConfig.FindEffectConfigByID("SHADINGMODEL");

			DisableEffect(shadingModelEffect);

			string pbrKeyword = shadingModelEffect.keywords[1].keyword;

			target.EnableKeyword(pbrKeyword);
			target.SetFloat("_ShadingModel", 1.0f);
		}

		protected void EnableLightModelClassic()
		{
			AllIn13DEffectConfig lightModelEffect = propertiesConfig.FindEffectConfigByID("LIGHTMODEL");

			DisableEffect(lightModelEffect);

			string lightModelClassicKeyword = lightModelEffect.keywords[1].keyword;

			target.EnableKeyword(lightModelClassicKeyword);
			target.SetFloat(lightModelEffect.keywordPropertyName, 1.0f);
		}

		protected void EnableSpecularClassic()
		{
			AllIn13DEffectConfig specularModelEffect = propertiesConfig.FindEffectConfigByID("SPECULARMODEL");

			DisableEffect(specularModelEffect);

			string specularModelClassicKeyword = specularModelEffect.keywords[1].keyword;

			target.EnableKeyword(specularModelClassicKeyword);
			target.SetFloat(specularModelEffect.keywordPropertyName, 1.0f);
		}

		protected void SetBlendSrc(BlendMode blendMode)
		{
			target.SetInt("_BlendSrc", (int)blendMode);
		}

		protected void SetBlendDst(BlendMode blendMode)
		{
			target.SetInt("_BlendDst", (int)blendMode);
		}

		protected void SetAlphaPreset()
		{
			target.SetFloat("_RenderPreset", 2);
		}

		protected void SetOpaquePreset()
		{
			target.SetFloat("_RenderPreset", 1);
		}

		protected void ApplyConversionProperty(ConversionProperty conversionProperty, Material from, Material target, ref bool propertyActive)
		{
			string propertyNameFrom = string.Empty;
			string propertyNameTarget = conversionProperty.propertyName;

			if (from.HasProperty(conversionProperty.propertyName))
			{
				propertyNameFrom = conversionProperty.propertyName;
			}
			else
			{
				for (int i = 0; i < conversionProperty.alternativeNames.Length; i++)
				{
					if (from.HasProperty(conversionProperty.alternativeNames[i]))
					{
						propertyNameFrom = conversionProperty.alternativeNames[i];
						break;
					}
				}
			}

			if (!string.IsNullOrEmpty(propertyNameFrom))
			{
				switch (conversionProperty.propertyType)
				{
					case ConversionPropertyType.TEXTURE:
						Texture texValue = from.GetTexture(propertyNameFrom);
						Vector2 texOffset = from.GetTextureOffset(propertyNameFrom);
						Vector2 texScale = from.GetTextureScale(propertyNameFrom);

						target.SetTexture(propertyNameTarget, texValue);

						target.SetTextureOffset(propertyNameTarget, texOffset);
						target.SetTextureScale(propertyNameTarget, texScale);
						
						propertyActive = texValue != null;

						break;
					case ConversionPropertyType.FLOAT:
						float floatValue = from.GetFloat(propertyNameFrom);
						target.SetFloat(propertyNameTarget, floatValue);

						propertyActive = true;

						break;
					case ConversionPropertyType.COLOR:
						Color colorValue = from.GetColor(propertyNameFrom);
						target.SetColor(propertyNameTarget, colorValue);

						propertyActive = true;

						break;
					case ConversionPropertyType.VECTOR:
						Vector4 vectorValue = from.GetVector(propertyNameFrom);
						target.SetVector(propertyNameTarget, vectorValue);

						propertyActive = true;

						break;
				}
			}
		}
	}
}
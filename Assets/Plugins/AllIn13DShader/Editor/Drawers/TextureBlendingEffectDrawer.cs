using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class TextureBlendingEffectDrawer : AbstractEffectDrawer
	{
		private int mainTexPropertyIndex;
		private EffectProperty mainNormalEffectProperty;

		private EffectProperty effectPropBlendingSource;

		private EffectProperty effectPropTexBlendingMask;
		private EffectProperty effectPropBlendingMaskCutoffG;
		private EffectProperty effectPropBlendingMaskSmoothnessG;
		private EffectProperty effectPropBlendingMaskCutoffB;
		private EffectProperty effectPropBlendingMaskSmoothnessB;

		private EffectProperty effectPropBlendingMaskCutoffWhite;
		private EffectProperty effectPropBlendingMaskSmoothnessWhite;

		private EffectProperty effectPropBlendingMode;

		private EffectProperty effectPropBlendingTextureG;
		private EffectProperty effectPropBlendingTextureB;
		private EffectProperty effectPropBlendingTextureWhite;

		private EffectProperty effectPropBlendingNormalMapG;
		private EffectProperty effectPropBlendingNormalMapB;
		private EffectProperty effectPropBlendingNormalMapWhite;

		public TextureBlendingEffectDrawer(EffectProperty mainNormalMapEffectProperty, AllIn13DShaderInspectorReferences references, PropertiesConfig propertiesConfig) : base(references, propertiesConfig)
		{
			this.drawerID = Constants.TEXTURE_BLENDING_EFFECT_DRAWER_ID;

			mainTexPropertyIndex = FindPropertyIndex("_MainTex");
			this.mainNormalEffectProperty = mainNormalMapEffectProperty;

			this.effectConfig = propertiesConfig.FindEffectConfigByID("TEXTURE_BLENDING");

			this.effectPropBlendingSource = effectConfig.FindEffectPropertyByName("_TextureBlendingSource");

			this.effectPropTexBlendingMask = effectConfig.FindEffectPropertyByName("_TexBlendingMask");
			this.effectPropBlendingMaskCutoffG = effectConfig.FindEffectPropertyByName("_BlendingMaskCutoffG");
			this.effectPropBlendingMaskSmoothnessG = effectConfig.FindEffectPropertyByName("_BlendingMaskSmoothnessG");
			this.effectPropBlendingMaskCutoffB = effectConfig.FindEffectPropertyByName("_BlendingMaskCutoffB");
			this.effectPropBlendingMaskSmoothnessB = effectConfig.FindEffectPropertyByName("_BlendingMaskSmoothnessB");

			this.effectPropBlendingMaskCutoffWhite = effectConfig.FindEffectPropertyByName("_BlendingMaskCutoffWhite");
			this.effectPropBlendingMaskSmoothnessWhite = effectConfig.FindEffectPropertyByName("_BlendingMaskSmoothnessWhite");

			this.effectPropBlendingMode = effectConfig.FindEffectPropertyByName("_TextureBlendingMode");

			this.effectPropBlendingTextureG		= effectConfig.FindEffectPropertyByName("_BlendingTextureG");
			this.effectPropBlendingTextureB		= effectConfig.FindEffectPropertyByName("_BlendingTextureB");
			this.effectPropBlendingTextureWhite = effectConfig.FindEffectPropertyByName("_BlendingTextureWhite");

			this.effectPropBlendingNormalMapG		= effectConfig.FindEffectPropertyByName("_BlendingNormalMapG");
			this.effectPropBlendingNormalMapB		= effectConfig.FindEffectPropertyByName("_BlendingNormalMapB");
			this.effectPropBlendingNormalMapWhite	= effectConfig.FindEffectPropertyByName("_BlendingNormalMapWhite");
		}

		protected override void DrawProperties()
		{
			bool isRGBMode = references.IsKeywordEnabled("_TEXTUREBLENDINGMODE_RGB");
			bool isBlendingSourceTexture = references.IsKeywordEnabled("_TEXTUREBLENDINGSOURCE_TEXTURE");
			bool isNormalEnabled = references.IsKeywordEnabled("_NORMAL_MAP_ON");

			DrawProperty(effectPropBlendingSource);
			if(IsEffectPropertyVisible(effectPropTexBlendingMask, references.targetMats))
			{
				DrawProperty(effectPropTexBlendingMask);
			}


			DrawProperty(effectPropBlendingMode);

			GUILayout.Space(20f);

			MaterialProperty matPropertyMainNormalMap = references.matProperties[mainNormalEffectProperty.propertyIndex];
			if (isRGBMode)
			{
				EffectPropertyDrawer.DrawProperty(
					materialProperty: references.matProperties[mainTexPropertyIndex],
					labelPrefix: string.Empty,
					displayName: $"{references.matProperties[mainTexPropertyIndex].displayName} (R)",
					allowReset: true,
					references: references);

				if (isNormalEnabled)
				{
					EffectPropertyDrawer.DrawProperty(
						materialProperty: matPropertyMainNormalMap,
						labelPrefix: string.Empty,
						displayName: $"{matPropertyMainNormalMap.displayName} (R)",
						allowReset: true,
						references: references);
				}

				if (isBlendingSourceTexture)
				{
					GUILayout.Space(20f);
				}

				DrawProperty(effectPropBlendingTextureG);
				if (isNormalEnabled)
				{
					DrawProperty(effectPropBlendingNormalMapG);
				}

				if (isBlendingSourceTexture)
				{
					DrawProperty(effectPropBlendingMaskCutoffG);
					DrawProperty(effectPropBlendingMaskSmoothnessG);
					GUILayout.Space(20f);
				}

				DrawProperty(effectPropBlendingTextureB);
				if (isNormalEnabled)
				{
					DrawProperty(effectPropBlendingNormalMapB);
				}

				if (isBlendingSourceTexture)
				{
					DrawProperty(effectPropBlendingMaskCutoffB);
					DrawProperty(effectPropBlendingMaskSmoothnessB);
					GUILayout.Space(20f);
				}
			}
			else
			{
				EffectPropertyDrawer.DrawProperty(
					materialProperty: references.matProperties[mainTexPropertyIndex],
					labelPrefix: string.Empty,
					displayName: $"{references.matProperties[mainTexPropertyIndex].displayName} (Black)",
					allowReset: true,
					references: references);

				if (isNormalEnabled)
				{
					EffectPropertyDrawer.DrawProperty(
						materialProperty: matPropertyMainNormalMap,
						labelPrefix: string.Empty,
						displayName: $"{matPropertyMainNormalMap.displayName} (R)",
						allowReset: true,
						references: references);
				}

				DrawProperty(effectPropBlendingTextureWhite);
				if (isNormalEnabled)
				{
					DrawProperty(effectPropBlendingNormalMapWhite);
				}

				if (isBlendingSourceTexture)
				{
					DrawProperty(effectPropBlendingMaskCutoffWhite);
					DrawProperty(effectPropBlendingMaskSmoothnessWhite);
				}
			}
		}
	}
}
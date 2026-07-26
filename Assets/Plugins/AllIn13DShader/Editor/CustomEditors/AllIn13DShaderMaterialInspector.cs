using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	[CanEditMultipleObjects]
	public class AllIn13DShaderMaterialInspector : ShaderGUI
	{
		private PropertiesConfigCollection propertiesConfigCollection;
		private PropertiesConfig currentPropertiesConfig;

		private AllIn13DShaderInspectorReferences inspectorReferences;
		private AbstractEffectDrawer[] drawers;
		private GlobalPropertiesDrawer globalPropertiesDrawer;
		private AdvancedPropertiesDrawer advancedPropertiesDrawer;

		private MaterialPresetCollection blendingModeCollection;

		private MaterialProperty matPropertyRenderPreset;
		private MaterialProperty matPropertyBlendSrc;
		private MaterialProperty matPropertyBlendDst;
		private MaterialProperty matPropertyZWrite;
		
		private int lastRenderQueue;
		private float lasTimeRebuilt;

		private static CommonStyles commonStyles;

		private void RefreshReferences(MaterialEditor materialEditor, MaterialProperty[] properties)
		{
			if (inspectorReferences == null)
			{
				inspectorReferences = new AllIn13DShaderInspectorReferences();
				inspectorReferences.Setup(materialEditor, properties);

				if (lastRenderQueue > 0)
				{
					for(int i = 0; i < inspectorReferences.targetMats.Length; i++)
					{
						inspectorReferences.targetMats[i].renderQueue = lastRenderQueue;
					}
				}
			}

			if (propertiesConfigCollection == null)
			{
				string[] guids = AssetDatabase.FindAssets("PropertiesConfigCollection t:PropertiesConfigCollection");
				if(guids.Length == 0)
				{
					Debug.LogWarning("PropertiesConfigCollection not found in the project. Configuring...");
					this.propertiesConfigCollection = PropertiesConfigCreator.CreateConfig();
					Debug.LogWarning("AllIn13DShader configured");
				}
				else
				{
					string path = AssetDatabase.GUIDToAssetPath(guids[0]);
					propertiesConfigCollection = AssetDatabase.LoadAssetAtPath<PropertiesConfigCollection>(path);
				}

				RefreshPropertiesConfig();

				lasTimeRebuilt = (float)EditorApplication.timeSinceStartup;
			}

			if (blendingModeCollection == null)
			{
				blendingModeCollection = (MaterialPresetCollection)EditorUtils.FindAsset<ScriptableObject>("BlendingModeCollection");
			}

			CreateDrawers();

			matPropertyRenderPreset = inspectorReferences.matProperties[currentPropertiesConfig.renderPreset];
			matPropertyBlendSrc = inspectorReferences.matProperties[currentPropertiesConfig.blendSrcIdx];
			matPropertyBlendDst = inspectorReferences.matProperties[currentPropertiesConfig.blendDstIdx];
			matPropertyZWrite = inspectorReferences.matProperties[currentPropertiesConfig.zWriteIndex];

			//We ensure that data is refreshed. Sometimes objects are not null but we need to refresh the references
			inspectorReferences.Setup(materialEditor, properties);

			if(commonStyles == null)
			{
				commonStyles = new CommonStyles();
			}

			RefreshDrawers();
		}

		private void ResetReferences()
		{
			this.propertiesConfigCollection = null;
			this.currentPropertiesConfig = null;

			inspectorReferences = null;
			drawers = null;

			globalPropertiesDrawer = null;
			advancedPropertiesDrawer = null;
		}

		private void CreateDrawers()
		{
			if (drawers == null)
			{
				drawers = new AbstractEffectDrawer[0];

				GeneralEffectDrawer generalEffectDrawer = new GeneralEffectDrawer(inspectorReferences, currentPropertiesConfig);


				EffectProperty mainNormalMapProperty = currentPropertiesConfig.FindEffectProperty("NORMAL_MAP", "_NormalMap");

				TriplanarEffectDrawer triplanarEffectDrawer = new TriplanarEffectDrawer(mainNormalMapProperty, inspectorReferences, currentPropertiesConfig);
				ColorRampEffectDrawer colorRampEffectDrawer = new ColorRampEffectDrawer(inspectorReferences, currentPropertiesConfig);
				OutlineEffectDrawer outlineEffectDrawer = new OutlineEffectDrawer(inspectorReferences, currentPropertiesConfig);
				TextureBlendingEffectDrawer vertexColorEffectDrawer = new TextureBlendingEffectDrawer(mainNormalMapProperty, inspectorReferences, currentPropertiesConfig);

				ArrayUtility.Add(ref drawers, generalEffectDrawer);
				ArrayUtility.Add(ref drawers, triplanarEffectDrawer);
				ArrayUtility.Add(ref drawers, colorRampEffectDrawer);
				ArrayUtility.Add(ref drawers, outlineEffectDrawer);
				ArrayUtility.Add(ref drawers, vertexColorEffectDrawer);

				advancedPropertiesDrawer = new AdvancedPropertiesDrawer(currentPropertiesConfig.advancedProperties, currentPropertiesConfig.blendSrcIdx, currentPropertiesConfig.blendDstIdx, inspectorReferences);
			}

			if (globalPropertiesDrawer == null)
			{
				globalPropertiesDrawer = new GlobalPropertiesDrawer();
			}
		}

		private void RefreshDrawers()
		{
			for(int i = 0; i < drawers.Length; i++)
			{
				drawers[i].Refresh(inspectorReferences);
			}
		}

		private AbstractEffectDrawer FindEffectDrawerByID(string drawerID)
		{
			AbstractEffectDrawer res = null;

			for (int i = 0; i < drawers.Length; i++)
			{
				if (drawers[i].ID == drawerID)
				{
					res = drawers[i];
					break;
				}
			}

			return res;
		}

		public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
		{
			base.AssignNewShaderToMaterial(material, oldShader, newShader);
		}

		private void RefreshPropertiesConfig()
		{
			Shader shader = inspectorReferences.GetShader();
			currentPropertiesConfig = propertiesConfigCollection.FindPropertiesConfigByShader(shader);

			inspectorReferences.SetOutlineEffect(currentPropertiesConfig);
			inspectorReferences.SetCastShadowsEffect(currentPropertiesConfig);
		}

		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
		{
			if (lasTimeRebuilt <= EditorPrefs.GetFloat(Constants.LAST_TIME_SHADER_PROPERTIES_REBUILT_KEY, float.MaxValue))
			{
				ResetReferences();
				lasTimeRebuilt = (float)EditorApplication.timeSinceStartup;
			}

			if (inspectorReferences != null && drawers != null)
			{
				inspectorReferences.Setup(materialEditor, properties);
			}

			RefreshReferences(materialEditor, properties);

			commonStyles.InitStyles();

#if ALLIN13DSHADER_URP
			bool urpCorrectlyConfigured = URPConfigurator.IsURPCorrectlyConfigured();
			if (!urpCorrectlyConfigured)
			{
				EditorGUILayout.LabelField(CommonMessages.URP_PIPELINE_NOT_ASSIGNED, commonStyles.warningLabel);
				EditorUtils.DrawButtonLink(CommonMessages.URP_PIPELINE_NOT_ASSIGNED_DOC_LINK);

				GUILayout.Space(50f);
			}

			EditorGUI.BeginDisabledGroup(!urpCorrectlyConfigured);
#endif

			DrawPresetsTabs();
			DrawAdvancedProperties();
			DrawGlobalProperties();
			DrawEffects();

#if ALLIN13DSHADER_URP
			EditorGUI.EndDisabledGroup();
#endif


			bool shaderChanged = false;
			for (int i = 0; i < inspectorReferences.targetMats.Length; i++)
			{
				Material targetMat = inspectorReferences.targetMats[i];

				lastRenderQueue = targetMat.renderQueue;

				CheckPasses(targetMat);
				shaderChanged = shaderChanged || CheckShader(targetMat);
			}

			if (shaderChanged)
			{
				ResetReferences();
			}
		}

		private bool CheckShader(Material targetMat)
		{
			bool isOutline = AllIn13DEffectConfig.IsEffectEnabled(inspectorReferences.outlineEffectConfig, inspectorReferences);
			bool castShadowsEnabled = AllIn13DEffectConfig.IsEffectEnabled(inspectorReferences.castShadowsEffectConfig, inspectorReferences);

			bool shaderChanged = false;

			Shader oldShader = null;
			Shader newShader = null;
			Shader shaderToCompare = null;

			if (isOutline)
			{
				if (castShadowsEnabled)
				{
					shaderToCompare = inspectorReferences.shOutline;
				}
				else
				{
					shaderToCompare = inspectorReferences.shOutlineNoShadowCaster;
				}
			}
			else
			{
				if (castShadowsEnabled)
				{
					shaderToCompare = inspectorReferences.shStandard;
				}
				else
				{
					shaderToCompare = inspectorReferences.shStandardNoShadowCaster;
				}
			}

			if (targetMat.shader != shaderToCompare)
			{
				oldShader = targetMat.shader;
				newShader = shaderToCompare;

				shaderChanged = true;
			}

			if (shaderChanged)
			{
				targetMat.shader = newShader;
			}

			return shaderChanged;
		}

		private bool CheckOutlineShader(ref bool shaderChanged, Material targetMat)
		{
			bool res = false;

			bool outlineEnabled = AllIn13DEffectConfig.IsEffectEnabled(inspectorReferences.outlineEffectConfig, inspectorReferences);
			shaderChanged = false;

			Shader oldShader = null;
			Shader newShader = null;

			if (outlineEnabled)
			{
				if(targetMat.shader != inspectorReferences.shOutline)
				{
					oldShader = inspectorReferences.shStandard;
					newShader = inspectorReferences.shOutline;

					shaderChanged = true;
				}
			}
			else
			{
				if (targetMat.shader != inspectorReferences.shStandard)
				{
					oldShader = inspectorReferences.shOutline;
					newShader = inspectorReferences.shStandard;

					shaderChanged = true;
				}
			}

			if (shaderChanged)
			{
				targetMat.shader = newShader;
			}

			res = outlineEnabled;

			return res;
		}

		//TODO: Reuse code from CheckOutlineShader
		private void CheckCastShadows(bool isOutline, ref bool shaderChanged, Material targetMat)
		{
			bool castShadowEnabled = AllIn13DEffectConfig.IsEffectEnabled(inspectorReferences.castShadowsEffectConfig, inspectorReferences);
			
			Shader referenceShaderNoShadowCaster = inspectorReferences.shStandardNoShadowCaster;
			if (isOutline)
			{
				referenceShaderNoShadowCaster = inspectorReferences.shOutlineNoShadowCaster;
			}

			Shader referenceShadowWithShadowCaster = inspectorReferences.shStandard;
			if (isOutline)
			{
				referenceShadowWithShadowCaster = inspectorReferences.shOutline;
			}

			Shader oldShader = null;
			Shader newShader = targetMat.shader;

			if (!castShadowEnabled)
			{
				if (targetMat.shader != referenceShaderNoShadowCaster)
				{
					oldShader = targetMat.shader;
					newShader = referenceShaderNoShadowCaster;

					shaderChanged = true;
				}
			}
			else
			{
				if (targetMat.shader != referenceShadowWithShadowCaster)
				{
					oldShader = targetMat.shader;
					newShader = referenceShadowWithShadowCaster;

					shaderChanged = true;
				}
			}

			if (shaderChanged)
			{
				targetMat.shader = newShader;
			}
		}

		private void CheckPasses(Material targetMat)
		{
			if (targetMat.IsKeywordEnabled("_LIGHTMODEL_FASTLIGHTING") || targetMat.IsKeywordEnabled("_LIGHTMODEL_NONE"))
			{
				targetMat.SetShaderPassEnabled("ForwardAdd", false);
			}
			else
			{
				targetMat.SetShaderPassEnabled("ForwardAdd", true);
			}
		}

		private void DrawPresetsTabs()
		{
			EditorGUI.BeginChangeCheck();

			string[] texts = blendingModeCollection.CreateStringsArray();


			int presetIndex = (int)matPropertyRenderPreset.floatValue;
			if (presetIndex >= blendingModeCollection.presets.Length)
			{
				presetIndex = 1;
				matPropertyRenderPreset.floatValue = presetIndex;
			}

			BlendingMode previousPreset = blendingModeCollection[presetIndex];
			if(previousPreset == null)
			{
				previousPreset = blendingModeCollection[0];
			}

			int newIndex = (int)matPropertyRenderPreset.floatValue;
			newIndex = GUILayout.SelectionGrid(newIndex, texts, 3, inspectorReferences.tabButtonStyle);
			matPropertyRenderPreset.floatValue = newIndex;
			if (EditorGUI.EndChangeCheck())
			{
				BlendingMode selectedPreset = blendingModeCollection[newIndex];
				for(int i = 0; i < inspectorReferences.targetMats.Length; i++)
				{
					Material targetMat = inspectorReferences.targetMats[i];
					ApplyMaterialPreset(targetMat, previousPreset, selectedPreset);
				}
			}
		}

		private void DrawAdvancedProperties()
		{
			advancedPropertiesDrawer.Draw();
		}

		private void DrawGlobalProperties()
		{
			globalPropertiesDrawer.Draw(currentPropertiesConfig.singleProperties, inspectorReferences);
		}

		private void DrawEffects()
		{
			int globalEffectIndex = 0;
			for (int groupIdx = 0; groupIdx < currentPropertiesConfig.effectsGroups.Length; groupIdx++)
			{
				EffectGroup effectGroup = currentPropertiesConfig.effectsGroups[groupIdx];
				if (effectGroup.effects.Length <= 0) { continue; }

				EditorGUILayout.Separator();
				EditorUtils.DrawLine(Color.grey, 1, 3);
				GUILayout.Label(effectGroup.DisplayName, inspectorReferences.bigLabelStyle);

				for (int effectIdx = 0; effectIdx < effectGroup.effects.Length; effectIdx++)
				{
					AllIn13DEffectConfig effectConfig = effectGroup.effects[effectIdx];

					globalEffectIndex++;

					AbstractEffectDrawer drawer = FindEffectDrawerByID(effectConfig.effectDrawerID);
					drawer.Draw(currentPropertiesConfig, effectConfig, globalEffectIndex);
				}
			}
		}

		private void ApplyMaterialPreset(Material targetMat, BlendingMode previousPresset, BlendingMode newPreset)
		{
			matPropertyBlendSrc.floatValue = (float)newPreset.blendSrc;
			matPropertyBlendDst.floatValue = (float)newPreset.blendDst;
			matPropertyZWrite.floatValue = newPreset.depthWrite ? 1.0f : 0.0f;


			lastRenderQueue = (int)newPreset.renderQueue;
			targetMat.renderQueue = lastRenderQueue;

			if (previousPresset != newPreset && previousPresset.defaultEnabledEffects != null)
			{
				for (int i = 0; i < previousPresset.defaultEnabledEffects.Length; i++)
				{
					string effectID = previousPresset.defaultEnabledEffects[i];
					AllIn13DEffectConfig effectConfig = currentPropertiesConfig.FindEffectConfigByID(effectID);

					for(int matIdx = 0; matIdx < inspectorReferences.targetMats.Length; matIdx++)
					{
						Material mat = inspectorReferences.targetMats[matIdx];
						AllIn13DEffectConfig.DisableEffectToggle(effectConfig, inspectorReferences, mat);
					}
				}
			}

			if (newPreset.defaultEnabledEffects != null)
			{
				for (int i = 0; i < newPreset.defaultEnabledEffects.Length; i++)
				{
					string effectID = newPreset.defaultEnabledEffects[i];
					AllIn13DEffectConfig effectConfig = currentPropertiesConfig.FindEffectConfigByID(effectID);

					for (int matIdx = 0; matIdx < inspectorReferences.targetMats.Length; matIdx++)
					{
						Material mat = inspectorReferences.targetMats[matIdx];
						AllIn13DEffectConfig.EnableEffectToggle(effectConfig, inspectorReferences, mat);
					}
				}
			}
		}
	}
}
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static AllIn13DShader.MaterialOverrideData;

namespace AllIn13DShader
{
	public class OverrideMaterialsTabDrawer
	{
		private enum State
		{
			NONE = 0,
			PREVIEW = 1,
		}

		private AllIn13DShaderWindow parentWindow;

		private CommonStyles commonStyles;

		private PropertySelectorAuxWindow propertySelectorWindow;

		private State state;

		private static MaterialOverrideData overrideData;
		private SerializedObject dataSO;
		private SerializedProperty spFolders;

		private GUIStyle propertiesStyle;

		public OverrideMaterialsTabDrawer()
		{
			Initialize();
		}

		private void Initialize()
		{
			overrideData = ScriptableObject.CreateInstance<MaterialOverrideData>();
			overrideData.Initialize();

			state = State.NONE;

			dataSO = new SerializedObject(overrideData);
			spFolders = dataSO.FindProperty("folders");
		}

		public void Show()
		{
			EditorApplication.wantsToQuit	+= OnWantsToQuit;
			EditorSceneManager.sceneClosing += OnSceneClosing;
			EditorSceneManager.sceneSaving	+= OnSceneSaving;
			EditorSceneManager.sceneSaved	+= OnSceneSaved;
		}

		public void Hide()
		{
			EditorApplication.wantsToQuit	-= OnWantsToQuit;
			EditorSceneManager.sceneClosing -= OnSceneClosing;
			EditorSceneManager.sceneSaving	-= OnSceneSaving;
			EditorSceneManager.sceneSaved	-= OnSceneSaved;

			Close();
		}

		public void Setup(CommonStyles commonStyles, AllIn13DShaderWindow parentWindow)
		{
			this.commonStyles = commonStyles;
			this.parentWindow = parentWindow;
		}

		public void Draw()
		{
			if(propertiesStyle == null)
			{
				propertiesStyle = new GUIStyle(EditorStyles.helpBox);
				propertiesStyle.margin = new RectOffset(0, 0, 0, 0);
			}

			dataSO.Update();

			GUILayout.Space(10f);

			if (state == State.NONE)
			{
				DrawStateNone();
			}
			else
			{
				DrawStatePreview();
			}

			dataSO.ApplyModifiedProperties();
		}

		private void StartOverrideProcess()
		{
			overrideData.ResetData();
			overrideData.CreateRendererOverride();

			overrideData.ShowPreviewChanges();

			state = State.PREVIEW;
		}

		private void EndOverrideProcess(bool applyChanges)
		{
			if (applyChanges)
			{
				List<Material> affectedMaterials = overrideData.CollectAffectedMaterials();

				string title = "Overriding AllIn13D materials";
				string message = $"You are about to override {affectedMaterials.Count} materials";
				string okButton = "Override";
				string cancelButton = "Cancel";

				bool isOk = EditorUtility.DisplayDialog(title, message, okButton, cancelButton);

				if (isOk)
				{
					overrideData.ApplyChangesToMaterials(affectedMaterials);
					overrideData.EndOverrideProcess();

					if(overrideData.applyTarget == MaterialOverrideData.ApplyTarget.CURRENT_SCENE)
					{
						EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
						AssetDatabase.SaveAssets();
					}

					state = State.NONE;
				}
			}
			else
			{
				overrideData.EndOverrideProcess();
				state = State.NONE;
			}
		}

		private void DrawStateNone()
		{
			if (GUILayout.Button("Start"))
			{
				StartOverrideProcess();
			}
		}

		private void DrawStatePreview()
		{
			overrideData.applyTarget = (MaterialOverrideData.ApplyTarget)EditorGUILayout.EnumPopup("Apply Target", overrideData.applyTarget);
			if(overrideData.applyTarget == MaterialOverrideData.ApplyTarget.SELECTED_FOLDERS)
			{
				EditorGUILayout.PropertyField(spFolders);
			}

			GUILayout.Space(20f);

			if (GUILayout.Button("+"))
			{
				propertySelectorWindow = PropertySelectorAuxWindow.GetWindow<PropertySelectorAuxWindow>(title: "Select Property", utility: true);
				propertySelectorWindow.Setup(PropertyAddedCallback);
			}

			GUILayout.Space(20f);

			EditorGUI.BeginChangeCheck();

			for(int i = 0; i < overrideData.generalPropertiesOverrides.Count; i++)
			{
				DrawOverriddenEffectProperty(overrideData.generalPropertiesOverrides[i]);
			}

			if (!overrideData.IsEmpty())
			{
				GUILayout.Space(10f);
			}

			for (int i = 0; i < overrideData.effectOverrides.Count; i++)
			{
				DrawEffectOverride(overrideData.effectOverrides[i]);
			}

			if (EditorGUI.EndChangeCheck())
			{
				overrideData.ShowPreviewChanges();
			}

			EditorGUILayout.BeginHorizontal();

			EditorGUI.BeginDisabledGroup(!overrideData.IsApplyEnabled() || overrideData.IsEmpty());
			if (GUILayout.Button("Apply"))
			{
				EndOverrideProcess(applyChanges: true);
			}
			EditorGUI.EndDisabledGroup();

			if (GUILayout.Button("Cancel"))
			{
				EndOverrideProcess(applyChanges: false);
			}
			
			EditorGUILayout.EndHorizontal();
		}

		private void DrawEffectOverride(AbstractEffectOverride effectOverride)
		{
			EditorGUILayout.BeginHorizontal();

			float lastLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 150f;
			EditorGUI.BeginChangeCheck();
			effectOverride.overrideEnabled = EditorGUILayout.Toggle($"{effectOverride.displayName} Override", effectOverride.overrideEnabled);
			if (EditorGUI.EndChangeCheck())
			{
				overrideData.RebuildPreviewMaterial();
			}
			EditorGUIUtility.labelWidth = lastLabelWidth;

			EditorGUI.BeginDisabledGroup(!effectOverride.overrideEnabled);
			if (effectOverride is EffectToggleOverride)
			{
				EffectToggleOverride effectToggleOverride = (EffectToggleOverride)effectOverride;
				effectToggleOverride.boolValue = EditorGUILayout.Toggle("On/Off", effectToggleOverride.boolValue);
			}
			else if(effectOverride is EffectEnumOverride)
			{
				EffectEnumOverride effectEnumOverride = (EffectEnumOverride)effectOverride;
				effectEnumOverride.index = EditorGUILayout.Popup("Value", effectEnumOverride.index, effectEnumOverride.enumOptions);
			}
			EditorGUI.EndDisabledGroup();

			if (GUILayout.Button("-", GUILayout.Width(50f)))
			{
				overrideData.RemoveEffectOverride(effectOverride);
			}

			EditorGUILayout.EndHorizontal();

			DrawEffectOverrideProperties(effectOverride);
		}

		private void DrawEffectOverrideProperties(AbstractEffectOverride effectOverride)
		{
			EditorGUILayout.BeginVertical(propertiesStyle);
			for (int i = 0; i < effectOverride.propertyOverrides.Count; i++)
			{
				DrawOverriddenEffectProperty(effectOverride.propertyOverrides[i]);
			}
			EditorGUILayout.EndVertical();
		}

		private void DrawOverriddenEffectProperty(PropertyOverride propertyOverride)
		{
			EditorGUILayout.BeginHorizontal();
			if (propertyOverride.isKeywordsProperty)
			{
				propertyOverride.floatValue = EditorGUILayout.Popup(propertyOverride.displayName, (int)propertyOverride.floatValue, propertyOverride.keywordsEnumOptions);
			}
			else
			{
				switch (propertyOverride.shaderPropertyType)
				{
					case ShaderPropertyType.Float:
						propertyOverride.floatValue = EditorGUILayout.FloatField(propertyOverride.displayName, propertyOverride.floatValue);
						break;
					case ShaderPropertyType.Range:
						propertyOverride.floatValue = EditorGUILayout.Slider(propertyOverride.displayName, propertyOverride.floatValue, propertyOverride.rangeLimits.x, propertyOverride.rangeLimits.y);
						break;
					case ShaderPropertyType.Int:
						propertyOverride.intValue = EditorGUILayout.IntField(propertyOverride.displayName, propertyOverride.intValue);
						break;
					case ShaderPropertyType.Color:
						propertyOverride.colorValue = EditorGUILayout.ColorField(propertyOverride.displayName, propertyOverride.colorValue);
						break;
					case ShaderPropertyType.Texture:
						propertyOverride.texValue = (Texture)EditorGUILayout.ObjectField(propertyOverride.displayName, propertyOverride.texValue, typeof(Texture), false);
						break;
					case ShaderPropertyType.Vector:
						propertyOverride.vectorValue = EditorGUILayout.Vector4Field(propertyOverride.displayName, propertyOverride.vectorValue);
						break;
				}
			}

			if (GUILayout.Button("-", GUILayout.Width(50f)))
			{
				overrideData.RemovePropertyOverride(propertyOverride);
				overrideData.RebuildPreviewMaterial();
			}

			EditorGUILayout.EndHorizontal();
		}

		private void PropertyAddedCallback(AllIn13DEffectConfig effectConfig, int propertyIndex, Shader shader, 
			PropertySelectorAuxWindow.TypeOfPropertyAdded typeOfPropertyAdded)
		{
			if(typeOfPropertyAdded == PropertySelectorAuxWindow.TypeOfPropertyAdded.EFFECT_MAIN)
			{
				overrideData.AddCopmleteEffectOverride(effectConfig, shader);
			}
			else if(typeOfPropertyAdded == PropertySelectorAuxWindow.TypeOfPropertyAdded.GLOBAL_PROPERTY)
			{
				overrideData.AddGeneralPropertyOverride(propertyIndex, shader);
			}
			//else if(typeOfPropertyAdded == PropertySelectorAuxWindow.TypeOfPropertyAdded.EFFECT_MAIN)
			//{
			//	overrideData.AddEffectOverride(effectConfig);
			//}

			overrideData.ShowPreviewChanges();
			parentWindow.Repaint();
		}

		public void Close()
		{
			EndOverrideProcess(applyChanges: false);
			//EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
			//AssetDatabase.SaveAssets();

			if (propertySelectorWindow != null)
			{
				propertySelectorWindow.Close();
			}
		}

		private void OnSceneClosing(Scene scene, bool removingScene)
		{
			Close();
			EditorSceneManager.MarkSceneDirty(scene);
			AssetDatabase.SaveAssets();

			Initialize();
		}

		private void OnSceneSaving(Scene scene, string path)
		{
			overrideData.UseMaterialSource();
			EditorSceneManager.MarkSceneDirty(scene);
		}

		private void OnSceneSaved(Scene scene)
		{
			if(state == State.PREVIEW)
			{
				overrideData.UsePreviewMaterials();
			}
		}

		private bool OnWantsToQuit()
		{
			bool res = true;

			if(state == State.PREVIEW)
			{
				bool dialog = EditorUtility.DisplayDialog("Overriding in process", "You are using preview materials. Finish override process before closing Unity", "End override process", "Cancel");
				if (dialog)
				{
					Close();
				}

				res = false;
			}

			return res;
		}

	}
}
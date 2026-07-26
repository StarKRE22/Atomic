using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AllIn13DShader
{
	public static class EffectPropertyDrawer
	{
		public static void DrawMainProperty(int globalEffectIndex, AllIn13DEffectConfig effectConfig, AllIn13DShaderInspectorReferences references)
		{
			EditorGUILayout.BeginHorizontal();

			string label = $"{globalEffectIndex}. {effectConfig.displayName}";
			
			switch (effectConfig.effectConfigType)
			{
				case EffectConfigType.EFFECT_TOGGLE:
					DrawMainPropertyToggle(label, effectConfig, references);
					break;
				case EffectConfigType.EFFECT_ENUM:
					DrawMainPropertyEnum(label, effectConfig, references);
					break;
			}

			EditorGUILayout.EndHorizontal();
		}

		public static void DrawMainPropertyToggle(string label, AllIn13DEffectConfig effectConfig, AllIn13DShaderInspectorReferences references)
		{
			bool isEffectEnabled = AllIn13DEffectConfig.IsEffectEnabled(effectConfig, references);
			
			EditorGUI.BeginChangeCheck();

			string tooltip = effectConfig.keywords[0].keyword + " (C#)";
			GUIContent guiContent = new GUIContent(label, tooltip);

			bool effectEnabledInAllMaterials = true;
			bool effectDisabledInAllMaterials = true;
			for (int i = 0; i < references.targetMats.Length; i++)
			{
				bool effectEnabledThisMat = AllIn13DEffectConfig.IsEffectEnabled(effectConfig, references.targetMats[i]);
				
				effectEnabledInAllMaterials = effectEnabledInAllMaterials && effectEnabledThisMat;
				effectDisabledInAllMaterials = effectDisabledInAllMaterials && !effectEnabledThisMat;
			}

			bool mixedValue = (!(effectEnabledInAllMaterials || effectDisabledInAllMaterials)) && references.targetMats.Length > 1;
			string style = mixedValue ? "ToggleMixed" : "Toggle";

			isEffectEnabled = GUILayout.Toggle(isEffectEnabled, guiContent, style);
			if (EditorGUI.EndChangeCheck())
			{
				for(int i = 0; i < references.targetMats.Length; i++)
				{
					Material mat = references.targetMats[i];

					if (isEffectEnabled)
					{
						AllIn13DEffectConfig.EnableEffect(effectConfig, references, mat);
					}
					else
					{
						AllIn13DEffectConfig.DisableEffect(effectConfig, references, mat);
					}
				}

				references.matProperties[effectConfig.keywordPropertyIndex].floatValue = isEffectEnabled ? 1f : 0f;

				EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

				references.SetMaterialsDirty();
			}
		}

		public static void DrawMainPropertyEnum(string label, AllIn13DEffectConfig effectConfig, AllIn13DShaderInspectorReferences references)
		{
			int selectedIndex = 0;
			//bool isEffectEnabled = AllIn13DEffectConfig.IsEffectEnabled(effectConfig, ref selectedIndex, references);


			bool sameEnumValueInAllMaterials = true;
			for(int i = 0; i < references.targetMats.Length; i++)
			{
				int enumIdx = -1;
				Material mat = references.targetMats[i];
				AllIn13DEffectConfig.IsEffectEnabled(effectConfig, ref enumIdx, mat);

				if(i == 0)
				{
					selectedIndex = enumIdx;
				}
				else
				{
					sameEnumValueInAllMaterials = sameEnumValueInAllMaterials && (enumIdx == selectedIndex);
				}
			}

			

			EditorGUI.BeginChangeCheck();

			string tooltip = effectConfig.keywords[selectedIndex].keyword + " (C#)";
			GUIContent guiContent = new GUIContent(label, tooltip);
			
			EditorGUI.showMixedValue = !sameEnumValueInAllMaterials;
			selectedIndex = EditorGUILayout.Popup(guiContent, selectedIndex, effectConfig.keywordsDisplayNames);
			EditorGUI.showMixedValue = false;

			if (EditorGUI.EndChangeCheck())
			{
				for (int i = 0; i < references.targetMats.Length; i++)
				{
					Material mat = references.targetMats[i];

					if (selectedIndex >= 0)
					{
						AllIn13DEffectConfig.EnableEffectByIndex(effectConfig, selectedIndex, references, mat);
					}
					else
					{
						AllIn13DEffectConfig.DisableEffect(effectConfig, references, mat);
					}
				}

				references.matProperties[effectConfig.keywordPropertyIndex].floatValue = selectedIndex;

				EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

				references.SetMaterialsDirty();
			}
		}

		public static void DrawProperty(int propertyIndex, string labelPrefix, bool allowReset, AllIn13DShaderInspectorReferences references)
		{
			MaterialProperty targetProperty = references.matProperties[propertyIndex];
			DrawProperty(targetProperty, labelPrefix, allowReset, references);
		}

		public static void DrawProperty(EffectProperty effectProperty, string labelPrefix, bool allowReset, AllIn13DShaderInspectorReferences references)
		{
			DrawProperty(effectProperty.propertyIndex, labelPrefix, effectProperty.allowReset, references);
		}

		public static void DrawProperty(EffectProperty effectProperty, AllIn13DShaderInspectorReferences references)
		{
			DrawProperty(effectProperty.propertyIndex, references);
		}

		public static void DrawProperty(int propertyIndex, AllIn13DShaderInspectorReferences references)
		{
			DrawProperty(propertyIndex, string.Empty, true, references);
		}

		public static void DrawProperty(MaterialProperty materialProperty, AllIn13DShaderInspectorReferences references)
		{
			DrawProperty(materialProperty, string.Empty, true, references);
		}

		public static void DrawProperty(MaterialProperty materialProperty, bool allowReset, AllIn13DShaderInspectorReferences references)
		{
			DrawProperty(materialProperty, string.Empty, allowReset, references);
		}

		public static void DrawProperty(MaterialProperty materialProperty, string labelPrefix, bool allowReset, AllIn13DShaderInspectorReferences references)
		{
			DrawProperty(
				materialProperty: materialProperty, 
				labelPrefix: labelPrefix, 
				displayName: materialProperty.displayName,
				allowReset: allowReset, 
				references: references);
		}

		public static void DrawProperty(MaterialProperty materialProperty, string labelPrefix, string displayName, bool allowReset, AllIn13DShaderInspectorReferences references)
		{
			string label = $"{labelPrefix} {displayName}";
			string tooltip = materialProperty.name + "(C#)";


			EditorGUILayout.BeginHorizontal();

			DrawProperty(materialProperty, label, tooltip, references);
			if (allowReset)
			{
				DrawResetButton(materialProperty, references);
			}

			EditorGUILayout.EndHorizontal();
		}

		public static void DrawProperty(MaterialProperty targetProperty, string label, string tooltip, AllIn13DShaderInspectorReferences references)
		{
			GUIContent propertyLabel = new GUIContent();
			propertyLabel.text = label;
			propertyLabel.tooltip = tooltip;

			references.editorMat.ShaderProperty(targetProperty, propertyLabel);
		}

		public static void DrawResetButton(MaterialProperty targetProperty, AllIn13DShaderInspectorReferences references)
		{
			GUIContent resetButtonLabel = new GUIContent();
			resetButtonLabel.text = "R";
			resetButtonLabel.tooltip = "Resets to default value";
			if (GUILayout.Button(resetButtonLabel, GUILayout.Width(20)))
			{
				for (int i = 0; i < references.targetMats.Length; i++)
				{
					Material mat = references.targetMats[i];
					AllIn13DEffectConfig.ResetProperty(targetProperty, references, mat);
				}
			}
		}
	}
}
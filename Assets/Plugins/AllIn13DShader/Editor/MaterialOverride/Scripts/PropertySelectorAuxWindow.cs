using System;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class PropertySelectorAuxWindow : EditorWindow
	{
		public enum SelectionType
		{
			GLOBAL_PROPERTY,
			EFFECT_GROUP,
		}

		public enum TypeOfPropertyAdded
		{
			GLOBAL_PROPERTY,
			EFFECT_PROPERTY,
			EFFECT_MAIN,
		}

		private PropertiesConfigCollection propertiesConfigCollection;

		private Shader allIn13DShader;
		private Shader allIn13DShaderOutline;

		private PropertiesConfig propertiesConfigNormal;
		private PropertiesConfig propertiesConfigOutline;

		private int effectGroupIdx;
		private int effectIdx;
		private int propertyIdx;
		private int globalPropertyIdx;

		private EffectGroup selectedEffectGroup;
		private AllIn13DEffectConfig selectedEffectConfig;
		private EffectProperty selectedEffectProperty;

		private TypeOfPropertyAdded typeOfPropertyAdded;
		private SelectionType selectionType;

		private Action<AllIn13DEffectConfig, int, Shader, TypeOfPropertyAdded> propertyOverrideAddedCallback;

		private void OnEnable()
		{
			this.allIn13DShader = Shader.Find(Constants.SHADER_FULL_NAME_ALLIN13D);
			this.allIn13DShaderOutline = Shader.Find(Constants.SHADER_FULL_NAME_ALLIN13D);

			this.effectIdx = 0;
			this.propertyIdx = 0;
			this.globalPropertyIdx = 0;

			propertiesConfigCollection = EditorUtils.FindAsset<PropertiesConfigCollection>("PropertiesConfigCollection");
			propertiesConfigNormal = propertiesConfigCollection.FindPropertiesConfigByShader(allIn13DShader);
		}

		public void Setup(Action<AllIn13DEffectConfig, int, Shader, TypeOfPropertyAdded> propertyOverrideAddedCallback)
		{
			this.propertyOverrideAddedCallback = propertyOverrideAddedCallback;
		}

		private void OnGUI()
		{
			DrawEffectGroupSelector();

			if(selectionType == SelectionType.EFFECT_GROUP)
			{
				DrawEffectSelector();
				//DrawPropertySelector();
			}
			else if(selectionType == SelectionType.GLOBAL_PROPERTY)
			{
				DrawGlobalPropertySelector();
			}

			GUILayout.Space(20f);

			if (GUILayout.Button("Add"))
			{
				AddProperty();
			}
		}

		private void DrawEffectGroupSelector()
		{
			string[] effectsGroupsNames = propertiesConfigNormal.GetEffectGroupsDisplayNames();
			ArrayUtility.Insert(ref effectsGroupsNames, 0, "Global Properties");
			effectGroupIdx = EditorGUILayout.Popup("Group", effectGroupIdx, effectsGroupsNames);

			if(effectGroupIdx == 0)
			{
				selectionType = SelectionType.GLOBAL_PROPERTY;
				this.selectedEffectGroup = null;
			}
			else
			{
				selectionType = SelectionType.EFFECT_GROUP;
				this.selectedEffectGroup = propertiesConfigNormal.effectsGroups[effectGroupIdx - 1];
			}
		}

		private void DrawEffectSelector()
		{
			string[] effectsNames = selectedEffectGroup.GetEffectsNames();
			effectIdx = EditorGUILayout.Popup("Effect", effectIdx, effectsNames);

			selectedEffectConfig = selectedEffectGroup.effects[effectIdx];
		}

		private void DrawGlobalPropertySelector()
		{
			string[] globalPropertyNames = propertiesConfigNormal.GetGlobalPropertyNames();
			globalPropertyIdx = EditorGUILayout.Popup("Property", globalPropertyIdx, globalPropertyNames);
		}

		private void DrawPropertySelector()
		{
			string[] propertyNames = selectedEffectConfig.GetPropertyNames();

			string overrideEffectMsg = "Enable or Disable";
			if (selectedEffectConfig.keywords.Count > 1)
			{
				overrideEffectMsg = selectedEffectConfig.displayName;
				overrideEffectMsg += " (";
				for (int i = 0; i < selectedEffectConfig.keywords.Count; i++) 
				{
					overrideEffectMsg += selectedEffectConfig.keywords[i].displayName;
					if(i < selectedEffectConfig.keywords.Count - 1)
					{
						overrideEffectMsg += ", ";
					}
				}
				overrideEffectMsg += ")";
			}

			ArrayUtility.Add(ref propertyNames, overrideEffectMsg);

			propertyIdx = EditorGUILayout.Popup("Property", propertyIdx, propertyNames);

			if(propertyIdx <= selectedEffectConfig.effectProperties.Count - 1)
			{
				selectedEffectProperty = selectedEffectConfig.effectProperties[propertyIdx];
			}
			else
			{
				selectedEffectProperty = null;
			}
		}

		private void AddProperty()
		{
			if(selectionType == SelectionType.GLOBAL_PROPERTY)
			{
				typeOfPropertyAdded = TypeOfPropertyAdded.GLOBAL_PROPERTY;
			}
			else
			{
				if(selectedEffectProperty == null)
				{
					typeOfPropertyAdded = TypeOfPropertyAdded.EFFECT_MAIN;
				}
				else
				{
					typeOfPropertyAdded = TypeOfPropertyAdded.EFFECT_PROPERTY;
				}
			}

			if (propertyOverrideAddedCallback != null)
			{
				switch (typeOfPropertyAdded)
				{
					case TypeOfPropertyAdded.GLOBAL_PROPERTY:
						propertyOverrideAddedCallback(selectedEffectConfig, propertiesConfigNormal.singleProperties[globalPropertyIdx], allIn13DShader, typeOfPropertyAdded);
						break;
					case TypeOfPropertyAdded.EFFECT_MAIN:
						propertyOverrideAddedCallback(selectedEffectConfig, -1, allIn13DShader, typeOfPropertyAdded);
						break;
					//case TypeOfPropertyAdded.EFFECT_PROPERTY:
					//	propertyOverrideAddedCallback(selectedEffectConfig, selectedEffectProperty.propertyIndex, allIn13DShader, typeOfPropertyAdded);
					//	break;
				}
			}
		
			Close();
		}
	}
}
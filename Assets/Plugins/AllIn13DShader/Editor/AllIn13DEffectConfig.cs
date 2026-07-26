using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static AllIn13DShader.EffectsExtraData;

namespace AllIn13DShader
{
	[System.Serializable]
	public class AllIn13DEffectConfig
	{
		public string effectName;
		public string displayName;
		public string group;
		
		public int keywordPropertyIndex;
		public string keywordPropertyName;

		public string effectDrawerID;
		public string dependentOnEffect;
		public string incompatibleWithEffectID;
		public string docURL;
		public MessageByKeywords[] customMessages;

		//public List<string> keywords;
		public List<EffectKeywordData> keywords;
		public string[] keywordsDisplayNames;
		public List<EffectProperty> effectProperties;

		public string disabledKeyword;

		public EffectConfigType effectConfigType;

		public AllIn13DEffectConfig(
			string displayName, string propertyName, int propertyIndex, EffectConfigType effectConfigType,
			EffectAttributeData data, EffectsExtraData effectsExtraData)
		{
			this.displayName = displayName;
			this.keywordPropertyName = propertyName;
			this.keywordPropertyIndex = propertyIndex;
			this.effectConfigType = effectConfigType;

			this.effectName = data.effectID;
			this.group = data.groupID;
			this.effectDrawerID = data.drawerID;
			this.dependentOnEffect = data.dependentEffectID;
			this.incompatibleWithEffectID = data.incompatibleWithEffectID;

			EffectsExtraData.ExtraData extraData = effectsExtraData.GetExtraDataByEffectID(effectName);
			if(extraData != null)
			{
				this.docURL = extraData.docURL;
				this.customMessages = extraData.customMessages;
			}

			this.keywords = new List<EffectKeywordData>();
			this.effectProperties = new List<EffectProperty>();
			this.keywordsDisplayNames = new string[0];
		}

		public void AddKeyword(EffectKeywordData kw)
		{
			keywords.Add(kw);
		}

		public void AddKeywords(EffectKeywordData[] kws)
		{
			keywords.AddRange(kws);
		}

		public void Setup()
		{
			for(int i = 0; i < keywords.Count; i++)
			{
				ArrayUtility.Add(ref keywordsDisplayNames, keywords[i].displayName);
			}
		}

		public EffectProperty FindEffectPropertyByIndex(int propertyIndex)
		{
			EffectProperty res = null;

			for (int i = 0; i < effectProperties.Count; i++)
			{
				if (effectProperties[i].propertyIndex == propertyIndex)
				{
					res = effectProperties[i];
					break;
				}
			}

			return res;
		}

		public EffectProperty FindEffectPropertyByName(string propertyName)
		{
			EffectProperty res = null;

			for(int i = 0; i < effectProperties.Count; i++)
			{
				if (effectProperties[i].propertyName == propertyName)
				{
					res = effectProperties[i];
					break;
				}
			}

			return res;
		}

		public EffectProperty CreateEffectProperty(int propertyIndex, string propertyName, string displayName, EffectPropertyAttributeData data)
		{
			EffectProperty res = new EffectProperty(this, propertyIndex, propertyName, displayName, data.keywordsOp, data.allowReset);
			effectProperties.Add(res);

			for (int i = 0; i < data.keywords.Count; i++)
			{
				res.AddKeyword(data.keywords[i]);
			}

			for(int i = 0; i < data.incompatibleWithKws.Count; i++)
			{
				res.AddIncompatibleKeyword(data.incompatibleWithKws[i]);
			}

			res.AddPropertyKeywords(data.propertyKeywords);

			return res;
		}

		public string GetCustomMessage(Material[] targetMats)
		{
			string res = string.Empty;
			if(targetMats.Length == 1)
			{
				res = GetCustomMessage(targetMats[0]);
			}
			return res;
		}

		public string GetCustomMessage(Material targetMat)
		{
			string res = string.Empty;

			if(customMessages != null && customMessages.Length > 0)
			{	
				for(int i = 0; i < customMessages.Length; i++)
				{
					MessageByKeywords customMessage = customMessages[i];

					if (customMessage.IsMessageEnabled(targetMat))
					{
						res = customMessage.message;
						break;
					}
				}
			}

			return res;
		}

		public static bool IsEffectEnabled(AllIn13DEffectConfig effectConfig, AllIn13DShaderInspectorReferences references)
		{
			int selectedIndex = 0;
			return IsEffectEnabled(effectConfig, ref selectedIndex, references);
		}

		public static bool IsEffectEnabled(AllIn13DEffectConfig effectConfig, Material targetMat)
		{
			int selectedIndex = 0;
			return IsEffectEnabled(effectConfig, ref selectedIndex, targetMat);
		}

		public static bool IsEffectEnabled(AllIn13DEffectConfig effectConfig, ref int selectedIndex, AllIn13DShaderInspectorReferences references)
		{
			bool res = true;
			for (int i = 0; i < references.targetMats.Length; i++)
			{
				res = res && IsEffectEnabled(effectConfig, ref selectedIndex, references.targetMats[i]);
			}

			return res;
		}

		public static bool IsEffectEnabled(AllIn13DEffectConfig effectConfig, ref int selectedIndex, Material targetMat)
		{
			selectedIndex = 0;

			LocalKeyword[] enabledKeywords = targetMat.enabledKeywords;
			bool res = false;

			if (effectConfig.keywords.Count == 1)
			{
				for (int i = 0; i < enabledKeywords.Length; i++)
				{
					if (enabledKeywords[i].name == effectConfig.keywords[0].keyword)
					{
						res = true;
						selectedIndex = i;
						break;
					}
				}
			}
			else
			{
				for (int i = 0; i < effectConfig.keywords.Count; i++)
				{
					string keywordToCheck = effectConfig.keywords[i].keyword;
					for (int j = 0; j < enabledKeywords.Length; j++)
					{
						if (enabledKeywords[j].name == keywordToCheck && i > 0)
						{
							res = true;
							break;
						}
					}

					if (res)
					{
						selectedIndex = i;
						break;
					}
				}
			}

			return res;
		}


		public static void ResetProperty(MaterialProperty targetProperty, AllIn13DShaderInspectorReferences references, Material targetMat)
		{
			if(references.materialWithDefaultValues == null)
			{
				references.materialWithDefaultValues = new Material(targetMat.shader);
			}

			Shader shader = targetMat.shader;
			int propertyIndex = shader.FindPropertyIndex(targetProperty.name);
			if (targetProperty.propertyType == ShaderPropertyType.Float || targetProperty.propertyType == ShaderPropertyType.Range)
			{
				targetProperty.floatValue = references.materialWithDefaultValues.GetFloat(targetProperty.name);
			}
			else if (targetProperty.propertyType == ShaderPropertyType.Vector)
			{
				targetProperty.vectorValue = references.materialWithDefaultValues.GetVector(targetProperty.name);
			}
			else if (targetProperty.propertyType == ShaderPropertyType.Color)
			{
				targetProperty.colorValue = references.materialWithDefaultValues.GetColor(targetProperty.name);
			}
			else if (targetProperty.propertyType == ShaderPropertyType.Texture)
			{
				targetProperty.textureValue = references.materialWithDefaultValues.GetTexture(targetProperty.name);
			}
		}

		public static void EnableEffect(AllIn13DEffectConfig effectConfig, AllIn13DShaderInspectorReferences references, Material targetMat)
		{
			for (int i = 0; i < effectConfig.keywords.Count; i++)
			{
				string kw = effectConfig.keywords[i].keyword;
				targetMat.EnableKeyword(kw);
			}
		}

		public static void EnableEffectToggle(AllIn13DEffectConfig effectConfig, AllIn13DShaderInspectorReferences references, Material targetMat)
		{
			targetMat.EnableKeyword(effectConfig.keywords[0].keyword);
			references.matProperties[effectConfig.keywordPropertyIndex].floatValue = 1f;
		}

		public static void DisableEffectToggle(AllIn13DEffectConfig effectConfig, AllIn13DShaderInspectorReferences references, Material targetMat)
		{
			targetMat.DisableKeyword(effectConfig.keywords[0].keyword);
			references.matProperties[effectConfig.keywordPropertyIndex].floatValue = 0f;
		}

		public static void EnableEffectByIndex(AllIn13DEffectConfig effectConfig, int index, AllIn13DShaderInspectorReferences references, Material targetMat)
		{
			DisableEffect(effectConfig, references, targetMat);
			string kwToEnable = effectConfig.keywords[index].keyword;

			targetMat.EnableKeyword(kwToEnable);
		}

		public static void DisableEffect(AllIn13DEffectConfig effectConfig, AllIn13DShaderInspectorReferences references, Material targetMat)
		{
			for (int i = 0; i < effectConfig.keywords.Count; i++)
			{
				string kw = effectConfig.keywords[i].keyword;

				targetMat.DisableKeyword(kw);
			}
		}

		public string[] GetPropertyNames()
		{
			string[] res = new string[effectProperties.Count];

			for(int i = 0; i < effectProperties.Count; i++)
			{
				res[i] = $"{effectProperties[i].displayName} ({effectProperties[i].propertyName})";
 			}

			return res;
		}
	}
}
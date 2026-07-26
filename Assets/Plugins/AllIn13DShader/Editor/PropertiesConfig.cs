using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	[System.Serializable]
	public class PropertiesConfig
	{
		public Shader shader;

		public EffectGroup[] effectsGroups;
		public List<int> singleProperties;
		public List<int> advancedProperties;

		public int renderPreset; 
		public int blendSrcIdx;
		public int blendDstIdx;
		public int zWriteIndex;

		public void CreateConfig(EffectGroupGlobalConfigCollection effectGroupGlobalConfigCollection, EffectsExtraData effectsExtraData)
		{
			effectsGroups = new EffectGroup[0];
			singleProperties = new List<int>();
			advancedProperties = new List<int>();

			CreateGroups(effectGroupGlobalConfigCollection);

			int numProperties = shader.GetPropertyCount();

			for (int i = 0; i < numProperties; i++)
			{
				string[] attributes = shader.GetPropertyAttributes(i);
				string displayName	= shader.GetPropertyDescription(i);
				string propertyName = shader.GetPropertyName(i);

				if (propertyName == Constants.MATPROPERTY_RENDERING_MODE)
				{
					this.renderPreset = i;
				}
				else
				{
					if (attributes.Length > 0)
					{
						int idxOffset = 0;
						string firstAttribute = attributes[0];
						string secondAttribute = string.Empty;
						if(attributes.Length >= 2)
						{
							secondAttribute = attributes[1];
						}



						if (firstAttribute.Contains(Constants.EFFECT_ATTRIBUTE_PREFIX))
						{
							ConfigureEffect(displayName, i, propertyName, firstAttribute, attributes[idxOffset + 1], effectsExtraData);
						}
						else if (firstAttribute.StartsWith(Constants.EFFECT_PROPERTY_ATTRIBUTE_PREFIX))
						{
							EffectPropertyAttributeData effectPropertyAttributeData = GetEffectPropertyAttributeData(firstAttribute, secondAttribute);
							ConfigureProperty(effectPropertyAttributeData, i, propertyName, displayName);
						}
						else if (firstAttribute.Contains(Constants.SINGLE_PROPERTY_ATTRIBUTE))
						{
							singleProperties.Add(i);
						}
						else if (firstAttribute.Contains(Constants.ADVANCED_PROPERTY_ATTRIBUTE))
						{
							advancedProperties.Add(i);
						}
					}
				}
				if (propertyName == Constants.MATPROPERTY_BLEND_SRC)
				{
					this.blendSrcIdx = i;
				}
				else if (propertyName == Constants.MATPROPERTY_BLEND_DST)
				{
					this.blendDstIdx = i;
				}
			}

			zWriteIndex = FindPropertyIndex("_ZWrite");
		}

		private void CreateGroups(EffectGroupGlobalConfigCollection effectGroupGlobalConfigCollection)
		{
			for (int i = 0; i < effectGroupGlobalConfigCollection.effectGroupGlobalConfigs.Length; i++)
			{
				EffectGroupGlobalConfig effectGroupGlobalConfig = effectGroupGlobalConfigCollection.effectGroupGlobalConfigs[i];
				//string groupID = Constants.EFFECTS_GROUPS_IDS[i];

				EffectGroup group = new EffectGroup(effectGroupGlobalConfig);
				ArrayUtility.Add(ref effectsGroups, group);
			}
		}

		private EffectPropertyAttributeData GetEffectPropertyAttributeData(string attribute, string secondAttribute)
		{
			EffectPropertyAttributeData res = new EffectPropertyAttributeData();

			MatchCollection matchCollection = Regex.Matches(attribute, Constants.REGEX_EFFECT_PROOPERTY_COMPLETE);
			if (matchCollection != null && matchCollection.Count > 0)
			{
				res.parentEffectID = matchCollection[0].Groups[1].Value;
				string[] keywordListSplit = matchCollection[0].Groups[4].Value.Split(",");
				for(int i = 0; i < keywordListSplit.Length; i++)
				{
					string kw = keywordListSplit[i].Trim();
					res.AddKeyword(kw);
				}

				string[] incompatibleKwsSplit = matchCollection[0].Groups[3].Value.Split(",");
				for(int i = 0; i < incompatibleKwsSplit.Length; i++)
				{
					string incompatibleKw = incompatibleKwsSplit[i];
					res.AddIncompatibleKeyword(incompatibleKw);
				}

				string strAllowReset = matchCollection[0].Groups[5].Value.ToUpper();
				res.allowReset = strAllowReset == "TRUE";
			
				string strKeywordsOp = matchCollection[0].Groups[2].Value.ToUpper();
				if (!string.IsNullOrEmpty(strKeywordsOp))
				{
					res.keywordsOp = strKeywordsOp == "AND" ? KeywordsOp.AND : KeywordsOp.OR;
				}
			}
			else
			{
				matchCollection = Regex.Matches(attribute, Constants.REGEX_EFFECT_PROPERTY);
				
				string[] contentSplit = matchCollection[0].Groups[1].Value.Split(",");

				string parentEffectID = contentSplit[0].Trim();
				res.parentEffectID = parentEffectID;

				if (contentSplit.Length > 1)
				{
					for (int i = 1; i < contentSplit.Length; i++)
					{
						string kw = $"_{parentEffectID}_{contentSplit[i].Trim()}";
						res.AddKeyword(kw);
					}
				}
				else
				{
					string kw = $"_{parentEffectID}_ON";
					res.AddKeyword(kw);
				}
			}

			if (!string.IsNullOrEmpty(secondAttribute))
			{
				matchCollection = Regex.Matches(secondAttribute, Constants.REGEX_KEYWORDS_ENUM);
				if (matchCollection.Count >= 1)
				{
					string[] contentSplit = matchCollection[0].Groups[1].Value.Split(",");
					res.AddPropertyKeywords(contentSplit);
				}
			}

			return res;
		}

		private EffectKeywordData[] GetParentEffectKeywords(string attribute, string effectName)
		{
			MatchCollection matchCollection = Regex.Matches(attribute, Constants.REGEX_PARENT_EFFECT_KEYWORDS);
			string[] matchSplitted = matchCollection[0].Groups[1].Value.Split(",");
			EffectKeywordData[] res = new EffectKeywordData[matchSplitted.Length];
			for (int i = 0; i < matchSplitted.Length; i++)
			{
				matchSplitted[i] = matchSplitted[i].Trim();

				string displayName = matchSplitted[i];

				string keyword = string.Empty;
				if(matchSplitted.Length == 1)
				{
					keyword = $"{matchSplitted[i].ToUpper()}";
				}
				else
				{
					keyword = $"_{effectName}_{matchSplitted[i].ToUpper()}";
				}

				res[i] = new EffectKeywordData(keyword, displayName);
				//matchSplitted[i] = matchSplitted[i].ToUpper();
			}

			return res;
		}

		private EffectAttributeData GetEffectAttributeData(string rawAttribute)
		{
			EffectAttributeData res = new EffectAttributeData();

			Match match = Regex.Match(rawAttribute, Constants.REGEX_EFFECT);
			res.effectID = match.Groups[1].Value.Trim();
			res.groupID = match.Groups[2].Value.Trim();
			res.dependentEffectID = match.Groups[4].Value.Trim();
			res.incompatibleWithEffectID = match.Groups[5].Value.Trim();
			res.docEnabled = match.Groups[6].Value.Trim().ToUpper() == "TRUE";
			res.drawerID = match.Groups[7].Value.Trim();

			if (string.IsNullOrEmpty(res.drawerID))
			{
				res.drawerID = Constants.GENERAL_EFFECT_DRAWER_ID;
			}

			return res;
		}

		private void ConfigureEffect(string displayName, int propertyIndex, string propertyName, string rawEffectAttribute, string attributeKeywords,
			EffectsExtraData effectsExtraData)
		{
			EffectAttributeData effectAttributeData = GetEffectAttributeData(rawEffectAttribute);

			string effectName = effectAttributeData.effectID;

			AllIn13DEffectConfig effectConfig = FindEffectConfigByID(effectAttributeData.effectID);
			EffectKeywordData[] keywordsDatas = GetParentEffectKeywords(attributeKeywords, effectName);

			EffectConfigType effectConfigType = keywordsDatas.Length == 1 ? EffectConfigType.EFFECT_TOGGLE : EffectConfigType.EFFECT_ENUM;
			if (effectConfig == null)
			{
				effectConfig = new AllIn13DEffectConfig(displayName, propertyName, propertyIndex, effectConfigType, 
					effectAttributeData, effectsExtraData);

				effectConfig.AddKeywords(keywordsDatas);
				effectConfig.Setup();

				EffectGroup effectGroup = GetEffecGroupByID(effectAttributeData.groupID);
				effectGroup.AddEffect(effectConfig);
			}
		}

		private EffectGroup GetEffecGroupByID(string groupID)
		{
			EffectGroup res = null;

			for (int i = 0; i < effectsGroups.Length; i++)
			{
				if (effectsGroups[i].GroupID == groupID)
				{
					res = effectsGroups[i];
					break;
				}
			}

			return res;
		}

		//private void ConfigureProperty(string[] propertyContent, int propertyIndex, string propertyName)
		//{
		//	string effectID = propertyContent[0];
		//	List<string> keywords = new List<string>();

		//	if (propertyContent.Length > 1)
		//	{
		//		for (int i = 1; i < propertyContent.Length; i++)
		//		{
		//			string kw = $"_{effectID}_{propertyContent[i]}";
		//			keywords.Add(kw);
		//		}
		//	}
		//	else
		//	{
		//		string kw = $"_{effectID}_ON";
		//		keywords.Add(kw);
		//	}

		//	ConfigureProperty(effectID, keywords, propertyIndex, propertyName);
		//}

		private void ConfigureProperty(EffectPropertyAttributeData data, int propertyIndex, string propertyName, string displayName)
		{
			AllIn13DEffectConfig effectConfig = FindEffectConfigByID(data.parentEffectID);
			EffectProperty effectProperty = effectConfig.CreateEffectProperty(propertyIndex, propertyName, displayName, data);
		}

		public AllIn13DEffectConfig FindEffectConfigByID(string effectID)
		{
			AllIn13DEffectConfig res = null;

			for (int groupIdx = 0; groupIdx < effectsGroups.Length; groupIdx++)
			{
				EffectGroup effectGroup = effectsGroups[groupIdx];

				res = effectGroup.FindEffectByID(effectID);

				if (res != null)
				{
					break;
				}
			}

			return res;
		}

		public EffectProperty FindEffectProperty(string effectID, string propertyName)
		{
			AllIn13DEffectConfig effectConfig = FindEffectConfigByID(effectID);

			EffectProperty res = effectConfig.FindEffectPropertyByName(propertyName);
			return res;
		}

		public int FindPropertyIndex(string propertyName)
		{
			int res = shader.FindPropertyIndex(propertyName);
			return res;
		}

		//public EffectProperty FindGeneralEffectProperty(string propertyName)
		//{
		//	EffectProperty res = null;

		//	for(int i = 0; i < singleProperties.Count; i++)
		//	{
		//		break;
		//	}

		//	return res;
		//}

		public List<AllIn13DEffectConfig> GetAllEffects()
		{
			List<AllIn13DEffectConfig> res = new List<AllIn13DEffectConfig>();

			for (int groupIdx = 0; groupIdx < effectsGroups.Length; groupIdx++)
			{
				res.AddRange(effectsGroups[groupIdx].effects);
			}

			return res;
		}

		public EffectGroup FindEffectGroupByID(string groupID)
		{
			EffectGroup res = null;

			for(int i = 0; i < effectsGroups.Length; i++)
			{
				if (effectsGroups[i].GroupID == groupID)
				{
					res = effectsGroups[i];
					break;
				}
			}

			return res;
		}

		public string[] GetEffectGroupsIDs()
		{
			string[] res = new string[effectsGroups.Length];

			for(int i = 0; i < effectsGroups.Length; i++)
			{
				res[i] = effectsGroups[i].GroupID;
			}

			return res;
		}

		public string[] GetEffectGroupsDisplayNames()
		{
			string[] res = new string[effectsGroups.Length];

			for(int i = 0; i < effectsGroups.Length; i++)
			{
				res[i] = effectsGroups[i].DisplayName;
			}

			return res;
		}

		public string[] GetGlobalPropertyNames()
		{
			string[] res = new string[singleProperties.Count];

			for(int i = 0; i < singleProperties.Count; i++)
			{
				res[i] = shader.GetPropertyDescription(singleProperties[i]);
			}

			return res;
		}
	}
}
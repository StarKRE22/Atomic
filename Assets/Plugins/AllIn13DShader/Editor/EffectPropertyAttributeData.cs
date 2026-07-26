using System.Collections.Generic;

namespace AllIn13DShader
{
	public class EffectPropertyAttributeData
	{
		public string parentEffectID;
		public List<string> keywords;
		public List<string> incompatibleWithKws;
		public List<string> propertyKeywords;
		public bool allowReset;
		public KeywordsOp keywordsOp;

		public EffectPropertyAttributeData()
		{
			parentEffectID = string.Empty;
			keywords = new List<string>();
			incompatibleWithKws = new List<string>();
			propertyKeywords = new List<string>();
			allowReset = true;
			keywordsOp = KeywordsOp.OR;
		}

		public void AddKeyword(string keyword)
		{
			this.keywords.Add(keyword);
		}

		public void AddIncompatibleKeyword(string keyword)
		{
			this.incompatibleWithKws.Add(keyword);
		}

		public void AddPropertyKeyword(string propertyKeyword)
		{
			this.propertyKeywords.Add(propertyKeyword);
		}

		public void AddPropertyKeywords(string[] propertyKeywords)
		{
			for(int i = 0; i < propertyKeywords.Length; i++)
			{
				AddPropertyKeyword(propertyKeywords[i].Trim());
			}
		}
	}
}
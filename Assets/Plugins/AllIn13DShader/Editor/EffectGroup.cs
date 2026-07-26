using System.Collections.Generic;
using UnityEditor;

namespace AllIn13DShader
{
	[System.Serializable]
	public class EffectGroup
	{
		public EffectGroupGlobalConfig effectGroupConfig;
		public AllIn13DEffectConfig[] effects;

		public string GroupID
		{
			get
			{
				return effectGroupConfig.groupID;
			}
		}

		public string DisplayName
		{
			get
			{
				return effectGroupConfig.displayName;
			}
		}

		public EffectGroup(EffectGroupGlobalConfig effectGroupConfig)
		{
			this.effectGroupConfig = effectGroupConfig;
			this.effects = new AllIn13DEffectConfig[0];
		}

		public void AddEffect(AllIn13DEffectConfig effect)
		{
			ArrayUtility.Add(ref effects, effect);
		}

		public AllIn13DEffectConfig FindEffectByID(string effectID)
		{
			AllIn13DEffectConfig res = null;

			for (int i = 0; i < effects.Length; i++)
			{
				if (effects[i].effectName == effectID)
				{
					res = effects[i];
					break;
				}
			}

			return res;
		}

		public List<EffectProperty> GetEffectPropertyFlatList()
		{
			List<EffectProperty> res = new List<EffectProperty>();

			for(int i = 0; i < effects.Length; i++)
			{
				res.AddRange(effects[i].effectProperties);
			}

			return res;
		}

		public int FindEffectIndexByID(string effectID)
		{
			int res = -1;

			for(int i = 0; i < effects.Length; i++)
			{
				if (effects[i].effectName == effectID)
				{
					res = i;
					break;
				}
			}

			return res;
		}

		public string[] GetEffectsNames()
		{
			string[] res = new string[effects.Length];

			for(int i = 0; i < effects.Length; i++)
			{
				res[i] = effects[i].displayName;
			}

			return res;
		}
	}
}
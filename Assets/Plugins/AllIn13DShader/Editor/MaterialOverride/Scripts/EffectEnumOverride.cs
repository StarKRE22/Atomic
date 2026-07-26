using UnityEngine;

namespace AllIn13DShader
{
	public class EffectEnumOverride : AbstractEffectOverride
	{
		public string[] enumOptions;

		public int index;

		public EffectEnumOverride(AllIn13DEffectConfig effectConfig) : base(effectConfig)
		{
			enumOptions = new string[effectConfig.keywords.Count];
			for(int i = 0; i < enumOptions.Length; i++)
			{
				enumOptions[i] = effectConfig.keywords[i].displayName;
			}
		}

		protected override void ApplyMainPropertyChanges(Material mat)
		{
			for(int i = 0; i < keywords.Count; i++)
			{
				mat.DisableKeyword(keywords[i].keyword);
			}

			mat.EnableKeyword(keywords[index].keyword);
			mat.SetFloat(propertyName, (float)index);
		}
	}
}
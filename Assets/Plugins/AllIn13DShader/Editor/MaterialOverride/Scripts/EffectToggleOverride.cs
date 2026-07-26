using UnityEngine;

namespace AllIn13DShader
{
	public class EffectToggleOverride : AbstractEffectOverride
	{
		public bool boolValue;

		public EffectToggleOverride(AllIn13DEffectConfig effectConfig) : base(effectConfig)
		{

		}

		protected override void ApplyMainPropertyChanges(Material mat)
		{
			if (boolValue)
			{
				mat.EnableKeyword(keywords[0].keyword);
				mat.SetFloat(propertyName, 1f);
			}
			else
			{
				mat.DisableKeyword(keywords[0].keyword);
				mat.SetFloat(propertyName, 0f);
			}
		}
	}
}
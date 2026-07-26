using System.Collections.Generic;
using UnityEngine;

namespace AllIn13DShader
{
	public abstract class AbstractEffectOverride
	{
		public List<PropertyOverride> propertyOverrides;

		public string displayName;
		public string propertyName;

		public bool overrideEnabled;

		public List<EffectKeywordData> keywords;

		public AbstractEffectOverride(AllIn13DEffectConfig effectConfig)
		{
			this.displayName = effectConfig.displayName;
			this.propertyName = effectConfig.keywordPropertyName;
			this.keywords = new List<EffectKeywordData>(effectConfig.keywords);

			this.propertyOverrides = new List<PropertyOverride>();
		}

		public void AddPropertyOverride(EffectProperty effectProperty, Shader shader)
		{
			PropertyOverride propertyOverride = new PropertyOverride(this, effectProperty, shader);

			AddPropertyOverride(propertyOverride);
		}

		public void AddPropertyOverride(int propertyIndex, Shader shader)
		{
			PropertyOverride propertyOverride = new PropertyOverride(this, propertyIndex, shader);

			AddPropertyOverride(propertyOverride);
		}

		private void AddPropertyOverride(PropertyOverride propertyOverride)
		{
			if (!propertyOverrides.Contains(propertyOverride))
			{
				propertyOverrides.Add(propertyOverride);
			}
		}

		public virtual void ApplyChangesToMaterial(Material mat)
		{
			if (overrideEnabled)
			{
				ApplyMainPropertyChanges(mat);
			}

			for(int i = 0; i < propertyOverrides.Count; i++)
			{
				propertyOverrides[i].ApplyChangesToMaterial(mat);
			}
		}

		protected abstract void ApplyMainPropertyChanges(Material mat);

		public bool RemovePropertyOverride(PropertyOverride propertyOverrideToRemove)
		{
			bool res = propertyOverrides.Remove(propertyOverrideToRemove);
			return res;
		}

		public override bool Equals(object obj)
		{
			bool res = false;

			if(obj is AbstractEffectOverride)
			{
				AbstractEffectOverride abstractEffectOverride = (AbstractEffectOverride)obj;
				res = propertyName == abstractEffectOverride.propertyName;
			}

			return res;
		}

		public override int GetHashCode()
		{
			return propertyName.GetHashCode();
		}
	}
}
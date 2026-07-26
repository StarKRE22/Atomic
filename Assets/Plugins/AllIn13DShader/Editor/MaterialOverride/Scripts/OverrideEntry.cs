using UnityEngine;

namespace AllIn13DShader
{
	public abstract class OverrideEntry
	{
		public enum OverrideCategory
		{
			KEYWORD_TOGGLE,
			KEYWORD_ENUM,
			COMMON,
			GLOBAL_PROPERTY,
		}

		public OverrideCategory overrideCategory;


		public OverrideEntry(OverrideCategory overrideCategory)
		{
			this.overrideCategory = overrideCategory;
		}

		public abstract void ApplyChangesToMaterial(Material mat);

		public override bool Equals(object obj)
		{
			bool res = false;

			if (obj is OverrideEntry)
			{
				OverrideEntry overrideEntry = (OverrideEntry)obj;
				res = overrideCategory == overrideEntry.overrideCategory;
			}

			return res;
		}

		public override int GetHashCode()
		{
			return overrideCategory.GetHashCode();
		}
	}
}
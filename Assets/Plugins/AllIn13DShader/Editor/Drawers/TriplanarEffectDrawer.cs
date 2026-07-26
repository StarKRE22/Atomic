using UnityEngine;

namespace AllIn13DShader
{
	public class TriplanarEffectDrawer : AbstractEffectDrawer
	{
		private int mainTexPropertyIndex;
		private EffectProperty mainNormalMapProperty;
		
		public TriplanarEffectDrawer(EffectProperty mainNormalMapProperty, AllIn13DShaderInspectorReferences references, PropertiesConfig propertiesConfig) : base(references, propertiesConfig)
		{
			this.drawerID = Constants.TRIPLANAR_EFFECT_DRAWER_ID;
			this.mainNormalMapProperty = mainNormalMapProperty;

			mainTexPropertyIndex = FindPropertyIndex("_MainTex");
		}

		protected override void DrawProperties()
		{
			EffectPropertyDrawer.DrawProperty(references.matProperties[mainTexPropertyIndex], false, references);
			
			DrawProperty(effectConfig.effectProperties[0]);
			DrawProperty(effectConfig.effectProperties[1]);

			if (IsEffectPropertyVisible(mainNormalMapProperty, references.targetMats))
			{
				EditorUtils.DrawLine(Color.grey, 1, 3);
				DrawProperty(mainNormalMapProperty, false);
				DrawProperty(effectConfig.effectProperties[2]);
			}

			EditorUtils.DrawLine(Color.grey, 1, 3);
			DrawProperty(effectConfig.effectProperties[3]);
			DrawProperty(effectConfig.effectProperties[4]);
		}
	}
}
using UnityEditor;

namespace AllIn13DShader
{
	public class ColorRampEffectDrawer : AbstractEffectDrawer
	{
		public ColorRampEffectDrawer(AllIn13DShaderInspectorReferences references, PropertiesConfig propertiesConfig) : base(references, propertiesConfig)
		{
			this.drawerID = Constants.COLOR_RAMP_EFFECT_DRAWER_ID;
		}

		protected override void DrawProperties()
		{
			for(int i = 0; i < effectConfig.effectProperties.Count; i++)
			{
				DrawProperty(effectConfig.effectProperties[i]);
				if (i == 2)
				{
					EditorGUILayout.LabelField("*Set the Color Ramp Texture to Repeat Wrap Mode if using Tiling and/or Scroll Speed", references.smallLabelStyle);
				}
			}
		}
	}
}
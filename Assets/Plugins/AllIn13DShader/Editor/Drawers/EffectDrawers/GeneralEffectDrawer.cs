using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class GeneralEffectDrawer : AbstractEffectDrawer
	{
		public GeneralEffectDrawer(AllIn13DShaderInspectorReferences references, PropertiesConfig propertiesConfig) : base(references, propertiesConfig)
		{
			this.drawerID = Constants.GENERAL_EFFECT_DRAWER_ID;

			
		}
	}
}
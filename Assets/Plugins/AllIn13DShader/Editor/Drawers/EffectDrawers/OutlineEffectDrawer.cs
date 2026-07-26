using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class OutlineEffectDrawer : AbstractEffectDrawer
	{
		private MaterialProperty stencilRefMatProperty;

		public OutlineEffectDrawer(AllIn13DShaderInspectorReferences references, PropertiesConfig propertiesConfig) : base(references, propertiesConfig)
		{
			this.drawerID = Constants.OUTLINE_DRAWER_ID;

			stencilRefMatProperty = FindPropertyByName("_StencilRef");
		}

		protected override void DrawProperties()
		{
			base.DrawProperties();

			if (stencilRefMatProperty != null)
			{
				EffectPropertyDrawer.DrawProperty(stencilRefMatProperty, references);
			}
		}
	}
}
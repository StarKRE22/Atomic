using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class AllIn13DShaderGradientDrawer : MaterialPropertyDrawer
	{
		private GradientEditorDrawer gradientEditorDrawer;

		private void RefreshReferences(MaterialProperty prop)
		{
			if(gradientEditorDrawer == null)
			{
				gradientEditorDrawer = new GradientEditorDrawer();
			}
		}

		public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
		{
			RefreshReferences(prop);

			Texture texValue = prop.textureValue;

			EditorGUI.BeginChangeCheck();
			Texture newTex = gradientEditorDrawer.Draw(position, texValue);
			if (EditorGUI.EndChangeCheck())
			{
				prop.textureValue = newTex;
			}
		}

		public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
		{
			float res = 0f;
			if(gradientEditorDrawer != null)
			{
				res = gradientEditorDrawer.GetPropertyHeight();
			}

			return res;
		}
	}
}
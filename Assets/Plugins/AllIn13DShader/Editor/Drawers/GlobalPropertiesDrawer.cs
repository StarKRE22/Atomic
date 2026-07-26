using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class GlobalPropertiesDrawer
	{
		private List<int> globalPropertiesIndices;
		private AllIn13DShaderInspectorReferences references;

		public GlobalPropertiesDrawer()
		{
		}

		public void Draw(List<int> globalPropertiesIndices, AllIn13DShaderInspectorReferences references)
		{
			this.globalPropertiesIndices = globalPropertiesIndices;
			this.references = references;

			GUILayout.Label("Global Properties", references.bigLabelStyle);

			EditorGUILayout.BeginVertical(references.propertiesStyle);
			for (int i = 0; i < globalPropertiesIndices.Count; i++)
			{
				MaterialProperty matProperty = references.matProperties[globalPropertiesIndices[i]];
				EffectPropertyDrawer.DrawProperty(matProperty, references);
			}
			EditorGUILayout.EndVertical();
		}
	}
}
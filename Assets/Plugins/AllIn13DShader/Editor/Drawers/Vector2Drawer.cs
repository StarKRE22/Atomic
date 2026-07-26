using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class Vector2Drawer : MaterialPropertyDrawer
	{
		public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
		{
			if (prop.propertyType != UnityEngine.Rendering.ShaderPropertyType.Vector)
			{
				EditorGUI.LabelField(position, label, "Vector3Drawer only works with Vector properties.");
				return;
			}

			EditorGUI.BeginChangeCheck();

			// Get current vector4 value
			Vector4 vec4Value = prop.vectorValue;

			// Convert to Vector2 for editing
			Vector2 vec2Value = new Vector2(vec4Value.x, vec4Value.y);

			// Create property field for Vector3
			vec2Value = EditorGUI.Vector2Field(position, label, vec2Value);

			if (EditorGUI.EndChangeCheck())
			{
				// Convert back to Vector4, preserving the w component
				prop.vectorValue = new Vector4(vec2Value.x, vec2Value.y, vec4Value.z, vec4Value.w);
			}
		}

		public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
		{
			return EditorGUIUtility.singleLineHeight;
		}
	}
}
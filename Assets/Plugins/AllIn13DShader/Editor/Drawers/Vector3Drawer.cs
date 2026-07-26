using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
    public class Vector3Drawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            if(prop.propertyType != UnityEngine.Rendering.ShaderPropertyType.Vector) {
                EditorGUI.LabelField(position, label, "Vector3Drawer only works with Vector properties.");
                return;
            }

            EditorGUI.BeginChangeCheck();
        
            // Get current vector4 value
            Vector4 vec4Value = prop.vectorValue;
        
            // Convert to Vector3 for editing
            Vector3 vec3Value = new Vector3(vec4Value.x, vec4Value.y, vec4Value.z);
        
            // Create property field for Vector3
            vec3Value = EditorGUI.Vector3Field(position, label, vec3Value);
        
            if(EditorGUI.EndChangeCheck()) {
                // Convert back to Vector4, preserving the w component
                prop.vectorValue = new Vector4(vec3Value.x, vec3Value.y, vec3Value.z, vec4Value.w);
            }
        }
    
        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using UnityEditor;
using UnityEngine;

namespace StylizedWater3
{
    public class WaveProfileDrawer : MaterialPropertyDrawer
    {
        private const float EDIT_BTN_WIDTH = 50f;
        
        public override void OnGUI (Rect position, MaterialProperty prop, String label, MaterialEditor editor)
        {
            MaterialEditor.BeginProperty(prop);

            // Setup
            WaveProfile profile = WaveProfileEditor.LoadFromLUT(prop.textureValue);

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;

            Rect labelRect = position;
            labelRect.width = EditorGUIUtility.labelWidth + 37;
            EditorGUI.LabelField(labelRect, label);
            
            Rect fieldRect = position;
            fieldRect.x = labelRect.width;
            fieldRect.width = (EditorGUIUtility.fieldWidth * 4f) - EDIT_BTN_WIDTH;
            profile = (WaveProfile)EditorGUI.ObjectField(fieldRect, profile, typeof(WaveProfile), false);

            Rect editBtnRect = position;
            editBtnRect.x = fieldRect.x + fieldRect.width;
            editBtnRect.width = EDIT_BTN_WIDTH;
            
            if (GUI.Button(editBtnRect, "Edit"))
            {
                Selection.activeObject = WaveProfileEditor.LoadFromLUT(prop.textureValue);
            }
            
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.textureValue = profile.shaderParametersLUT;
            }
            
            MaterialEditor.EndProperty();
        }
    }
}
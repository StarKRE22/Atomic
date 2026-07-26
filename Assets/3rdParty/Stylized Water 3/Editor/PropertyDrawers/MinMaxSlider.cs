// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using UnityEditor;
using UnityEngine;

namespace StylizedWater3
{
    //Use as "[MinMaxSlider(0, 5)]" on a material property. Proper should be a Vector type
    public class MinMaxSliderDrawer : MaterialPropertyDrawer
    {
        private readonly float min;
        private readonly float max;
        
        private Rect minFieldRect;
        private Rect sliderFieldRect;
        private Rect maxFieldRect;

        public MinMaxSliderDrawer(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
        
        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            MaterialEditor.BeginProperty(prop);

            float minVal = prop.vectorValue.x;
            float maxVal = prop.vectorValue.y;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            
            Rect labelRect = position;
            labelRect.width = EditorGUIUtility.labelWidth;
            EditorGUI.LabelField(labelRect, label);

            minFieldRect = labelRect;
            minFieldRect.x = labelRect.x + labelRect.width;
            minFieldRect.width = EditorGUIUtility.fieldWidth;
            EditorGUI.FloatField(minFieldRect, minVal);

            sliderFieldRect = labelRect;
            sliderFieldRect.x = minFieldRect.x + minFieldRect.width + 5f;
            sliderFieldRect.width = position.width - (EditorGUIUtility.fieldWidth * 2f) - labelRect.width - 10f;
            
            EditorGUI.MinMaxSlider(sliderFieldRect, ref minVal, ref maxVal, min, max);
            
            maxFieldRect = labelRect;
            maxFieldRect.x = sliderFieldRect.x + sliderFieldRect.width + 5f;
            maxFieldRect.width = EditorGUIUtility.fieldWidth;
            EditorGUI.FloatField(maxFieldRect, maxVal);
            
            if (EditorGUI.EndChangeCheck())
            {
                prop.vectorValue = new Vector4(minVal, maxVal);
            }
            EditorGUI.showMixedValue = false;
            
            MaterialEditor.EndProperty();
        }
    }
}
// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace StylizedWater3
{
    [CustomPropertyDrawer(typeof(HeightQuerySystem.Interface))]
    public class HeightInterfaceDrawer : PropertyDrawer
    {
        private bool waveProfileMismatch;
        private bool renderFeatureSetup;

        private bool enabled;

        private void OnEnable()
        {
            enabled = true;

            CheckRenderFeature();
        }

        private void CheckRenderFeature()
        {
            renderFeatureSetup = StylizedWaterEditor.IsRenderFeatureSetup();
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(!enabled) OnEnable();
            
            GUILayout.Space(-GetPropertyHeight(property, label));
            
            EditorGUILayout.LabelField("Water Height Interface", EditorStyles.boldLabel);
            
            var methodProperty = property.FindPropertyRelative("method");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(methodProperty);
            if (EditorGUI.EndChangeCheck())
            {
                CheckRenderFeature();
            }
            
            if (methodProperty.intValue == (int)HeightQuerySystem.Interface.Method.CPU)
            {
                EditorGUILayout.Separator();
                
                EditorGUI.BeginChangeCheck();
                
                EditorGUI.indentLevel++;
                
                var waterObject = property.FindPropertyRelative("waterObject");
                var autoFind = property.FindPropertyRelative("autoFind");
                using (new EditorGUI.DisabledScope(autoFind.boolValue))
                {
                    EditorGUILayout.PropertyField(waterObject);
                }
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(autoFind);
                EditorGUI.indentLevel--;

                var waveProfile = property.FindPropertyRelative("waveProfile");
                EditorGUILayout.PropertyField(waveProfile);
                if (waveProfile.objectReferenceValue == null)
                {
                    if (waterObject.objectReferenceValue)
                    {
                        UI.DrawNotification(true, "A wave profile must assigned", "Try get", () =>
                        {
                            waveProfile.objectReferenceValue = WaveProfileEditor.LoadFromWaterObject(waterObject.objectReferenceValue as WaterObject);
                        }, MessageType.Error);
                    }
                    else
                    {
                        UI.DrawNotification("A wave profile must assigned", MessageType.Error);
                    }
                }
                else
                {
                    if (waveProfileMismatch)
                    {
                        UI.DrawNotification(true, "The wave profile does not match the one used on the water material." +
                                                  "\n\nWave animations will likely appear out of sync", "Attempt fix",() =>
                        {
                            WaterObject obj = (WaterObject)waterObject.objectReferenceValue;
                            waveProfile.objectReferenceValue = WaveProfileEditor.LoadFromMaterial(obj.material);
                        }, MessageType.Warning);
                    }
                }
                var waterLevelSource = property.FindPropertyRelative("waterLevelSource");
                EditorGUILayout.PropertyField(waterLevelSource);

                if (waterLevelSource.intValue == (int)HeightQuerySystem.Interface.WaterLevelSource.FixedValue)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("waterLevel"));
                    EditorGUI.indentLevel--;
                }
                
                EditorGUI.indentLevel--;

                if (EditorGUI.EndChangeCheck())
                {
                    waveProfileMismatch = false;

                    if (waveProfile.objectReferenceValue && waterObject.objectReferenceValue)
                    {
                        WaveProfile profile = (WaveProfile)waveProfile.objectReferenceValue;
                        WaterObject obj = (WaterObject)waterObject.objectReferenceValue;

                        WaveProfile materialProfile = WaveProfileEditor.LoadFromMaterial(obj.material);
                        waveProfileMismatch = materialProfile != profile;
                    }
                }
            }
            else
            {
                UI.DrawRenderFeatureSetupError(ref renderFeatureSetup);
                
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("");

                    if (GUILayout.Button(new GUIContent(" Inspect Queries", EditorGUIUtility.FindTexture("_Help"))))
                    {
                        HeightQuerySystemEditor.HeightQueryInspector.Open();
                    }
                }
            }
        }

        /*
        private PropertyField waterObjectField;
        private PropertyField waveProfileField;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();

            Label headerLabel = new Label("Height interface");
            container.Add(headerLabel);

            var methodProperty = property.FindPropertyRelative("method");
            // Create property fields.
            var methodField = new PropertyField(methodProperty);
            methodField.RegisterValueChangeCallback(OnMethodChange);

            waterObjectField = new PropertyField(property.FindPropertyRelative("waterObject"));
            var autoFindProperty = property.FindPropertyRelative("autoFind");
            var autoFind = new PropertyField(autoFindProperty, "Auto find");
            
            waveProfileField = new PropertyField(property.FindPropertyRelative("waveProfile"));
            
            autoFind.RegisterValueChangeCallback(OnAutoFindChange);
            
            // Add fields to the container.
            container.Add(methodField);

            container.Add(waterObjectField);
            container.Add(autoFind);
            
            container.Add(waveProfileField);
            container.Add(new PropertyField(property.FindPropertyRelative("waterLevelSource")));
            container.Add(new PropertyField(property.FindPropertyRelative("waterLevel")));
            
            return container;
        }

        private void OnMethodChange(SerializedPropertyChangeEvent evt)
        {
            HeightQuerySystem.Interface.Method method = (HeightQuerySystem.Interface.Method)evt.changedProperty.intValue;

            waterObjectField.visible = method == HeightQuerySystem.Interface.Method.CPU;
            waveProfileField.visible = method == HeightQuerySystem.Interface.Method.CPU;
        }

        private void OnAutoFindChange(SerializedPropertyChangeEvent evt)
        {
            waterObjectField.SetEnabled(!evt.changedProperty.boolValue);
        }
        */
    }
}
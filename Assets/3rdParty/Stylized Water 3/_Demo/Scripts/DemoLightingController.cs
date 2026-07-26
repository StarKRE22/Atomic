// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StylizedWater3.Demo
{
    [ExecuteAlways]
    public class DemoLightingController : MonoBehaviour
    {
        [Serializable]
        public class Preset
        {
            public Material skybox;

            [Header("Lighting")]
    
            [Range(0f, 90f)]
            public float sunAngle = 45;
            [Range(0f, 360f)]
            public float sunRotation = 0f;

            public float intensity = 1f;
            public Color tint = Color.white;
    
            [Header("Fog")]
            public Color fogColor = Color.white;
            [Range(0.0001f, 0.01f)]
            public float fogDensity = 0.002f;
        }

        [Min(0)]
        public int activeIndex = 0;
        public Preset[] presets = Array.Empty<Preset>();

        public ReflectionProbe reflectionProbe;
        
        [NonSerialized]
        private Material m_skybox;
        private Light sun;
        
        private void OnEnable()
        {
            ApplyPreset(activeIndex);
        }

        private void OnDisable()
        {
            RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
        }

        public void ApplyPreset(int index = -1)
        {
            if (index < 0) index = activeIndex;
            
            if (this.gameObject.activeInHierarchy == false) return;
            if (index > presets.Length) return;

            Preset preset = presets[index];

            Light[] lights = FindObjectsByType<Light>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i].type == LightType.Directional) sun = lights[i];
            }

            if (m_skybox == null || preset.skybox.mainTexture != RenderSettings.skybox.mainTexture)
            {
                CreateSkyboxMat(preset.skybox);
            }
            
            sun.intensity = preset.intensity;
            sun.color = preset.tint;

        
            m_skybox.CopyPropertiesFromMaterial(preset.skybox);
            m_skybox.mainTexture = preset.skybox.mainTexture;
            
            sun.transform.eulerAngles = new Vector3(preset.sunAngle, preset.sunRotation, 0f);
            m_skybox.SetFloat("_Rotation", -sun.transform.eulerAngles.y);
            
            RenderSettings.skybox = m_skybox;
            RenderSettings.fogColor = preset.fogColor;
            RenderSettings.fogDensity = preset.fogDensity;

            if (reflectionProbe)
            {
                reflectionProbe.RenderProbe();
                //RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
                //RenderSettings.customReflectionTexture = reflectionProbe.texture;
            }
            else
            {
                //RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
            }
        }
        
        private void CreateSkyboxMat(Material source)
        {
            m_skybox = new Material(source);
            m_skybox.name = "Temp skybox";
        }
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(DemoLightingController))]
    public class DemoLightingControllerEditor : Editor
    {
        private DemoLightingController component;
        private SerializedProperty presets;

        private SerializedProperty reflectionProbe;
        
        private string proSkinPrefix => EditorGUIUtility.isProSkin ? "d_" : "";
        
        private void OnEnable()
        {
            component = (DemoLightingController)target;
            presets = serializedObject.FindProperty("presets");
            reflectionProbe = serializedObject.FindProperty("reflectionProbe");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(reflectionProbe);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Presets", EditorStyles.boldLabel);
            
            for (int i = 0; i < presets.arraySize; i++)
            {
                if (GUILayout.Button("Set Active"))
                {
                    component.activeIndex = i;
                    component.ApplyPreset(i);
                }

                using (new EditorGUI.DisabledGroupScope(component.activeIndex != i))
                {
                    EditorGUI.BeginChangeCheck();
                    
                    using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        using (new EditorGUILayout.VerticalScope())
                        {
                            GUILayout.Space(5f);

                            SerializedProperty param = presets.GetArrayElementAtIndex(i);
                            
                            EditorGUILayout.PropertyField(param);

                            GUILayout.Space(5f);
                        }

                        if (GUILayout.Button(new GUIContent("", EditorGUIUtility.IconContent(proSkinPrefix + "TreeEditor.Trash").image, "Remove parameter"), EditorStyles.miniButton, GUILayout.Width(30f))) presets.DeleteArrayElementAtIndex(i);

                    }
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (component.activeIndex == i)
                        {
                            component.ApplyPreset(i);
                        }
                    }
                }
                
                GUILayout.Space(3f);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(new GUIContent(" Add", EditorGUIUtility.IconContent(proSkinPrefix + "Toolbar Plus").image, "Insert new parameter"), EditorStyles.miniButton, GUILayout.Width(60f)))
                {
                    presets.InsertArrayElementAtIndex(presets.arraySize);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
    #endif
    
}
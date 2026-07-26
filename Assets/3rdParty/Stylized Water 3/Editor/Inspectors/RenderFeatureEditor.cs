// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

#if URP
using System;
using UnityEditor;
using UnityEngine;

namespace StylizedWater3
{
    [CustomEditor(typeof(StylizedWaterRenderFeature))]
    public partial class RenderFeatureEditor : Editor
    {
        private SerializedProperty screenSpaceReflectionSettings;
        
        private SerializedProperty allowDirectionalCaustics;
        
        private SerializedProperty heightPrePassSettings;
        private SerializedProperty terrainHeightPrePassSettings;

        private void OnEnable()
        {
            screenSpaceReflectionSettings = serializedObject.FindProperty("screenSpaceReflectionSettings");
            
            allowDirectionalCaustics = serializedObject.FindProperty("allowDirectionalCaustics");
            
            heightPrePassSettings = serializedObject.FindProperty("heightPrePassSettings");
            #if SWS_DEV
            terrainHeightPrePassSettings = serializedObject.FindProperty("terrainHeightPrePassSettings");
            #endif

            EnableFlowMapEditor();
            DynamicEffectsOnEnable();
        }
        
        public override void OnInspectorGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField($"Version {AssetInfo.INSTALLED_VERSION}", EditorStyles.miniLabel);

                if (GUILayout.Button(new GUIContent(" Documentation", EditorGUIUtility.FindTexture("_Help"))))
                {
                    Application.OpenURL(AssetInfo.DOC_URL);
                }
                if (GUILayout.Button(new GUIContent(" Debugger", EditorGUIUtility.IconContent("Profiler.Rendering").image, "Inspect the render buffer outputs")))
                {
                    RenderTargetDebuggerWindow.Open();
                }
                
            }
            EditorGUILayout.Space();
            
            UI.DrawNotification(PipelineUtilities.RenderGraphEnabled() == false, "Render Graph is disabled, functionality on this render feature will not be functional", "Enable", () =>
            {
                UnityEngine.Rendering.Universal.RenderGraphSettings settings = UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<UnityEngine.Rendering.Universal.RenderGraphSettings>();
                settings.enableRenderCompatibilityMode = false;
            }, MessageType.Error);

            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(allowDirectionalCaustics);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(screenSpaceReflectionSettings);
            if(screenSpaceReflectionSettings.isExpanded) EditorGUILayout.HelpBox("This feature is available for preview, no configurable settings are available yet", MessageType.Info);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(heightPrePassSettings);
            if (heightPrePassSettings.isExpanded)
            {
                HeightPrePass.Settings settings = ((StylizedWaterRenderFeature)target).heightPrePassSettings;

                string statsText = "";
                
                statsText += $"Current resolution: {PlanarProjection.CalculateResolution(settings.range, settings.cellsPerUnit, 16, settings.maxResolution)}px\n";

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(EditorGUIUtility.labelWidth);
                    EditorGUILayout.HelpBox(statsText, MessageType.None);
                }
                
                EditorGUILayout.HelpBox("This will pre-render all the water geometry's height (including any displacement effects) into a buffer. Allowing other shaders to access this information." +
                                        "\n\nSee the Height.hlsl shader library for the API, or use the \"Sample Water Height\" Sub-graph in Shader Graph." +
                                        "\n\nThis is for advanced users, and the \"GPU Async Readback\" buoyancy API makes use of this", MessageType.Info);
                if (HeightQuerySystem.RequiresHeightPrepass)
                {
                    EditorGUILayout.HelpBox("Height Pre-pass is forcible enabled at the moment, because there are height queries being issued from script.", MessageType.Info);
                    
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
            
            #if SWS_DEV
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(terrainHeightPrePassSettings);
            #endif
            EditorGUILayout.Space();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
            
            DrawFlowMapEditor();
            DynamicEffectsOnInspectorGUI();
            
            UI.DrawFooter();
        }

        partial void EnableFlowMapEditor();
        partial void DrawFlowMapEditor();
        
        partial void DynamicEffectsOnEnable();
        partial void DynamicEffectsOnInspectorGUI();
    }
}
#endif
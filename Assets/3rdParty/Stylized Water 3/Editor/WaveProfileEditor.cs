// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace StylizedWater3
{
    [CustomEditor(typeof(WaveProfile))]
    public class WaveProfileEditor: Editor
    {
        private SerializedProperty amplitudeMultiplier;
        private SerializedProperty waveLengthMultiplier;
        private SerializedProperty steepnessMultiplier;
        
        private SerializedProperty waveLengthCurve;
        private SerializedProperty amplitudeCurve;
        private SerializedProperty steepnessCurve;
        private SerializedProperty steepnessClamping;
        
        private SerializedProperty layers;
        
        public void OnEnable()
        {
            amplitudeMultiplier = serializedObject.FindProperty("amplitudeMultiplier");
            waveLengthMultiplier = serializedObject.FindProperty("waveLengthMultiplier");
            steepnessMultiplier = serializedObject.FindProperty("steepnessMultiplier");
            
            waveLengthCurve = serializedObject.FindProperty("waveLengthCurve");
            amplitudeCurve = serializedObject.FindProperty("amplitudeCurve");
            steepnessCurve = serializedObject.FindProperty("steepnessCurve");
            steepnessClamping = serializedObject.FindProperty("steepnessClamping");

            layers = serializedObject.FindProperty("layers");
        }
        
        private string iconPrefix => EditorGUIUtility.isProSkin ? "d_" : "";
        private bool expandLayers = true;

        public override void OnInspectorGUI()
        {
            UI.DrawHeader();
            
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            WaveProfile instance = (WaveProfile)target;

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();         
                UI.ExpandTooltips = GUILayout.Toggle(UI.ExpandTooltips, new GUIContent(" Toggle tooltips", EditorGUIUtility.IconContent(UI.iconPrefix + (UI.ExpandTooltips ? "animationvisibilitytoggleon" : "animationvisibilitytoggleoff")).image), "Button");
            }
            
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Multipliers", EditorStyles.boldLabel);
            using (new EditorGUILayout.HorizontalScope())
            {
                UI.PropertyField(waveLengthMultiplier);
                if (GUILayout.Button("Apply", EditorStyles.miniButton))
                {
                    for (int i = 0; i < layers.arraySize; i++)
                    {
                        SerializedProperty layer = layers.GetArrayElementAtIndex(i);
                        SerializedProperty waveLength = layer.FindPropertyRelative("waveLength");

                        waveLength.floatValue *= waveLengthMultiplier.floatValue;
                    }
                    waveLengthMultiplier.floatValue = 1f;
                }
            }
            
            using (new EditorGUILayout.HorizontalScope())
            {
                UI.PropertyField(amplitudeMultiplier);
                if (GUILayout.Button("Apply", EditorStyles.miniButton))
                {
                    for (int i = 0; i < layers.arraySize; i++)
                    {
                        SerializedProperty layer = layers.GetArrayElementAtIndex(i);
                        SerializedProperty amp = layer.FindPropertyRelative("amplitude");

                        amp.floatValue *= amplitudeMultiplier.floatValue;
                    }
                    amplitudeMultiplier.floatValue = 1f;
                }
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                UI.PropertyField(steepnessMultiplier);
                if (GUILayout.Button("Apply", EditorStyles.miniButton))
                {
                    for (int i = 0; i < layers.arraySize; i++)
                    {
                        SerializedProperty layer = layers.GetArrayElementAtIndex(i);
                        SerializedProperty steepness = layer.FindPropertyRelative("steepness");

                        steepness.floatValue *= steepnessMultiplier.floatValue;
                    }
                    steepnessMultiplier.floatValue = 1f;
                }
            }

            UI.PropertyField(waveLengthCurve);
            UI.PropertyField(amplitudeCurve);
            UI.PropertyField(steepnessCurve);
            
            EditorGUILayout.Space();
            UI.PropertyField(steepnessClamping);

            EditorGUILayout.Space();

            if (GUILayout.Button("Open procedural editor"))
            {
                WizardWindow window = EditorWindow.CreateWindow<WizardWindow>("Wave creation wizard");
                window.target = instance;
                window.Show();
            }
            
            EditorGUILayout.Space();
            
            expandLayers = EditorGUILayout.BeginFoldoutHeaderGroup(expandLayers, $"Layers ({layers.arraySize})");

            if (expandLayers)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(EditorGUIUtility.labelWidth);
                    
                    if (GUILayout.Button("Enable all", EditorStyles.miniButtonLeft))
                    {
                        SetEnabledStateForAllLayers(true);
                    }
                    if (GUILayout.Button("Disable all", EditorStyles.miniButtonRight))
                    {
                        SetEnabledStateForAllLayers(false);
                    }
                }

                for (int i = 0; i < layers.arraySize; i++)
                {
                    SerializedProperty layer = layers.GetArrayElementAtIndex(i);
                    SerializedProperty enabled = layer.FindPropertyRelative("enabled");

                    using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
                    {
                        enabled.boolValue = EditorGUILayout.ToggleLeft($" Layer #{i+1}", enabled.boolValue, EditorStyles.boldLabel);

                        if (GUILayout.Button(new GUIContent("", EditorGUIUtility.IconContent("d_TreeEditor.Trash").image, "Remove"), EditorStyles.miniButtonRight, GUILayout.MaxWidth(50f)))
                        {
                            layers.DeleteArrayElementAtIndex(i);
                            break;
                        }
                    }

                    if (enabled.boolValue)
                    {
                        using (new EditorGUILayout.VerticalScope())
                        {
                            EditorGUILayout.Space(1f);
                            EditorGUI.indentLevel++;

                            UI.PropertyField(layer.FindPropertyRelative("waveLength"));
                            UI.PropertyField(layer.FindPropertyRelative("amplitude"));
                            UI.PropertyField(layer.FindPropertyRelative("steepness"));

                            EditorGUILayout.Space();

                            SerializedProperty mode = layer.FindPropertyRelative("mode");
                            UI.PropertyField(mode);

                            if (mode.intValue == (int)WaveProfile.Wave.Mode.Directional)
                            {
                                UI.PropertyField(layer.FindPropertyRelative("direction"));
                            }
                            else
                            {
                                UI.PropertyField(layer.FindPropertyRelative("origin"));
                            }

                            EditorGUI.indentLevel--;
                            EditorGUILayout.Space(10f);

                        }
                    }
                }
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                
                if (GUILayout.Button(new GUIContent("Add", EditorGUIUtility.IconContent(iconPrefix + "Toolbar Plus").image, "Add new item"), GUILayout.Width(60f)))
                {
                    layers.InsertArrayElementAtIndex(layers.arraySize);
                    
                    SerializedProperty layer = layers.GetArrayElementAtIndex(layers.arraySize-1);
                    layer.FindPropertyRelative("waveLength").floatValue -= 1;
                    layer.FindPropertyRelative("direction").floatValue -= 45;
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                instance.UpdateShaderParameters();
                
                instance.RecalculateAverages();
            }
            
            /*
            EditorGUILayout.LabelField("Average steepness: " + instance.averageSteepness);
            EditorGUILayout.LabelField("Average amplitude: " + instance.averageAmplitude);

            if (instance.shaderParametersLUT)
            {
                Rect r = EditorGUILayout.GetControlRect();
                //r.width = 8 * 4;
                //r.height = r.width * (2f / layers.arraySize);
                r.height *= 2f;
                EditorGUI.DrawPreviewTexture(r, instance.shaderParametersLUT);
                //GUI.DrawTexture(r, instance.shaderParametersLUT);
            }
            */
        }

        private void SetEnabledStateForAllLayers(bool state)
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            
            for (int i = 0; i < layers.arraySize; i++)
            {
                SerializedProperty layer = layers.GetArrayElementAtIndex(i);
                SerializedProperty enabled = layer.FindPropertyRelative("enabled");

                enabled.boolValue = state;
            }
            
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        //Handles correct behaviour when double-clicking an asset assigned to a field
        //Otherwise the OS prompts to open it
        [UnityEditor.Callbacks.OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Object target = EditorUtility.InstanceIDToObject(instanceID);

            if (target is WaveProfile)
            {
                Selection.activeObject = target;
                
                return true;
            }
            
            if (target is Texture2D)
            {
                var path = AssetDatabase.GetAssetPath(instanceID);
                Object asset = AssetDatabase.LoadMainAssetAtPath(path);

                if (asset is WaveProfile)
                {
                    Selection.activeObject = target;
                
                    return true;
                }
            }

            return false; 
        }
        
        [MenuItem("Assets/Create/Water/Wave Profile")]
        private static void CreateAsset()
        {
            WaveProfile asset = ScriptableObject.CreateInstance<WaveProfile>();
            ProjectWindowUtil.CreateAsset(asset, "New Wave Profile.asset");

            asset.UpdateShaderParameters();
        }

        public static WaveProfile LoadFromLUT(Texture lut)
        {
            if (lut == null) return null;
            
            string assetPath = AssetDatabase.GetAssetPath(lut);
                
            //Debug.Log($"Wave profile loaded: {assetPath}");
            
            return (WaveProfile)AssetDatabase.LoadMainAssetAtPath(assetPath);
        }

        public static WaveProfile LoadFromMaterial(Material material)
        {
            Texture lut = material.GetTexture("_WaveProfile");

            return LoadFromLUT(lut);
        }
        
        public static WaveProfile LoadFromWaterObject(WaterObject waterObject)
        {
            return LoadFromMaterial(waterObject.material);
        }

        public override bool HasPreviewGUI()
        {
            return false;
        }
        
        public static WaveProfile GetDefault()
        {
            string assetPath = AssetDatabase.GUIDToAssetPath("1ca4610f5bf9e2f4bb39535503a79eeb");
            
            return (WaveProfile)AssetDatabase.LoadMainAssetAtPath(assetPath);
        }
        
        class WizardWindow : EditorWindow
        {
            public WaveProfile target;
            public SerializedProperty proceduralSettings;

            [SerializeField]
            private SerializedObject serializedObject;
            
            private void Initialize()
            {
                initialized = true;
                
                if(!target) Debug.LogError("Created null");
                
                serializedObject = new SerializedObject(target);
                proceduralSettings = serializedObject.FindProperty("proceduralSettings");
            }

            [NonSerialized]
            private bool initialized;
            
            private void OnGUI()
            {
                if (!initialized) Initialize();
                
                EditorGUILayout.ObjectField("Editing: ", target, typeof(WaveProfile), false);

                if (!target) return;
                
                EditorGUILayout.LabelField("Procedural parameters", EditorStyles.boldLabel);
                
                serializedObject.Update();
                EditorGUI.BeginChangeCheck();

                UI.PropertyField(proceduralSettings);
                
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    
                    Apply(target.proceduralSettings);
                }
                
                /*
                if (target.shaderParametersLUT)
                {                
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Output LUT (visualized)", EditorStyles.boldLabel);
                    Rect r = EditorGUILayout.GetControlRect();
                    //r.width = 8 * 4;
                    r.height = 37;
                    EditorGUI.DrawPreviewTexture(r, target.shaderParametersLUT);
                }
                */
            }

            private void Apply(WaveProfile.ProceduralSettings settings)
            {
                Undo.RecordObject(target, "Randomized wave profile");

                settings.Apply((WaveProfile)target);
                
                    
                EditorUtility.SetDirty(target);
            }
        }
    }
}
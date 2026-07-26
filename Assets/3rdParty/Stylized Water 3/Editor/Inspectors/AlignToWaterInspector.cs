// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using PrefabStageUtility = UnityEditor.SceneManagement.PrefabStageUtility;

namespace StylizedWater3
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AlignToWater))]
    public class AlignToWaterInspector : Editor
    {
        AlignToWater script;

        SerializedProperty heightInterface;

        SerializedProperty surfaceSize;
        
        SerializedProperty heightOffset;
        SerializedProperty rollAmount;
        
        SerializedProperty rotation;
        SerializedProperty smoothing;
        
        private bool isRiver;
        private bool wavesEnabled;
        
        private string proSkinPrefix => EditorGUIUtility.isProSkin ? "d_" : "";
        
        private void OnEnable()
        {
            script = (AlignToWater)target;

            heightInterface = serializedObject.FindProperty("heightInterface");

            surfaceSize = serializedObject.FindProperty("surfaceSize");
            
            heightOffset = serializedObject.FindProperty("heightOffset");
            rollAmount = serializedObject.FindProperty("rollAmount");
            
            rotation = serializedObject.FindProperty("rotation");
            smoothing = serializedObject.FindProperty("smoothing");
            
            ValidateMaterial();
        }
        
        public override void OnInspectorGUI()
        {
            UI.DrawHeader();
            
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(EditorGUIUtility.labelWidth);
                AlignToWater.EnableInEditor =
                    GUILayout.Toggle(AlignToWater.EnableInEditor, new GUIContent(" Run in edit-mode (global)", EditorGUIUtility.IconContent(
                        (AlignToWater.EnableInEditor ? "animationvisibilitytoggleon" : "animationvisibilitytoggleoff")).image), "Button");
            }
            
            EditorGUILayout.Space();

            serializedObject.Update();
            
            if (script.rigidbody)
            {
                EditorGUILayout.HelpBox("RigidBody attachment detected! A fair reminder that this is not a physics-compatible component." +
                                        "\n\n" +
                                        "Physics will be overriden and its position and rotation will be set directly." +
                                        "\nVertical forces applied to it will cause visible jittering.", MessageType.Warning);
            }

            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(heightInterface);
            
            //UI.DrawNotification(isRiver, "Material has river mode enabled, buoyancy only works for flat water bodies", MessageType.Error);
            //UI.DrawNotification(!wavesEnabled && !isRiver, "Material used on the water object does not have waves enabled.", MessageType.Error);
            
            /*
            if (script.waterObject && script.waterObject.material)
            {
                UI.DrawNotification((script.waterObject.material.GetFloat("_WorldSpaceUV") == 0f), "Material must use world-projected UV", "Change", ()=> script.waterObject.material.SetFloat("_WorldSpaceUV", 1f), MessageType.Error);
            }
            */
            
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(surfaceSize);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(heightOffset);
            EditorGUILayout.PropertyField(rollAmount);
            
            EditorGUILayout.Separator();
            
            EditorGUILayout.PropertyField(rotation);
            EditorGUILayout.PropertyField(smoothing);

            UI.DrawNotification(smoothing.boolValue == false && script.rigidbody, "Smoothing is required to mitigate jittering of the RigidBody", MessageType.Warning);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                ValidateMaterial();
            }
            
            UI.DrawFooter();
        }

        private void ValidateMaterial()
        {
            /*
            if (script.waterObject && script.waterObject.material)
            {
                if (script.waterObject.material != script.waterObject.meshRenderer.sharedMaterial) script.waterObject.material = script.waterObject.meshRenderer.sharedMaterial;
                
                wavesEnabled = WaveParameters.WavesEnabled(script.waterObject.material);
                isRiver = script.waterObject.material.IsKeywordEnabled("_RIVER");
            }
            */
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlayfulSystems.ProgressBar;

[CustomEditor(typeof(BarViewColor))]
[CanEditMultipleObjects]
public class BarViewColorDrawer : Editor {
    SerializedProperty graphic;
    SerializedProperty canOverrideColor;
    SerializedProperty defaultColor;
    SerializedProperty useGradient;
    SerializedProperty barGradient;

    SerializedProperty flashOnGain;
    SerializedProperty gainColor;
    SerializedProperty flashOnLoss;
    SerializedProperty lossColor;
    SerializedProperty flashTime;

    void OnEnable() {
        graphic = serializedObject.FindProperty("graphic");
        canOverrideColor = serializedObject.FindProperty("canOverrideColor");
        defaultColor = serializedObject.FindProperty("defaultColor");
        useGradient = serializedObject.FindProperty("useGradient");
        barGradient = serializedObject.FindProperty("barGradient");

        flashOnGain = serializedObject.FindProperty("flashOnGain");
        gainColor = serializedObject.FindProperty("gainColor");
        flashOnLoss = serializedObject.FindProperty("flashOnLoss");
        lossColor = serializedObject.FindProperty("lossColor");
        flashTime = serializedObject.FindProperty("flashTime");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(graphic);

        EditorGUILayout.PropertyField(canOverrideColor);
        EditorGUILayout.PropertyField(defaultColor);
        EditorGUILayout.PropertyField(useGradient);

        if (useGradient.boolValue)
            EditorGUILayout.PropertyField(barGradient);

        EditorGUILayout.PropertyField(flashOnGain);

        if (flashOnGain.boolValue)
            EditorGUILayout.PropertyField(gainColor);

        EditorGUILayout.PropertyField(flashOnLoss);

        if (flashOnLoss.boolValue)
            EditorGUILayout.PropertyField(lossColor);

        if (flashOnGain.boolValue || flashOnLoss.boolValue)
            EditorGUILayout.PropertyField(flashTime);

        serializedObject.ApplyModifiedProperties();
    }
}
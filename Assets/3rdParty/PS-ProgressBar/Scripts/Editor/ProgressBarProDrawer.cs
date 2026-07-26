using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProgressBarPro))]
[CanEditMultipleObjects]
public class ProgressBarProDrawer : Editor {
    SerializedProperty value;
    SerializedProperty views;
    SerializedProperty animateBar;
    SerializedProperty animationType;
    SerializedProperty animTime;

    void OnEnable() {
        value = serializedObject.FindProperty("m_value");
        views = serializedObject.FindProperty("views");
        animateBar = serializedObject.FindProperty("animateBar");
        animationType = serializedObject.FindProperty("animationType");
        animTime = serializedObject.FindProperty("animTime");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(value);
        EditorGUILayout.Space();

        // Only show the damage progress bar if all the objects have the same damage value:
        if (value.hasMultipleDifferentValues)
            DrawProgressBar(0f, "Multiple Values");
        else
            DrawProgressBar(value.floatValue, "Value");

        EditorGUILayout.PropertyField(animateBar);

        if (animateBar.boolValue) { 
            EditorGUILayout.PropertyField(animationType);
            EditorGUILayout.PropertyField(animTime, new GUIContent(animationType.enumValueIndex == (int) ProgressBarPro.AnimationType.FixedTimeForChange ? "Animation Duration" : "Animation Speed"));
        }

        EditorGUILayout.PropertyField(views, true);

        EditorGUILayout.BeginHorizontal(); {
            EditorGUILayout.PrefixLabel(new GUIContent(" "));

            if (GUILayout.Button("Detect View Components in Children"))
                TriggerDetection();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    void DrawProgressBar(float value, string label) {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
    }

    void TriggerDetection() {
        if (targets != null && targets.Length > 0) { 
            for (int i = 0; i < targets.Length; i++)
                TriggerDetection(targets[i] as ProgressBarPro);
        }
        else { 
            TriggerDetection(target as ProgressBarPro);
        }
    }

    void TriggerDetection(ProgressBarPro progressBarPro) {
        if (progressBarPro == null)
            return;

        Undo.RecordObject(progressBarPro, "Detected View Components in Children");
        progressBarPro.DetectViewObjects();
        EditorUtility.SetDirty(progressBarPro);
    }


}
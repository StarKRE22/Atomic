using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class EntityManagerWindow : EditorWindow
    {
        private string _entityType = "IEntity";
        private string _namespace = "SampleGame";
        private string _directory = "Assets/Scripts/";

        private readonly List<string> _imports = new();
        private Vector2 _scrollPos;

        [MenuItem("Window/Atomic/Entities/Entity Manager")]
        public static void ShowWindow()
        {
            EntityManagerWindow window = GetWindow<EntityManagerWindow>("Entity Manager");
            window.minSize = new Vector2(400, 280);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            this.DrawEntityType();
            this.DrawNamespace();
            this.DrawDirectory();
            GUILayout.Space(10);
            this.DrawImports();
            GUILayout.FlexibleSpace();
            this.DrawGenerateButton();
        }

        private void DrawEntityType()
        {
            _entityType = EditorGUILayout.TextField("Entity Type", _entityType);
        }

        private void DrawNamespace()
        {
            _namespace = EditorGUILayout.TextField("Namespace", _namespace);
        }

        private void DrawDirectory()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Directory");

            if (GUILayout.Button("Select...", GUILayout.Width(80)))
            {
                string selected = EditorUtility.OpenFolderPanel("Select Output Folder", "Assets", "");
                if (!string.IsNullOrEmpty(selected))
                {
                    if (selected.StartsWith(Application.dataPath))
                        _directory = $"Assets{selected[Application.dataPath.Length..]}";
                    else
                        EditorUtility.DisplayDialog("Warning", "Folder must be inside the Unity project Assets folder.", "OK");
                }
            }

            EditorGUILayout.LabelField(_directory);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawImports()
        {
            GUILayout.Label("Imports", EditorStyles.boldLabel);
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(100));
         
            for (int i = 0; i < _imports.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _imports[i] = EditorGUILayout.TextField(_imports[i]);
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    _imports.RemoveAt(i);
                    GUIUtility.ExitGUI();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("+ Add Import")) 
                _imports.Add(string.Empty);
        }

        private void DrawGenerateButton()
        {
            if (GUILayout.Button("Generate File", GUILayout.Height(35)))
            {
                EntityBehavioursGenerator.GenerateFile(
                    _entityType,
                    _namespace,
                    _imports.ToArray(),
                    _directory
                );
            }
        }
    }
}
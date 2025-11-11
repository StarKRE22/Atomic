using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class EntityWizardWindow : EditorWindow
    {
        [SerializeField]
        private string _entityType = "IEntity";

        [SerializeField]
        private string _namespace = "SampleGame";
        
        [SerializeField]
        private string _directory = "Assets/Scripts/";
        
        [SerializeField]
        private EntityBaseType _entityBaseType;

        private readonly List<string> _imports = new();
        private Vector2 _scrollPos;

        [MenuItem("Window/Atomic/Entities/Entity Manager")]
        public static void ShowWindow()
        {
            EntityWizardWindow window = GetWindow<EntityWizardWindow>("Entity Manager");
            window.minSize = new Vector2(400, 280);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            DrawEntityType();
            DrawNamespace();
            DrawDirectory();
            GUILayout.Space(10);
            DrawImports();
            GUILayout.FlexibleSpace();
            DrawGenerateButton();
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
                        EditorUtility.DisplayDialog("Warning",
                            "Folder must be inside the Unity project Assets folder.", "OK");
                }
            }

            EditorGUILayout.LabelField(_directory);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawImports()
        {
            // --- Заголовок и кнопка "+" справа ---
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Imports", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", GUILayout.Width(25)))
                _imports.Add("SampleGame");
            EditorGUILayout.EndHorizontal();

            // --- Список импорта ---
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(120));
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
        }

        private void DrawGenerateButton()
        {
            if (GUILayout.Button("Generate", GUILayout.Height(35)))
            {
                EntityWizardGenerator.Generate(new EntityWizardGenerator.GenerateArgs
                {
                    directory = _directory,
                    entityType = _entityType,
                    imports = _imports.ToArray(),
                    ns = _namespace
                });
            }
        }
    }
}
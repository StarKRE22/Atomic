using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class EntityDomainWindow : EditorWindow
    {
        [SerializeField] 
        private string _entityType = "GameContext";

        [SerializeField] 
        private string _namespace = "SampleGame.Gameplay";
        
        [SerializeField] 
        private string _directory = "Assets/Scripts/";

        [SerializeField]
        private EntityBaseType _entityBaseType = EntityBaseType.SceneEntity;

        private readonly List<string> _imports = new();
        private Vector2 _scrollPos;

        [MenuItem("Window/Atomic/Entities/Entity Wizard")]
        public static void ShowWindow()
        {
            EntityDomainWindow window = GetWindow<EntityDomainWindow>("Entity Wizard");
            window.minSize = new Vector2(400, 320);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            DrawEntityType();
            DrawEntityBaseType(); // 🧩 Добавлено: выбор типа EntityBaseType
        
            GUILayout.Space(10);
            DrawNamespace();
            DrawDirectory();
            DrawImports();

            GUILayout.FlexibleSpace();
            DrawGenerateButton();
        }

        private void DrawEntityType()
        {
            _entityType = EditorGUILayout.TextField("Entity", _entityType);
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

        // 🧩 Новый метод для выбора базового типа
        private void DrawEntityBaseType()
        {
            _entityBaseType = (EntityBaseType)EditorGUILayout.EnumPopup("Base Type", _entityBaseType);
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
                EntityDomainGenerator.Generate(new EntityDomainGenerator.GenerateArgs
                {
                    directory = _directory,
                    entityType = _entityType,
                    imports = _imports.ToArray(),
                    ns = _namespace,
                    baseType = _entityBaseType // 🧩 передаем выбранный тип
                });
            }
        }
    }
}

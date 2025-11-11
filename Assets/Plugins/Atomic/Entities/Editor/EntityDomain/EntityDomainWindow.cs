using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    internal sealed class EntityDomainWindow : EditorWindow
    {
        [SerializeField] private string _entityType = "GameContext";
        [SerializeField] private string _namespace = "SampleGame.Gameplay";
        [SerializeField] private string _directory = "Assets/Scripts/";

        [SerializeField] private EntityMode _entityMode = EntityMode.SceneEntity;

        [SerializeField] private bool _proxyRequired = true;
        [SerializeField] private bool _worldRequired = true;
        [SerializeField] private bool _viewRequired = true;

        [SerializeField]
        private InstallerMode _installerMode =
            InstallerMode.ScriptableEntityInstaller | InstallerMode.SceneEntityInstaller;

        [SerializeField]
        private AspectMode _aspectMode = AspectMode.SceneEntityAspect | AspectMode.ScriptableEntityAspect;

        [SerializeField]
        private PoolMode _poolMode = PoolMode.SceneEntityPool | PoolMode.PrefabEntityPool;

        [SerializeField]
        private FactoryMode _factoryMode = FactoryMode.ScriptableEntityFactory | FactoryMode.SceneEntityFactory;

        [SerializeField]
        private BakerMode _bakerMode = BakerMode.SceneEntityBaker | BakerMode.SceneEntityBakerOptimized;

        private readonly List<string> _imports = new();
        private Vector2 _scrollPos;

        [MenuItem("Window/Atomic/Entities/Entity Wizard")]
        public static void ShowWindow()
        {
            EntityDomainWindow window = GetWindow<EntityDomainWindow>("Entity Wizard");
            window.minSize = new Vector2(450, 580);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            DrawHeader();
            DrawEntityType();
            DrawEntityBaseType();

            GUILayout.Space(8);
            DrawNamespace();
            DrawDirectory();

            GUILayout.Space(8);
            DrawOptions();

            GUILayout.Space(10);
            DrawImports();

            GUILayout.FlexibleSpace();
            DrawGenerateButton();
        }

        private void DrawHeader()
        {
            GUILayout.Label("Entity Domain Generator", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("Configure entity settings, modes, and generation options.", MessageType.Info);
        }

        private void DrawEntityType()
        {
            _entityType = EditorGUILayout.TextField("Entity", _entityType);
        }

        private void DrawEntityBaseType()
        {
            _entityMode = (EntityMode)EditorGUILayout.EnumPopup("Base Type", _entityMode);
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

        // --- Основные опции генерации ---
        private void DrawOptions()
        {
            GUILayout.Space(6);
            GUILayout.Label("Generation Options", EditorStyles.boldLabel);
            GUILayout.Space(4);

            _proxyRequired = EditorGUILayout.ToggleLeft("Generate Proxy", _proxyRequired);
            _worldRequired = EditorGUILayout.ToggleLeft("Generate World", _worldRequired);
            _viewRequired = EditorGUILayout.ToggleLeft("Generate View", _viewRequired);

            GUILayout.Space(6);
            GUILayout.Label("Installer Mode", EditorStyles.boldLabel);
            _installerMode = (InstallerMode)EditorGUILayout.EnumFlagsField(_installerMode);

            GUILayout.Space(4);
            GUILayout.Label("Aspect Mode", EditorStyles.boldLabel);
            _aspectMode = (AspectMode)EditorGUILayout.EnumFlagsField(_aspectMode);

            GUILayout.Space(4);
            GUILayout.Label("Pool Mode", EditorStyles.boldLabel);
            _poolMode = (PoolMode)EditorGUILayout.EnumFlagsField(_poolMode);

            GUILayout.Space(4);
            GUILayout.Label("Factory Mode", EditorStyles.boldLabel);
            _factoryMode = (FactoryMode)EditorGUILayout.EnumFlagsField(_factoryMode);

            GUILayout.Space(4);
            GUILayout.Label("Baker Mode", EditorStyles.boldLabel);
            _bakerMode = (BakerMode)EditorGUILayout.EnumFlagsField(_bakerMode);
        }

        private void DrawImports()
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Imports", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", GUILayout.Width(25)))
                _imports.Add("SampleGame");
            EditorGUILayout.EndHorizontal();

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
        }

        private void DrawGenerateButton()
        {
            GUILayout.Space(10);
            if (GUILayout.Button("Generate All", GUILayout.Height(40)))
            {
                EntityDomainGenerator.Generate(new EntityDomainGenerator.GenerateArgs
                {
                    directory = _directory,
                    entityType = _entityType,
                    imports = _imports.ToArray(),
                    ns = _namespace,
                    entityMode = _entityMode,
                    proxyRequired = _proxyRequired,
                    worldRequired = _worldRequired,
                    viewRequired = _viewRequired,
                    installerMode = _installerMode,
                    aspectMode = _aspectMode,
                    poolMode = _poolMode,
                    factoryMode = _factoryMode,
                    bakerMode = _bakerMode
                });
            }
        }
    }
}
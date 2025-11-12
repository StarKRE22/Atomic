using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    internal sealed class EntityDomainWindow : EditorWindow
    {
        [SerializeField]
        private string _entityType = "CustomEntity";

        [SerializeField]
        private string _namespace = "SampleGame";

        [SerializeField]
        private string _directory = "Assets/Scripts/";

        [SerializeField]
        private EntityMode _entityMode = EntityMode.SceneEntity;

        [SerializeField]
        private bool _proxyRequired = true;

        [SerializeField]
        private bool _worldRequired = true;

        [SerializeField]
        private EntityInstallerMode _installerMode =
            EntityInstallerMode.ScriptableEntityInstaller |
            EntityInstallerMode.SceneEntityInstaller;

        [SerializeField]
        private EntityAspectMode _aspectMode =
            EntityAspectMode.SceneEntityAspect |
            EntityAspectMode.ScriptableEntityAspect;

        [SerializeField]
        private EntityPoolMode _poolMode =
            EntityPoolMode.SceneEntityPool |
            EntityPoolMode.PrefabEntityPool;

        [SerializeField]
        private EntityFactoryMode _factoryMode =
            EntityFactoryMode.ScriptableEntityFactory |
            EntityFactoryMode.SceneEntityFactory;

        [SerializeField]
        private EntityBakerMode _bakerMode =
            EntityBakerMode.SceneEntityBaker |
            EntityBakerMode.SceneEntityBakerOptimized;

        [SerializeField]
        private EntityViewMode _viewMode =
            EntityViewMode.EntityView |
            EntityViewMode.EntityViewCatalog |
            EntityViewMode.EntityViewPool |
            EntityViewMode.EntityCollectionView;

        private readonly List<string> _imports = new();

        private Vector2 _scrollPos;

        [MenuItem("Window/Atomic/Entities/Entity Domain Generator")]
        public static void ShowWindow()
        {
            EntityDomainWindow window = GetWindow<EntityDomainWindow>("Entity Domain Generator");
            window.minSize = new Vector2(450, 580);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            DrawHeader();

            GUILayout.Space(8);
            DrawNamespace();
            DrawDirectory();

            GUILayout.Space(8);
            DrawEntityType();

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
            EditorGUILayout.BeginHorizontal();

            _entityType = EditorGUILayout.TextField(new GUIContent("Entity"), _entityType);
            _entityMode = (EntityMode) EditorGUILayout.EnumPopup(_entityMode, GUILayout.Width(200));

            if (GUILayout.Button("Generate", GUILayout.Width(90)))
            {
                EntityInterfaceGenerator.Generate($"I{_entityType}", _namespace, _imports.ToArray(), _directory);
                EntityConcreteGenerator.Generate(_entityMode, _entityType, $"I{_entityType}", _namespace,
                    _imports.ToArray(), _directory);
                EntityBehaviourGenerator.Generate($"I{_entityType}", _namespace, _imports.ToArray(), _directory);
            }


            EditorGUILayout.EndHorizontal();
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
            GUILayout.Space(4);

            DrawProxy();
            DrawWorld();

            GUILayout.Space(6);
            DrawInstallers();

            GUILayout.Space(4);
            DrawAspects();

            GUILayout.Space(4);
            DrawPools();

            GUILayout.Space(4);
            GUILayout.Label("Factory Mode", EditorStyles.boldLabel);
            _factoryMode = (EntityFactoryMode) EditorGUILayout.EnumFlagsField(_factoryMode);

            GUILayout.Space(4);
            GUILayout.Label("Baker Mode", EditorStyles.boldLabel);
            _bakerMode = (EntityBakerMode) EditorGUILayout.EnumFlagsField(_bakerMode);
        }

        private void DrawPools()
        {
            using (new EditorGUI.DisabledScope(_entityMode is not (EntityMode.SceneEntity or EntityMode.SceneEntitySingleton)))
            {
                EditorGUILayout.BeginHorizontal();
                
                GUILayout.Label("Pools", GUILayout.Width(70));
                _poolMode = (EntityPoolMode) EditorGUILayout.EnumFlagsField(_poolMode, GUILayout.ExpandWidth(true));
                
                if (GUILayout.Button("Generate", GUILayout.Width(90)))
                {
                    EntityPoolGenerator.Generate(
                        _poolMode,
                        _entityType,
                        $"I{_entityType}",
                        _namespace,
                        _directory,
                        _imports.ToArray()
                    );
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawProxy()
        {
            using (new EditorGUI.DisabledScope(
                       _entityMode is not (EntityMode.SceneEntity or EntityMode.SceneEntitySingleton)))
            {
                EditorGUILayout.BeginHorizontal();

                _proxyRequired = EditorGUILayout.Toggle("SceneEntityProxy", _proxyRequired);

                if (GUILayout.Button("Generate", GUILayout.Width(90)))
                {
                    SceneEntityProxyGenerator.Generate(
                        _entityType,
                        $"I{_entityType}",
                        _namespace,
                        _imports.ToArray(),
                        _directory
                    );
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawWorld()
        {
            using (new EditorGUI.DisabledScope(
                       _entityMode is not (EntityMode.SceneEntity or EntityMode.SceneEntitySingleton)))
            {
                EditorGUILayout.BeginHorizontal();

                _worldRequired = EditorGUILayout.Toggle("SceneEntityWorld", _worldRequired);

                if (GUILayout.Button("Generate", GUILayout.Width(90))) 
                    SceneEntityWorldGenerator.Generate(_entityType, _namespace, _imports.ToArray(), _directory);

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawInstallers()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Installers", GUILayout.Width(70));
            _installerMode =
                (EntityInstallerMode) EditorGUILayout.EnumFlagsField(_installerMode, GUILayout.ExpandWidth(true));

            if (GUILayout.Button("Generate", GUILayout.Width(90)))
            {
                EntityInstallerGenerator.Generate(
                    _installerMode,
                    _entityType,
                    $"I{_entityType}",
                    _namespace,
                    _directory,
                    _imports.ToArray()
                );
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawAspects()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Aspects", GUILayout.Width(70));
            _aspectMode = (EntityAspectMode) EditorGUILayout.EnumFlagsField(_aspectMode, GUILayout.ExpandWidth(true));

            if (GUILayout.Button("Generate", GUILayout.Width(90)))
            {
                EntityAspectGenerator.Generate(
                    _aspectMode,
                    _entityType,
                    $"I{_entityType}",
                    _namespace,
                    _directory,
                    _imports.ToArray()
                );
            }

            EditorGUILayout.EndHorizontal();
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
                    installerMode = _installerMode,
                    aspectMode = _aspectMode,
                    poolMode = _poolMode,
                    factoryMode = _factoryMode,
                    bakerMode = _bakerMode,
                    viewMode = _viewMode
                });
            }
        }
    }
}
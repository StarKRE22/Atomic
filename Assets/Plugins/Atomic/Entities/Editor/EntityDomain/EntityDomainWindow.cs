using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Interactive Unity Editor window for generating entity domain boilerplate code:
    /// interfaces, concrete entities, installers, factories, pools, bakers, and views.
    /// </summary>
    internal sealed class EntityDomainWindow : EditorWindow
    {
        [Header("Base Settings")]
        [SerializeField]
        private string _entityType = "CustomEntity";
        
        [SerializeField]
        private string _namespace = "SampleGame";
     
        [SerializeField]
        private string _directory = "Assets/Scripts/";
        
        [SerializeField]
        private EntityMode _entityMode = EntityMode.SceneEntity;

        [Space(5)]
        [SerializeField]
        private bool _proxyRequired = true;
        
        [SerializeField]
        private bool _worldRequired = true;

        [Header("Generation Modes")]
        [SerializeField]
        private EntityInstallerMode _installerMode =
            EntityInstallerMode.ScriptableEntityInstaller | EntityInstallerMode.SceneEntityInstaller;

        [SerializeField]
        private EntityAspectMode _aspectMode =
            EntityAspectMode.SceneEntityAspect | EntityAspectMode.ScriptableEntityAspect;

        [SerializeField]
        private EntityPoolMode _poolMode =
            EntityPoolMode.SceneEntityPool | EntityPoolMode.PrefabEntityPool;

        [SerializeField]
        private EntityFactoryMode _factoryMode =
            EntityFactoryMode.ScriptableEntityFactory | EntityFactoryMode.SceneEntityFactory;

        [SerializeField]
        private EntityBakerMode _bakerMode =
            EntityBakerMode.Standard | EntityBakerMode.Optimized;

        [SerializeField]
        private EntityViewMode _viewMode =
            EntityViewMode.EntityView | EntityViewMode.EntityViewCatalog |
            EntityViewMode.EntityViewPool | EntityViewMode.EntityCollectionView;

        private readonly List<string> _imports = new();
        private Vector2 _scrollPos;

        [MenuItem("Window/Atomic/Entities/Entity Domain Generator")]
        public static void ShowWindow()
        {
            var window = GetWindow<EntityDomainWindow>("Entity Domain Generator");
            window.minSize = new Vector2(480, 480);
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            DrawHeader();

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawDirectory();
                DrawNamespace();
                DrawImports();
            }

            EditorGUILayout.Space(6);
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawEntityType();
            }

            EditorGUILayout.Space(6);
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Generation Options", EditorStyles.boldLabel);
                EditorGUILayout.Space(3);

                DrawProxy();
                DrawWorld();

                EditorGUILayout.Space(4);
                DrawInstallers();
                DrawAspects();
                DrawPools();
                DrawFactories();
                DrawBakers();
                DrawUI();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.Space(6);
            DrawGenerateButton();
        }

        private void DrawHeader()
        {
            GUILayout.Label("Entity Domain Generator", EditorStyles.largeLabel);
            EditorGUILayout.HelpBox(
                "Generate full entity domain structure: interfaces, entities, pools, factories, bakers, and views.\n" +
                "Choose what to include and where to save.",
                MessageType.Info
            );
        }

        private void DrawEntityType()
        {
            EditorGUILayout.BeginHorizontal();
            _entityType = EditorGUILayout.TextField(new GUIContent("Entity Type"), _entityType);
            _entityMode = (EntityMode) EditorGUILayout.EnumPopup(_entityMode, GUILayout.Width(160));

            if (GUILayout.Button("Generate", GUILayout.Width(100)))
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
            _namespace = EditorGUILayout.TextField(new GUIContent("Namespace"), _namespace);
        }

        private void DrawDirectory()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Directory");
            if (GUILayout.Button("Select...", GUILayout.Width(90)))
            {
                string selected = EditorUtility.OpenFolderPanel("Select Output Folder", "Assets", "");
                if (!string.IsNullOrEmpty(selected))
                {
                    if (selected.StartsWith(Application.dataPath))
                        _directory = $"Assets{selected[Application.dataPath.Length..]}";
                    else
                        EditorUtility.DisplayDialog("Warning", "Folder must be inside the Unity project Assets folder.",
                            "OK");
                }
            }

            EditorGUILayout.LabelField(_directory);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawImports()
        {
            GUILayout.Space(4);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Imports", GUILayout.Width(50));
            if (GUILayout.Button("+", GUILayout.Width(25)))
                _imports.Add("SampleGame");
            EditorGUILayout.EndHorizontal();

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
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

        private void DrawProxy()
        {
            using (new EditorGUI.DisabledScope(
                       _entityMode is not (EntityMode.SceneEntity or EntityMode.SceneEntitySingleton)))
            {
                EditorGUILayout.BeginHorizontal();
                _proxyRequired = EditorGUILayout.Toggle("SceneEntityProxy", _proxyRequired);
                if (GUILayout.Button("Generate", GUILayout.Width(100)))
                {
                    SceneEntityProxyGenerator.Generate(_entityType, $"I{_entityType}", _namespace, _imports.ToArray(),
                        _directory);
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
                if (GUILayout.Button("Generate", GUILayout.Width(100)))
                    SceneEntityWorldGenerator.Generate(_entityType, _namespace, _imports.ToArray(), _directory);
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawInstallers()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Installers", GUILayout.Width(80));
            _installerMode = (EntityInstallerMode) EditorGUILayout.EnumFlagsField(_installerMode);
            if (GUILayout.Button("Generate", GUILayout.Width(100)))
            {
                EntityInstallerGenerator.Generate(_installerMode, _entityType, $"I{_entityType}", _namespace,
                    _directory, _imports.ToArray());
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawAspects()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Aspects", GUILayout.Width(80));
            _aspectMode = (EntityAspectMode) EditorGUILayout.EnumFlagsField(_aspectMode);
            if (GUILayout.Button("Generate", GUILayout.Width(100)))
            {
                EntityAspectGenerator.Generate(_aspectMode, _entityType, $"I{_entityType}", _namespace, _directory,
                    _imports.ToArray());
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawPools()
        {
            using (new EditorGUI.DisabledScope(
                       _entityMode is not (EntityMode.SceneEntity or EntityMode.SceneEntitySingleton)))
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Pools", GUILayout.Width(80));
                _poolMode = (EntityPoolMode) EditorGUILayout.EnumFlagsField(_poolMode);
                if (GUILayout.Button("Generate", GUILayout.Width(100)))
                {
                    EntityPoolGenerator.Generate(_poolMode, _entityType, $"I{_entityType}", _namespace, _directory,
                        _imports.ToArray());
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawFactories()
        {
            using (new EditorGUI.DisabledScope(_entityMode is not (EntityMode.Entity or EntityMode.EntitySingleton)))
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Factories", GUILayout.Width(80));
                _factoryMode = (EntityFactoryMode) EditorGUILayout.EnumFlagsField(_factoryMode);
                if (GUILayout.Button("Generate", GUILayout.Width(100)))
                {
                    EntityFactoryGenerator.Generate(_factoryMode, _entityType, $"I{_entityType}", _namespace,
                        _directory, _imports.ToArray());
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawBakers()
        {
            using (new EditorGUI.DisabledScope(_entityMode is not (EntityMode.Entity or EntityMode.EntitySingleton)))
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Bakers", GUILayout.Width(80));
                _bakerMode = (EntityBakerMode) EditorGUILayout.EnumFlagsField(_bakerMode);
                if (GUILayout.Button("Generate", GUILayout.Width(100)))
                {
                    EntityBakerGenerator.Generate(_bakerMode, _entityType, $"I{_entityType}", _namespace,
                        _imports.ToArray(), _directory);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawUI()
        {
            using (new EditorGUI.DisabledScope(_entityMode is not (EntityMode.Entity or EntityMode.EntitySingleton)))
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("UI Views", GUILayout.Width(80));
                _viewMode = (EntityViewMode) EditorGUILayout.EnumFlagsField(_viewMode);
                if (GUILayout.Button("Generate", GUILayout.Width(100)))
                    EntityUIGenerator.Generate(_viewMode, _entityType, $"I{_entityType}", _namespace,
                        _imports.ToArray(), _directory);
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawGenerateButton()
        {
            EditorGUILayout.Space(10);

            // Создаем стиль кнопки с жирным шрифтом
            GUIStyle boldButtonStyle = new(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 14 // можно увеличить, чтобы кнопка выглядела выразительнее
            };

            GUI.backgroundColor = new Color(0f, 0.83f, 1f);

            if (GUILayout.Button("GENERATE DOMAIN", boldButtonStyle, GUILayout.Height(40)))
            {
                GUI.backgroundColor = Color.white;
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

            GUI.backgroundColor = Color.white;
        }
    }
}
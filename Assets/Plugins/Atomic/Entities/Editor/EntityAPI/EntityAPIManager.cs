#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    internal static class EntityAPIManager
    {
        private sealed class CreateAPIAction : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                File.WriteAllText(pathName, EntityAPITemplate.Instance);
                AssetDatabase.Refresh();

                Object obj = AssetDatabase.LoadAssetAtPath<Object>(pathName);
                ProjectWindowUtil.ShowCreatedAsset(obj);
            }
        }
        
        private static readonly EntityAPISettings _settings;
        private static double _currentTime;
        private static List<EntityAPIAsset> _activeAssets;

        static EntityAPIManager()
        {
            _settings = EntityAPISettings.Instance;
            _currentTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += Update;
            LoadAssets();
        }
        
        private static void Update()
        {
            if (!_settings.autoRefresh)
                return;

            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - _currentTime >= _settings.autoRefreshPeriod)
            {
                RefreshAPI();
                _currentTime = currentTime;
            }
        }

        [MenuItem("Tools/Atomic/Entities/Compile Entity API", priority = 1)]
        public static void CompileAPI()
        {
            LoadAssets();

            if (_activeAssets == null)
                return;

            for (int i = 0, cont = _activeAssets.Count; i < cont; i++)
            {
                EntityAPIAsset asset = _activeAssets[i];
                if (!asset.IsValid)
                    continue;

                EntityAPIAsset.Settings settings = asset.GetSettings();
                EntityAPIGenerator.CreateFile(settings);
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
        
        [MenuItem("Tools/Atomic/Entities/Select Entity API Settings", priority = 10)]
        public static void SelectSetttings() => Selection.activeObject = EntityAPISettings.Instance;

        [MenuItem("Tools/Atomic/Entities/Refresh Entity API", priority = 1)]
        public static void RefreshAPI()
        {
            if (_activeAssets == null)
                return;

            for (int i = 0, cont = _activeAssets.Count; i < cont; i++)
            {
                EntityAPIAsset asset = _activeAssets[i];
                if (!asset.IsValid)
                    continue;

                EntityAPIAsset.Settings settings = asset.GetSettings();
                EntityAPIGenerator.UpdateFile(settings);
            }
        }
        
        [MenuItem("Assets/Create/Atomic/Entities/Entity API", priority = 1)]
        public static void CreateAPI()
        {
            string path = GetSelectedPathOrFallback();
            const string defaultName = "SampleEntityAPI.yaml";
            string fullPath = Path.Combine(path, defaultName);
            Texture2D icon = EditorGUIUtility.IconContent("TextAsset Icon").image  as Texture2D;
            
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                ScriptableObject.CreateInstance<CreateAPIAction>(),
                fullPath,
                icon,
                null
            );
        }

        private static string GetSelectedPathOrFallback()
        {
            string path = "Assets";

            foreach (var obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    path = Path.GetDirectoryName(path);
                
                break;
            }

            return path;
        }

        private static void LoadAssets()
        {
            string[] assetPaths = AssetDatabase.FindAssets("API")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(IsEntityAPIFile)
                .ToArray();

            int count = assetPaths.Length;
            _activeAssets = new List<EntityAPIAsset>(count);

            for (int i = 0; i < count; i++)
            {
                string filePath = assetPaths[i];
                _activeAssets.Add(new EntityAPIAsset(filePath));
            }
        }

        private static bool IsEntityAPIFile(string path)
        {
            if (!path.EndsWith(".yaml") && !path.EndsWith(".yml"))
                return false;

            IEnumerable<string> lines = File.ReadLines(path);
            string first = lines.FirstOrDefault();
            if (string.IsNullOrEmpty(first))
                return false;

            return Regex.IsMatch(first, @"^header\s*:\s*EntityAPI$");
        }
    }
}
#endif
#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Atomic.Entities;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace Atomic.Events
{
    [InitializeOnLoad]
    internal static class EventAPIManager
    {
        private sealed class CreateEntityAPIAction : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                File.WriteAllText(pathName, EventAPITemplate.Value);
                AssetDatabase.Refresh();

                Object obj = AssetDatabase.LoadAssetAtPath<Object>(pathName);
                ProjectWindowUtil.ShowCreatedAsset(obj);
            }
        }
        
        private static readonly EventAPISettings _settings;
        private static double _currentTime;
        private static List<EventAPIAsset> _assets;

        static EventAPIManager()
        {
            _settings = EventAPISettings.Instance;
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
                EventAPIManager.RefreshAPI();
                _currentTime = currentTime;
            }
        }

        public static void CompileAPI()
        {
            LoadAssets();

            if (_assets == null)
                return;

            for (int i = 0, cont = _assets.Count; i < cont; i++)
            {
                EventAPIAsset asset = _assets[i];
                if (!asset.IsValid)
                    continue;

                IEventAPIConfig config = asset.GetConfiguration();
                EventAPIGenerator.CreateFile(config);
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        public static void RefreshAPI()
        {
            if (_assets == null)
                return;

            for (int i = 0, cont = _assets.Count; i < cont; i++)
            {
                EventAPIAsset asset = _assets[i];
                if (!asset.IsValid)
                    continue;

                IEventAPIConfig config = asset.GetConfiguration();
                EventAPIGenerator.UpdateFile(config);
            }
        }

        public static void CreateAPI()
        {
            string filePath = EditorUtility.SaveFilePanel(
                "Create Event API...",
                "Assets",
                "SampleEventAPI.yaml",
                "yaml"
            );

            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(EventAPITemplate.Value);

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static void LoadAssets()
        {
            string[] assetPaths = AssetDatabase.FindAssets("API")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(IsEventApiFile)
                .ToArray();

            int count = assetPaths.Length;
            _assets = new List<EventAPIAsset>(count);

            for (int i = 0; i < count; i++)
            {
                string filePath = assetPaths[i];
                _assets.Add(new EventAPIAsset(filePath));
            }
        }

        private static bool IsEventApiFile(string path)
        {
            if (!path.EndsWith(".yaml") && !path.EndsWith(".yml"))
                return false;

            IEnumerable<string> lines = File.ReadLines(path);
            string first = lines.FirstOrDefault();
            return !string.IsNullOrEmpty(first) && Regex.IsMatch(first, @"^header\s*:\s*EventBusAPI$");
        }
    }
}
#endif
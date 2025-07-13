#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

namespace Atomic.Events
{
    [InitializeOnLoad]
    internal static class EventAPIManager
    {
        private static List<EventAPIAsset> _assets;

        static EventAPIManager()
        {
            LoadAssets();
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
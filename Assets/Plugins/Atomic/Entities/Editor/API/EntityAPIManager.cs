#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    internal static class EntityAPIManager
    {
        private static List<EntityAPIAsset> _assets;

        static EntityAPIManager()
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
                EntityAPIAsset asset = _assets[i];
                if (!asset.IsValid)
                    continue;

                IEntityAPIConfiguration configuration = asset.GetConfiguration();
                EntityAPIGenerator.CreateFile(configuration);
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
                EntityAPIAsset asset = _assets[i];
                if (!asset.IsValid)
                    continue;

                IEntityAPIConfiguration configuration = asset.GetConfiguration();
                EntityAPIGenerator.UpdateFile(configuration);
            }
        }

        public static void CreateAPI()
        {
            string filePath = EditorUtility.SaveFilePanel(
                "Create Entity API...",
                "Assets",
                "SampleEntityAPI.yaml",
                "yaml"
            );

            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(EntityAPITemplate.Value);

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static void LoadAssets()
        {
            string[] assetPaths = AssetDatabase.FindAssets("API")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(IsEntityAPIFile)
                .ToArray();

            int count = assetPaths.Length;
            _assets = new List<EntityAPIAsset>(count);

            for (int i = 0; i < count; i++)
            {
                string filePath = assetPaths[i];
                _assets.Add(new EntityAPIAsset(filePath));
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
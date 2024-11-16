using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    public static class EntityAPIManager
    {
        private const string ASSET_NAME = "EntityAPI";

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
            writer.Write(GetAssetContent());

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static void LoadAssets()
        {
            string[] assetPaths = AssetDatabase.FindAssets(ASSET_NAME)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => path.EndsWith($"{ASSET_NAME}.yaml") || path.EndsWith($"{ASSET_NAME}.yml"))
                .ToArray();

            int count = assetPaths.Length;
            _assets = new List<EntityAPIAsset>(count);

            for (int i = 0; i < count; i++)
            {
                string filePath = assetPaths[i];
                Debug.Log($"LOAD ASSET {filePath}");
                _assets.Add(new EntityAPIAsset(filePath));
            }
        }

        private static string GetAssetContent() =>
            "namespace: SampleGame\n" +
            "className: SampleEntityAPI\n" +
            "directory: Assets/Scripts/Codegen\n " +
            "\n" +
            "imports:\n" +
            "- UnityEngine\n" +
            "- Atomic.Entities\n" +
            "\n" +
            "tags:\n" +
            "- Player\n" +
            "- Enemy\n" +
            "- Resource\n" +
            "\n" +
            "values:\n" +
            "- Health: int\n" +
            "- Speed: float\n" +
            "- Transform: Transform\n";
    }
}

//
// public static void Generate()
// {
//     GenerateByYaml();
//     GenerateByClass();
// }
//
// private static void GenerateByYaml()
// {
//     // Найти все потенциальные YAML-файлы
//     string[] guids = AssetDatabase.FindAssets("EntityAPI");
//
//     // Список для хранения подходящих файлов
//     foreach (string filePath in guids
//                  .Select(AssetDatabase.GUIDToAssetPath)
//                  .Where(path => path.EndsWith("EntityAPI.yaml")))
//     {
//         IEntityAPIConfiguration configuration = new EntityAPIConfiguration(filePath);
//         EntityAPIGenerator.CreateFile(configuration);
//     }
// }
//
// private static void GenerateByClass()
// {
//     Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
//     foreach (Assembly assembly in assemblies)
//     {
//         Type[] types = assembly.GetTypes();
//         foreach (Type type in types)
//         {
//             if (!type.IsInterface && !type.IsAbstract &&
//                 typeof(IEntityAPIConfiguration).IsAssignableFrom(type))
//             {
//                 if (type == typeof(EntityAPIConfiguration))
//                     continue;
//
//                 var configuration = (IEntityAPIConfiguration) Activator.CreateInstance(type);
//                 EntityAPIGenerator.CreateFile(configuration);
//             }
//         }
//     }
// }
using System.Linq;
using UnityEditor;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    public static class EntityAPIManager
    {
        private const string ASSET_NAME = "EntityAPI";
        private const string ASSET_EXTENSION = "yaml";
        
        private static EntityAPIAsset[] _assets;
        
        static EntityAPIManager()
        {
            LoadAssets();
        }
        
        public static void Compile()
        {
            if (_assets == null)
                return;

            for (int i = 0, cont = _assets.Length; i < cont; i++)
            {
                EntityAPIAsset asset = _assets[i];
                IEntityAPIConfiguration configuration = asset.GetConfiguration();
                EntityAPIGenerator.CreateFile(configuration);
            }

            AssetDatabase.Refresh();
        }

        public static void Refresh()
        {
            if (_assets == null)
                return;

            for (int i = 0, cont = _assets.Length; i < cont; i++)
            {
                EntityAPIAsset asset = _assets[i];
                IEntityAPIConfiguration configuration = asset.GetConfiguration();
                EntityAPIGenerator.UpdateFile(configuration);
            }
        }

        private static void LoadAssets()
        {
            string[] assetPaths = AssetDatabase.FindAssets(ASSET_NAME)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => path.EndsWith($"{ASSET_NAME}.{ASSET_EXTENSION}"))
                .ToArray();

            int count = assetPaths.Length;
            _assets = new EntityAPIAsset[count];
            
            for (int i = 0; i < count; i++)
            {
                string filePath = assetPaths[i];
                EntityAPIAsset configuration = new EntityAPIAsset(filePath);
                _assets[i] = configuration;
            }
        }
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
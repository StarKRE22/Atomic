using System.Linq;
using UnityEditor;

namespace Atomic.Entities
{
    public static class EntityAPIManager
    {
        private static IEntityAPIConfiguration[] _configurations;

        public static void Compile()
        {
            string[] files = AssetDatabase.FindAssets("EntityAPI")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => path.EndsWith("EntityAPI.yaml"))
                .ToArray();

            int count = files.Length;
            _configurations = new IEntityAPIConfiguration[count];
            
            for (int i = 0; i < count; i++)
            {
                string filePath = files[i];
                var configuration = new EntityAPIConfiguration(filePath);
                EntityAPIGenerator.CreateFile(configuration);
                _configurations[i] = configuration;
            }
            
            AssetDatabase.Refresh();
        }

        public static void Refresh()
        {
            for (int i = 0, cont = _configurations.Length; i < cont; i++)
            {
                IEntityAPIConfiguration configuration = _configurations[i];
                EntityAPIGenerator.UpdateFile(configuration);
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
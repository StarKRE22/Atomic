using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Assembly = System.Reflection.Assembly;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    public static class EntityAPIController
    {
        static EntityAPIController()
        {
            EditorApplication.update += Update;
            // CompilationPipeline.compilationFinished += Generate;
        }

        private static void Update()
        {
        }

        public static void Generate()
        {
            GenerateByYaml();
            GenerateByClass();
            AssetDatabase.Refresh();
        }

        private static void GenerateByYaml()
        {
            // Найти все потенциальные YAML-файлы
            string[] guids = AssetDatabase.FindAssets("EntityAPI");

            // Список для хранения подходящих файлов
            foreach (string filePath in guids
                         .Select(AssetDatabase.GUIDToAssetPath)
                         .Where(path => path.EndsWith("EntityAPI.yaml")))
            {
                IEntityAPIConfiguration configuration = new EntityAPIConfiguration(filePath);
                EntityAPIGenerator.GenerateFile(configuration);
            }
        }

        private static void GenerateByClass()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (!type.IsInterface && !type.IsAbstract && typeof(IEntityAPIConfiguration).IsAssignableFrom(type))
                    {
                        if (type == typeof(EntityAPIConfiguration))
                            continue;

                        var configuration = (IEntityAPIConfiguration) Activator.CreateInstance(type);
                        EntityAPIGenerator.GenerateFile(configuration);
                    }
                }
            }
        }
    }
}
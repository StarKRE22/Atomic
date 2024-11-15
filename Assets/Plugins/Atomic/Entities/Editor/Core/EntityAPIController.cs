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
            // CompilationPipeline.compilationFinished += Generate;
        }

        public static void Generate()
        {
            Debug.Log("GENERATE!");
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (!type.IsInterface && !type.IsAbstract && typeof(IEntityAPIConfiguration).IsAssignableFrom(type))
                    {
                        Debug.Log($"CONFIGURATION {type.FullName}");
                        var configuration = (IEntityAPIConfiguration) Activator.CreateInstance(type);
                        EntityAPIGenerator.GenerateFile(configuration);
                    }
                }
            }

            //
            // IEnumerable<IEntityAPIConfiguration> configurations = AppDomain
            //     .CurrentDomain.GetAssemblies()
            //     .First(it => it.FullName.Contains("Editor"))
            //     .GetTypes()
            //     .Where(type => typeof(IEntityAPIConfiguration).IsAssignableFrom(type)
            //                    && !type.IsInterface
            //                    && !type.IsAbstract
            //     ).Select(it => (IEntityAPIConfiguration) Activator.CreateInstance(it));
            //
            // Debug.Log($"CONFIGURATIONS {configurations.Count()}");
            // foreach (IEntityAPIConfiguration configuration in configurations)
            //     EntityAPIGenerator.GenerateFile(configuration);
            //
            AssetDatabase.Refresh();
        }
    }
}
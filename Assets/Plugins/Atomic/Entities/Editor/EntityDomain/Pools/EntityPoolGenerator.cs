using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityPoolGenerator
    {
        public static void Generate(
            EntityPoolMode mode,
            string entityType,
            string interfaceType,
            string ns,
            string directory,
            string[] imports
        )
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                EditorUtility.DisplayDialog("Error", "Entity Type cannot be empty.", "OK");
                return;
            }

            bool scenePoolRequired = mode.HasFlag(EntityPoolMode.SceneEntityPool);
            bool prefabPoolRequired = mode.HasFlag(EntityPoolMode.PrefabEntityPool);

            Directory.CreateDirectory(directory);

            if (scenePoolRequired)
                GenerateSceneEntityPool(entityType, interfaceType, ns, directory, imports);

            if (prefabPoolRequired)
                GeneratePrefabEntityPool(entityType, ns, directory, imports);

            AssetDatabase.Refresh();
        }

        private static void GenerateSceneEntityPool(
            string entityType,
            string interfaceType,
            string ns,
            string directory,
            string[] imports
        )
        {
            string filePath = Path.Combine(directory, $"{entityType}Pool.cs");
            string content = GenerateSceneEntityPoolContent(entityType, interfaceType, ns, imports);
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        private static void GeneratePrefabEntityPool(
            string entityType,
            string ns,
            string directory,
            string[] imports
        )
        {
            string filePath = Path.Combine(directory, $"{entityType}PrefabPool.cs");
            string content = GeneratePrefabEntityPoolContent(ns, entityType, imports);
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        private static string GenerateSceneEntityPoolContent(string entityType, string interfaceType, string ns,
            string[] imports)
        {
            var sb = new StringBuilder();

            // --- Imports ---
            sb.AppendLine("using Atomic.Entities;");
            if (imports is {Length: > 0})
            {
                foreach (string import in imports.Where(i => !string.IsNullOrWhiteSpace(i)))
                {
                    string clean = import.Trim();
                    sb.AppendLine(clean.StartsWith("using") ? clean : $"using {clean};");
                }
            }

            sb.AppendLine();
            sb.AppendLine("/**");
            sb.AppendLine(" * Created by Entity Domain Generator.");
            sb.AppendLine(" */");
            sb.AppendLine();

            // --- Namespace + Pool Class ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine(
                $"    /// A Unity-integrated pool for <see cref=\"{entityType}\"/> entities that exist within a scene.");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    /// <remarks>");
            sb.AppendLine(
                $"    /// Implements <see cref=\"IEntityPool{{{interfaceType}}}\"/> for renting and returning scene-based entities.");
            sb.AppendLine($"    /// </remarks>");
            sb.AppendLine(
                $"    public sealed class {entityType}Pool : SceneEntityPool<{entityType}>, IEntityPool<{interfaceType}>");
            sb.AppendLine("    {");
            sb.AppendLine($"        {interfaceType} IEntityPool<{interfaceType}>.Rent() => this.Rent();");
            sb.AppendLine();
            sb.AppendLine(
                $"        void IEntityPool<{interfaceType}>.Return({interfaceType} entity) => this.Return(({entityType})entity);");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GeneratePrefabEntityPoolContent(string ns, string entityType, string[] imports)
        {
            var sb = new StringBuilder();

            // --- Imports ---
            sb.AppendLine("using Atomic.Entities;");
            if (imports is {Length: > 0})
            {
                foreach (string import in imports.Where(i => !string.IsNullOrWhiteSpace(i)))
                {
                    string clean = import.Trim();
                    sb.AppendLine(clean.StartsWith("using") ? clean : $"using {clean};");
                }
            }

            sb.AppendLine();
            sb.AppendLine("/**");
            sb.AppendLine(" * Created by Entity Domain Generator.");
            sb.AppendLine(" */");
            sb.AppendLine();

            // --- Namespace + Prefab Pool Class ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine(
                $"    /// A prefab-based entity pool for managing <see cref=\"{entityType}\"/> instances at runtime.");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    /// <remarks>");
            sb.AppendLine(
                $"    /// Useful for dynamically spawning and reusing <see cref=\"{entityType}\"/> prefabs in gameplay scenes.");
            sb.AppendLine($"    /// </remarks>");
            sb.AppendLine($"    public sealed class {entityType}PrefabPool : PrefabEntityPool<{entityType}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityBakerGenerator
    {
        public static void Generate(
            EntityBakerMode mode,
            string entityType,
            string entityInterface,
            string ns,
            string[] imports,
            string directory
        )
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                EditorUtility.DisplayDialog("Error", "Entity Type cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(entityInterface))
            {
                EditorUtility.DisplayDialog("Error", "Entity Interface cannot be empty.", "OK");
                return;
            }

            bool standardRequired = mode.HasFlag(EntityBakerMode.Standard);
            bool optimizedRequired = mode.HasFlag(EntityBakerMode.Optimized);
            bool bothRequired = standardRequired && optimizedRequired;

            if (standardRequired)
                GenerateFile(entityType, entityInterface, ns, imports, directory, EntityBakerMode.Standard, bothRequired);

            if (optimizedRequired)
                GenerateFile(entityType, entityInterface, ns, imports, directory, EntityBakerMode.Optimized, bothRequired);
        }

        private static void GenerateFile(
            string entityType,
            string entityInterface,
            string ns,
            string[] imports,
            string directory,
            EntityBakerMode mode,
            bool bothRequired
        )
        {
            // --- Имя класса и файла ---
            bool isOptimized = mode == EntityBakerMode.Optimized;
            string suffix = bothRequired && isOptimized ? "Optimized" : string.Empty;
            string className = $"{entityType}Baker{suffix}";
            string fileName = $"{className}.cs";

            // --- Определяем базовый тип и XML комментарии ---
            string baseType;
            string summary;
            string remarks;

            switch (mode)
            {
                case EntityBakerMode.Standard:
                    baseType = $"SceneEntityBaker<{entityInterface}>";
                    summary = $"Converts a scene GameObject into a C# <see cref=\"{entityInterface}\"/> entity.";
                    remarks =
                        $"Derive from this class to define baking logic for <see cref=\"{entityType}\"/> entities in the scene.\n" +
                        $"This version provides standard conversion hooks without additional optimizations.";
                    break;

                case EntityBakerMode.Optimized:
                    baseType = $"SceneEntityBakerOptimized<{entityInterface}, {entityType}View>";
                    summary =
                        $"An optimized Unity baker for <see cref=\"{entityType}\"/> entities that requires a matching <see cref=\"{entityType}View\"/>.";
                    remarks =
                        $"Use this version when you need high-performance conversion of <see cref=\"{entityType}\"/> entities, " +
                        $"leveraging cached <see cref=\"EntityView{{T}}\"/> components. Automatically enforces " +
                        $"<see cref=\"RequireComponent\"/> for <see cref=\"{entityType}View\"/>.";
                    break;

                case EntityBakerMode.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported baker mode.");
            }

            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, fileName);
            string content = GenerateContent(entityType, className, ns, imports, mode, baseType, summary, remarks);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static string GenerateContent(
            string entityType,
            string className,
            string ns,
            string[] imports,
            EntityBakerMode mode,
            string baseType,
            string summary,
            string remarks
        )
        {
            var sb = new StringBuilder();

            // --- Imports ---
            sb.AppendLine("using Atomic.Entities;");
            sb.AppendLine("using UnityEngine;");
            if (imports is { Length: > 0 })
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

            // --- Namespace ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            // --- XML Docs ---
            sb.AppendLine("    /// <summary>");
            sb.AppendLine($"    /// {summary}");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    /// <remarks>");
            foreach (string line in remarks.Split('\n'))
                sb.AppendLine($"    /// {line.Trim()}");
            sb.AppendLine("    /// </remarks>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine("    /// Created automatically by <b>Entity Domain Generator</b>.");
            sb.AppendLine("    /// </remarks>");

            // --- Attributes (для оптимизированного режима) ---
            if (mode == EntityBakerMode.Optimized)
                sb.AppendLine($"    [RequireComponent(typeof({entityType}View))]");

            // --- Класс ---
            sb.AppendLine($"    public abstract class {className} : {baseType}");
            sb.AppendLine("    {");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
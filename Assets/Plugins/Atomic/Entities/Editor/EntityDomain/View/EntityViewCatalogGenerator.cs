using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityViewCatalogGenerator
    {
        public static void GenerateFile(
            string entityType,
            string interfaceType,
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

            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, $"{entityType}ViewCatalog.cs");
            string content = GenerateContent(entityType, interfaceType, ns, imports);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static string GenerateContent(string entityType, string interfaceType, string ns, string[] imports)
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

            // --- XML Documentation ---
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// A Unity <see cref=\"ScriptableObject\"/> catalog that maps entity identifiers to their corresponding view prefabs.");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine($"    /// This catalog defines the available views for <see cref=\"{interfaceType}\"/> entities.");
            sb.AppendLine("    /// It is used by runtime view pools and factories to spawn and manage the correct prefab instances.");
            sb.AppendLine("    /// </remarks>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine("    /// Created automatically by <b>Entity Domain Generator</b>.");
            sb.AppendLine("    /// </remarks>");

            // --- Attributes + Class Declaration ---
            sb.AppendLine("    [CreateAssetMenu(");
            sb.AppendLine($"        fileName = \"{entityType}ViewCatalog\",");
            sb.AppendLine($"        menuName = \"{ns.Replace('.', '/')}/New {entityType}ViewCatalog\"");
            sb.AppendLine("    )]");
            sb.AppendLine($"    public sealed class {entityType}ViewCatalog : EntityViewCatalog<{interfaceType}, {entityType}View>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
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
            if (imports is {Length: > 0})
            {
                foreach (string import in imports.Where(i => !string.IsNullOrWhiteSpace(i)))
                {
                    string clean = import.Trim();
                    sb.AppendLine(clean.StartsWith("using") ? clean : $"using {clean};");
                }
            }

            sb.AppendLine();

            // --- Namespace + ViewCatalog ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
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
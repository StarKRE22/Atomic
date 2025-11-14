using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityViewGenerator
    {
        public static void Generate(
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

            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, $"{entityType}View.cs");
            string content = GenerateContent(entityType, entityInterface, ns, imports);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static string GenerateContent(string entityType, string entityInterface, string ns, string[] imports)
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
            sb.AppendLine("/**");
            sb.AppendLine(" * Created by Entity Domain Generator.");
            sb.AppendLine(" */");
            sb.AppendLine();

            // --- Namespace ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            // --- XML Documentation ---
            sb.AppendLine("    /// <summary>");
            sb.AppendLine($"    /// Represents a concrete Unity view bound to the <see cref=\"{entityInterface}\"/> entity type.");
            sb.AppendLine("    /// </summary>");

            // --- Class Declaration ---
            sb.AppendLine($"    public class {entityType}View : EntityView<{entityInterface}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
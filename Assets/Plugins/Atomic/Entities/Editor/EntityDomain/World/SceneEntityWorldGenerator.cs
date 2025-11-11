using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    public static class SceneEntityWorldGenerator
    {
        public static void GenerateFile(string entityType, string ns, string[] imports, string directory)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                EditorUtility.DisplayDialog("Error", "Entity Type cannot be empty.", "OK");
                return;
            }

            try
            {
                Directory.CreateDirectory(directory);
                string filePath = Path.Combine(directory, $"{entityType}World.cs");
                string content = GenerateContent(ns, entityType, imports);

                File.WriteAllText(filePath, content, Encoding.UTF8);
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Success", $"World generated successfully:\n{filePath}", "OK");
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog("Error", $"Generation failed:\n{ex.Message}", "OK");
            }
        }

        private static string GenerateContent(string ns, string entityType, string[] imports)
        {
            var sb = new StringBuilder();

            // --- Imports ---
            sb.AppendLine("using Atomic.Entities;");
            if (imports is { Length: > 0 })
            {
                foreach (string import in imports.Where(i => !string.IsNullOrWhiteSpace(i)))
                {
                    string clean = import.Trim();
                    sb.AppendLine(clean.StartsWith("using") ? clean : $"using {clean};");
                }
            }

            sb.AppendLine();

            // --- Namespace + World Class ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"    public sealed class {entityType}World : SceneEntityWorld<{entityType}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}

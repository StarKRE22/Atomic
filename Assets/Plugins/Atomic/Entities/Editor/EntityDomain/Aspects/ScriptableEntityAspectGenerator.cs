using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class ScriptableEntityAspectGenerator
    {
        public static void GenerateFile(
            string entityType,
            string ns,
            string directory,
            string[] imports,
            bool hasPrefix = false
        )
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                EditorUtility.DisplayDialog("Error", "Entity Type cannot be empty.", "OK");
                return;
            }

            string prefix = hasPrefix ? "Scriptable" : string.Empty;
            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, $"{prefix}{entityType}Aspect.cs");
            string content = GenerateContent(ns, entityType, imports, prefix);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static string GenerateContent(string ns, string entityType, string[] imports, string prefix)
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

            // --- Namespace + Aspect Class ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"    public abstract class {prefix}{entityType}Aspect : ScriptableEntityAspect<{entityType}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
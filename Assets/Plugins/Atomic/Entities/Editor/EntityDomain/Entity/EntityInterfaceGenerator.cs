using System;
using System.IO;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityInterfaceGenerator
    {
        private const string Indent = "    ";

        public static void GenerateFile(string interfaceType, string ns, string[] imports, string directory)
        {
            if (string.IsNullOrWhiteSpace(interfaceType))
            {
                EditorUtility.DisplayDialog("Error", "Interface name cannot be empty.", "OK");
                return;
            }

            try
            {
                Directory.CreateDirectory(directory);
                string filePath = Path.Combine(directory, $"{interfaceType}.cs");
                string content = GenerateContent(ns, imports, interfaceType);
                File.WriteAllText(filePath, content, Encoding.UTF8);

                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("Success", $"Interface generated successfully:\n{filePath}", "OK");
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog("Error", "Generation failed:\n" + ex.Message, "OK");
            }
        }

        private static string GenerateContent(string ns, string[] imports, string interfaceName)
        {
            var sb = new StringBuilder();

            // --- Imports ---
            sb.AppendLine("using Atomic.Entities;");
            
            if (imports is { Length: > 0 })
            {
                foreach (string import in imports)
                {
                    if (string.IsNullOrWhiteSpace(import))
                        continue;

                    string clean = import.Trim();
                    sb.AppendLine(!clean.StartsWith("using") ? $"using {clean};" : clean);
                }
            }

            // Обязательно добавим пустую строку между using и namespace
            sb.AppendLine();

            // --- Namespace + Interface ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"{Indent}public interface {interfaceName} : IEntity");
            sb.AppendLine($"{Indent}{{");
            sb.AppendLine($"{Indent}}}");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}

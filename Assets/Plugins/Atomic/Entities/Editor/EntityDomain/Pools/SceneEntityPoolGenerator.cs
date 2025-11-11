using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class SceneEntityPoolGenerator
    {
        public static void GenerateFile(
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

            try
            {
                Directory.CreateDirectory(directory);
                string filePath = Path.Combine(directory, $"{entityType}Pool.cs");
                string content = GenerateContent(entityType, interfaceType, ns, imports);

                File.WriteAllText(filePath, content, Encoding.UTF8);
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Success", $"Pool generated successfully:\n{filePath}", "OK");
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog("Error", $"Generation failed:\n{ex.Message}", "OK");
            }
        }

        private static string GenerateContent(string entityType, string interfaceType, string ns, string[] imports)
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

            // --- Namespace + Pool Class ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
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
    }
}
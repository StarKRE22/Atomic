using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class ScriptableEntityFactoryGenerator
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
                string filePath = Path.Combine(directory, $"{entityType}Factory.cs");
                string content = GenerateContent(entityType, interfaceType, ns, imports);

                File.WriteAllText(filePath, content, Encoding.UTF8);
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Success", $"Factory generated successfully:\n{filePath}", "OK");
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog("Error", $"Generation failed:\n{ex.Message}", "OK");
            }
        }

        private static string GenerateContent(
            string entityType,
            string interfaceType,
            string ns,
            string[] imports
        )
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

            // --- Namespace + Scriptable Factory ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"   public abstract class {entityType}Factory : ScriptableEntityFactory<{interfaceType}>");
            sb.AppendLine("    {");
            sb.AppendLine("        public string Name => this.name;");
            sb.AppendLine();
            sb.AppendLine($"       public sealed override {interfaceType} Create()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var entity = new {entityType}(");
            sb.AppendLine("                this.Name,");
            sb.AppendLine("                this.initialTagCapacity,");
            sb.AppendLine("                this.initialValueCapacity,");
            sb.AppendLine("                this.initialBehaviourCapacity");
            sb.AppendLine("            );");
            sb.AppendLine("            this.Install(entity);");
            sb.AppendLine("            return entity;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        protected abstract void Install({interfaceType} entity);");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
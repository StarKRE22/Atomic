using UnityEditor;
using System;
using System.IO;
using System.Text;

namespace Atomic.Entities
{
    internal static class EntityBehaviourGenerator
    {
        private const string Indent = "    ";

        private static readonly string[] EventNames =
        {
            "Init", "Enable", "Disable", "Dispose",
            "Tick", "FixedTick", "LateTick", "Gizmos"
        };

        private static readonly string[] BaseInterfaces =
        {
            "IEntityInit", "IEntityEnable", "IEntityDisable", "IEntityDispose",
            "IEntityTick", "IEntityFixedTick", "IEntityLateTick", "IEntityGizmos"
        };

        public static void GenerateFile(string entityType, string ns, string[] imports, string directory)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                EditorUtility.DisplayDialog("Error", "Entity Type cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(entityType))
                throw new ArgumentException("EntityType cannot be empty.", nameof(entityType));

            try
            {
                Directory.CreateDirectory(directory);
                string filePath = Path.Combine(directory, $"{entityType}Behaviours.cs");
                string content = GenerateContent(ns, imports, entityType);
                File.WriteAllText(filePath, content, Encoding.UTF8);

                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("Success", $"File generated successfully:\n{filePath}", "OK");
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog("Error", "Generation failed:\n" + ex.Message, "OK");
            }
        }

        private static string GenerateContent(string ns, string[] imports, string entityType)
        {
            var sb = new StringBuilder();

            // --- Imports ---
            sb.AppendLine("using Atomic.Entities;");

            if (imports is {Length: > 0})
            {
                foreach (string import in imports)
                {
                    if (string.IsNullOrWhiteSpace(import))
                        continue;

                    string clean = import.Trim();
                    sb.AppendLine(!clean.StartsWith("using") ? $"using {clean};" : clean);
                }
            }

            sb.AppendLine();

            // --- Namespace + Body ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            for (int i = 0; i < BaseInterfaces.Length; i++)
            {
                sb.AppendLine($"{Indent}public interface {entityType}{EventNames[i]} : {BaseInterfaces[i]}<{entityType}>");
                sb.AppendLine($"{Indent}{{");
                sb.AppendLine($"{Indent}}}");
                sb.AppendLine();
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
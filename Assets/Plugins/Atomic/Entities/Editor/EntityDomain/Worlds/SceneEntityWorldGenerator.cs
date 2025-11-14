using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class SceneEntityWorldGenerator
    {
        public static void Generate(string entityType, string ns, string[] imports, string directory)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                EditorUtility.DisplayDialog("Error", "Entity Type cannot be empty.", "OK");
                return;
            }

            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, $"{entityType}World.cs");
            string content = GenerateContent(ns, entityType, imports);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
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
            sb.AppendLine("/**");
            sb.AppendLine(" * Created by Entity Domain Generator.");
            sb.AppendLine(" */");
            sb.AppendLine();

            // --- Namespace + World Class ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine("    /// <summary>");
            sb.AppendLine($"    /// A Unity-integrated world manager that handles all <see cref=\"{entityType}\"/> entities in the current scene.");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine($"    /// This component hooks into Unity’s lifecycle (Awake, OnEnable, Update, OnDisable, etc.) to automatically");
            sb.AppendLine($"    /// manage creation, updates, and disposal of <see cref=\"{entityType}\"/> entities at runtime.");
            sb.AppendLine("    /// </remarks>");
            sb.AppendLine("    /// <example>");
            sb.AppendLine($"    /// Attach this component to a GameObject in your scene to automatically discover and manage all <see cref=\"{entityType}\"/> entities.");
            sb.AppendLine("    /// </example>");
            sb.AppendLine($"    public sealed class {entityType}World : SceneEntityWorld<{entityType}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
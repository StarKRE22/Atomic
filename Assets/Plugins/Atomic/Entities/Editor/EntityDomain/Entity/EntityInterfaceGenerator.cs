using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityInterfaceGenerator
    {
        private const string Indent = "    ";

        public static void Generate(string interfaceType, string ns, string[] imports, string directory)
        {
            if (string.IsNullOrWhiteSpace(interfaceType))
            {
                EditorUtility.DisplayDialog("Error", "Interface name cannot be empty.", "OK");
                return;
            }

            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, $"{interfaceType}.cs");
            string content = GenerateContent(ns, imports, interfaceType);
            File.WriteAllText(filePath, content, Encoding.UTF8);

            AssetDatabase.Refresh();
        }

        private static string GenerateContent(string ns, string[] imports, string interfaceName)
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

            // --- Generator comment ---
            sb.AppendLine("/**");
            sb.AppendLine(" * Created by Entity Domain Generator.");
            sb.AppendLine(" */");
            sb.AppendLine();

            // --- Namespace + Interface ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"{Indent}/// <summary>");
            sb.AppendLine($"{Indent}/// Represents a specialized entity interface that extends the core <see cref=\"IEntity\"/> contract.");
            sb.AppendLine($"{Indent}/// It follows the Entity–State–Behaviour architectural pattern, providing structure for identity (tags),");
            sb.AppendLine($"{Indent}/// data (values), and modular behaviours within the Atomic Entity framework.");
            sb.AppendLine($"{Indent}/// </summary>");
            sb.AppendLine($"{Indent}/// <remarks>");
            sb.AppendLine($"{Indent}/// Created by <b>Entity Domain Generator</b>.");
            sb.AppendLine($"{Indent}/// </remarks>");
            sb.AppendLine($"{Indent}public interface {interfaceName} : IEntity");
            sb.AppendLine($"{Indent}{{");
            sb.AppendLine($"{Indent}}}");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class SceneEntityProxyGenerator
    {
        public static void Generate(
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
            string filePath = Path.Combine(directory, $"{entityType}Proxy.cs");
            string content = GenerateContent(entityType, interfaceType, ns, imports);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
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
            sb.AppendLine("/**");
            sb.AppendLine(" * Created by Entity Domain Generator.");
            sb.AppendLine(" */");
            sb.AppendLine();

            // --- Namespace + Proxy Class ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            sb.AppendLine("    /// <summary>");
            sb.AppendLine(
                $"    /// A Unity <see cref=\"MonoBehaviour\"/> proxy that forwards all <see cref=\"{interfaceType}\"/> calls to an underlying <see cref=\"{entityType}\"/> entity."
            );
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine(
                $"    /// This proxy allows interacting with an <see cref=\"{interfaceType}\"/> instance inside the Unity scene while decoupling logic from GameObjects.");
            sb.AppendLine(
                "    /// It acts as a transparent forwarder for all entity functionality — values, lifecycle, tags, and behaviours.");
            sb.AppendLine("    ///");
            sb.AppendLine(
                "    /// Use this component to expose scene-level access to an entity while keeping logic modular and testable.");
            sb.AppendLine("    ///");
            sb.AppendLine("    /// **Collider Interaction Note**:");
            sb.AppendLine("    /// If your entity includes multiple colliders (e.g., hitboxes or triggers),");
            sb.AppendLine(
                $"    /// place <c>{entityType}Proxy</c> on each and reference the same source <see cref=\"{entityType}\"/>.");
            sb.AppendLine("    /// This provides unified access regardless of which collider was hit.");
            sb.AppendLine("    ///");
            sb.AppendLine("    /// <example>");
            sb.AppendLine(
                $"    /// Example: Detecting hits from any collider on an <see cref=\"{interfaceType}\"/> entity:");
            sb.AppendLine("    /// <code>");
            sb.AppendLine("    /// void OnTriggerEnter(Collider other)");
            sb.AppendLine("    /// {");
            sb.AppendLine($"    ///     if (other.TryGetComponent(out {interfaceType} proxy))");
            sb.AppendLine("    ///     {");
            sb.AppendLine($"    ///         Debug.Log($\"Hit entity: {entityType}\");");
            sb.AppendLine("    ///     }");
            sb.AppendLine("    /// }");
            sb.AppendLine("    /// </code>");
            sb.AppendLine("    /// </example>");
            sb.AppendLine("    /// </remarks>");
            sb.AppendLine(
                $"    public sealed class {entityType}Proxy : SceneEntityProxy<{entityType}>, {interfaceType}");
            sb.AppendLine("    {");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
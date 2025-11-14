using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityInstallerGenerator
    {
        public static void Generate(
            EntityInstallerMode mode,
            string entityType,
            string entityInterface,
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

            if (string.IsNullOrWhiteSpace(entityInterface))
            {
                EditorUtility.DisplayDialog("Error", "Entity Interface cannot be empty.", "OK");
                return;
            }

            bool scriptableInstallerRequired = mode.HasFlag(EntityInstallerMode.ScriptableEntityInstaller);
            bool sceneInstallerRequired = mode.HasFlag(EntityInstallerMode.SceneEntityInstaller);
            bool iInstallerRequired = mode.HasFlag(EntityInstallerMode.IEntityInstaller);

            bool bothRequired = scriptableInstallerRequired && sceneInstallerRequired;

            if (scriptableInstallerRequired)
            {
                GenerateFile(
                    entityType,
                    entityInterface,
                    ns,
                    directory,
                    imports,
                    EntityInstallerMode.ScriptableEntityInstaller,
                    bothRequired
                );
            }

            if (sceneInstallerRequired)
            {
                GenerateFile(
                    entityType,
                    entityInterface,
                    ns,
                    directory,
                    imports,
                    EntityInstallerMode.SceneEntityInstaller,
                    bothRequired
                );
            }

            if (iInstallerRequired)
            {
                GenerateInterfaceFile(entityType, entityInterface, ns, directory, imports);
            }
        }

        private static void GenerateFile(
            string entityType,
            string entityInterface,
            string ns,
            string directory,
            string[] imports,
            EntityInstallerMode mode,
            bool usePrefix
        )
        {
            string prefix;
            string baseType;
            string summary;
            string remarks;

            switch (mode)
            {
                case EntityInstallerMode.SceneEntityInstaller:
                    prefix = usePrefix ? "Scene" : string.Empty;
                    baseType = "SceneEntityInstaller";
                    summary =
                        "A Unity <see cref=\"MonoBehaviour\"/> that can be attached to a GameObject " +
                        $"to perform installation logic on an <see cref=\"{entityInterface}\"/> during runtime or initialization.";
                    remarks =
                        "Used to declaratively configure entities placed in a scene.  \n" +
                        "In the Editor, it supports automatic refresh via <c>OnValidate</c>.";
                    break;

                case EntityInstallerMode.ScriptableEntityInstaller:
                    prefix = usePrefix ? "Scriptable" : string.Empty;
                    baseType = "ScriptableEntityInstaller";
                    summary =
                        "A Unity <see cref=\"ScriptableObject\"/> that defines reusable logic for installing or configuring " +
                        $"an <see cref=\"{entityInterface}\"/>.";
                    remarks =
                        "This is useful for defining shared configuration logic that can be applied to multiple entities, " +
                        "such as setting default values, tags, or attaching behaviors.  \n" +
                        "Supports both runtime and edit-time contexts via utility methods.";
                    break;

                case EntityInstallerMode.None:
                case EntityInstallerMode.IEntityInstaller:
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported installer mode.");
            }

            Directory.CreateDirectory(directory);

            string fileName = $"{prefix}{entityType}Installer.cs";
            string filePath = Path.Combine(directory, fileName);
            string content = GenerateContent(entityType, entityInterface, ns, imports, prefix, baseType, summary,
                remarks);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static void GenerateInterfaceFile(
            string entityType,
            string entityInterface,
            string ns,
            string directory,
            string[] imports
        )
        {
            Directory.CreateDirectory(directory);

            string fileName = $"I{entityType}Installer.cs";
            string filePath = Path.Combine(directory, fileName);

            var sb = new StringBuilder();

            // --- Header ---
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

            // --- Namespace ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine("    /// <summary>");
            sb.AppendLine(
                $"    /// Interface for installing and configuring an <see cref=\"{entityInterface}\"/> instance.");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine($"    public interface I{entityType}Installer : IEntityInstaller<{entityInterface}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static string GenerateContent(
            string entityType,
            string entityInterface,
            string ns,
            string[] imports,
            string prefix,
            string baseType,
            string summary,
            string remarks
        )
        {
            var sb = new StringBuilder();

            // --- Header ---
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

            // --- Namespace ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            // --- XML Docs ---
            sb.AppendLine("    /// <summary>");
            sb.AppendLine($"    /// {summary}");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    /// <remarks>");
            foreach (string line in remarks.Split('\n'))
                sb.AppendLine($"    /// {line.Trim()}");
            sb.AppendLine("    /// </remarks>");

            // --- Class ---
            sb.AppendLine($"    public abstract class {prefix}{entityType}Installer : {baseType}<{entityInterface}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
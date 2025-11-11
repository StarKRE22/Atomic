using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityAspectGenerator
    {
        public static void GenerateAspects(
            AspectMode mode,
            string concreteType,
            string ns,
            string directory,
            string[] imports
        )
        {
            bool scriptableAspectRequired = mode.HasFlag(AspectMode.ScriptableEntityAspect);
            bool sceneAspectRequired = mode.HasFlag(AspectMode.SceneEntityAspect);
            bool bothRequired = scriptableAspectRequired && sceneAspectRequired;

            // Если оба флага включены — используем префиксы ("Scene" / "Scriptable")
            // Если только один — без префиксов

            if (scriptableAspectRequired)
            {
                GenerateFile(
                    concreteType,
                    ns,
                    directory,
                    imports,
                    AspectMode.ScriptableEntityAspect,
                    bothRequired
                );
            }

            if (sceneAspectRequired)
            {
                GenerateFile(
                    concreteType,
                    ns,
                    directory,
                    imports,
                    AspectMode.SceneEntityAspect,
                    bothRequired
                );
            }
        }

        private static void GenerateFile(
            string entityType,
            string ns,
            string directory,
            string[] imports,
            AspectMode mode,
            bool usePrefix
        )
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                EditorUtility.DisplayDialog("Error", "Entity Type cannot be empty.", "OK");
                return;
            }

            string prefix;
            string baseType;
            string summary;
            string remarks;

            // Определяем, какой тип аспекта создаётся
            switch (mode)
            {
                case AspectMode.SceneEntityAspect:
                    prefix = usePrefix ? "Scene" : string.Empty;
                    baseType = "SceneEntityAspect";
                    summary =
                        "Represents a scene-based entity aspect that can be applied or discarded on a specific entity type.";
                    remarks =
                        "Inherit from this class to create reusable <see cref=\"MonoBehaviour\"/> components that encapsulate " +
                        "logic to apply and discard behaviors or properties on <see cref=\"" + entityType +
                        "\"/> entities during runtime. " +
                        "Attach this component to a GameObject in your scene to use it.";
                    break;

                case AspectMode.ScriptableEntityAspect:
                    prefix = usePrefix ? "Scriptable" : string.Empty;
                    baseType = "ScriptableEntityAspect";
                    summary =
                        "Represents a scriptable-based entity aspect that can be applied or discarded on a specific entity type.";
                    remarks =
                        "Inherit from this class to create reusable <see cref=\"ScriptableObject\"/> assets that encapsulate " +
                        "logic to apply and discard behaviors or properties on <see cref=\"" + entityType +
                        "\"/> entities during runtime. " +
                        "Create and configure instances via the Unity project assets.";
                    break;

                case AspectMode.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported aspect mode.");
            }

            string fileName = $"{prefix}{entityType}Aspect.cs";
            string filePath = Path.Combine(directory, fileName);
            string content = GenerateContent(ns, entityType, imports, prefix, baseType, summary, remarks);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static string GenerateContent(
            string ns,
            string entityType,
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

            // --- Namespace ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            // --- XML Docs ---
            sb.AppendLine("    /// <summary>");
            sb.AppendLine($"    /// {summary}");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine($"    /// {remarks}");
            sb.AppendLine("    /// </remarks>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine("    /// Created automatically by <b>Entity Domain Generator</b>.");
            sb.AppendLine("    /// </remarks>");

            // --- Class ---
            sb.AppendLine($"    public abstract class {prefix}{entityType}Aspect : {baseType}<{entityType}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
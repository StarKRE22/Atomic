using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityAspectGenerator
    {
        public static void Generate(
            EntityAspectMode mode,
            string concreteType,
            string interfaceType,
            string ns,
            string directory,
            string[] imports
        )
        {
            if (string.IsNullOrWhiteSpace(concreteType))
            {
                EditorUtility.DisplayDialog("Error", "Concrete Entity Type cannot be empty.", "OK");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(interfaceType))
            {
                EditorUtility.DisplayDialog("Error", "Interface Entity Type cannot be empty.", "OK");
                return;
            }
            
            bool scriptableAspectRequired = mode.HasFlag(EntityAspectMode.ScriptableEntityAspect);
            bool sceneAspectRequired = mode.HasFlag(EntityAspectMode.SceneEntityAspect);
            bool bothRequired = scriptableAspectRequired && sceneAspectRequired;

            if (scriptableAspectRequired)
            {
                GenerateFile(
                    concreteType,
                    interfaceType,
                    ns,
                    directory,
                    imports,
                    EntityAspectMode.ScriptableEntityAspect,
                    bothRequired
                );
            }

            if (sceneAspectRequired)
            {
                GenerateFile(
                    concreteType,
                    interfaceType,
                    ns,
                    directory,
                    imports,
                    EntityAspectMode.SceneEntityAspect,
                    bothRequired
                );
            }
        }

        private static void GenerateFile(
            string concreteType,
            string interfaceType,
            string ns,
            string directory,
            string[] imports,
            EntityAspectMode mode,
            bool usePrefix
        )
        {
            string prefix;
            string baseType;
            string summary;
            string remarks;

            switch (mode)
            {
                case EntityAspectMode.SceneEntityAspect:
                    prefix = usePrefix ? "Scene" : string.Empty;
                    baseType = "SceneEntityAspect";
                    summary =
                        "Represents a scene-based entity aspect that can be applied or discarded on a specific entity type.";
                    remarks =
                        "Inherit from this class to create reusable <see cref=\"MonoBehaviour\"/> components that encapsulate " +
                        "logic to apply and discard behaviors or properties on <see cref=\"" + concreteType +
                        "\"/> entities during runtime. " +
                        "Attach this component to a GameObject in your scene to use it.";
                    break;

                case EntityAspectMode.ScriptableEntityAspect:
                    prefix = usePrefix ? "Scriptable" : string.Empty;
                    baseType = "ScriptableEntityAspect";
                    summary =
                        "Represents a scriptable-based entity aspect that can be applied or discarded on a specific entity type.";
                    remarks =
                        "Inherit from this class to create reusable <see cref=\"ScriptableObject\"/> assets that encapsulate " +
                        "logic to apply and discard behaviors or properties on <see cref=\"" + concreteType +
                        "\"/> entities during runtime. " +
                        "Create and configure instances via the Unity project assets.";
                    break;

                case EntityAspectMode.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported aspect mode.");
            }

            Directory.CreateDirectory(directory);
            
            string fileName = $"{prefix}{concreteType}Aspect.cs";
            string filePath = Path.Combine(directory, fileName);
            string content = GenerateContent(concreteType, interfaceType, ns, imports, prefix, baseType, summary, remarks);

            File.WriteAllText(filePath, content, Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        private static string GenerateContent(
            string concreteType,
            string interfaceType,
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
            sb.AppendLine($"    /// {remarks}");
            sb.AppendLine("    /// </remarks>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine("    /// Created automatically by <b>Entity Domain Generator</b>.");
            sb.AppendLine("    /// </remarks>");

            // --- Class ---
            sb.AppendLine($"    public abstract class {prefix}{concreteType}Aspect : {baseType}<{interfaceType}>");
            sb.AppendLine("    {");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
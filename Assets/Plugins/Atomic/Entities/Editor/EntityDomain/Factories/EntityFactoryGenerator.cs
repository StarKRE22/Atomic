using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityFactoryGenerator
    {
        public static void Generate(
            EntityFactoryMode mode,
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

            bool scriptableRequired = mode.HasFlag(EntityFactoryMode.ScriptableEntityFactory);
            bool sceneRequired = mode.HasFlag(EntityFactoryMode.SceneEntityFactory);
            bool bothRequired = scriptableRequired && sceneRequired;

            if (scriptableRequired)
            {
                GenerateFile(
                    concreteType,
                    interfaceType,
                    ns,
                    directory,
                    imports,
                    EntityFactoryMode.ScriptableEntityFactory,
                    bothRequired
                );
            }

            if (sceneRequired)
            {
                GenerateFile(
                    concreteType,
                    interfaceType,
                    ns,
                    directory,
                    imports,
                    EntityFactoryMode.SceneEntityFactory,
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
            EntityFactoryMode mode,
            bool usePrefix
        )
        {
            string prefix;
            string baseType;
            string summary;
            string remarks;
            string example;

            switch (mode)
            {
                case EntityFactoryMode.SceneEntityFactory:
                    prefix = usePrefix ? "Scene" : string.Empty;
                    baseType = "SceneEntityFactory";
                    summary =
                        $"A Unity-integrated factory for creating scene-based <see cref=\"{concreteType}\"/> entities.";
                    remarks =
                        $"Derive from this class to create runtime factories that instantiate <see cref=\"{concreteType}\"/> entities " +
                        $"directly within the current scene. It integrates with Unity’s object lifecycle and can be attached to GameObjects.";
                    example =
                        $"Attach this factory to a GameObject to dynamically create <see cref=\"{concreteType}\"/> entities at runtime.";
                    break;

                case EntityFactoryMode.ScriptableEntityFactory:
                    prefix = usePrefix ? "Scriptable" : string.Empty;
                    baseType = "ScriptableEntityFactory";
                    summary = $"A reusable asset-based factory for creating <see cref=\"{concreteType}\"/> entities.";
                    remarks =
                        $"Derive from this class to define reusable, configurable factories for <see cref=\"{concreteType}\"/> entities " +
                        $"that can be created from Unity’s asset system. Ideal for asset-driven design patterns using <see cref=\"ScriptableObject\"/>.";
                    example =
                        $"Create a new <see cref=\"ScriptableObject\"/> instance in your project to define a reusable <see cref=\"{concreteType}\"/> factory.";
                    break;

                case EntityFactoryMode.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported factory mode.");
            }

            Directory.CreateDirectory(directory);

            string fileName = $"{prefix}{concreteType}Factory.cs";
            string filePath = Path.Combine(directory, fileName);
            string content = GenerateContent(concreteType, interfaceType, ns, imports, prefix, baseType, summary, remarks, example);

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
            string remarks,
            string example
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
            sb.AppendLine("    /// <example>");
            sb.AppendLine($"    /// {example}");
            sb.AppendLine("    /// </example>");
            sb.AppendLine("    /// <remarks>");
            sb.AppendLine("    /// Created automatically by <b>Entity Domain Generator</b>.");
            sb.AppendLine("    /// </remarks>");

            // --- Class ---
            sb.AppendLine($"    public abstract class {prefix}{concreteType}Factory : {baseType}<{interfaceType}>");
            sb.AppendLine("    {");
            sb.AppendLine("        public string Name => this.name;");
            sb.AppendLine();
            sb.AppendLine($"        public sealed override {interfaceType} Create()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var entity = new {concreteType}(");
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
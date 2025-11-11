using System;
using System.IO;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityConcreteGenerator
    {
        private const string Indent = "    ";
        
        public static void GenerateFile(
            string className,
            string ns,
            string[] imports,
            string directory,
            BaseMode baseMode,
            string interfaceName
        )
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                EditorUtility.DisplayDialog("Error", "Class name cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(interfaceName))
            {
                EditorUtility.DisplayDialog("Error", "Interface name cannot be empty.", "OK");
                return;
            }

            try
            {
                Directory.CreateDirectory(directory);
                string filePath = Path.Combine(directory, $"{className}.cs");
                string content = GenerateContent(ns, imports, className, baseMode, interfaceName);
                File.WriteAllText(filePath, content, Encoding.UTF8);

                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("Success", $"Class generated successfully:\n{filePath}", "OK");
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog("Error", "Generation failed:\n" + ex.Message, "OK");
            }
        }

        private static string GenerateContent(
            string ns,
            string[] imports,
            string className,
            BaseMode baseMode,
            string interfaceName
        )
        {
            var sb = new StringBuilder();

            // --- Imports ---
            sb.AppendLine("using Atomic.Entities;");

            if (imports is { Length: > 0 })
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

            string baseClassDecl = GetBaseClassDeclaration(baseMode, className);
            sb.AppendLine($"{Indent}public sealed class {className} : {baseClassDecl}, {interfaceName}");
            sb.AppendLine($"{Indent}{{");

            string constructorBlock = GetConstructorBlock(baseMode, className);
            if (!string.IsNullOrEmpty(constructorBlock))
                sb.Append(constructorBlock);

            sb.AppendLine($"{Indent}}}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GetBaseClassDeclaration(BaseMode baseMode, string className)
        {
            return baseMode switch
            {
                BaseMode.Entity => "Entity",
                BaseMode.EntitySingleton => $"EntitySingleton<{className}>",
                BaseMode.SceneEntity => "SceneEntity",
                BaseMode.SceneEntitySingleton => $"SceneEntitySingleton<{className}>",
                _ => "Entity"
            };
        }

        private static string GetConstructorBlock(BaseMode baseMode, string className)
        {
            var sb = new StringBuilder();

            // Только Entity и EntitySingleton имеют расширенные конструкторы
            if (baseMode is BaseMode.Entity or BaseMode.EntitySingleton)
            {
                bool includeEmptyCtor = baseMode == BaseMode.EntitySingleton;

                if (includeEmptyCtor)
                {
                    sb.AppendLine($"{Indent}{Indent}public {className}()");
                    sb.AppendLine($"{Indent}{Indent}{{");
                    sb.AppendLine($"{Indent}{Indent}}}");
                    sb.AppendLine();
                }

                // === Constructor #1: generic string/object collections ===
                sb.AppendLine($"{Indent}{Indent}/// <summary>");
                sb.AppendLine($"{Indent}{Indent}/// Creates a new entity with the specified name, tags, values, behaviours, and optional settings.");
                sb.AppendLine($"{Indent}{Indent}/// </summary>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"name\">The name of the entity. If <c>null</c>, an empty string is used.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"tags\">Optional collection of tag identifiers.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"values\">Optional collection of key-value pairs.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"behaviours\">Optional collection of behaviours to attach.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"settings\">Optional entity settings. If <c>null</c>, <see cref=\"Settings.disposeValues\"/> defaults to <c>true</c>.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <remarks>");
                sb.AppendLine($"{Indent}{Indent}/// The constructor initializes internal capacities based on the provided collections,");
                sb.AppendLine($"{Indent}{Indent}/// then adds all specified tags, values, and behaviours immediately.");
                sb.AppendLine($"{Indent}{Indent}/// </remarks>");
                sb.AppendLine($"{Indent}{Indent}public {className}(");
                sb.AppendLine($"{Indent}{Indent}    string name,");
                sb.AppendLine($"{Indent}{Indent}    IEnumerable<string> tags,");
                sb.AppendLine($"{Indent}{Indent}    IEnumerable<KeyValuePair<string, object>> values,");
                sb.AppendLine($"{Indent}{Indent}    IEnumerable<IEntityBehaviour> behaviours,");
                sb.AppendLine($"{Indent}{Indent}    Settings? settings = null");
                sb.AppendLine($"{Indent}{Indent}) : base(name, tags, values, behaviours, settings)");
                sb.AppendLine($"{Indent}{Indent}{{");
                sb.AppendLine($"{Indent}{Indent}}}");
                sb.AppendLine();

                // === Constructor #2: int-based variant ===
                sb.AppendLine($"{Indent}{Indent}public {className}(");
                sb.AppendLine($"{Indent}{Indent}    string name,");
                sb.AppendLine($"{Indent}{Indent}    IEnumerable<int> tags,");
                sb.AppendLine($"{Indent}{Indent}    IEnumerable<KeyValuePair<int, object>> values,");
                sb.AppendLine($"{Indent}{Indent}    IEnumerable<IEntityBehaviour> behaviours,");
                sb.AppendLine($"{Indent}{Indent}    Settings? settings = null");
                sb.AppendLine($"{Indent}{Indent}) : base(name, tags, values, behaviours, settings)");
                sb.AppendLine($"{Indent}{Indent}{{");
                sb.AppendLine($"{Indent}{Indent}}}");
                sb.AppendLine();

                // === Constructor #3: capacity-based ===
                sb.AppendLine($"{Indent}{Indent}/// <summary>");
                sb.AppendLine($"{Indent}{Indent}/// Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.");
                sb.AppendLine($"{Indent}{Indent}/// </summary>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"name\">The name of the entity. If <c>null</c>, an empty string is used.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"tagCapacity\">Initial capacity for tag storage to minimize memory allocations.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"valueCapacity\">Initial capacity for value storage to minimize memory allocations.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"behaviourCapacity\">Initial capacity for behaviour storage to minimize memory allocations.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <param name=\"settings\">Optional entity settings. If <c>null</c>, <see cref=\"Settings.disposeValues\"/> defaults to <c>true</c>.</param>");
                sb.AppendLine($"{Indent}{Indent}/// <remarks>");
                sb.AppendLine($"{Indent}{Indent}/// Pre-allocates internal structures for efficient usage and registers the entity in <see cref=\"EntityRegistry\"/>.");
                sb.AppendLine($"{Indent}{Indent}/// </remarks>");
                sb.AppendLine($"{Indent}{Indent}public {className}(");
                sb.AppendLine($"{Indent}{Indent}    string name = null,");
                sb.AppendLine($"{Indent}{Indent}    int tagCapacity = 0,");
                sb.AppendLine($"{Indent}{Indent}    int valueCapacity = 0,");
                sb.AppendLine($"{Indent}{Indent}    int behaviourCapacity = 0,");
                sb.AppendLine($"{Indent}{Indent}    Settings? settings = null");
                sb.AppendLine($"{Indent}{Indent}) : base(name, tagCapacity, valueCapacity, behaviourCapacity, settings)");
                sb.AppendLine($"{Indent}{Indent}{{");
                sb.AppendLine($"{Indent}{Indent}}}");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
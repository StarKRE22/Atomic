using System.IO;
using System.Text;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class EntityConcreteGenerator
    {
        private const string Indent = "    ";

        public static void Generate(
            EntityMode entityMode,
            string concreteType,
            string interfaceType,
            string ns,
            string[] imports,
            string directory
        )
        {
            if (string.IsNullOrWhiteSpace(concreteType))
            {
                EditorUtility.DisplayDialog("Error", "Class name cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(interfaceType))
            {
                EditorUtility.DisplayDialog("Error", "Interface name cannot be empty.", "OK");
                return;
            }

            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, $"{concreteType}.cs");
            string content = GenerateContent(entityMode, concreteType, interfaceType, ns, imports);
            File.WriteAllText(filePath, content, Encoding.UTF8);

            AssetDatabase.Refresh();
        }

        private static string GenerateContent(
            EntityMode entityMode,
            string concreteType,
            string interfaceType,
            string ns,
            string[] imports
        )
        {
            var sb = new StringBuilder();

            // --- Imports ---
            sb.AppendLine("using Atomic.Entities;");

            if (entityMode.HasFlag(EntityMode.Entity) || entityMode.HasFlag(EntityMode.EntitySingleton)) 
                sb.AppendLine("using System.Collections.Generic;");
            
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
            sb.AppendLine("/**");
            sb.AppendLine(" * Created by Entity Domain Generator.");
            sb.AppendLine(" */");
            sb.AppendLine();

            // --- Namespace ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            // --- XML summary based on entity type ---
            string summary = GetSummary(entityMode, interfaceType);
            sb.Append(summary);

            string baseClassDecl = GetBaseClassDeclaration(entityMode, concreteType);
            sb.AppendLine($"{Indent}public sealed class {concreteType} : {baseClassDecl}, {interfaceType}");
            sb.AppendLine($"{Indent}{{");

            string constructorBlock = GetConstructorBlock(entityMode, concreteType);
            if (!string.IsNullOrEmpty(constructorBlock))
                sb.Append(constructorBlock);

            sb.AppendLine($"{Indent}}}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        // --- Entity type description ---
        private static string GetSummary(EntityMode mode, string interfaceName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Indent}/// <summary>");

            switch (mode)
            {
                case EntityMode.Entity:
                    sb.AppendLine(
                        $"{Indent}/// Represents the core implementation of an <see cref=\"{interfaceName}\"/> in the framework.");
                    sb.AppendLine(
                        $"{Indent}/// This class follows the Entity–State–Behaviour pattern, providing a modular container");
                    sb.AppendLine($"{Indent}/// for dynamic state, tags, behaviours, and lifecycle management.");
                    break;

                case EntityMode.EntitySingleton:
                    sb.AppendLine($"{Indent}/// Abstract base class for singleton entities.");
                    sb.AppendLine(
                        $"{Indent}/// Ensures a single globally accessible instance of type <typeparamref name=\"E\"/>.");
                    sb.AppendLine($"{Indent}/// Supports both default constructor and factory-based creation.");
                    break;

                case EntityMode.SceneEntity:
                    sb.AppendLine(
                        $"{Indent}/// Represents a Unity <see cref=\"SceneEntity\"/> implementation for <see cref=\"{interfaceName}\"/>.");
                    sb.AppendLine(
                        $"{Indent}/// This component can be instantiated directly in a Scene and composed via the Unity Inspector.");
                    break;

                case EntityMode.SceneEntitySingleton:
                    sb.AppendLine(
                        $"{Indent}/// A base class for singleton scene entities. Ensures a single instance of the entity exists");
                    sb.AppendLine(
                        $"{Indent}/// per scene or globally, depending on the <see cref=\"_dontDestroyOnLoad\"/> flag.");
                    break;

                default:
                    sb.AppendLine($"{Indent}/// Represents a generated entity implementation.");
                    break;
            }

            sb.AppendLine($"{Indent}/// </summary>");
            return sb.ToString();
        }

        private static string GetBaseClassDeclaration(EntityMode entityMode, string className)
        {
            return entityMode switch
            {
                EntityMode.Entity => "Entity",
                EntityMode.EntitySingleton => $"EntitySingleton<{className}>",
                EntityMode.SceneEntity => "SceneEntity",
                EntityMode.SceneEntitySingleton => $"SceneEntitySingleton<{className}>",
                _ => "Entity"
            };
        }

        private static string GetConstructorBlock(EntityMode entityMode, string className)
        {
            var sb = new StringBuilder();

            // Только Entity и EntitySingleton имеют расширенные конструкторы
            if (entityMode is EntityMode.Entity or EntityMode.EntitySingleton)
            {
                bool includeEmptyCtor = entityMode == EntityMode.EntitySingleton;

                if (includeEmptyCtor)
                {
                    sb.AppendLine($"{Indent}{Indent}public {className}()");
                    sb.AppendLine($"{Indent}{Indent}{{");
                    sb.AppendLine($"{Indent}{Indent}}}");
                    sb.AppendLine();
                }

                sb.AppendLine($"{Indent}{Indent}/// <summary>");
                sb.AppendLine(
                    $"{Indent}{Indent}/// Creates a new entity with the specified name, tags, values, behaviours, and optional settings.");
                sb.AppendLine($"{Indent}{Indent}/// </summary>");
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

                sb.AppendLine($"{Indent}{Indent}/// <summary>");
                sb.AppendLine(
                    $"{Indent}{Indent}/// Creates a new entity with the specified name, tags, values, behaviours, and optional settings.");
                sb.AppendLine($"{Indent}{Indent}/// </summary>");
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

                sb.AppendLine($"{Indent}{Indent}/// <summary>");
                sb.AppendLine(
                    $"{Indent}{Indent}/// Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.");
                sb.AppendLine($"{Indent}{Indent}/// </summary>");
                sb.AppendLine($"{Indent}{Indent}public {className}(");
                sb.AppendLine($"{Indent}{Indent}    string name = null,");
                sb.AppendLine($"{Indent}{Indent}    int tagCapacity = 0,");
                sb.AppendLine($"{Indent}{Indent}    int valueCapacity = 0,");
                sb.AppendLine($"{Indent}{Indent}    int behaviourCapacity = 0,");
                sb.AppendLine($"{Indent}{Indent}    Settings? settings = null");
                sb.AppendLine(
                    $"{Indent}{Indent}) : base(name, tagCapacity, valueCapacity, behaviourCapacity, settings)");
                sb.AppendLine($"{Indent}{Indent}{{");
                sb.AppendLine($"{Indent}{Indent}}}");
            }

            return sb.ToString();
        }
    }
}
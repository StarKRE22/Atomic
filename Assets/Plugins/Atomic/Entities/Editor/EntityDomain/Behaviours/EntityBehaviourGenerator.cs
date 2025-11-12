using UnityEditor;
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

        private static readonly string[] Summaries =
        {
            "Provides initialization logic for the strongly-typed <see cref=\"{0}\"/> entity.",
            "Handles enable-time logic for the strongly-typed <see cref=\"{0}\"/> entity.",
            "Handles disable-time logic for the strongly-typed <see cref=\"{0}\"/> entity.",
            "Provides cleanup logic for the strongly-typed <see cref=\"{0}\"/> entity.",
            "Handles per-frame update logic for the strongly-typed <see cref=\"{0}\"/> entity.",
            "Handles fixed update logic for the strongly-typed <see cref=\"{0}\"/> entity.",
            "Handles late update logic for the strongly-typed <see cref=\"{0}\"/> entity.",
            "Provides editor visualization logic for the strongly-typed <see cref=\"{0}\"/> entity."
        };

        private static readonly string[] Remarks =
        {
            "Automatically invoked when an <see cref=\"{0}\"/> instance is created and enters the initialization phase.\nTypically used to set up component references, register event listeners, or assign default values.",
            "Automatically invoked when an <see cref=\"{0}\"/> instance becomes active or enabled.\nCommonly used to re-enable systems or resume behavior execution.",
            "Automatically invoked when an <see cref=\"{0}\"/> instance becomes inactive or disabled.\nUseful for pausing updates or temporarily suspending logic without disposing the entity.",
            "Automatically called when an <see cref=\"{0}\"/> instance is destroyed or disposed.\nUsed to release resources, unsubscribe from events, or reset state.",
            "Automatically invoked during the main update loop.\nTypically used for time-dependent gameplay logic such as movement, state updates, or input processing.",
            "Automatically invoked during Unity’s fixed update cycle, synchronized with the physics system.\nCommonly used for deterministic or physics-based updates.",
            "Automatically invoked after all standard update calls within the frame.\nTypically used for camera adjustments, cleanup, or visual synchronization logic.",
            "Automatically invoked when the entity is visible in the Unity Editor Scene view.\nCommonly used to draw debug information, wireframes, or gizmo markers."
        };

        public static void Generate(string entityType, string ns, string[] imports, string directory)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                EditorUtility.DisplayDialog("Error", "Entity Type cannot be empty.", "OK");
                return;
            }

            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, $"{entityType}Behaviours.cs");
            string content = GenerateContent(ns, imports, entityType);
            File.WriteAllText(filePath, content, Encoding.UTF8);

            AssetDatabase.Refresh();
        }

        private static string GenerateContent(string ns, string[] imports, string entityType)
        {
            var sb = new StringBuilder();

            // --- Header ---
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
            sb.AppendLine("/**");
            sb.AppendLine(" * Created by Entity Domain Generator.");
            sb.AppendLine(" */");
            sb.AppendLine();

            // --- Namespace + Body ---
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            for (int i = 0; i < BaseInterfaces.Length; i++)
            {
                string summary = string.Format(Summaries[i], entityType);
                string remarks = string.Format(Remarks[i], entityType);

                sb.AppendLine($"{Indent}/// <summary>");
                sb.AppendLine($"{Indent}/// {summary}");
                sb.AppendLine($"{Indent}/// </summary>");
                sb.AppendLine($"{Indent}/// <remarks>");
                foreach (var line in remarks.Split('\n'))
                    sb.AppendLine($"{Indent}/// {line}");
              
                sb.AppendLine($"{Indent}/// </remarks>");
                sb.AppendLine($"{Indent}public interface {entityType}{EventNames[i]} : {BaseInterfaces[i]}<{entityType}>");
                sb.AppendLine($"{Indent}{{");
                sb.AppendLine($"{Indent}}}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
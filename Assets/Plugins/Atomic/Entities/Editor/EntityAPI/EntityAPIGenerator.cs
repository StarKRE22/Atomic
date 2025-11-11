#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Atomic.Entities
{
    internal static class EntityAPIGenerator
    {
        private const string AGGRESSIVE_INLINING = "\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]";
        private const string UNSAFE_SUFFIX = "Unsafe";
        private const string REF_MODIFIER = "ref";
        private const string PARAM_NAME = "entity";

        public static void CreateFile(EntityAPIAsset.Settings settings)
        {
            string directoryPath = settings.Directory;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string className = settings.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";

            string ns = settings.Namespace;
            IReadOnlyCollection<string> imports = settings.Imports;
            IReadOnlyCollection<string> tags = settings.Tags;
            IReadOnlyDictionary<string, string> values = settings.Values;
            bool useInlining = settings.AggressiveInlining;
            bool unsafeAccess = settings.UnsafeAccess;
            string entityType = settings.EntityType;

            string content = GenerateContent(
                ns,
                className,
                imports,
                tags,
                values,
                entityType,
                useInlining,
                unsafeAccess
            );

            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }

        public static void UpdateFile(EntityAPIAsset.Settings settings)
        {
            string directoryPath = settings.Directory;
            if (!Directory.Exists(directoryPath))
                return;

            string className = settings.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";
            if (!File.Exists(filePath))
                return;

            string ns = settings.Namespace;
            IReadOnlyCollection<string> imports = settings.Imports;
            IReadOnlyCollection<string> tags = settings.Tags;
            IReadOnlyDictionary<string, string> values = settings.Values;

            string entityType = settings.EntityType;
            bool useInlining = settings.AggressiveInlining;
            bool unsafeAccess = settings.UnsafeAccess;

            string content = GenerateContent(
                ns,
                className,
                imports,
                tags,
                values,
                entityType,
                useInlining,
                unsafeAccess
            );
            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }

        private static string GenerateContent(
            string ns,
            string className,
            IEnumerable<string> imports,
            IReadOnlyCollection<string> tags,
            IReadOnlyDictionary<string, string> values,
            string entityType,
            bool useInlining,
            bool unsafeAccess
        )
        {
            StringBuilder sb = new StringBuilder();
            int tagsCount = tags.Count;
            int valuesCount = values.Count;

            //Generate comments:
            sb.AppendLine("/**");
            sb.AppendLine("* Code generation. Don't modify! ");
            sb.AppendLine("**/");
            sb.AppendLine();

            //Generate imports:
            sb.AppendLine("using Atomic.Entities;");
            sb.AppendLine("using static Atomic.Entities.EntityNames;");
            
            if (useInlining)
                sb.AppendLine("using System.Runtime.CompilerServices;");

            //Generate UnityEditor using
            sb.AppendLine("#if UNITY_EDITOR");
            sb.AppendLine("using UnityEditor;");
            sb.AppendLine("#endif");

            foreach (string import in imports)
                sb.AppendLine($"using {import};");

            //Generate namespace:
            sb.AppendLine();
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            sb.AppendLine("#if UNITY_EDITOR");
            sb.AppendLine("\t[InitializeOnLoad]");
            sb.AppendLine("#endif");

            //Generate class header:
            sb.AppendLine($"\tpublic static class {className}");
            sb.AppendLine("\t{");

            //Generate tags:
            if (tagsCount > 0)
            {
                sb.AppendLine();
                sb.AppendLine("\t\t///Tags");
                foreach (string tag in tags)
                    sb.AppendLine($"\t\tpublic static readonly int {tag};");
            }

            //Generate values:
            if (valuesCount > 0)
            {
                if (tagsCount > 0) sb.AppendLine();
                
                sb.AppendLine("\t\t///Values");

                foreach ((string name, string type) in values)
                {
                    string typeName = IsObjectType(type) ? string.Empty : $"// {type}";
                    sb.AppendLine($"\t\tpublic static readonly int {name}; {typeName}");
                }
            }
            
            //Generate static constructor
            sb.AppendLine();
            sb.AppendLine($"\t\tstatic {className}()");
            sb.AppendLine("\t\t{");
            
            ////Generate tags:
            if (tagsCount > 0)
            {
                sb.AppendLine("\t\t\t//Tags");
                foreach (string tag in tags)
                    sb.AppendLine($"\t\t\t{tag} = NameToId(nameof({tag}));");
            }
            
            ////Generate values:
            if (valuesCount > 0)
            {
                if (tagsCount > 0) sb.AppendLine();

                sb.AppendLine("\t\t\t//Values");

                foreach ((string name, string type) in values) 
                    sb.AppendLine($"\t\t\t{name} = NameToId(nameof({name}));");
            }

            sb.AppendLine("\t\t}");

            //Generate tag extensions:
            if (tagsCount > 0)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("\t\t///Tag Extensions");
                foreach (string tag in tags)
                    GenerateTagExtensions(sb, tag, entityType, useInlining);
            }

            //Generate value extensions:
            if (valuesCount > 0)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("\t\t///Value Extensions");

                foreach (var (name, type) in values)
                    GenerateValueExtensions(sb, name, type, entityType, useInlining, unsafeAccess);
            }

            //Generate end of class:
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void GenerateTagExtensions(StringBuilder sb, string tag, string entity, bool useInlining)
        {
            sb.AppendLine();

            sb.AppendLine($"\t\t#region {tag}");
            sb.AppendLine();
            
            //Has:
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Has{tag}Tag(this {entity} {PARAM_NAME}) => {PARAM_NAME}.HasTag({tag});");

            //Add:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Add{tag}Tag(this {entity} {PARAM_NAME}) => {PARAM_NAME}.AddTag({tag});");

            //Del:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Del{tag}Tag(this {entity} {PARAM_NAME}) => {PARAM_NAME}.DelTag({tag});");
            
            sb.AppendLine();
            sb.AppendLine("\t\t#endregion");
        }

        private static void GenerateValueExtensions(
            StringBuilder sb,
            string value,
            string type,
            string entity,
            bool useInlining,
            bool unsafeAccess
        )
        {
            sb.AppendLine();
            
            sb.AppendLine($"\t\t#region {value}");
            sb.AppendLine();

            string unsafeSuffix = unsafeAccess ? UNSAFE_SUFFIX : string.Empty;
            string refModifier = unsafeAccess ? REF_MODIFIER : string.Empty;

            //Get:
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static {type} Get{value}(this {entity} {PARAM_NAME}) => " +
                          $"{PARAM_NAME}.GetValue{unsafeSuffix}<{type}>({value});");

            //Get Ref:
            if (unsafeAccess)
            {
                sb.AppendLine();
                sb.AppendLine($"\t\tpublic static {refModifier} {type} Ref{value}(this {entity} {PARAM_NAME}) => " +
                              $"{refModifier} {PARAM_NAME}.GetValue{unsafeSuffix}<{type}>({value});");
            }
            
            //TryGet:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine(
                $"\t\tpublic static bool TryGet{value}(this {entity} {PARAM_NAME}, out {type} value) =>" +
                $" {PARAM_NAME}.TryGetValue{unsafeSuffix}({value}, out value);");

            //Add:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static void Add{value}(this {entity} {PARAM_NAME}, {type} value) => " +
                          $"{PARAM_NAME}.AddValue({value}, value);");

            //Has:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Has{value}(this {entity} {PARAM_NAME}) => " +
                          $"{PARAM_NAME}.HasValue({value});");

            //Del:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Del{value}(this {entity} {PARAM_NAME}) => " +
                          $"{PARAM_NAME}.DelValue({value});");

            //Set:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static void Set{value}(this {entity} {PARAM_NAME}, {type} value) => " +
                          $"{PARAM_NAME}.SetValue({value}, value);");
            
            sb.AppendLine();
            sb.AppendLine("\t\t#endregion");
        }

        private static bool IsObjectType(string type) => string.IsNullOrEmpty(type) || type is "object" or "Object";
    }
}
#endif
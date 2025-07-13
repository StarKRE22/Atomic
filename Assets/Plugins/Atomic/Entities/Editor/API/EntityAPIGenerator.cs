#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Atomic.Entities
{
    internal static class EntityAPIGenerator
    {
        private const string NAMESPACE = "Atomic.Entities";
        private const string AGRESSIVE_INLINING = "\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]";
        private const string UNSAFE_SUFFIX = "Unsafe";
        private const string REF_MODIFIER = "ref";

        public static void CreateFile(IEntityAPIConfiguration configuration)
        {
            string directoryPath = configuration.Directory;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string className = configuration.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";

            string ns = configuration.Namespace;
            IReadOnlyCollection<string> imports = configuration.GetImports();
            IReadOnlyCollection<string> tags = configuration.GetTags();
            IDictionary<string, string> values = configuration.GetValues();
            bool useInlining = configuration.AggressiveInlining;
            bool unsafeAccess = configuration.UnsafeAccess;
            string entityType = configuration.EntityType;

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

        public static void UpdateFile(IEntityAPIConfiguration configuration)
        {
            string directoryPath = configuration.Directory;
            if (!Directory.Exists(directoryPath))
                return;

            string className = configuration.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";
            if (!File.Exists(filePath))
                return;

            string ns = configuration.Namespace;
            IReadOnlyCollection<string> imports = configuration.GetImports();
            IReadOnlyCollection<string> tags = configuration.GetTags();
            IDictionary<string, string> values = configuration.GetValues();
            string entityType = configuration.EntityType;
            bool useInlining = configuration.AggressiveInlining;
            bool unsafeAccess = configuration.UnsafeAccess;

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
            IDictionary<string, string> values,
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
            sb.AppendLine($"using {NAMESPACE};");

            if (useInlining)
                sb.AppendLine("using System.Runtime.CompilerServices;");

            foreach (string import in imports)
                sb.AppendLine($"using {import};");

            //Generate start of class:
            sb.AppendLine();
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"\tpublic static class {className}");
            sb.AppendLine("\t{");

            //Generate tags:
            if (tagsCount > 0)
            {
                sb.AppendLine("\t\t///Tags");
                foreach (string tag in tags)
                    GenerateTag(sb, tag);
            }

            //Generate values:
            if (valuesCount > 0)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("\t\t///Values");

                foreach ((string name, string type) in values)
                    GenerateValue(sb, name, type);
            }

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

        private static void GenerateValue(StringBuilder sb, string key, string type)
        {
            int id = EntityUtils.NameToId(key);
            string typeName = IsBaseType(type) ? "" : $"// {type}";
            sb.AppendLine($"\t\tpublic const int {key} = {id}; {typeName}");
        }

        private static void GenerateTag(StringBuilder sb, string tag)
        {
            int id = EntityUtils.NameToId(tag);
            sb.AppendLine($"\t\tpublic const int {tag} = {id};");
        }

        private static void GenerateTagExtensions(StringBuilder sb, string tag, string entity, bool useInlining)
        {
            sb.AppendLine();

            //Has:
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Has{tag}Tag(this {entity} obj) => obj.HasTag({tag});");

            //Add:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Add{tag}Tag(this {entity} obj) => obj.AddTag({tag});");

            //Del:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Del{tag}Tag(this {entity} obj) => obj.DelTag({tag});");
        }

        private static void GenerateValueExtensions(
            StringBuilder sb,
            string name,
            string type,
            string entity,
            bool useInlining,
            bool unsafeAccess
        )
        {
            sb.AppendLine();

            string unsafeSuffix = unsafeAccess ? UNSAFE_SUFFIX : string.Empty;
            string refModifier = unsafeAccess ? REF_MODIFIER : string.Empty;

            //Get:
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static {type} Get{name}(this {entity} obj) => " +
                          $"obj.GetValue{unsafeSuffix}<{type}>({name});");
            
            //Get Ref:
            if (unsafeAccess)
            {
                sb.AppendLine();
                sb.AppendLine($"\t\tpublic static {refModifier} {type} Ref{name}(this {entity} obj) => " +
                              $"{refModifier} obj.GetValue{unsafeSuffix}<{type}>({name});");
            }
            
            
            //TryGet:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine(
                $"\t\tpublic static bool TryGet{name}(this {entity} obj, out {type} value) =>" +
                $" obj.TryGetValue{unsafeSuffix}({name}, out value);");

            //Add:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static void Add{name}(this {entity} obj, {type} value) => " +
                          $"obj.AddValue({name}, value);");

            //Has:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Has{name}(this {entity} obj) => " +
                          $"obj.HasValue({name});");

            //Del:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Del{name}(this {entity} obj) => " +
                          $"obj.DelValue({name});");

            //Set:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static void Set{name}(this {entity} obj, {type} value) => " +
                          $"obj.SetValue({name}, value);");
        }

        private static bool IsBaseType(string type)
        {
            return string.IsNullOrEmpty(type) || type is "object" or "Object";
        }
    }
}
#endif
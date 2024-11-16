#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Atomic.Entities
{
    public static class EntityAPIGenerator
    {
        private const string NAMESPACE = "Atomic.Entities";
        private const string ENTITY_CLASS = "IEntity";
        private const string AGRESSIVE_INLINING = "\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]";

        public static void CreateFile(IEntityAPIConfiguration configuration)
        {
            string directoryPath = configuration.Directory;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string className = configuration.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";

            string ns = configuration.Namespace;
            IEnumerable<string> imports = configuration.GetImports();
            IEnumerable<string> tags = configuration.GetTags();
            IDictionary<string, string> values = configuration.GetValues();

            string content = GenerateContent(ns, className, imports, tags, values);
            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }
        
        public static void UpdateFile(IEntityAPIConfiguration configuration)
        {
            Debug.Log($"UPDATE FILE {configuration.ClassName}");

            string directoryPath = configuration.Directory;
            if (!Directory.Exists(directoryPath))
                return;

            string className = configuration.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";
            if (!File.Exists(filePath))
                return;

            string ns = configuration.Namespace;
            IEnumerable<string> imports = configuration.GetImports();
            IEnumerable<string> tags = configuration.GetTags();
            IDictionary<string, string> values = configuration.GetValues();

            string content = GenerateContent(ns, className, imports, tags, values);
            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }

        private static string GenerateContent(
            string ns,
            string className,
            IEnumerable<string> imports,
            IEnumerable<string> tags,
            IDictionary<string, string> values
        )
        {
            StringBuilder sb = new StringBuilder();

            //Generate comments:
            sb.AppendLine("/**");
            sb.AppendLine("* Code generation. Don't modify! ");
            sb.AppendLine("**/");
            sb.AppendLine();

            //Generate imports:
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine($"using {NAMESPACE};");
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
            sb.AppendLine("\t\t///Tags");
            foreach (string tag in tags)
                GenerateTag(sb, tag);

            //Generate values:
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("\t\t///Values");

            foreach ((string name, string type) in values)
                GenerateValue(sb, name, type);

            //Generate tag extensions:
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("\t\t///Tag Extensions");
            foreach (string tag in tags)
                GenerateTagExtensions(sb, tag);

            //Generate value extensions:
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("\t\t///Value Extensions");

            foreach (var (name, type) in values)
                GenerateValueExtensions(sb, name, type);

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

        private static void GenerateTagExtensions(StringBuilder sb, string tag)
        {
            sb.AppendLine();

            //Has:
            sb.AppendLine($"\t\tpublic static bool Has{tag}Tag(this {ENTITY_CLASS} obj) => obj.HasTag({tag});");

            //Not:
            sb.AppendLine($"\t\tpublic static bool Not{tag}Tag(this {ENTITY_CLASS} obj) => !obj.HasTag({tag});");

            //Add:
            sb.AppendLine($"\t\tpublic static bool Add{tag}Tag(this {ENTITY_CLASS} obj) => obj.AddTag({tag});");

            //Del:
            sb.AppendLine($"\t\tpublic static bool Del{tag}Tag(this {ENTITY_CLASS} obj) => obj.DelTag({tag});");
        }

        private static void GenerateValueExtensions(StringBuilder sb, string name, string type)
        {
            sb.AppendLine();

            if (IsBaseType(type))
            {
                //Get:
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static object Get{name}(this {ENTITY_CLASS} obj) => " +
                              $"obj.GetValue({name});");

                //TryGet:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine(
                    $"\t\tpublic static bool TryGet{name}(this {ENTITY_CLASS} obj, out object value) => " +
                    $"obj.TryGetValue({name}, out value);");

                //Add:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Add{name}(this {ENTITY_CLASS} obj, object value) => " +
                              $"obj.AddValue({name}, value);");

                //Del:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Del{name}(this {ENTITY_CLASS} obj) => " +
                              $"obj.DelValue({name});");

                //Set:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static void Set{name}(this {ENTITY_CLASS} obj, object value) => " +
                              $"obj.SetValue({name}, value);");

                //Has:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Has{name}(this {ENTITY_CLASS} obj) => " +
                              $"obj.HasValue({name});");
            }
            else
            {
                //Get:
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static {type} Get{name}(this {ENTITY_CLASS} obj) => " +
                              $"obj.GetValue<{type}>({name});");

                //TryGet:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine(
                    $"\t\tpublic static bool TryGet{name}(this {ENTITY_CLASS} obj, out {type} value) =>" +
                    $" obj.TryGetValue({name}, out value);");

                //Add:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Add{name}(this {ENTITY_CLASS} obj, {type} value) => " +
                              $"obj.AddValue({name}, value);");

                //Has:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Has{name}(this {ENTITY_CLASS} obj) => " +
                              $"obj.HasValue({name});");

                //Del:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Del{name}(this {ENTITY_CLASS} obj) => " +
                              $"obj.DelValue({name});");

                //Set:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static void Set{name}(this {ENTITY_CLASS} obj, {type} value) => " +
                              $"obj.SetValue({name}, value);");
            }
        }
        
        private static bool IsBaseType(string type)
        {
            return string.IsNullOrEmpty(type) || type == "object"; //TODO: Regex
        }
    }
}
#endif
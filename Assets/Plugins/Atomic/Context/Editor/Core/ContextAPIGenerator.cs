using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Atomic.Contexts
{
    internal static class ContextAPIGenerator
    {
        private const string NAMESPACE = "Atomic.Contexts";
        private const string AGRESSIVE_INLINING = "\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]";

        public static void CreateFile(IContextAPIConfiguration configuration)
        {
            string directoryPath = configuration.Directory;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string className = configuration.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";

            string ns = configuration.Namespace;
            IReadOnlyCollection<string> imports = configuration.GetImports();
            IDictionary<string, string> values = configuration.GetValues();
            bool useInlining = configuration.AggressiveInlining;
            string entityType = configuration.ContextType;

            string content = GenerateContent(ns, className, imports, values, entityType, useInlining);
            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }

        public static void UpdateFile(IContextAPIConfiguration configuration)
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
            IDictionary<string, string> values = configuration.GetValues();
            string entityType = configuration.ContextType;
            bool useInlining = configuration.AggressiveInlining;

            string content = GenerateContent(ns, className, imports, values, entityType, useInlining);
            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }

        private static string GenerateContent(
            string ns,
            string className,
            IEnumerable<string> imports,
            IDictionary<string, string> values,
            string entityType,
            bool useInlining
        )
        {
            StringBuilder sb = new StringBuilder();
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

            //Generate values:
            if (valuesCount > 0)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("\t\t///Values");

                foreach ((string name, string type) in values)
                    GenerateValue(sb, name, type);
            }
            
            //Generate value extensions:
            if (valuesCount > 0)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("\t\t///Value Extensions");

                foreach (var (name, type) in values)
                    GenerateValueExtensions(sb, name, type, entityType, useInlining);
            }

            //Generate end of class:
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void GenerateValue(StringBuilder sb, string key, string type)
        {
            int id = ContextUtils.NameToId(key);
            string typeName = IsBaseType(type) ? "" : $"// {type}";
            sb.AppendLine($"\t\tpublic const int {key} = {id}; {typeName}");
        }
        
        private static void GenerateValueExtensions(
            StringBuilder sb,
            string name,
            string type,
            string entity,
            bool useInlining
        )
        {
            sb.AppendLine();

            if (IsBaseType(type))
            {
                //Get:
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static object Get{name}(this {entity} obj) => " +
                              $"obj.GetValue({name});");

                //TryGet:
                sb.AppendLine();
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine(
                    $"\t\tpublic static bool TryGet{name}(this {entity} obj, out object value) => " +
                    $"obj.TryGetValue({name}, out value);");

                //Add:
                sb.AppendLine();
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Add{name}(this {entity} obj, object value) => " +
                              $"obj.AddValue({name}, value);");

                //Del:
                sb.AppendLine();
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Del{name}(this {entity} obj) => " +
                              $"obj.DelValue({name});");

                //Set:
                sb.AppendLine();
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static void Set{name}(this {entity} obj, object value) => " +
                              $"obj.SetValue({name}, value);");

                //Has:
                sb.AppendLine();
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Has{name}(this {entity} obj) => " +
                              $"obj.HasValue({name});");
            }
            else
            {
                //Get:
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static {type} Get{name}(this {entity} obj) => " +
                              $"obj.GetValue<{type}>({name});");

                //TryGet:
                sb.AppendLine();
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine(
                    $"\t\tpublic static bool TryGet{name}(this {entity} obj, out {type} value) =>" +
                    $" obj.TryGetValue({name}, out value);");

                //Add:
                sb.AppendLine();
                if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Add{name}(this {entity} obj, {type} value) => " +
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
        }

        private static bool IsBaseType(string type)
        {
            return string.IsNullOrEmpty(type) || type is "object" or "Object";
        }
    }
}
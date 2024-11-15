using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
namespace Atomic.Entities
{
    public static class EntityAPIGenerator
    {
        private const string NAMESPACE = "Atomic.Entities";
        private const string ENTITY_CLASS = "IEntity";
        private const string AGRESSIVE_INLINING = "\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]";

        public static void GenerateFile(IEntityAPIConfig config)
        {
            string ns = config.Namespace;
            string className = config.ClassName;
            IEnumerable<string> imports = config.GetImports();
            IEnumerable<string> tags = config.GetTags();
            IDictionary<string, Type> values = config.GetValues();
            string content = GenerateContent(ns, className, imports, tags, values);
        }

        private static string GenerateContent(
            string ns,
            string className,
            IEnumerable<string> imports,
            IEnumerable<string> tags,
            IDictionary<string, Type> values
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

            foreach ((string name, Type type) in values)
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
        }

        private static void GenerateValue(StringBuilder sb, string name, Type type)
        {
            int id = new PropertyName(name).GetHashCode();
            string typeName = type == null || type == typeof(object) ? "" : $"// {type.FullName}";
            sb.AppendLine($"\t\tpublic const int {name} = {id}; {typeName}");
        }

        private static void GenerateTag(StringBuilder sb, string tag)
        {
            int id = new PropertyName(tag).GetHashCode();
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

        private static void GenerateValueExtensions(StringBuilder sb, string valueName, Type type)
        {
            sb.AppendLine();

            if (type == null || type == typeof(object))
            {
                //Get:
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static object Get{valueName}(this {ENTITY_CLASS} obj) => " +
                              $"obj.GetValue({valueName});");

                //TryGet:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine(
                    $"\t\tpublic static bool TryGet{valueName}(this {ENTITY_CLASS} obj, out object value) => " +
                    $"obj.TryGetValue({valueName}, out value);");

                //Add:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Add{valueName}(this {ENTITY_CLASS} obj, object value) => " +
                              $"obj.AddValue({valueName}, value);");

                //Del:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Del{valueName}(this {ENTITY_CLASS} obj) => " +
                              $"obj.DelValue({valueName});");

                //Set:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static void Set{valueName}(this {ENTITY_CLASS} obj, object value) => " +
                              $"obj.SetValue({valueName}, value);");

                //Has:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Has{valueName}(this {ENTITY_CLASS} obj) => " +
                              $"obj.HasValue({valueName});");
            }
            else
            {
                string typeName = type.Name;

                //Get:
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static {typeName} Get{valueName}(this {ENTITY_CLASS} obj) => " +
                              $"obj.GetValue<{typeName}>({valueName});");

                //TryGet:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine(
                    $"\t\tpublic static bool TryGet{valueName}(this {ENTITY_CLASS} obj, out {typeName} value) =>" +
                    $" obj.TryGetValue({valueName}, out value);");
                
                //Add:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Add{valueName}(this {ENTITY_CLASS} obj, {typeName} value) => " +
                              $"obj.AddValue({valueName}, value);");

                //Has:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Has{valueName}(this {ENTITY_CLASS} obj) => " +
                              $"obj.HasValue({typeName});");
                
                //Del:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static bool Del{valueName}(this {ENTITY_CLASS} obj) => " +
                              $"obj.DelValue({valueName});");

                //Set:
                sb.AppendLine();
                sb.AppendLine(AGRESSIVE_INLINING);
                sb.AppendLine($"\t\tpublic static void Set{valueName}(this {ENTITY_CLASS} obj, {type} value) => " +
                              $"obj.SetValue({valueName}, value);");
            }
        }


    }
}
#endif



//
//         internal static void Generate(ValueConfig valueConfig, int index, bool forceMode = true)
//         {
//             string suffix = valueConfig.suffix;
//             string @namespace = valueConfig.@namespace;
//             string directoryPath = valueConfig.directoryPath;
//             string[] imports = valueConfig.imports;
//
//             if (!Directory.Exists(directoryPath))
//             {
//                 if (forceMode)
//                 {
//                     Directory.CreateDirectory(directoryPath);
//                 }
//             }
//
//             GenerateCategory(valueConfig.categories[index], @namespace, suffix, directoryPath, imports, forceMode);
//
//             EditorUtility.SetDirty(valueConfig);
//             AssetDatabase.SaveAssets();
//
//             if (forceMode)
//             {
//                 AssetDatabase.Refresh();
//             }
//         }
//
//         internal static void Generate(ValueConfig valueConfig)
//         {
//             string suffix = valueConfig.suffix;
//             string @namespace = valueConfig.@namespace;
//             string directoryPath = valueConfig.directoryPath;
//             string[] imports = valueConfig.imports;
//
//             if (!Directory.Exists(directoryPath))
//             {
//                 Directory.CreateDirectory(directoryPath);
//             }
//
//             foreach (ValueConfig.Category category in valueConfig.categories)
//             {
//                 GenerateCategory(category, @namespace, suffix, directoryPath, imports, true);
//             }
//
//             AssetDatabase.SaveAssets();
//             AssetDatabase.Refresh();
//         }
//
//         
//         
//         
//          private const string NAMESPACE = "Atomic.Entities";
//         private const string ENTITY_CLASS = "IEntity";
//
//         internal static void Generate(TagsConfig catalog, bool forceMode = true)
//         {
//             if (catalog == null)
//             {
//                 return;
//             }
//
//             if (!forceMode && !File.Exists($"{catalog.directoryPath}/{catalog.className}.cs"))
//             {
//                 return;
//             }
//
//             string directoryPath = catalog.directoryPath;
//             if (!Directory.Exists(directoryPath))
//             {
//                 if (forceMode)
//                 {
//                     Directory.CreateDirectory(directoryPath);
//                 }
//             }
//
//             string selectedPath = $"{directoryPath}/{catalog.className}.cs";
//             string @namespace = catalog.@namespace;
//
//             using StreamWriter writer = new StreamWriter(selectedPath);
//             writer.WriteLine("/**");
//             writer.WriteLine("* Code generation. Don't modify! ");
//             writer.WriteLine("**/");
//
//             writer.WriteLine();
//
//             //Generate imports:
//             writer.WriteLine("using UnityEngine;");
//             writer.WriteLine($"using {NAMESPACE};");
//             
//             writer.WriteLine();
//
//             //Generate class:
//             writer.WriteLine($"namespace {@namespace}");
//             writer.WriteLine("{");
//             writer.WriteLine($"    public static class {catalog.className}");
//             writer.WriteLine("    {");
//
//             //Generate keys:
//             writer.WriteLine("        ///Keys");
//             var items = catalog.items;
//             for (int i = 0, count = items.Count; i < count; i++)
//             {
//                 TagsConfig.Item item = items[i];
//                 writer.WriteLine($"        public const int {item.type} = {item.id};");
//             }
//
//             writer.WriteLine();
//             writer.WriteLine();
//
//             //Generate extensions:
//             writer.WriteLine("        ///Extensions");
//             for (int i = 0, count = items.Count; i < count; i++)
//             {
//                 TagsConfig.Item item = items[i];
//                 writer.WriteLine(
//                     $"        public static bool Has{item.type}Tag(this {ENTITY_CLASS} obj) => obj.HasTag({item.type});");
//
//                 writer.WriteLine(
//                     $"        public static bool Not{item.type}Tag(this {ENTITY_CLASS} obj) => !obj.HasTag({item.type});");
//                 
//                 writer.WriteLine(
//                     $"        public static bool Add{item.type}Tag(this {ENTITY_CLASS} obj) => obj.AddTag({item.type});");
//                 writer.WriteLine(
//                     $"        public static bool Del{item.type}Tag(this {ENTITY_CLASS} obj) => obj.DelTag({item.type});");
//
//                 if (i < count - 1)
//                 {
//                     writer.WriteLine();
//                 }
//             }
//
//             writer.WriteLine("    }");
//             writer.WriteLine("}");
//
//             EditorUtility.SetDirty(catalog);
//             AssetDatabase.SaveAssets();
//
//             if (forceMode)
//             {
//                 AssetDatabase.Refresh();
//             }
//         }
//     }
//
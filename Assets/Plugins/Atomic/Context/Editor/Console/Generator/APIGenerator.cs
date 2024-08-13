#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace Atomic.Contexts
{
    internal static class APIGenerator
    {
        private const string NAMESPACE = "Atomic.Contexts";
        private const string CLASS = "IContext";

        private const string VALUE = "Value";
        private const string GET = "Get";
        private const string ADD = "Add";
        private const string REMOVE = "Del";
        private const string RESOLVE = "Resolve";
        private const string SET = "Set";
        private const string HAS = "Has";

        internal static void Generate(APICatalog catalog)
        {
            foreach (APICategory category in catalog)
            {
                GenerateCategory(category, true);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        internal static void GenerateCategory(APICatalog catalog, int categoryIndex, bool force = true)
        {
            APICategory category = catalog.GetCategory(categoryIndex);
            GenerateCategory(category, force);

            EditorUtility.SetDirty(catalog);
            AssetDatabase.SaveAssets();

            if (force)
            {
                AssetDatabase.Refresh();
            }
        }

        private static void GenerateCategory(APICategory category, bool refresh)
        {
            string directoryPath = category.DirectoryPath;
            if (!Directory.Exists(directoryPath) && refresh)
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            string filePath = category.FilePath;
            if (!refresh && !File.Exists(filePath))
            {
                return;
            }

            using StreamWriter writer = new StreamWriter(filePath);

            GenerateMessage(writer);
            writer.WriteLine();

            GenerateImports(category.Imports, writer);
            writer.WriteLine();

            GenerateClassHeader(writer, category.Namespace, category.ClassName);

            IReadOnlyList<APIItem> items = category.GetItems();
            
            GenerateKeyConsts(writer, items);
            writer.WriteLine();
            writer.WriteLine();

            GenerateExtensionMethods(writer, items);

            writer.WriteLine("    }");
            writer.WriteLine("}");
        }

        private static void GenerateMessage(StreamWriter writer)
        {
            writer.WriteLine("/**");
            writer.WriteLine("* Code generation. Don't modify! ");
            writer.WriteLine("**/");
        }

        private static void GenerateExtensionMethods(StreamWriter writer, IReadOnlyList<APIItem> items)
        {
            writer.WriteLine("\t\t///Extensions");
            for (int i = 0, count = items.Count; i < count; i++)
            {
                APIItem item = items[i];
                string name = item.name;
                string type = item.type;

                if (string.IsNullOrEmpty(type))
                {
                    GenerateDefaultExtensionMethods(writer, name);
                }
                else
                {
                    GenerateTypedExtensionsMethods(writer, type, name);
                }

                if (i < count - 1)
                {
                    writer.WriteLine();
                }
            }
        }

        private static void GenerateImports(string[] imports, StreamWriter writer)
        {
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine($"using {NAMESPACE};");
            writer.WriteLine("using System.Runtime.CompilerServices;");

            for (int i = 0, count = imports.Length; i < count; i++)
            {
                writer.WriteLine($"using {imports[i]};");
            }
        }

        private static void GenerateClassHeader(StreamWriter writer, string @namespace, string className)
        {
            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.WriteLine($"\tpublic static class {className}");
            writer.WriteLine("\t{");
        }

        private static void GenerateKeyConsts(StreamWriter writer, IReadOnlyList<APIItem> items)
        {
            writer.WriteLine("\t\t///Keys");
            for (int i = 0, count = items.Count; i < count; i++)
            {
                APIItem item = items[i];
                string itemType = string.IsNullOrEmpty(item.type) ? "" : $"// {item.type}";
                writer.WriteLine($"\t\tpublic const int {item.name} = {item.id}; {itemType}");
            }
        }

        private static void GenerateDefaultExtensionMethods(StreamWriter writer, string name)
        {
            //Get:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static object {GET}{name}(this {CLASS} obj) => " +
                             $"obj.{GET}{VALUE}({name});");
            writer.WriteLine();

            //Try Get:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool Try{GET}{name}(this {CLASS} obj, out object value) => " +
                             $"obj.Try{GET}{VALUE}({name}, out value);");
            writer.WriteLine();

            //Resolve:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static object {RESOLVE}{name}(this {CLASS} obj) => " +
                             $"obj.{RESOLVE}{VALUE}({name});");
            writer.WriteLine();

            //Try Resolve:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool Try{RESOLVE}{name}(this {CLASS} obj, out object value) => " +
                             $"obj.Try{RESOLVE}{VALUE}({name}, out value);");
            writer.WriteLine();

            //Add:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool {ADD}{name}(this {CLASS} obj, object value) => " +
                             $"obj.{ADD}{VALUE}({name}, value);");
            writer.WriteLine();

            //Remove:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool {REMOVE}{name}(this {CLASS} obj) => " +
                             $"obj.{REMOVE}{VALUE}({name});");
            writer.WriteLine();

            //Set:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static void {SET}{name}(this {CLASS} obj, object value) => " +
                             $"obj.{SET}{VALUE}({name}, value);");
            writer.WriteLine();

            //Has:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool {HAS}{name}(this {CLASS} obj) => " +
                             $"obj.{HAS}{VALUE}({name});");
        }

        private static void GenerateTypedExtensionsMethods(StreamWriter writer, string type, string name)
        {
            //Get:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static {type} {GET}{name}(this {CLASS} obj) => " +
                             $"obj.{RESOLVE}{VALUE}<{type}>({name});");
            writer.WriteLine();

            //Try Get:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool Try{GET}{name}(this {CLASS} obj, out {type} value) => " +
                             $"obj.Try{RESOLVE}{VALUE}({name}, out value);");
            writer.WriteLine();

            //Add:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool {ADD}{name}(this {CLASS} obj, {type} value) => " +
                             $"obj.{ADD}{VALUE}({name}, value);");
            writer.WriteLine();

            //Remove:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool {REMOVE}{name}(this {CLASS} obj) => " +
                             $"obj.{REMOVE}{VALUE}({name});");
            writer.WriteLine();

            //Set:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static void {SET}{name}(this {CLASS} obj, {type} value) => " +
                             $"obj.{SET}{VALUE}({name}, value);");
            writer.WriteLine();

            //Has:
            WriteAggressiveInlining(writer);
            writer.WriteLine($"\t\tpublic static bool {HAS}{name}(this {CLASS} obj) => " +
                             $"obj.{HAS}{VALUE}({name});");
        }

        private static void WriteAggressiveInlining(StreamWriter writer)
        {
            writer.WriteLine("\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]");
        }
    }
}
#endif
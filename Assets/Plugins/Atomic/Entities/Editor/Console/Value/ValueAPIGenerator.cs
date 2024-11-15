#if UNITY_EDITOR
using System.IO;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class ValueAPIGenerator
    {
        private const string NAMESPACE = "Atomic.Entities";
        private const string ENTITY_CLASS = "IEntity";

        internal static void Generate(ValueConfig valueConfig, int index, bool refresh = true)
        {
            string suffix = valueConfig.suffix;
            string @namespace = valueConfig.@namespace;
            string directoryPath = valueConfig.directoryPath;
            string[] imports = valueConfig.imports;

            if (!Directory.Exists(directoryPath))
            {
                if (refresh)
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            GenerateCategory(valueConfig.categories[index], @namespace, suffix, directoryPath, imports, refresh);

            EditorUtility.SetDirty(valueConfig);
            AssetDatabase.SaveAssets();

            if (refresh)
            {
                AssetDatabase.Refresh();
            }
        }

        internal static void Generate(ValueConfig valueConfig)
        {
            string suffix = valueConfig.suffix;
            string @namespace = valueConfig.@namespace;
            string directoryPath = valueConfig.directoryPath;
            string[] imports = valueConfig.imports;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (ValueConfig.Category category in valueConfig.categories)
            {
                GenerateCategory(category, @namespace, suffix, directoryPath, imports, true);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void GenerateCategory(ValueConfig.Category category,
            string @namespace,
            string suffix,
            string directoryPath,
            string[] imports,
            bool refresh
        )
        {
            string className = $"{category.name}{suffix}";

            string path = $"{directoryPath}/{className}.cs";
            if (!refresh && !File.Exists(path))
            {
                return;
            }
            
            
            using StreamWriter writer = new StreamWriter(path);
            writer.WriteLine("/**");
            writer.WriteLine("* Code generation. Don't modify! ");
            writer.WriteLine("**/");

            writer.WriteLine();

            //Generate imports:
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine($"using {NAMESPACE};");
            writer.WriteLine("using System.Runtime.CompilerServices;");

            for (int i = 0, count = imports.Length; i < count; i++)
            {
                writer.WriteLine($"using {imports[i]};");
            }

            writer.WriteLine();

            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.WriteLine($"    public static class {className}");
            writer.WriteLine("    {");

            //Generate keys:
            writer.WriteLine("        ///Keys");
            var items = category.indexes;
            for (int i = 0, count = items.Count; i < count; i++)
            {
                ValueConfig.Item item = items[i];

                string itemType = string.IsNullOrEmpty(item.type) ? "" : $"// {item.type}";
                writer.WriteLine($"        public const int {item.name} = {item.id}; {itemType}");

                // writer.WriteLine(string.IsNullOrEmpty(item.type)
                //     ? "        //Undefined type"
                //     : $"        [Contract(typeof({item.type}))]");

                // writer.WriteLine($"        public const int {item.name} = {item.id};");

                // if (i < count -1)
                // {
                //     writer.WriteLine();
                // }
            }

            writer.WriteLine();
            writer.WriteLine();

            //Generate extensions:
            writer.WriteLine("        ///Extensions");
            for (int i = 0, count = items.Count; i < count; i++)
            {
                ValueConfig.Item item = items[i];
                string itemName = item.name;
                string itemType = item.type;

                if (string.IsNullOrEmpty(itemType))
                {
                    GenerateObjectExtensions(writer, itemName);
                }
                else
                {
                    GenerateTypedExtensions(writer, itemType, itemName);
                }

                if (i < count - 1)
                {
                    writer.WriteLine();
                }
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
        }

        private static void GenerateObjectExtensions(StreamWriter writer, string itemName)
        {
            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static object Get{itemName}(this {ENTITY_CLASS} obj) => obj.GetValue({itemName});");
            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static bool TryGet{itemName}(this {ENTITY_CLASS} obj, out object value) => obj.TryGetValue({itemName}, out value);");
            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static bool Add{itemName}(this {ENTITY_CLASS} obj, object value) => obj.AddValue({itemName}, value);");
            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static bool Del{itemName}(this {ENTITY_CLASS} obj) => obj.DelValue({itemName});");
            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static void Set{itemName}(this {ENTITY_CLASS} obj, object value) => obj.SetValue({itemName}, value);");
            
            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static bool Has{itemName}(this {ENTITY_CLASS} obj) => obj.HasValue({itemName});");
            writer.WriteLine();
        }

        private static void GenerateTypedExtensions(StreamWriter writer, string itemType, string itemName)
        {
            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static {itemType} Get{itemName}(this {ENTITY_CLASS} obj) => obj.GetValue<{itemType}>({itemName});");
            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static bool TryGet{itemName}(this {ENTITY_CLASS} obj, out {itemType} value) => obj.TryGetValue({itemName}, out value);");
            writer.WriteLine();

        
            
            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static bool Add{itemName}(this {ENTITY_CLASS} obj, {itemType} value) => obj.AddValue({itemName}, value);");

            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static bool Has{itemName}(this {ENTITY_CLASS} obj) => obj.HasValue({itemName});");
            writer.WriteLine();
            

            // if (IsLogicType(itemType))
            // {
            //     writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            //     writer.WriteLine(
            //         $"        public static void Del{itemName}(this {OBJECT_CLASS} obj) => obj.DelElement({itemName});");
            // }
            // else
            // {

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static bool Del{itemName}(this {ENTITY_CLASS} obj) => obj.DelValue({itemName});");
            // }

            writer.WriteLine();

            // if (IsLogicType(itemType))
            // {
            //     writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            //     writer.WriteLine(
            //         $"        public static void Set{itemName}(this {OBJECT_CLASS} obj, {itemType} value) => obj.SetElement({itemName}, value);");
            // }
            // else
            // {

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine(
                $"        public static void Set{itemName}(this {ENTITY_CLASS} obj, {itemType} value) => obj.SetValue({itemName}, value);");
            // }
        }

        // private static bool IsLogicType(string itemType)
        // {
        //     foreach (var type in allTypes)
        //     {
        //         if (type.FullName == itemType || type.Name == itemType)
        //         {
        //             return true;
        //         }
        //     }
        //
        //     return false;
        // }
    }
}
#endif


// writer.WriteLine($"        public const int {item.name} = {item.id}; {keyType}");
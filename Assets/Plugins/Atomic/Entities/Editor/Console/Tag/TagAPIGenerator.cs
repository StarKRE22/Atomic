#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace Atomic.Entities
{
    internal static class TagAPIGenerator
    {
        private const string NAMESPACE = "Atomic.Entities";
        private const string ENTITY_CLASS = "IEntity";

        internal static void Generate(TagsConfig catalog, bool refresh = true)
        {
            if (catalog == null)
            {
                return;
            }

            if (!refresh && !File.Exists($"{catalog.directoryPath}/{catalog.className}.cs"))
            {
                return;
            }

            string directoryPath = catalog.directoryPath;
            if (!Directory.Exists(directoryPath))
            {
                if (refresh)
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            string selectedPath = $"{directoryPath}/{catalog.className}.cs";
            string @namespace = catalog.@namespace;

            using StreamWriter writer = new StreamWriter(selectedPath);
            writer.WriteLine("/**");
            writer.WriteLine("* Code generation. Don't modify! ");
            writer.WriteLine("**/");

            writer.WriteLine();

            //Generate imports:
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine($"using {NAMESPACE};");
            
            writer.WriteLine();

            //Generate class:
            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.WriteLine($"    public static class {catalog.className}");
            writer.WriteLine("    {");

            //Generate keys:
            writer.WriteLine("        ///Keys");
            var items = catalog.items;
            for (int i = 0, count = items.Count; i < count; i++)
            {
                TagsConfig.Item item = items[i];
                writer.WriteLine($"        public const int {item.type} = {item.id};");
            }

            writer.WriteLine();
            writer.WriteLine();

            //Generate extensions:
            writer.WriteLine("        ///Extensions");
            for (int i = 0, count = items.Count; i < count; i++)
            {
                TagsConfig.Item item = items[i];
                writer.WriteLine(
                    $"        public static bool Has{item.type}Tag(this {ENTITY_CLASS} obj) => obj.HasTag({item.type});");
                writer.WriteLine(
                    $"        public static bool Add{item.type}Tag(this {ENTITY_CLASS} obj) => obj.AddTag({item.type});");
                writer.WriteLine(
                    $"        public static bool Del{item.type}Tag(this {ENTITY_CLASS} obj) => obj.DelTag({item.type});");

                if (i < count - 1)
                {
                    writer.WriteLine();
                }
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");

            EditorUtility.SetDirty(catalog);
            AssetDatabase.SaveAssets();

            if (refresh)
            {
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif
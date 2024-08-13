#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Contexts
{
    [Serializable]
    internal sealed class APICategory
    {
#if ODIN_INSPECTOR
        [GUIColor(0.2f, 0.54f, 0.2f)]
#endif
        public string Name;

#if ODIN_INSPECTOR
        [Title("Settings")]
#endif
        public string Namespace = "Atomic.Contexts";
        public string DirectoryPath = "Assets/Codegen";
        public string ClassSuffix = "API";
        public string[] Imports = Array.Empty<string>();

        public string ClassName => $"{this.Name}{this.ClassSuffix}";
        public string FilePath => $"{this.DirectoryPath}/{this.ClassName}.cs";

#if ODIN_INSPECTOR
        [Title("Items")]
#endif
        [SerializeField]
        private List<APIItem> items = new();

        internal APICategory()
        {
        }

        internal APICategory(string name)
        {
            this.Name = name;
            this.items = new List<APIItem>();
        }

        internal IReadOnlyList<APIItem> GetItems()
        {
            return this.items;
        }

        internal int GetIndexOfItem(string name)
        {
            for (int i = 0, count = this.items.Count; i < count; i++)
            {
                APIItem item = this.items[i];
                if (item.name == name)
                {
                    return i;
                }
            }

            return -1;
        }

        internal bool IsUniqueItemName(string name)
        {
            return this.items.Count(it => it.name == name) == 1;
        }

        internal APIItem GetItem(int index)
        {
            return this.items[index];
        }

        internal void AddItem(int id, string name, string type)
        {
            this.items.Add(new APIItem(id, name, type));
        }

        internal IEnumerable<int> GetItemIds()
        {
            return this.items.Select(it => it.id);
        }

        internal int IndexOfItem(string name)
        {
            for (int i = 0, count = this.items.Count; i < count; i++)
            {
                APIItem item = this.items[i];
                if (item.name == name)
                {
                    return i;
                }
            }

            return -1;
        }

        internal string[] GetItemNamesWithIds()
        {
            return this.items.Select(it => $"{it.name}").ToArray();
        }

        internal string[] GetItemNames()
        {
            return this.items.Select(it => it.name).ToArray();
        }


        internal bool IsEmpty()
        {
            return this.items == null || this.items.Count == 0;
        }

        internal void RemoveItemAt(int index)
        {
            if (this.items != null && this.items.Count > index)
            {
                this.items.RemoveAt(index);
            }
        }

        internal bool HasItemWithName(string name)
        {
            return this.items.Any(key => key.type == name);
        }
    }
}
#endif
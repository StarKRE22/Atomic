#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Atomic.Entities
{
    //TODO: Add catergories!
    public sealed class TagsConfig : ScriptableObject
    {
        [SerializeField]
        public string @namespace = "Atomic.Entities";

        [SerializeField]
        public string directoryPath = "Assets/Codegen";

        [SerializeField]
        public string className = "TagAPI";

        [Space]
        [SerializeField]
        public List<Item> items = new();

        [Serializable]
        public sealed class Item : IComparable<Item>
        {
            public int id;
            public string type;

            public int CompareTo(Item other)
            {
                return this.id.CompareTo(other.id);
            }
        }


        public bool TryFindNameById(int id, out string name)
        {
            for (int i = 0, count = this.items.Count; i < count; i++)
            {
                Item item = this.items[i];
                if (item.id == id)
                {
                    name = item.type;
                    return true;
                }
            }


            name = "undefined";
            return false;
        }

        public string FindNameById(int id)
        {
            foreach (var item in this.items)
            {
                if (item.id == id)
                {
                    return item.type;
                }
            }

            return "undefined";
        }

        public int IndexOfItem(int id)
        {
            for (int i = 0, count = this.items.Count; i < count; i++)
            {
                Item item = this.items[i];
                if (item.id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        public int IndexOfItem(string type)
        {
            for (int i = 0, count = this.items.Count; i < count; i++)
            {
                Item item = this.items[i];
                if (item.type == type)
                {
                    return i;
                }
            }

            return -1;
        }


        public bool HasDuplicatedId(out int duplicatedId)
        {
            if (this.items == null || this.items.Count == 0)
            {
                duplicatedId = default;
                return false;
            }

            int count = this.items.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (this.items[i].id == this.items[j].id)
                    {
                        duplicatedId = this.items[i].id;
                        return true;
                    }
                }
            }

            duplicatedId = default;
            return false;
        }

        public bool HasDuplicatedType(out string duplicatedType)
        {
            if (this.items == null || this.items.Count == 0)
            {
                duplicatedType = default;
                return false;
            }

            int count = this.items.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (this.items[i].type == this.items[j].type)
                    {
                        duplicatedType = this.items[i].type;
                        return true;
                    }
                }
            }

            duplicatedType = default;
            return false;
        }

        public void RemoveItemAt(int index)
        {
            this.items.RemoveAt(index);
        }

        public bool TypeExists(string type)
        {
            return this.items.Any(key => key.type == type);
        }

        public int GetFreeId()
        {
            List<int> freeIds = new List<int>();
            for (int i = 0, count = this.items.Count; i < count; i++)
            {
                freeIds.Add(i + 1);
            }

            foreach (Item item in this.items)
            {
                freeIds.Remove(item.id);
            }

            if (freeIds.Count > 0)
            {
                return freeIds[0];
            }

            return this.items.Count + 1;
        }

        public void AddItem(int id, string type)
        {
            this.items.Add(new Item
            {
                id = id,
                type = type
            });
        }

        public bool IsUniqueueType(string type)
        {
            return this.items.Count(it => it.type == type) == 1;
        }

        public bool IsUniqueueId(int id)
        {
            return this.items.Count(it => it.id == id) == 1;
        }

        public bool AllMatchesPattern(string regex)
        {
            return this.items.All(it => Regex.IsMatch(it.type, regex));
        }

        public string[] GetItemTypes()
        {
            return this.items.Select(it => it.type).ToArray();
        }

        public Item GetItem(int index)
        {
            return this.items[index];
        }

        public string[] GetItemTypesWithIds()
        {
            return this.items.Select(it => $"{it.type}").ToArray();
        }
    }
}
#endif
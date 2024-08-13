#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Atomic.Entities
{
    //Расширить атомарку чтобы можно было делать разные конфигурации
    public sealed class ValueConfig : ScriptableObject
    {
        [SerializeField]
        public bool local;
        
        [SerializeField]
        public string @namespace = "Atomic.Entities";

        [SerializeField]
        public string directoryPath = "Assets/Codegen";

        [SerializeField]
        public string suffix = "API";

        [SerializeField]
        public string[] imports = Array.Empty<string>();

        [SerializeField]
        public List<Category> categories = new();

        [Serializable]
        public sealed class Category
        {
            public string name;
            public List<Item> indexes = new();

            public int IndexOfItem(string name)
            {
                for (int i = 0, count = this.indexes.Count; i < count; i++)
                {
                    Item item = this.indexes[i];
                    if (item.name == name)
                    {
                        return i;
                    }
                }

                return -1;
            }

            public string[] GetItemNamesWithIds()
            {
                return this.indexes.Select(it => $"{it.name}").ToArray();
            }

            public string[] GetItemNames()
            {
                return this.indexes.Select(it => it.name).ToArray();
            }

            public Item GetItem(int index)
            {
                return this.indexes[index];
            }

            public bool IsEmpty()
            {
                return this.indexes == null || this.indexes.Count == 0;
            }
        }

        [Serializable]
        public sealed class Item : IComparable<Item>
        {
            public int id;
            public string name;
            public string type;

            public int CompareTo(Item other)
            {
                return this.id.CompareTo(other.id);
            }
        }

        public bool HasDuplicatedId(out int duplicatedId)
        {
            if (this.categories == null || this.categories.Count == 0)
            {
                duplicatedId = default;
                return false;
            }

            List<int> allIdentifiers = new List<int>();
            foreach (var category in this.categories)
            {
                allIdentifiers.AddRange(category.indexes.Select(it => it.id));
            }

            int count = allIdentifiers.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (allIdentifiers[i] == allIdentifiers[j])
                    {
                        duplicatedId = allIdentifiers[i];
                        return true;
                    }
                }
            }

            duplicatedId = default;
            return false;
        }

        public bool HasDuplicatedName(int categoryIndex, out string duplicatedName)
        {
            if (this.categories == null || this.categories.Count == 0)
            {
                duplicatedName = default;
                return false;
            }

            if (categoryIndex < 0 || categoryIndex >= this.categories.Count)
            {
                duplicatedName = default;
                return false;
            }

            List<Item> indexes = categories[categoryIndex].indexes;
            int count = indexes.Count;

            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (indexes[i].name == indexes[j].name)
                    {
                        duplicatedName = indexes[i].name;
                        return true;
                    }
                }
            }

            duplicatedName = default;
            return false;
        }

        // public void Sort()
        // {
        //     if (this.categories == null || this.categories.Count == 0)
        //     {
        //         return;
        //     }
        //     
        //     foreach (Category category in this.categories)
        //     {
        //         category.indexes.Sort();
        //     }
        // }

        public void RemoveItemAt(int categoryIndex, int itemIndex)
        {
            if (this.categories != null && this.categories.Count > categoryIndex)
            {
                Category category = this.categories[categoryIndex];
                List<Item> items = category.indexes;
                if (items != null && items.Count > itemIndex)
                {
                    items.RemoveAt(itemIndex);
                }
            }
        }

        public int GetFreeId()
        {
            List<Item> allItems = new List<Item>();
            foreach (var category in categories)
            {
                allItems.AddRange(category.indexes);
            }

            int count = allItems.Count;
            List<int> freeIds = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                freeIds.Add(i + 1);
            }

            foreach (Item item in allItems)
            {
                freeIds.Remove(item.id);
            }

            if (freeIds.Count > 0)
            {
                return freeIds[0];
            }

            return count + 1;
        }

        public bool NameExists(int categoryIndex, string name)
        {
            return this.categories[categoryIndex].indexes.Any(key => key.type == name);
        }

        public void AddCategory(string name)
        {
            this.categories.Add(new Category
            {
                name = name,
                indexes = new List<Item>()
            });
        }

        public void AddItem(int categoryIndex, int id, string name, string type)
        {
            if (this.categories != null && this.categories.Count > categoryIndex)
            {
                this.categories[categoryIndex].indexes.Add(new Item
                {
                    id = id,
                    name = name,
                    type = type
                });
            }
        }

        public bool IsUniqueueName(int catalogIndex, string name)
        {
            if (this.categories == null)
            {
                return false;
            }
            
            return this.categories[catalogIndex].indexes.Count(it => it.name == name) == 1;
        }

        public bool IsUniqueueId(int id)
        {
            List<Item> allItems = new List<Item>();
            foreach (var category in categories)
            {
                allItems.AddRange(category.indexes);
            }

            return allItems.Count(it => it.id == id) == 1;
        }

        public bool AllMatchesPattern(string regex)
        {
            if (this.categories == null)
            {
                return false;
            }
            
            return categories.All(category => category.indexes.All(it => Regex.IsMatch(it.name, regex)));
        }

        public bool CategoryExists(string categoryName)
        {
            if (this.categories == null)
            {
                return false;
            }
            
            return this.categories.Any(it => it.name == categoryName);
        }

        public int IndexOfItemInCategory(string categoryName, string itemName)
        {
            foreach (Category category in this.categories)
            {
                if (category.name == categoryName)
                {
                    List<Item> items = category.indexes;
                    for (int i = 0, count = items.Count; i < count; i++)
                    {
                        if (items[i].name == itemName)
                        {
                            return i;
                        }
                    }

                    return -1;
                }
            }

            return -1;
        }

        public int IndexOfCategory(string categoryName, out Category category)
        {
            for (int i = 0, count = this.categories.Count; i < count; i++)
            {
                category = this.categories[i];
                if (category.name == categoryName)
                {
                    return i;
                }
            }

            category = default;
            return -1;
        }

        public string[] GetCategoryNames()
        {
            return this.categories.Select(it => it.name).ToArray();
        }

        public string[] GetCategoryNamesNotEmpty()
        {
            return this.categories
                .Where(it => !it.IsEmpty())
                .Select(it => it.name)
                .ToArray();
        }

        public string GetFullItemNameById(int itemId)
        {
            for (int i = 0, categoriesCount = this.categories.Count; i < categoriesCount; i++)
            {
                Category category = this.categories[i];
                List<Item> items = category.indexes;
                
                
                for (int j = 0, itemsCount = items.Count; j < itemsCount; j++)
                {
                    Item item = items[j];
                    if (item.id == itemId)
                    {
                        return $"{category.name}.{item.name} ({item.id})";
                    }
                }
            }

            return itemId.ToString();
        }

        public bool FindCategoryAndItemById(int itemId, out int categoryIndex, out int itemIndex)
        {
            for (int i = 0, categoriesCount = this.categories.Count; i < categoriesCount; i++)
            {
                var category = this.categories[i];

                List<Item> items = category.indexes;
                for (int j = 0, itemsCount = items.Count; j < itemsCount; j++)
                {
                    var item = items[j];
                    if (item.id == itemId)
                    {
                        categoryIndex = i;
                        itemIndex = j;
                        return true;
                    }
                }
            }

            categoryIndex = 0;
            itemIndex = 0;
            return false;
        }
    }
}
#endif
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
// ReSharper disable UnusedAutoPropertyAccessor.Local

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Contexts
{
    internal sealed class APICatalog : ScriptableObject, IEnumerable<APICategory>
    {
        [field: SerializeField]
        internal bool Inactive { get; private set; }

#if ODIN_INSPECTOR
        [DisableIf(nameof(Inactive))]
#endif
        [SerializeField]
        private List<APICategory> categories = new();

        internal int CategoryCount => this.categories.Count;
        
        internal APICategory GetCategory(int index)
        {
            return this.categories[index];
        }

        internal bool HasCategories()
        {
            return this.categories.Count > 0;
        }
        
        public IEnumerator<APICategory> GetEnumerator()
        {
            return this.categories.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
        public bool HasDuplicatedId(out int duplicatedId)
        {
            if (this.categories == null || this.categories.Count == 0)
            {
                duplicatedId = default;
                return false;
            }

            List<int> allIdentifiers = new List<int>();
            foreach (APICategory category in this.categories)
            {
                allIdentifiers.AddRange(category.GetItemIds());
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

            APICategory category = this.categories[categoryIndex];
            IReadOnlyList<APIItem> indexes = category.GetItems();
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

        public void RemoveItemAt(int categoryIndex, int itemIndex)
        {
            if (this.categories != null && this.categories.Count > categoryIndex)
            {
                APICategory category = this.categories[categoryIndex];
                category.RemoveItemAt(itemIndex);
            }
        }

        public int GetFreeId()
        {
            List<APIItem> allItems = new List<APIItem>();
            foreach (APICategory category in categories)
            {
                allItems.AddRange(category.GetItems());
            }

            int count = allItems.Count;
            
            List<int> freeIds = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                freeIds.Add(i + 1);
            }

            foreach (APIItem item in allItems)
            {
                freeIds.Remove(item.id);
            }

            if (freeIds.Count > 0)
            {
                return freeIds[0];
            }

            return count + 1;
        }

        public bool HasItemWithName(int categoryIndex, string name)
        {
            return this.categories[categoryIndex].HasItemWithName(name);
        }

        public void CreateCategory(string name)
        {
            this.categories.Add(new APICategory(name));
        }

        public void CreateItemForCategory(int categoryIndex, int id, string name, string type)
        {
            if (this.categories != null && this.categories.Count > categoryIndex)
            {
                this.categories[categoryIndex].AddItem(id, name,type);
            }
        }

        public bool IsUniqueueItemNameInCategory(int catalogIndex, string name)
        {
            return this.categories != null && 
                   this.categories[catalogIndex].IsUniqueItemName(name);
        }

        public bool IsUniqueueItemId(int id)
        {
            List<APIItem> allItems = new List<APIItem>();
            
            foreach (APICategory category in this.categories)
            {
                allItems.AddRange(category.GetItems());
            }

            return allItems.Count(it => it.id == id) == 1;
        }

        public bool AllItemsHasValidName(string regex)
        {
            return this.categories != null &&
                   categories.All(category => category.GetItems().All(it => Regex.IsMatch(it.name, regex)));
        }

        public bool CategoryWithNameExists(string categoryName)
        {
            return this.categories != null && this.categories.Any(it => it.Name == categoryName);
        }

        public int GetIndexOfItemInCategory(string categoryName, string itemName)
        {
            foreach (APICategory category in this.categories)
            {
                if (category.Name == categoryName)
                {
                    return category.GetIndexOfItem(itemName);
                }
            }

            return -1;
        }

        public int GetIndexOfCategory(string categoryName, out APICategory category)
        {
            for (int i = 0, count = this.categories.Count; i < count; i++)
            {
                category = this.categories[i];
                if (category.Name == categoryName)
                {
                    return i;
                }
            }

            category = default;
            return -1;
        }

        public string[] GetCategoryNames()
        {
            return this.categories
                .Select(it => it.Name)
                .ToArray();
        }

        public string[] GetNotEmptyCategoryNames()
        {
            return this.categories
                .Where(it => !it.IsEmpty())
                .Select(it => it.Name)
                .ToArray();
        }

        public string GetFullItemNameById(int itemId)
        {
            for (int i = 0, categoriesCount = this.categories.Count; i < categoriesCount; i++)
            {
                APICategory apiCategory = this.categories[i];
                IReadOnlyList<APIItem> items = apiCategory.GetItems();
                
                for (int j = 0, itemsCount = items.Count; j < itemsCount; j++)
                {
                    APIItem item = items[j];
                    if (item.id == itemId)
                    {
                        return $"{apiCategory.Name}.{item.name} ({item.id})";
                    }
                }
            }

            return itemId.ToString();
        }

        public bool FindCategoryAndItemById(int itemId, out int categoryIndex, out int itemIndex)
        {
            for (int i = 0, categoriesCount = this.categories.Count; i < categoriesCount; i++)
            {
                APICategory category = this.categories[i];
                IReadOnlyList<APIItem> items = category.GetItems();
                
                for (int j = 0, itemsCount = items.Count; j < itemsCount; j++)
                {
                    APIItem item = items[j];
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
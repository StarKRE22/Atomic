using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    [CreateAssetMenu(
        fileName = "EntityViewCatalog",
        menuName = "Atomic/Entities/New EntityViewCatalog"
    )]
    public class EntityViewCatalog : ScriptableObject
    {
        [SerializeField]
        private List<ViewBase> _prefabs;

        public int Count => _prefabs.Count;

        public KeyValuePair<string, ViewBase> GetPrefab(int index)
        {
            ViewBase view = _prefabs[index];
            return new KeyValuePair<string, ViewBase>(this.GetName(view), view);
        }

        public ViewBase GetPrefab(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                ViewBase prefab = _prefabs[i];
                string prefabName = this.GetName(prefab);
                if (prefabName == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }

        protected virtual string GetName(ViewBase prefab) => prefab.Name;
    }
}


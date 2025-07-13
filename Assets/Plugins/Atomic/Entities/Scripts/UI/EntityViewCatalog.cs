using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    [CreateAssetMenu(
        fileName = "Entity View Catalog",
        menuName = "Atomic/Entities/Entity View Catalog"
    )]
    public class EntityViewCatalog : ScriptableObject
    {
        [SerializeField]
        private List<EntityView> _prefabs;

        public int Count => _prefabs.Count;

        public KeyValuePair<string, EntityView> GetPrefab(int index)
        {
            EntityView view = _prefabs[index];
            return new KeyValuePair<string, EntityView>(this.GetName(view), view);
        }

        public EntityView GetPrefab(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                EntityView prefab = _prefabs[i];
                string prefabName = this.GetName(prefab);
                if (prefabName == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }

        protected virtual string GetName(EntityView prefab) => prefab.Name;
    }
}


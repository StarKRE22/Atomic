using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class ViewCatalog<E> : ScriptableObject where E : IEntity<E>
    {
        [SerializeField]
        private List<EntityViewBase> _prefabs;

        public int Count => _prefabs.Count;

        public KeyValuePair<string, EntityViewBase> GetPrefab(int index)
        {
            EntityViewBase view = _prefabs[index];
            return new KeyValuePair<string, EntityViewBase>(this.GetName(view), view);
        }

        public EntityViewBase GetPrefab(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                EntityViewBase prefab = _prefabs[i];
                string prefabName = this.GetName(prefab);
                if (prefabName == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }

        protected virtual string GetName(EntityViewBase prefab) => prefab.Name;
    }
}


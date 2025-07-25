using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntityViewCatalog<E> : ScriptableObject where E : IEntity
    {
        [SerializeField]
        private List<EntityViewBase<E>> _prefabs;

        public int Count => _prefabs.Count;

        public KeyValuePair<string, EntityViewBase<E>> GetPrefab(int index)
        {
            EntityViewBase<E> view = _prefabs[index];
            return new KeyValuePair<string, EntityViewBase<E>>(this.GetName(view), view);
        }

        public EntityViewBase<E> GetPrefab(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                EntityViewBase<E> prefab = _prefabs[i];
                string prefabName = this.GetName(prefab);
                if (prefabName == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }

        protected virtual string GetName(EntityViewBase<E> prefab) => prefab.Name;
    }
}


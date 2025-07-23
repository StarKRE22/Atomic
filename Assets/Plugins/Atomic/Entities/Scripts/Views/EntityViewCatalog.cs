using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntityViewCatalog<E> : ScriptableObject where E : IEntity
    {
        [SerializeField]
        private List<EntityViewAbstract<E>> _prefabs;

        public int Count => _prefabs.Count;

        public KeyValuePair<string, EntityViewAbstract<E>> GetPrefab(int index)
        {
            EntityViewAbstract<E> view = _prefabs[index];
            return new KeyValuePair<string, EntityViewAbstract<E>>(this.GetName(view), view);
        }

        public EntityViewAbstract<E> GetPrefab(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                EntityViewAbstract<E> prefab = _prefabs[i];
                string prefabName = this.GetName(prefab);
                if (prefabName == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }

        protected virtual string GetName(EntityViewAbstract<E> prefab) => prefab.Name;
    }
}


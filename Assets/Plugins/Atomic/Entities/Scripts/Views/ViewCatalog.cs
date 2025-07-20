using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class ViewCatalog<E> : ScriptableObject where E : IEntity<E>
    {
        [SerializeField]
        private List<AbstractView<E>> _prefabs;

        public int Count => _prefabs.Count;

        public KeyValuePair<string, AbstractView<E>> GetPrefab(int index)
        {
            AbstractView<E> view = _prefabs[index];
            return new KeyValuePair<string, AbstractView<E>>(this.GetName(view), view);
        }

        public AbstractView<E> GetPrefab(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                AbstractView<E> prefab = _prefabs[i];
                string prefabName = this.GetName(prefab);
                if (prefabName == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }

        protected virtual string GetName(AbstractView<E> prefab) => prefab.Name;
    }
}


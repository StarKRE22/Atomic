using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [DisallowMultipleComponent]
    public abstract class ViewPool<E> : MonoBehaviour where E : IEntity<E>
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private ViewCatalog<E>[] _catalogs;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, AbstractView<E>> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Stack<AbstractView<E>>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }
        
        public AbstractView<E> Rent(string name)
        {
            Stack<AbstractView<E>> pool = this.GetPool(name);
            if (pool.TryPop(out AbstractView<E> view))
                return view;

            if (!_prefabs.TryGetValue(name, out AbstractView<E> prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(string name, AbstractView<E> view)
        {
            Stack<AbstractView<E>> pool = this.GetPool(name);
            pool.Push(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Stack<AbstractView<E>> pool in _pools.Values)
            {
                foreach (AbstractView<E> view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Stack<AbstractView<E>> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Stack<AbstractView<E>> pool))
                return pool;

            pool = new Stack<AbstractView<E>>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefab(string entityName, AbstractView<E> prefab) => _prefabs.Add(entityName, prefab);

        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        public void AddPrefabs(ViewCatalog<E> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, AbstractView<E> value) = catalog.GetPrefab(i);
                _prefabs.Add(key, value);
            }
        }

        public void RemovePrefabs(ViewCatalog<E> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, _) = catalog.GetPrefab(i);
                _prefabs.Remove(key);
            }
        }
    }
}
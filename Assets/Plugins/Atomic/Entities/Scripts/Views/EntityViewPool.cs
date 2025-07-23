using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [DisallowMultipleComponent]
    public abstract class EntityViewPool<E> : MonoBehaviour where E : IEntity
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private EntityViewCatalog<E>[] _catalogs;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, AbstractEntityView<E>> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Stack<AbstractEntityView<E>>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }
        
        public AbstractEntityView<E> Rent(string name)
        {
            Stack<AbstractEntityView<E>> pool = this.GetPool(name);
            if (pool.TryPop(out AbstractEntityView<E> view))
                return view;

            if (!_prefabs.TryGetValue(name, out AbstractEntityView<E> prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(string name, AbstractEntityView<E> view)
        {
            Stack<AbstractEntityView<E>> pool = this.GetPool(name);
            pool.Push(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Stack<AbstractEntityView<E>> pool in _pools.Values)
            {
                foreach (AbstractEntityView<E> view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Stack<AbstractEntityView<E>> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Stack<AbstractEntityView<E>> pool))
                return pool;

            pool = new Stack<AbstractEntityView<E>>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefab(string entityName, AbstractEntityView<E> prefab) => _prefabs.Add(entityName, prefab);

        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        public void AddPrefabs(EntityViewCatalog<E> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, AbstractEntityView<E> value) = catalog.GetPrefab(i);
                _prefabs.Add(key, value);
            }
        }

        public void RemovePrefabs(EntityViewCatalog<E> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, _) = catalog.GetPrefab(i);
                _prefabs.Remove(key);
            }
        }
    }
}
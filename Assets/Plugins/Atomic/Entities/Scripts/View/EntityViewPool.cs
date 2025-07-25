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
        private readonly Dictionary<string, EntityViewBase<E>> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Stack<EntityViewBase<E>>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }
        
        public EntityViewBase<E> Rent(string name)
        {
            Stack<EntityViewBase<E>> pool = this.GetPool(name);
            if (pool.TryPop(out EntityViewBase<E> view))
                return view;

            if (!_prefabs.TryGetValue(name, out EntityViewBase<E> prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(string name, EntityViewBase<E> view)
        {
            Stack<EntityViewBase<E>> pool = this.GetPool(name);
            pool.Push(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Stack<EntityViewBase<E>> pool in _pools.Values)
            {
                foreach (EntityViewBase<E> view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Stack<EntityViewBase<E>> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Stack<EntityViewBase<E>> pool))
                return pool;

            pool = new Stack<EntityViewBase<E>>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefab(string entityName, EntityViewBase<E> prefab) => _prefabs.Add(entityName, prefab);

        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        public void AddPrefabs(EntityViewCatalog<E> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, EntityViewBase<E> value) = catalog.GetPrefab(i);
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
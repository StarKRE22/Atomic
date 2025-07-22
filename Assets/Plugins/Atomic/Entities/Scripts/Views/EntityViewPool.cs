using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [DisallowMultipleComponent]
    public abstract class EntityViewPool<E> : MonoBehaviour where E : IEntity<E>
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private ViewCatalog<E>[] _catalogs;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, EntityViewBase> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Stack<EntityViewBase>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }
        
        public EntityViewBase<E> Rent(string name)
        {
            Stack<EntityViewBase> pool = this.GetPool(name);
            if (pool.TryPop(out EntityViewBase view))
                return view;

            if (!_prefabs.TryGetValue(name, out EntityViewBase prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(string name, EntityViewBase<E> view)
        {
            Stack<EntityViewBase> pool = this.GetPool(name);
            pool.Push(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Stack<EntityViewBase> pool in _pools.Values)
            {
                foreach (EntityViewBase view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Stack<EntityViewBase> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Stack<EntityViewBase> pool))
                return pool;

            pool = new Stack<EntityViewBase>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefab(string entityName, EntityViewBase prefab) => _prefabs.Add(entityName, prefab);

        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        public void AddPrefabs(ViewCatalog<E> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, EntityViewBase value) = catalog.GetPrefab(i);
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
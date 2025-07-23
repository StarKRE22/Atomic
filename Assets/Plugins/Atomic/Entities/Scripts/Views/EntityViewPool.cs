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
        private readonly Dictionary<string, EntityViewAbstract<E>> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Stack<EntityViewAbstract<E>>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }
        
        public EntityViewAbstract<E> Rent(string name)
        {
            Stack<EntityViewAbstract<E>> pool = this.GetPool(name);
            if (pool.TryPop(out EntityViewAbstract<E> view))
                return view;

            if (!_prefabs.TryGetValue(name, out EntityViewAbstract<E> prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(string name, EntityViewAbstract<E> view)
        {
            Stack<EntityViewAbstract<E>> pool = this.GetPool(name);
            pool.Push(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Stack<EntityViewAbstract<E>> pool in _pools.Values)
            {
                foreach (EntityViewAbstract<E> view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Stack<EntityViewAbstract<E>> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Stack<EntityViewAbstract<E>> pool))
                return pool;

            pool = new Stack<EntityViewAbstract<E>>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefab(string entityName, EntityViewAbstract<E> prefab) => _prefabs.Add(entityName, prefab);

        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        public void AddPrefabs(EntityViewCatalog<E> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, EntityViewAbstract<E> value) = catalog.GetPrefab(i);
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
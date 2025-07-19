using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity View Pool")]
    [DisallowMultipleComponent]
    public class EntityViewPool : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private EntityViewCatalog[] _catalogs;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, EntityViewBase> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Queue<EntityViewBase>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }
        
        public EntityViewBase Rent(string name)
        {
            Queue<EntityViewBase> pool = this.GetPool(name);
            if (pool.TryDequeue(out EntityViewBase view))
                return view;

            if (!_prefabs.TryGetValue(name, out EntityViewBase prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(string name, EntityViewBase view)
        {
            Queue<EntityViewBase> pool = this.GetPool(name);
            pool.Enqueue(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Queue<EntityViewBase> pool in _pools.Values)
            {
                foreach (EntityViewBase view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Queue<EntityViewBase> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Queue<EntityViewBase> pool))
                return pool;

            pool = new Queue<EntityViewBase>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefab(string entityName, EntityViewBase prefab) => _prefabs.Add(entityName, prefab);

        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        public void AddPrefabs(EntityViewCatalog catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, EntityViewBase value) = catalog.GetPrefab(i);
                _prefabs.Add(key, value);
            }
        }

        public void RemovePrefabs(EntityViewCatalog catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, _) = catalog.GetPrefab(i);
                _prefabs.Remove(key);
            }
        }
    }
}
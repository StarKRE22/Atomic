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
        private readonly Dictionary<string, ViewBase> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Queue<ViewBase>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }
        
        public ViewBase Rent(string name)
        {
            Queue<ViewBase> pool = this.GetPool(name);
            if (pool.TryDequeue(out ViewBase view))
                return view;

            if (!_prefabs.TryGetValue(name, out ViewBase prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(string name, ViewBase view)
        {
            Queue<ViewBase> pool = this.GetPool(name);
            pool.Enqueue(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Queue<ViewBase> pool in _pools.Values)
            {
                foreach (ViewBase view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Queue<ViewBase> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Queue<ViewBase> pool))
                return pool;

            pool = new Queue<ViewBase>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefab(string entityName, ViewBase prefab) => _prefabs.Add(entityName, prefab);

        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        public void AddPrefabs(EntityViewCatalog catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, ViewBase value) = catalog.GetPrefab(i);
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
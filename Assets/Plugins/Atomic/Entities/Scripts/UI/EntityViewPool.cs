using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity View Pool")]
    [DisallowMultipleComponent]
    public sealed class EntityViewPool : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private EntityViewCatalog[] _catalogs;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, EntityView> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Queue<EntityView>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }

        public IEnumerator Iniitalize()
        {

            GameObject prefab = null;
            AsyncInstantiateOperation<GameObject> operation = InstantiateAsync(prefab, 2000);
            yield return operation;
            GameObject entityViewCatalogs = operation.Result[0];
        }

        public EntityView Rent(string name)
        {
            Queue<EntityView> pool = this.GetPool(name);
            if (pool.TryDequeue(out EntityView presenter))
                return presenter;

            if (!_prefabs.TryGetValue(name, out EntityView prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(string name, EntityView view)
        {
            Queue<EntityView> pool = this.GetPool(name);
            pool.Enqueue(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Queue<EntityView> pool in _pools.Values)
            {
                foreach (EntityView view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Queue<EntityView> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Queue<EntityView> pool))
                return pool;

            pool = new Queue<EntityView>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefabs(EntityViewCatalog catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, EntityView value) = catalog.GetPrefab(i);
                _prefabs.Add(key, value);
            }
        }

        public void AddPrefab(string entityName, EntityView prefab)
        {
            _prefabs.Add(entityName, prefab);
        }

        public void RemovePrefabs(EntityViewCatalog catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, _) = catalog.GetPrefab(i);
                _prefabs.Remove(key);
            }
        }

        public void RemovePrefab(string entityName)
        {
            _prefabs.Remove(entityName);
        }
    }
}
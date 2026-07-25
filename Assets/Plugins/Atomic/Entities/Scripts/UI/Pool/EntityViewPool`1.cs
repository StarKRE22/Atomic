#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Profiling;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public abstract class EntityViewPool<K, E, V> : MonoBehaviour
        where E : class, IEntity
        where V : EntityView<E>
    {
        private static readonly ProfilerMarker s_rentMarker = new($"EntityViewPool<{typeof(E).Name}>.Rent");
        private static readonly ProfilerMarker s_returnMarker = new($"EntityViewPool<{typeof(E).Name}>.Return");

        [Tooltip("The parent transform under which all pooled views will be stored")]
        [SerializeField]
        private Transform container;

        [SerializeField]
        private int initialCapacity = 32;

        [Space]
        [Tooltip("A list of view catalogs to preload view prefabs from on Awake")]
        [SerializeField]
        private EntityViewCatalog<E, V>[] catalogs;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<K, V> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<K, Stack<V>> _pools = new();

        private protected virtual void Awake()
        {
            this.RegisterCatalogs();
        }

        /// <summary>
        /// Registers a new view prefab to the pool by name.
        /// </summary>
        /// <param name="key">The name identifier for the view prefab.</param>
        /// <param name="prefab">The prefab to register.</param>
        public void Register(K key, V prefab) => _prefabs.Add(key, prefab);

        /// <summary>
        /// Removes a registered prefab from the pool.
        /// </summary>
        /// <param name="key">The name of the prefab to remove.</param>
        public void Unregister(K key) => _prefabs.Remove(key);

        /// <summary>
        /// Add all prefabs from a given catalogue to the internal registry.
        /// </summary>
        /// <param name="catalog">The catalogue containing view prefabs to register.</param>
        public void Register(EntityViewCatalog<E, V> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                V value = catalog.GetPrefab(i);
                _prefabs.Add(this.GetKey(value), value);
            }
        }

        /// <summary>
        /// Removes all prefabs from a given catalogue from the internal registry.
        /// </summary>
        /// <param name="catalog">The catalogue containing view prefabs to unregister.</param>
        public void Unregister(EntityViewCatalog<E, V> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                V prefab = catalog.GetPrefab(i);
                _prefabs.Remove(this.GetKey(prefab));
            }
        }

        /// <summary>
        /// Clears all pooled instances and destroys their GameObjects.
        /// </summary>
        public void Clear()
        {
            foreach (Stack<V> pool in _pools.Values)
            {
                foreach (V view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        public async ValueTask InitAsync(K key, int count)
        {
            if (!_prefabs.TryGetValue(key, out V prefab))
                throw new KeyNotFoundException($"EntityView<{typeof(E).Name}> with \"{key}\" was not present in pool!");

            AsyncInstantiateOperation<V> operation = InstantiateAsync(prefab, count, this.container);
            V[] views = await operation;

            int viewCount = views.Length;
            Stack<V> pool = this.GetOrCreatePool(key, Mathf.Max(viewCount, this.initialCapacity));

            for (int i = 0; i < viewCount; i++)
            {
                V view = views[i];
                view.gameObject.SetActive(false);
                pool.Push(view);
            }
        }
        
        public void Init(K key, int count)
        {
            if (!_prefabs.TryGetValue(key, out V prefab))
                throw new KeyNotFoundException($"EntityView<{typeof(E).Name}> with name \"{key}\" was not present in pool!");

            Stack<V> pool = this.GetOrCreatePool(key, Mathf.Max(count, this.initialCapacity));
            for (int i = 0; i < count; i++)
            {
                V view = Instantiate(prefab, this.container);
                view.gameObject.SetActive(false);
                pool.Push(view);
            }
        }

        private void RegisterCatalogs()
        {
            if (this.catalogs != null)
                for (int i = 0, count = this.catalogs.Length; i < count; i++)
                    this.Register(this.catalogs[i]);
        }

        internal V Rent(K key, Transform parent)
        {
            using (s_rentMarker.Auto())
            {
                Stack<V> pool = this.GetOrCreatePool(key, this.initialCapacity);
                if (pool.TryPop(out V view))
                {
                    view.transform.SetParent(parent);
                    view.gameObject.SetActive(true);
                    return view;
                }

                return !_prefabs.TryGetValue(key, out V prefab)
                    ? throw new KeyNotFoundException($"EntityView<{typeof(E).Name}> with key \"{key}\" was not present in pool!")
                    : Instantiate(prefab, parent);
            }
        }

        internal void Return(V view)
        {
            using (s_returnMarker.Auto())
            {
                Stack<V> pool = this.GetOrCreatePool(this.GetKey(view), this.initialCapacity);
                pool.Push(view);

                if (view)
                {
                    view.transform.parent = this.container;
                    view.gameObject.SetActive(false);
                }
            }
        }

        protected abstract K GetKey(V view);

        private Stack<V> GetOrCreatePool(K key, int initialCapacity)
        {
            if (!_pools.TryGetValue(key, out Stack<V> pool))
            {
                pool = new Stack<V>(initialCapacity);
                _pools.Add(key, pool);
            }

            return pool;
        }
    }
}
#endif
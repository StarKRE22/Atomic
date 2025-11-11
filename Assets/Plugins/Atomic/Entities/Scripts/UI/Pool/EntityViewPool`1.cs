#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A pool system for managing reusable EntityView instances.
    /// Supports preloading from catalogs, runtime instantiation, renting, and returning views to minimize runtime allocations.
    /// Use for efficient management of frequently spawned or displayed entity views.
    /// </summary>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/UI/EntityViewPool%601.md")]
    public abstract class EntityViewPool<E, V> : MonoBehaviour
        where E : class, IEntity
        where V : EntityView<E>
    {
        [Tooltip("The parent transform under which all pooled views will be stored")]
        [SerializeField]
        internal Transform container;

        [Space]
        [Tooltip("A list of view catalogs to preload view prefabs from on Awake")]
        [SerializeField]
        internal EntityViewCatalog<E, V>[] catalogs;

        /// <summary>
        /// A dictionary mapping view names to their prefab instances.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, V> _prefabs = new();

        /// <summary>
        /// A dictionary mapping view names to stacks of pooled instances.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Stack<V>> _pools = new();

        /// <summary>
        /// Called by Unity when the component is initialized.
        /// Loads prefabs from the assigned catalogs.
        /// </summary>
        protected virtual void Awake()
        {
            if (this.catalogs != null)
                for (int i = 0, count = this.catalogs.Length; i < count; i++)
                    this.RegisterPrefabs(this.catalogs[i]);
        }

        /// <summary>
        /// Rents a view by name from the pool. If the pool is empty, a new instance is created.
        /// </summary>
        /// <param name="name">The name of the view to retrieve.</param>
        /// <returns>A reusable <see cref="EntityView"/> instance.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the view prefab was not registered.</exception>
        public V Rent(string name)
        {
            Stack<V> pool = this.GetPool(name);
            if (pool.TryPop(out V view))
                return view;

            if (!_prefabs.TryGetValue(name, out V prefab))
                throw new KeyNotFoundException($"EntityView with name \"{name}\" was not present in EntityViewPool!");

            return Instantiate(prefab, this.container);
        }

        /// <summary>
        /// Returns a view back to its corresponding pool for future reuse.
        /// </summary>
        /// <param name="name">The name of the view being returned.</param>
        /// <param name="view">The view instance to return.</param>
        public void Return(string name, V view)
        {
            Stack<V> pool = this.GetPool(name);
            pool.Push(view);
            if (view)
                view.transform.parent = this.container;
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

        /// <summary>
        /// Retrieves the pool stack for a given view name. Creates one if it does not exist.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <returns>A stack of pooled views for the given name.</returns>
        private Stack<V> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Stack<V> pool))
                return pool;

            pool = new Stack<V>();
            _pools.Add(name, pool);
            return pool;
        }

        /// <summary>
        /// Registers a new view prefab to the pool by name.
        /// </summary>
        /// <param name="name">The name identifier for the view prefab.</param>
        /// <param name="prefab">The prefab to register.</param>
        public void RegisterPrefab(string name, V prefab) => _prefabs.Add(name, prefab);

        /// <summary>
        /// Removes a registered prefab from the pool.
        /// </summary>
        /// <param name="name">The name of the prefab to remove.</param>
        public void UnregisterPrefab(string name) => _prefabs.Remove(name);

        /// <summary>
        /// Adds all prefabs from a given catalog to the internal registry.
        /// </summary>
        /// <param name="catalog">The catalog containing view prefabs to register.</param>
        public void RegisterPrefabs(EntityViewCatalog<E, V> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, V value) = catalog.GetPrefab(i);
                _prefabs.Add(key, value);
            }
        }

        /// <summary>
        /// Removes all prefabs from a given catalog from the internal registry.
        /// </summary>
        /// <param name="catalog">The catalog containing view prefabs to unregister.</param>
        public void UnregisterPrefabs(EntityViewCatalog<E, V> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, _) = catalog.GetPrefab(i);
                _prefabs.Remove(key);
            }
        }
    }
}
#endif
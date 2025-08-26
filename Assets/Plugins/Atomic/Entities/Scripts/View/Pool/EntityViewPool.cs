#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity-based pool manager for reusing entity view instances based on their names.
    /// This reduces memory allocations and improves performance by avoiding frequent instantiations.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity View Pool")]
    [DisallowMultipleComponent]
    public class EntityViewPool : EntityViewPoolAbstract
    {
        [Tooltip("The parent transform under which all pooled views will be stored")]
        [SerializeField]
        internal Transform _container;

        [Space]
        [Tooltip("A list of view catalogs to preload view prefabs from on Awake")]
        [SerializeField]
        internal EntityViewCatalog[] _catalogs;

        /// <summary>
        /// A dictionary mapping view names to their prefab instances.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, EntityViewBase> _prefabs = new();

        /// <summary>
        /// A dictionary mapping view names to stacks of pooled instances.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Stack<EntityViewBase>> _pools = new();

        /// <summary>
        /// Called by Unity when the component is initialized.
        /// Loads prefabs from the assigned catalogs.
        /// </summary>
        protected virtual void Awake()
        {
            if (_catalogs != null)
                for (int i = 0, count = _catalogs.Length; i < count; i++)
                    this.AddPrefabs(_catalogs[i]);
        }

        /// <summary>
        /// Rents a view by name from the pool. If the pool is empty, a new instance is created.
        /// </summary>
        /// <param name="name">The name of the view to retrieve.</param>
        /// <returns>A reusable <see cref="EntityViewBase{E}"/> instance.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the view prefab was not registered.</exception>
        public sealed override EntityViewBase Rent(string name)
        {
            Stack<EntityViewBase> pool = this.GetPool(name);
            if (pool.TryPop(out EntityViewBase view))
                return view;

            if (!_prefabs.TryGetValue(name, out EntityViewBase prefab))
                throw new KeyNotFoundException($"Entity view with name \"{name}\" was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        /// <summary>
        /// Returns a view back to its corresponding pool for future reuse.
        /// </summary>
        /// <param name="name">The name of the view being returned.</param>
        /// <param name="view">The view instance to return.</param>
        public sealed override void Return(string name, EntityViewBase view)
        {
            Stack<EntityViewBase> pool = this.GetPool(name);
            pool.Push(view);
            if (view) 
                view.transform.parent = _container;
        }

        /// <summary>
        /// Clears all pooled instances and destroys their GameObjects.
        /// </summary>
        public sealed override void Clear()
        {
            foreach (Stack<EntityViewBase> pool in _pools.Values)
            {
                foreach (EntityViewBase view in pool)
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
        private Stack<EntityViewBase> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Stack<EntityViewBase> pool))
                return pool;

            pool = new Stack<EntityViewBase>();
            _pools.Add(name, pool);
            return pool;
        }

        /// <summary>
        /// Adds a new view prefab to the pool by name.
        /// </summary>
        /// <param name="entityName">The name identifier for the view prefab.</param>
        /// <param name="prefab">The prefab to register.</param>
        public void AddPrefab(string entityName, EntityViewBase prefab) => _prefabs.Add(entityName, prefab);

        /// <summary>
        /// Removes a registered prefab from the pool.
        /// </summary>
        /// <param name="entityName">The name of the prefab to remove.</param>
        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        /// <summary>
        /// Adds all prefabs from a given catalog to the internal registry.
        /// </summary>
        /// <param name="catalog">The catalog containing view prefabs to register.</param>
        public void AddPrefabs(EntityViewCatalog catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, EntityViewBase value) = catalog.GetPrefab(i);
                _prefabs.Add(key, value);
            }
        }

        /// <summary>
        /// Removes all prefabs from a given catalog from the internal registry.
        /// </summary>
        /// <param name="catalog">The catalog containing view prefabs to unregister.</param>
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
#endif
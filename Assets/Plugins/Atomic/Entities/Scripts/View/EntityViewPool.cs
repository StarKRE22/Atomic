#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="EntityViewPool{IEntity}"/>.
    /// Used to manage view pooling for base <see cref="IEntity"/> types.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity View Pool")]
    [DisallowMultipleComponent]
    public class EntityViewPool : EntityViewPool<IEntity>
    {
    }

    /// <summary>
    /// A Unity-based pool manager for reusing entity view instances based on their names.
    /// This reduces memory allocations and improves performance by avoiding frequent instantiations.
    /// </summary>
    /// <typeparam name="E">The type of entity associated with the views. Must implement <see cref="IEntity"/>.</typeparam>
    public abstract class EntityViewPool<E> : MonoBehaviour where E : IEntity
    {
        [Tooltip("The parent transform under which all pooled views will be stored")]
        [SerializeField]
        private Transform _container;

        [Tooltip("A list of view catalogs to preload view prefabs from on Awake")]
        [SerializeField]
        private EntityViewCatalog<E>[] _catalogs;

        /// <summary>
        /// A dictionary mapping view names to their prefab instances.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, EntityViewBase<E>> _prefabs = new();

        /// <summary>
        /// A dictionary mapping view names to stacks of pooled instances.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Stack<EntityViewBase<E>>> _pools = new();

        /// <summary>
        /// Called by Unity when the component is initialized.
        /// Loads prefabs from the assigned catalogs.
        /// </summary>
        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }

        /// <summary>
        /// Rents a view by name from the pool. If the pool is empty, a new instance is created.
        /// </summary>
        /// <param name="name">The name of the view to retrieve.</param>
        /// <returns>A reusable <see cref="EntityViewBase{E}"/> instance.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the view prefab was not registered.</exception>
        public EntityViewBase<E> Rent(string name)
        {
            Stack<EntityViewBase<E>> pool = this.GetPool(name);
            if (pool.TryPop(out EntityViewBase<E> view))
                return view;

            if (!_prefabs.TryGetValue(name, out EntityViewBase<E> prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        /// <summary>
        /// Returns a view back to its corresponding pool for future reuse.
        /// </summary>
        /// <param name="name">The name of the view being returned.</param>
        /// <param name="view">The view instance to return.</param>
        public void Return(string name, EntityViewBase<E> view)
        {
            Stack<EntityViewBase<E>> pool = this.GetPool(name);
            pool.Push(view);
            view.transform.parent = _container;
        }

        /// <summary>
        /// Clears all pooled instances and destroys their GameObjects.
        /// </summary>
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

        /// <summary>
        /// Retrieves the pool stack for a given view name. Creates one if it does not exist.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <returns>A stack of pooled views for the given name.</returns>
        private Stack<EntityViewBase<E>> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Stack<EntityViewBase<E>> pool))
                return pool;

            pool = new Stack<EntityViewBase<E>>();
            _pools.Add(name, pool);
            return pool;
        }

        /// <summary>
        /// Adds a new view prefab to the pool by name.
        /// </summary>
        /// <param name="entityName">The name identifier for the view prefab.</param>
        /// <param name="prefab">The prefab to register.</param>
        public void AddPrefab(string entityName, EntityViewBase<E> prefab) => _prefabs.Add(entityName, prefab);

        /// <summary>
        /// Removes a registered prefab from the pool.
        /// </summary>
        /// <param name="entityName">The name of the prefab to remove.</param>
        public void RemovePrefab(string entityName) => _prefabs.Remove(entityName);

        /// <summary>
        /// Adds all prefabs from a given catalog to the internal registry.
        /// </summary>
        /// <param name="catalog">The catalog containing view prefabs to register.</param>
        public void AddPrefabs(EntityViewCatalog<E> catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, EntityViewBase<E> value) = catalog.GetPrefab(i);
                _prefabs.Add(key, value);
            }
        }

        /// <summary>
        /// Removes all prefabs from a given catalog from the internal registry.
        /// </summary>
        /// <param name="catalog">The catalog containing view prefabs to unregister.</param>
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
#endif
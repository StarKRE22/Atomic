#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A multi-prefab object pool for scene-based entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The type of <see cref="SceneEntity"/> managed by the pool.</typeparam>
    /// <remarks>
    /// This pool allows renting and returning multiple different entity prefabs, each tracked by its own internal pool.
    /// Pools are created lazily and managed by prefab name. Supports pre-warming via <see cref="Init"/>.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Pooling/PrefabEntityPool%601.md")]
    public abstract class PrefabEntityPool<E> : MonoBehaviour, IPrefabEntityPool<E> where E : SceneEntity
    {
        private const string NUMBER_PATTERN = @"\s*\(\d+\)$";

        internal struct Pool
        {
            public Stack<E> stack;
            public Transform container;
            public GameObject go;
        }

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Pool> _pools = new();

        /// <summary>
        /// If not assigned, defaults to the GameObject this script is attached to.
        /// </summary>
        [Space]
        [Tooltip("Root container for pooled entities")]
        [SerializeField]
        private Transform _container;

        [Tooltip("Should don't destroy if scene changed?")]
        [SerializeField]
        private bool _dontDestroyOnLoad;
        
        protected virtual void Awake()
        {
            if (_container == null)
                _container = this.transform;

            if (_dontDestroyOnLoad) 
                DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Pre-initializes the pool for a specific prefab with a defined number of inactive entities.
        /// </summary>
        /// <param name="prefab">The prefab to pool.</param>
        /// <param name="count">How many instances to pre-instantiate.</param>
        public void Init(E prefab, int count)
        {
            string name = this.GetEntityName(prefab);

            if (!_pools.TryGetValue(name, out Pool pool))
            {
                pool = CreatePool(name);
                _pools.Add(name, pool);
            }

            for (int i = 0; i < count; i++)
            {
                E entity = CreateEntity(prefab, pool.container);
                entity.name = name;
                pool.stack.Push(entity);
            }
        }

        /// <inheritdoc />
        public E Rent(E prefab) => this.Rent(prefab, Vector3.zero, Quaternion.identity);

        /// <inheritdoc />
        public E Rent(E prefab, Transform parent) => this.Rent(prefab, parent.position, parent.rotation);

        /// <inheritdoc />
        public E Rent(E prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            string name = GetEntityName(prefab);

            if (!_pools.TryGetValue(name, out Pool pool))
            {
                pool = this.CreatePool(name);
                _pools.Add(name, pool);
            }

            if (pool.stack.TryPop(out E entity))
            {
                Transform tf = entity.transform;
                tf.SetParent(parent, false);
                tf.SetPositionAndRotation(position, rotation);
            }
            else
            {
                entity = this.CreateEntity(prefab, parent);
                entity.name = name;
                entity.transform.SetPositionAndRotation(position, rotation);
            }

            this.OnRent(entity);
            return entity;
        }

        /// <inheritdoc />
        public void Return(E entity)
        {
            string name = GetEntityName(entity);

            if (!_pools.TryGetValue(name, out Pool pool))
            {
                pool = this.CreatePool(name);
                _pools.Add(name, pool);
            }

            if (pool.stack.Contains(entity))
                return;

            this.OnReturn(entity);

            entity.transform.SetParent(pool.container, false);
            pool.stack.Push(entity);
        }

        /// <summary>
        /// Clears the pool for a specific prefab and destroys all associated entities and container.
        /// </summary>
        /// <param name="prefab">The prefab whose pool should be cleared.</param>
        public void Dispose(E prefab)
        {
            string objName = this.GetEntityName(prefab);

            if (!_pools.Remove(objName, out Pool pool))
                return;

            foreach (E entity in pool.stack)
            {
                this.OnDispose(entity);
                Destroy(entity);
            }

            Destroy(pool.go);
        }

        /// <summary>
        /// Clears all pools and destroys all pooled entities.
        /// </summary>
        public void Dispose()
        {
            foreach (KeyValuePair<string, Pool> pair in _pools)
            {
                Pool pool = pair.Value;
                foreach (E entity in pool.stack)
                {
                    this.OnDispose(entity);
                    Destroy(entity);
                }

                Destroy(pool.go);
            }

            _pools.Clear();
        }

        /// <summary>
        /// Called when a new entity instance is created for pooling.
        /// Use this to apply default inactive state or setup.
        /// </summary>
        /// <param name="entity">The new pooled entity.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnCreate(E entity) => entity.gameObject.SetActive(false);

        /// <summary>
        /// Called when an entity is rented from the pool.
        /// Default behavior activates the entity.
        /// </summary>
        /// <param name="entity">The rented entity.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnRent(E entity) => entity.gameObject.SetActive(true);

        /// <summary>
        /// Called when an entity is returned to the pool.
        /// Default behavior deactivates the entity.
        /// </summary>
        /// <param name="entity">The returned entity.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnReturn(E entity) => entity.gameObject.SetActive(false);

        /// <summary>
        /// Called when a pooled entity is destroyed (e.g., during pool cleanup).
        /// Override to dispose resources or unregister events.
        /// </summary>
        /// <param name="entity">The entity being disposed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDispose(E entity)
        {
        }

        /// <summary>
        /// Extracts a clean name from a prefab or entity instance, stripping Unity-generated suffixes like " (1)".
        /// Used internally to group entities by prefab type.
        /// </summary>
        /// <param name="entity">The entity to extract a base name from.</param>
        /// <returns>A clean prefab name for use as a pool key.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual string GetEntityName(E entity) =>
            Regex.Replace(entity.name, NUMBER_PATTERN, string.Empty).Trim();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Pool CreatePool(string name)
        {
            Stack<E> stack = new Stack<E>();
            Transform container = new GameObject($"<{name}s>").transform;
            container.parent = _container;

            return new Pool
            {
                stack = stack,
                container = container
            };
        }

        /// <summary>
        /// Instantiates a new entity instance from the prefab and initializes it.
        /// </summary>
        /// <returns>The newly created entity.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private E CreateEntity(E prefab, Transform container)
        {
            E entity = SceneEntity.Create(prefab, container);
            this.OnCreate(entity);
            return entity;
        }
    }
}
#endif
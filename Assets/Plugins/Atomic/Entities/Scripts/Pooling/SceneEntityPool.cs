#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Default non-generic implementation of a <see cref="SceneEntityPool{T}"/> for base <see cref="SceneEntity"/> types.
    /// </summary>
    /// <remarks>
    /// This component can be added to a GameObject in the Unity scene to manage pooling of <see cref="SceneEntity"/> instances
    /// without requiring generics. It implements <see cref="IEntityPool"/> for compatibility with systems that expect non-generic access.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject to preallocate and reuse pooled entities at runtime:
    /// <code>
    /// var pooledEntity = sceneEntityPool.Rent();
    /// sceneEntityPool.Return(pooledEntity);
    /// </code>
    /// </example>
    [AddComponentMenu("Atomic/Entities/Entity Pool")]
    [DisallowMultipleComponent]
    public class SceneEntityPool : SceneEntityPool<SceneEntity>, IEntityPool
    {
        /// <inheritdoc />
        IEntity IEntityPool<IEntity>.Rent() => this.Rent();

        /// <inheritdoc />
        void IEntityPool<IEntity>.Return(IEntity entity) => this.Return((SceneEntity) entity);

        public static SceneEntityPool Create(CreateArgs args) => Create<SceneEntityPool>(args);
    }

    /// <summary>
    /// A Unity MonoBehaviour-based entity pool for scene-bound entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The type of entity managed by this pool. Must inherit from <see cref="SceneEntity"/>.</typeparam>
    /// <remarks>
    /// This pool uses a prefab to instantiate entities and manages their reuse via a stack.
    /// Entities are activated/deactivated on rent/return, and can be preloaded using <see cref="Init(int)"/>.
    /// </remarks>
    public abstract class SceneEntityPool<E> : MonoBehaviour, IEntityPool<E> where E : SceneEntity
    {
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
#endif
        [SerializeField]
        [Tooltip("Whether to automatically initialize the pool in Awake().")]
        private bool _initOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(_initOnAwake))]
        [MinValue(0)]
#else
        [Min(0)]
#endif
        [SerializeField]
        [Tooltip("Initial number of entities to pre-instantiate in the pool on Awake.")]
        private int _initialCount;

        [Space]
        [SerializeField]
        [Tooltip("The prefab used to create pooled entity instances.")]
        private E _prefab;

        [SerializeField]
        [Tooltip("Optional container transform under which pooled entities are parented. Defaults to this GameObject.")]
        private Transform _container;

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        internal readonly Stack<E> _pooledEntities = new();

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        internal readonly HashSet<E> _rentEntities = new();

        /// <summary>
        /// Initializes the pool when the GameObject is activated, if <see cref="_initOnAwake"/> is <c>true</c>.
        /// </summary>
        private void Awake()
        {
            if (_container == null)
                _container = this.transform;

            if (_initOnAwake)
                this.Init(_initialCount);
        }

        /// <summary>
        /// Initializes the pool by pre-instantiating the specified number of entities.
        /// </summary>
        /// <param name="count">The number of entities to create and store in the pool.</param>
        public void Init(int count)
        {
            for (int i = 0; i < count; i++)
                _pooledEntities.Push(this.CreateEntity());
        }

        /// <summary>
        /// Rents (activates) an entity from the pool. If the pool is empty, a new instance is created.
        /// </summary>
        /// <returns>The rented entity.</returns>
        public E Rent()
        {
            if (!_pooledEntities.TryPop(out E entity))
                entity = this.CreateEntity();

            _rentEntities.Add(entity);
            this.OnRent(entity);
            return entity;
        }

        /// <summary>
        /// Returns (deactivates) an entity to the pool.
        /// </summary>
        /// <param name="entity">The entity to return. Must have been previously rented.</param>
        public void Return(E entity)
        {
            if (_rentEntities.Remove(entity))
            {
                this.OnReturn(entity);
                _pooledEntities.Push(entity);
            }
            else
            {
                Debug.LogWarning($"[EntityPool] Attempted to return untracked entity: {entity}", entity);
            }
        }

        /// <summary>
        /// Disposes all pooled and rent entities by destroying them and clearing the internal pool.
        /// </summary>
        public void Dispose()
        {
            foreach (E entity in _pooledEntities)
            {
                this.OnDispose(entity);
                Destroy(entity);
            }

            foreach (E entity in _rentEntities)
            {
                this.OnDispose(entity);
                Destroy(entity);
            }

            _pooledEntities.Clear();
            _rentEntities.Clear();
        }

        /// <summary>
        /// Called when a new entity instance is created.
        /// </summary>
        /// <param name="entity">The newly created entity.</param>
        protected virtual void OnCreate(E entity) =>
            entity.gameObject.SetActive(false);

        /// <summary>
        /// Called when a pooled entity is being permanently destroyed during disposal.
        /// </summary>
        /// <param name="entity">The entity being destroyed.</param>
        protected virtual void OnDispose(E entity)
        {
        }

        /// <summary>
        /// Called when an entity is rented from the pool.
        /// </summary>
        /// <param name="entity">The entity being rented.</param>
        protected virtual void OnRent(E entity) =>
            entity.gameObject.SetActive(true);

        /// <summary>
        /// Called when an entity is returned to the pool.
        /// </summary>
        /// <param name="entity">The entity being returned.</param>
        protected virtual void OnReturn(E entity)
        {
            entity.gameObject.SetActive(false);
            entity.transform.SetParent(_container);
        }

        /// <summary>
        /// Instantiates a new entity instance from the prefab and initializes it.
        /// </summary>
        /// <returns>The newly created entity.</returns>
        private E CreateEntity()
        {
            E entity = SceneEntity.Create(_prefab, _container);
            this.OnCreate(entity);
            return entity;
        }

        #region Static

        [Serializable]
        public struct CreateArgs
        {
            public string name;
            public E prefab;
            public Transform container;
            public bool initOnAwake;
            public int initialCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Create<T>(CreateArgs args) where T : SceneEntityPool<E>
        {
            var gameObject = new GameObject(args.name);
            gameObject.SetActive(false);
            T pool = gameObject.AddComponent<T>();
            pool._prefab = args.prefab;
            pool._container = args.container;
            pool._initOnAwake = args.initOnAwake;
            pool._initialCount = args.initialCount;
            gameObject.SetActive(true);
            return pool;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(SceneEntityPool<E> pool, float t = 0)
        {
            if (pool)
            {
                pool.Dispose();
                Destroy(pool.gameObject, t);
            }
        }

        #endregion
    }
}
#endif
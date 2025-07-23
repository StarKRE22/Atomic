#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
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
        [SerializeField]
        [Tooltip("The prefab used to create pooled entity instances.")]
        private E _prefab;

        [SerializeField]
        [Tooltip("Optional container transform under which pooled entities are parented. Defaults to this GameObject.")]
        private Transform _container;

        [SerializeField]
        [Tooltip("Whether to automatically initialize the pool in Awake().")]
        private bool _initOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(_initOnAwake))]
#endif
        [SerializeField]
        [Tooltip("Initial number of entities to pre-instantiate in the pool on Awake.")]
        private int _initialCount;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Stack<E> _pooledEntities = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly HashSet<E> _rentEntities = new();
        
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
        /// Disposes all pooled entities by destroying them and clearing the internal pool.
        /// </summary>
        public void Dispose()
        {
            foreach (E entity in _pooledEntities)
            {
                this.OnDispose(entity);
                Destroy(entity);
            }

            _pooledEntities.Clear();
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
    }
}
#endif
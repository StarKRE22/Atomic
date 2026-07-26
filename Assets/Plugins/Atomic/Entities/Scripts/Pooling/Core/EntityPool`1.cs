using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A simple object pool for entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type managed by the pool. Must implement <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This pool creates entities using an <see cref="IEntityFactory{E}"/> and supports reuse through
    /// <see cref="Rent"/> and <see cref="Return(TEntity)"/> methods.
    /// It also provides virtual lifecycle hooks for spawn, rent, return, and despawn operations.
    /// </remarks>
    public class EntityPool<TEntity, TArgs> : IEntityPool<TEntity>
        where TEntity : IEntity
        where TArgs : IArgs
    {
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private protected readonly Stack<TEntity> _pooledEntities = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private protected readonly HashSet<TEntity> _rentEntities = new();

        private readonly IEntityFactory<TEntity, TArgs> _factory;
        private readonly TArgs _args;
        private readonly ExpandMode _expandMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPool{E}"/> class using the specified factory.
        /// </summary>
        /// <param name="factory">The factory used to create new entity instances when needed.</param>
        /// <param name="args">The arguments passed to the factory when creating entities.</param>
        /// <param name="expandMode">Determines how the pool expands when empty. Defaults to <see cref="ExpandMode.ExpandByOne"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="factory"/> or <paramref name="args"/> is null.</exception>
        public EntityPool(IEntityFactory<TEntity, TArgs> factory, TArgs args, ExpandMode expandMode = ExpandMode.ExpandByOne)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _args = args ?? throw new ArgumentNullException(nameof(args));
            _expandMode = expandMode;
        }

        /// <summary>
        /// Pre-populates the pool with a specified number of entities.
        /// </summary>
        /// <param name="initialCount">The number of entities to create and store in the pool.</param>
        public void Init(int initialCount)
        {
            for (int i = 0; i < initialCount; i++)
            {
                TEntity entity = _factory.Create(_args);
                this.OnCreate(entity);
                _pooledEntities.Push(entity);
            }
        }

        /// <summary>
        /// Removes all entities from the pool and invokes the <see cref="OnDispose"/> hook for each.
        /// </summary>
        public void Dispose()
        {
            foreach (TEntity entity in _pooledEntities)
                this.OnDispose(entity);

            foreach (TEntity entity in _rentEntities)
                this.OnDispose(entity);

            _pooledEntities.Clear();
            _rentEntities.Clear();
        }

        /// <summary>
        /// Retrieves an entity from the pool or creates a new one if the pool is empty.
        /// Behavior when empty depends on <see cref="ExpandMode"/>.
        /// </summary>
        /// <returns>An available entity instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <see cref="ExpandMode.NoExpand"/> is set and the pool is empty.</exception>
        public TEntity Rent()
        {
            if (!_pooledEntities.TryPop(out TEntity entity))
                entity = this.Expand();

            _rentEntities.Add(entity);
            this.OnRent(entity);
            return entity;
        }

        private TEntity Expand()
        {
            switch (_expandMode)
            {
                case ExpandMode.NoExpand:
                    throw new InvalidOperationException(
                        $"[EntityPool] Pool is empty and ExpandMode is NoExpand. " +
                        $"Pre-instantiate more entities via Init() or switch to ExpandByOne/ExpandByDoubling.");

                case ExpandMode.ExpandByDoubling:
                    int count = _rentEntities.Count > 0 ? _rentEntities.Count : 1;
                    this.CreateEntities(count);
                    _pooledEntities.TryPop(out TEntity doubled);
                    return doubled;

                case ExpandMode.ExpandByOne:
                default:
                    TEntity entity = _factory.Create(_args);
                    this.OnCreate(entity);
                    return entity;
            }
        }

        private void CreateEntities(int count)
        {
            for (int i = 0; i < count; i++)
            {
                TEntity entity = _factory.Create(_args);
                this.OnCreate(entity);
                _pooledEntities.Push(entity);
            }
        }

        /// <summary>
        /// Returns an entity to the pool, making it available for future reuse.
        /// If the entity is already present, it will not be added again.
        /// </summary>
        /// <param name="entity">The entity to return to the pool.</param>
        public void Return(TEntity entity)
        {
            if (_rentEntities.Remove(entity))
            {
                this.OnReturn(entity);
                _pooledEntities.Push(entity);
            }
            else
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogWarning($"[EntityPool] Attempted to return untracked entity: {entity}");
#endif
            }
        }

        /// <summary>
        /// Called when a new entity is created and added to the pool.
        /// </summary>
        /// <param name="entity">The newly created entity.</param>
        protected virtual void OnCreate(TEntity entity)
        {
        }

        /// <summary>
        /// Called when the pool is being cleared and an entity is removed permanently.
        /// </summary>
        /// <param name="entity">The entity being despawned.</param>
        protected virtual void OnDispose(TEntity entity)
        {
        }

        /// <summary>
        /// Called when an entity is rented from the pool.
        /// </summary>
        /// <param name="entity">The entity being rented.</param>
        protected virtual void OnRent(TEntity entity)
        {
        }

        /// <summary>
        /// Called when an entity is returned to the pool.
        /// </summary>
        /// <param name="entity">The entity being returned.</param>
        protected virtual void OnReturn(TEntity entity)
        {
        }
    }
}
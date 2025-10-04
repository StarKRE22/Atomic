using System;
using System.Collections.Generic;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A simple object pool for entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The entity type managed by the pool. Must implement <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This pool creates entities using an <see cref="IEntityFactory{E}"/> and supports reuse through
    /// <see cref="Rent"/> and <see cref="Return(E)"/> methods.
    /// It also provides virtual lifecycle hooks for spawn, rent, return, and despawn operations.
    /// </remarks>
    public class EntityPool<E> : IEntityPool<E> where E : IEntity
    {
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private protected readonly Stack<E> _pooledEntities = new();
        
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private protected readonly HashSet<E> _rentEntities = new();
      
        private readonly IEntityFactory<E> _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPool{E}"/> class using the specified factory.
        /// </summary>
        /// <param name="factory">The factory used to create new entity instances when needed.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="factory"/> is null.</exception>
        public EntityPool(IEntityFactory<E> factory) =>
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));

        /// <summary>
        /// Pre-populates the pool with a specified number of entities.
        /// </summary>
        /// <param name="initialCount">The number of entities to create and store in the pool.</param>
        public void Init(int initialCount)
        {
            for (int i = 0; i < initialCount; i++)
            {
                E entity = _factory.Create();
                this.OnCreate(entity);
                _pooledEntities.Push(entity);
            }
        }

        /// <summary>
        /// Removes all entities from the pool and invokes the <see cref="OnDispose"/> hook for each.
        /// </summary>
        public void Dispose()
        {
            foreach (E entity in _pooledEntities)
                this.OnDispose(entity);

            foreach (E entity in _rentEntities) 
                this.OnDispose(entity);
            
            _pooledEntities.Clear();
            _rentEntities.Clear();
        }

        /// <summary>
        /// Retrieves an entity from the pool or creates a new one if the pool is empty.
        /// </summary>
        /// <returns>An available entity instance.</returns>
        public E Rent()
        {
            if (!_pooledEntities.TryPop(out E entity))
            {
                entity = _factory.Create();
                this.OnCreate(entity);
            }

            _rentEntities.Add(entity);
            this.OnRent(entity);
            return entity;
        }

        /// <summary>
        /// Returns an entity to the pool, making it available for future reuse.
        /// If the entity is already present, it will not be added again.
        /// </summary>
        /// <param name="entity">The entity to return to the pool.</param>
        public void Return(E entity)
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
        protected virtual void OnCreate(E entity)
        {
        }

        /// <summary>
        /// Called when the pool is being cleared and an entity is removed permanently.
        /// </summary>
        /// <param name="entity">The entity being despawned.</param>
        protected virtual void OnDispose(E entity)
        {
        }

        /// <summary>
        /// Called when an entity is rented from the pool.
        /// </summary>
        /// <param name="entity">The entity being rented.</param>
        protected virtual void OnRent(E entity)
        {
        }

        /// <summary>
        /// Called when an entity is returned to the pool.
        /// </summary>
        /// <param name="entity">The entity being returned.</param>
        protected virtual void OnReturn(E entity)
        {
        }
    }
}
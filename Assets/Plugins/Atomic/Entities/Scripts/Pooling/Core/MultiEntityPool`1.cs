using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A registry that manages multiple pools of entities, each identified by a unique key.
    /// </summary>
    /// <typeparam name="TKey">The key type used to identify each pool.</typeparam>
    /// <typeparam name="TEntity">The entity type managed by the pools. Must implement <see cref="IEntity"/>.</typeparam>
    public class MultiEntityPool<TKey, TEntity, TArgs> : IMultiEntityPool<TKey, TEntity>
        where TEntity : IEntity
        where TArgs : IArgs
    {
        /// <summary>
        /// Internal storage of pooled (available) entities, mapped by their pool key.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<TKey, Stack<TEntity>> _pooledEntities = new();

        /// <summary>
        /// Tracks entities that are currently rented, mapping them back to their original pool key.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<TEntity, TKey> _rentEntities = new();

        /// <summary>
        /// The factory registry used to create entities on demand.
        /// </summary>
        private readonly IMultiEntityFactory<TKey, TEntity, TArgs> _factory;
        private readonly TArgs _args;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiEntityPool{TKey,E}"/> class.
        /// </summary>
        /// <param name="factory">The factory registry used to create entities for each key.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="factory"/> is null.</exception>
        public MultiEntityPool(IMultiEntityFactory<TKey, TEntity, TArgs> factory, TArgs args)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _args = args ?? throw new ArgumentNullException(nameof(args));
        }

        /// <inheritdoc />
        public void Init(TKey key, int count)
        {
            if (!_pooledEntities.TryGetValue(key, out Stack<TEntity> pool))
            {
                pool = new Stack<TEntity>();
                _pooledEntities.Add(key, pool);
            }

            for (int i = 0; i < count; i++)
            {
                TEntity entity = _factory.Create(key, _args);
                this.OnCreate(entity);
                pool.Push(entity);
            }
        }

        /// <inheritdoc />
        public TEntity Rent(TKey key)
        {
            if (!_pooledEntities.TryGetValue(key, out Stack<TEntity> pool))
            {
                pool = new Stack<TEntity>();
                _pooledEntities.Add(key, pool);
            }

            if (!pool.TryPop(out TEntity entity))
            {
                entity = _factory.Create(key, _args);
                this.OnCreate(entity);
            }

            _rentEntities.Add(entity, key);
            this.OnRent(entity);
            return entity;
        }

        /// <inheritdoc />
        public void Return(TEntity entity)
        {
            if (!_rentEntities.Remove(entity, out TKey key))
                return;

            if (!_pooledEntities.TryGetValue(key, out Stack<TEntity> pool))
            {
                pool = new Stack<TEntity>();
                _pooledEntities.Add(key, pool);
            }

            if (pool.Contains(entity))
                return;

            this.OnReturn(entity);
            pool.Push(entity);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (KeyValuePair<TKey, Stack<TEntity>> pool in _pooledEntities)
            foreach (TEntity entity in pool.Value)
                this.OnDispose(entity);

            foreach (TEntity entity in _rentEntities.Keys)
                this.OnDispose(entity);

            _pooledEntities.Clear();
            _rentEntities.Clear();
        }

        /// <summary>
        /// Called when a new entity is created for the pool.
        /// </summary>
        /// <param name="entity">The newly created entity.</param>
        protected virtual void OnCreate(TEntity entity)
        {
        }

        /// <summary>
        /// Called when an entity is permanently removed from the pool (e.g., during <see cref="Clear"/>).
        /// </summary>
        /// <param name="entity">The entity being disposed.</param>
        protected virtual void OnDispose(TEntity entity)
        {
        }

        /// <summary>
        /// Called when an entity is rented from a pool.
        /// </summary>
        /// <param name="entity">The rented entity.</param>
        protected virtual void OnRent(TEntity entity)
        {
        }

        /// <summary>
        /// Called when an entity is returned to its pool.
        /// </summary>
        /// <param name="entity">The returned entity.</param>
        protected virtual void OnReturn(TEntity entity)
        {
        }
    }
}
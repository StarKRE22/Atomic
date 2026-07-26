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
        private readonly ExpandMode _expandMode;
        private readonly Dictionary<TKey, int> _rentedCounts = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiEntityPool{TKey,E}"/> class.
        /// </summary>
        /// <param name="factory">The factory registry used to create entities for each key.</param>
        /// <param name="args">The arguments passed to the factory when creating entities.</param>
        /// <param name="expandMode">Determines how the pool expands when empty. Defaults to <see cref="ExpandMode.ExpandByOne"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="factory"/> is null.</exception>
        public MultiEntityPool(IMultiEntityFactory<TKey, TEntity, TArgs> factory, TArgs args, ExpandMode expandMode = ExpandMode.ExpandByOne)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _args = args ?? throw new ArgumentNullException(nameof(args));
            _expandMode = expandMode;
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
                entity = this.Expand(key, pool);

            _rentEntities.Add(entity, key);

            if (!_rentedCounts.TryAdd(key, 1))
                _rentedCounts[key]++;

            this.OnRent(entity);
            return entity;
        }

        private TEntity Expand(TKey key, Stack<TEntity> pool)
        {
            switch (_expandMode)
            {
                case ExpandMode.NoExpand:
                    throw new InvalidOperationException(
                        $"[MultiEntityPool] Pool for key '{key}' is empty and ExpandMode is NoExpand. " +
                        $"Pre-instantiate more entities via Init() or switch to ExpandByOne/ExpandByDoubling.");

                case ExpandMode.ExpandByDoubling:
                    int count = _rentedCounts.TryGetValue(key, out int rented) && rented > 0 ? rented : 1;
                    this.CreateEntities(key, pool, count);
                    pool.TryPop(out TEntity doubled);
                    return doubled;

                default:
                    TEntity entity = _factory.Create(key, _args);
                    this.OnCreate(entity);
                    return entity;
            }
        }

        private void CreateEntities(TKey key, Stack<TEntity> pool, int count)
        {
            for (int i = 0; i < count; i++)
            {
                TEntity entity = _factory.Create(key, _args);
                this.OnCreate(entity);
                pool.Push(entity);
            }
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

            if (_rentedCounts.TryGetValue(key, out int count) && count > 0)
                _rentedCounts[key] = count - 1;

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
            _rentedCounts.Clear();
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
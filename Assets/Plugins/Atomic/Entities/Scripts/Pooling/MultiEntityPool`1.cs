using System;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A registry that manages multiple pools of entities, each identified by a unique key.
    /// </summary>
    /// <typeparam name="K">The key type used to identify each pool.</typeparam>
    /// <typeparam name="E">The entity type managed by the pools. Must implement <see cref="IEntity"/>.</typeparam>
    public class MultiEntityPool<K, E> : IMultiEntityPool<K, E> where E : IEntity
    {
        /// <summary>
        /// Internal storage of pooled (available) entities, mapped by their pool key.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<K, Stack<E>> _pooledEntities = new();

        /// <summary>
        /// Tracks entities that are currently rented, mapping them back to their original pool key.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<E, K> _rentEntities = new();

        /// <summary>
        /// The factory registry used to create entities on demand.
        /// </summary>
        private readonly IMultiEntityFactory<K, E> _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiEntityPool{TKey,E}"/> class.
        /// </summary>
        /// <param name="factory">The factory registry used to create entities for each key.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="factory"/> is null.</exception>
        public MultiEntityPool(IMultiEntityFactory<K, E> factory) =>
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));

        /// <inheritdoc />
        public void Init(K key, int count)
        {
            if (!_pooledEntities.TryGetValue(key, out Stack<E> pool))
            {
                pool = new Stack<E>();
                _pooledEntities.Add(key, pool);
            }

            for (int i = 0; i < count; i++)
            {
                E entity = _factory.Create(key);
                this.OnCreate(entity);
                pool.Push(entity);
            }
        }

        /// <inheritdoc />
        public E Rent(K key)
        {
            if (!_pooledEntities.TryGetValue(key, out Stack<E> pool))
            {
                pool = new Stack<E>();
                _pooledEntities.Add(key, pool);
            }

            if (!pool.TryPop(out E entity))
            {
                entity = _factory.Create(key);
                this.OnCreate(entity);
            }

            _rentEntities.Add(entity, key);
            this.OnRent(entity);
            return entity;
        }

        /// <inheritdoc />
        public void Return(E entity)
        {
            if (!_rentEntities.Remove(entity, out K key))
                return;

            if (!_pooledEntities.TryGetValue(key, out Stack<E> pool))
            {
                pool = new Stack<E>();
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
            foreach (KeyValuePair<K, Stack<E>> pool in _pooledEntities)
            foreach (E entity in pool.Value)
                this.OnDispose(entity);

            foreach (E entity in _rentEntities.Keys) 
                this.OnDispose(entity);
            
            _pooledEntities.Clear();
            _rentEntities.Clear();
        }

        /// <summary>
        /// Called when a new entity is created for the pool.
        /// </summary>
        /// <param name="entity">The newly created entity.</param>
        protected virtual void OnCreate(E entity)
        {
        }

        /// <summary>
        /// Called when an entity is permanently removed from the pool (e.g., during <see cref="Clear"/>).
        /// </summary>
        /// <param name="entity">The entity being disposed.</param>
        protected virtual void OnDispose(E entity)
        {
        }

        /// <summary>
        /// Called when an entity is rented from a pool.
        /// </summary>
        /// <param name="entity">The rented entity.</param>
        protected virtual void OnRent(E entity)
        {
        }

        /// <summary>
        /// Called when an entity is returned to its pool.
        /// </summary>
        /// <param name="entity">The returned entity.</param>
        protected virtual void OnReturn(E entity)
        {
        }
    }
}
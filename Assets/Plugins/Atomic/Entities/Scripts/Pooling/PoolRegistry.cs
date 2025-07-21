using System;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public class PoolRegistry<TKey, E> : IPoolRegistry<TKey, E> where E : IEntity<E>
    {
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<TKey, Stack<E>> _pools = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<E, TKey> _rentEntities = new();

        private readonly IEntityFactoryRegistry<TKey, E> _factory;

        protected PoolRegistry(IEntityFactoryRegistry<TKey, E> factory) =>
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));

        public void Initialize(TKey key, int count)
        {
            if (!_pools.TryGetValue(key, out Stack<E> pool))
            {
                pool = new Stack<E>();
                _pools.Add(key, pool);
            }

            for (int i = 0; i < count; i++)
            {
                E entity = _factory.Create(key);
                this.OnCreate(entity);
                pool.Push(entity);
            }
        }

        public E Rent(TKey key)
        {
            if (!_pools.TryGetValue(key, out Stack<E> pool))
            {
                pool = new Stack<E>();
                _pools.Add(key, pool);
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

        public void Return(E entity)
        {
            if (!_rentEntities.Remove(entity, out TKey key))
                return;

            if (!_pools.TryGetValue(key, out Stack<E> pool))
            {
                pool = new Stack<E>();
                _pools.Add(key, pool);
            }

            if (pool.Contains(entity))
                return;

            this.OnReturn(entity);
            pool.Push(entity);
        }

        public void Clear(TKey key)
        {
            if (_pools.TryGetValue(key, out Stack<E> pool))
            {
                foreach (E entity in pool)
                    this.OnDestroy(entity);

                pool.Clear();
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<TKey, Stack<E>> pool in _pools)
            foreach (E entity in pool.Value)
                this.OnDestroy(entity);
            
            _pools.Clear();
        }

        protected virtual void OnCreate(E entity)
        {
        }

        protected virtual void OnDestroy(E entity)
        {
        }

        protected virtual void OnRent(E entity)
        {
        }

        protected virtual void OnReturn(E entity)
        {
        }
    }
}
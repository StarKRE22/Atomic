using System;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public class GenericEntityPool : GenericEntityPool<string>, IGenericEntityPool
    {
        public GenericEntityPool(IGenericEntityFactory<string> factory) : base(factory)
        {
        }

        protected override string GetKey(IEntity entity) => entity.Name;
    }

    public abstract class GenericEntityPool<TKey> : IGenericEntityPool<TKey>
    {
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<TKey, Queue<IEntity>> _pools = new();
        private readonly IGenericEntityFactory<TKey> _factory;

        protected GenericEntityPool(IGenericEntityFactory<TKey> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public void Initialize(TKey key, in int count)
        {
            if (!_pools.TryGetValue(key, out Queue<IEntity> pool))
            {
                pool = new Queue<IEntity>();
                _pools.Add(key, pool);
            }
            
            for (int i = 0; i < count; i++)
            {
                IEntity entity = _factory.Create(key);
                this.OnCreate(entity);
                pool.Enqueue(entity);
            }
        }

        public IEntity Rent(TKey key)
        {
            if (!_pools.TryGetValue(key, out Queue<IEntity> pool))
            {
                pool = new Queue<IEntity>();
                _pools.Add(key, pool);
            }

            if (!pool.TryDequeue(out IEntity result))
            {
                result = _factory.Create(key);
                this.OnCreate(result);
            }

            this.OnRent(result);
            return result;
        }

        public void Return(IEntity entity)
        {
            TKey key = this.GetKey(entity);

            if (!_pools.TryGetValue(key, out Queue<IEntity> pool))
            {
                pool = new Queue<IEntity>();
                _pools.Add(key, pool);
            }

            if (pool.Contains(entity))
                return;

            this.OnReturn(entity);
            pool.Enqueue(entity);
        }

        protected abstract TKey GetKey(IEntity entity);

        protected virtual void OnCreate(IEntity entity)
        {
        }

        protected virtual void OnDestroy(IEntity entity)
        {
        }

        protected virtual void OnRent(IEntity entity)
        {
        }

        protected virtual void OnReturn(IEntity entity)
        {
        }
    }
}
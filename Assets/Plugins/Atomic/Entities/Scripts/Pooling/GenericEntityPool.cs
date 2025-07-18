using System;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public interface IGenericEntityPool : IGenericEntityPool<string>
    {
    }

    public interface IGenericEntityPool<in TKey>
    {
        void Initialize(TKey key, in int count);
        IEntity Rent(TKey key);
        void Return(IEntity entity);
    }
    
    public class GenericEntityPool : GenericEntityPool<string>, IGenericEntityPool
    {
        public GenericEntityPool(IEntityFactoryRegistry<string> factoryRegistry) : base(factoryRegistry)
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
        private readonly IEntityFactoryRegistry<TKey> _factoryRegistry;

        protected GenericEntityPool(IEntityFactoryRegistry<TKey> factoryRegistry)
        {
            _factoryRegistry = factoryRegistry ?? throw new ArgumentNullException(nameof(factoryRegistry));
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
                IEntity entity = _factoryRegistry.Create(key);
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
                result = _factoryRegistry.Create(key);
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
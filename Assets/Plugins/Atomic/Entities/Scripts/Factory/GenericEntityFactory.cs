using System.Collections.Generic;
using SampleGame;

namespace Atomic.Entities
{
    public interface IGenericEntityFactory : IGenericEntityFactory<string>
    {
    }

    public interface IGenericEntityFactory<TKey>
    {
        void Add(in TKey key, in IEntityFactory factory);
        void Remove(in TKey key);
        IEntity Create(in TKey key);
    }
    
    public class GenericEntityFactory : GenericEntityFactory<string>, IGenericEntityFactory
    {
        public GenericEntityFactory()
        {
        }

        public GenericEntityFactory(ScriptableEntityFactoryCatalog factoryCatalog) :base(factoryCatalog.GetEntities())
        {
        }

        public GenericEntityFactory(IEnumerable<KeyValuePair<string, IEntityFactory>> factorys) : base(factorys)
        {
        }

        public GenericEntityFactory(params KeyValuePair<string, IEntityFactory>[] factory) : base(factory)
        {
        }
    }

    public class GenericEntityFactory<TKey> : IGenericEntityFactory<TKey>
    {
        private readonly Dictionary<TKey, IEntityFactory> _factories;

        public GenericEntityFactory()
        {
            _factories = new Dictionary<TKey, IEntityFactory>();
        }

        public GenericEntityFactory(in IEnumerable<KeyValuePair<TKey, IEntityFactory>> factorys)
        {
            _factories = new Dictionary<TKey, IEntityFactory>(factorys);
        }

        public GenericEntityFactory(params KeyValuePair<TKey, IEntityFactory>[] factory)
        {
            _factories = new Dictionary<TKey, IEntityFactory>(factory);
        }

        public void Add(in TKey key, in IEntityFactory factory)
        {
            _factories.Add(key, factory);
        }

        public void Remove(in TKey key)
        {
            _factories.Remove(key);
        }

        public IEntity Create(in TKey key)
        {
            return !_factories.TryGetValue(key, out IEntityFactory factory)
                ? throw new KeyNotFoundException($"Can't create entity with key: {key}")
                : factory.Create();
        }
    }
}
using System.Collections.Generic;
using SampleGame;

namespace Atomic.Entities
{
    public class GenericEntityFactory : GenericEntityFactory<string>, IGenericEntityFactory
    {
        public GenericEntityFactory()
        {
        }

        public GenericEntityFactory(EntityPrototypeCatalog prototypeCatalog) :base(prototypeCatalog.GetEntities())
        {
        }

        public GenericEntityFactory(IEnumerable<KeyValuePair<string, IEntityFactory>> prototypes) : base(prototypes)
        {
        }

        public GenericEntityFactory(params KeyValuePair<string, IEntityFactory>[] prototype) : base(prototype)
        {
        }
    }

    public class GenericEntityFactory<TKey> : IGenericEntityFactory<TKey>
    {
        private readonly Dictionary<TKey, IEntityFactory> _prototypes;

        public GenericEntityFactory()
        {
            _prototypes = new Dictionary<TKey, IEntityFactory>();
        }

        public GenericEntityFactory(in IEnumerable<KeyValuePair<TKey, IEntityFactory>> prototypes)
        {
            _prototypes = new Dictionary<TKey, IEntityFactory>(prototypes);
        }

        public GenericEntityFactory(params KeyValuePair<TKey, IEntityFactory>[] prototype)
        {
            _prototypes = new Dictionary<TKey, IEntityFactory>(prototype);
        }

        public GenericEntityFactory<TKey> Register(in TKey key, in IEntityFactory prototype)
        {
            _prototypes.Add(key, prototype);
            return this;
        }

        public GenericEntityFactory<TKey> Unregister(in TKey key)
        {
            _prototypes.Remove(key);
            return this;
        }

        public IEntity Create(in TKey key)
        {
            return !_prototypes.TryGetValue(key, out IEntityFactory prototype)
                ? throw new KeyNotFoundException($"Can't create entity with key: {key}")
                : prototype.Create();
        }
    }
}
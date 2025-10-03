using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A concrete implementation of <see cref="MultiEntityFactory{TKey, E}"/> 
    /// specialized for <see cref="string"/> keys and <see cref="IEntity"/> entities.
    /// Implements <see cref="IMultiEntityFactory"/>.
    /// </summary>
    public class MultiEntityFactory : MultiEntityFactory<string, IEntity>, IMultiEntityFactory
    {
        /// <summary>
        /// Initializes a new empty <see cref="MultiEntityFactory"/> instance.
        /// </summary>
        public MultiEntityFactory()
        {
        }

        /// <summary>
        /// Initializes the factory using a catalog of scriptable entity factories.
        /// </summary>
        /// <param name="factories">A read-only dictionary providing the mapping of keys to entity factories.</param>
        public MultiEntityFactory(IReadOnlyDictionary<string, IEntityFactory<IEntity>> factories) :
            base(factories)
        {
        }

        /// <summary>
        /// Initializes the factory using the specified collection of key-factory pairs.
        /// </summary>
        /// <param name="factories">The collection of key-factory pairs to initialize the factory with.</param>
        public MultiEntityFactory(IEnumerable<KeyValuePair<string, IEntityFactory<IEntity>>> factories) :
            base(factories)
        {
        }

        /// <summary>
        /// Initializes the factory using a params array of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize the factory with.</param>
        public MultiEntityFactory(params KeyValuePair<string, IEntityFactory<IEntity>>[] factories) :
            base(factories)
        {
        }
    }
}
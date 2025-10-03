using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A specialized registry for managing <see cref="IEntity"/> factories, 
    /// using string-based keys for lookup and registration.
    /// </summary>
    /// <remarks>
    /// Inherits from the generic <see cref="MultiEntityFactory{TKey,E}"/> 
    /// with <c>TKey</c> as <see cref="string"/> and <c>TValue</c> as <see cref="IEntity"/>.
    /// </remarks>
    public class MultiEntityFactory : MultiEntityFactory<string, IEntity>, IMultiEntityFactory
    {
        /// <summary>
        /// Initializes a new empty factory.
        /// </summary>
        public MultiEntityFactory()
        {
        }

        /// <summary>
        /// Initializes the factory using a catalog of scriptable entity factories.
        /// </summary>
        /// <param name="factories">The map providing entity factories.</param>
        public MultiEntityFactory(IReadOnlyDictionary<string, IEntityFactory<IEntity>> factories) :
            base(factories)
        {
        }

        /// <summary>
        /// Initializes the factory using the specified collection of entity factories.
        /// </summary>
        /// <param name="factories">Key-factory pairs to initialize the factory with.</param>
        public MultiEntityFactory(IEnumerable<KeyValuePair<string, IEntityFactory<IEntity>>> factories) :
            base(factories)
        {
        }

        /// <summary>
        /// Initializes the factory using a params array of key-factory pairs.
        /// </summary>
        /// <param name="factories">The key-factory pairs to initialize with.</param>
        public MultiEntityFactory(params KeyValuePair<string, IEntityFactory<IEntity>>[] factories) :
            base(factories)
        {
        }
    }
}
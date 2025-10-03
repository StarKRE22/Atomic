using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="EntityCollection{E}"/> that operates specifically on <see cref="IEntity"/> instances.
    /// Provides the same functionality as the generic base class but simplifies usage when generic typing is unnecessary.
    /// </summary>
    public class EntityCollection : EntityCollection<IEntity>, IEntityCollection
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="EntityCollection"/> class.
        /// </summary>
        public EntityCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> class with the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of entities the collection can initially store without resizing.</param>
        public EntityCollection(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> class with the provided entities.
        /// </summary>
        /// <param name="entities">A parameter array of entities to populate the collection with.</param>
        public EntityCollection(params IEntity[] entities) : base(entities)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> class using a read-only collection.
        /// </summary>
        /// <param name="elements">A read-only collection of entities to populate the collection with.</param>
        public EntityCollection(IReadOnlyCollection<IEntity> elements) : base(elements)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> class using an enumerable.
        /// </summary>
        /// <param name="elements">An enumerable of entities to populate the collection with.</param>
        public EntityCollection(IEnumerable<IEntity> elements) : base(elements)
        {
        }
    }
}
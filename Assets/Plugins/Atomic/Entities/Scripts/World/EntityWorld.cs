using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic shortcut for <see cref="EntityWorld{IEntity}"/>.
    /// Manages a world of general-purpose <see cref="IEntity"/> instances with support for enabling,
    /// updating, and disposing all contained entities.
    /// </summary>
    /// <remarks>
    /// This class provides a convenient entry point for working with untyped or heterogeneous entities
    /// without requiring generic parameters.
    /// </remarks>
    public class EntityWorld : EntityWorld<IEntity>, IEntityWorld
    {
        /// <summary>
        /// Initializes an empty <see cref="EntityWorld"/> instance with no name.
        /// </summary>
        public EntityWorld()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld"/> with the specified entities and an empty name.
        /// </summary>
        /// <param name="entities">Entities to prepopulate the world with.</param>
        public EntityWorld(params IEntity[] entities) : base(entities)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld"/> with the specified entities and an empty name.
        /// </summary>
        /// <param name="entities">Entities to prepopulate the world with.</param>
        public EntityWorld(string name = null, params IEntity[] entities) : base(name, entities)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld"/> with a name and a collection of entities.
        /// </summary>
        /// <param name="name">The name of the world.</param>
        /// <param name="entities">The enumerable collection of entities to add.</param>
        public EntityWorld(string name, IEnumerable<IEntity> entities) : base(name, entities)
        {
        }
    }
}
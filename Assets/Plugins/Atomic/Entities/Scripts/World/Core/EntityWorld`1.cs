using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a runtime-managed world composed of entities of type <typeparamref name="E"/>.
    /// Provides lifecycle management including, enabling, updating, and disposing all entities in the collection.
    /// </summary>
    /// <typeparam name="E">The specific type of entity managed by this world. Must implement <see cref="IEntity"/>.</typeparam>
    public partial class EntityWorld<E> : EntityCollection<E>, IEntityWorld<E> where E : IEntity
    {
        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        private string _name;

        /// <summary>
        /// Initializes an empty <see cref="EntityWorld{E}"/> instance with no name.
        /// </summary>
        public EntityWorld() => _name = string.Empty;

        /// <summary>
        /// Initializes a new <see cref="EntityWorld{E}"/> with the specified entities and an empty name.
        /// </summary>
        /// <param name="entities">Entities to prepopulate the world with.</param>
        public EntityWorld(params E[] entities)
        {
            _name = string.Empty;
            this.AddRange(entities);
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld{E}"/> with a name and optional entities.
        /// </summary>
        /// <param name="name">The name of the world.</param>
        /// <param name="entities">Entities to prepopulate the world with.</param>
        public EntityWorld(string name = null, params E[] entities)
        {
            _name = name;
            this.AddRange(entities);
        }

        /// <summary>
        /// Initializes a new <see cref="EntityWorld{E}"/> with a name and a collection of entities.
        /// </summary>
        /// <param name="name">The name of the world.</param>
        /// <param name="entities">The enumerable collection of entities to add.</param>
        public EntityWorld(string name, IEnumerable<E> entities)
        {
            _name = name;
            this.AddRange(entities);
        }
        
        /// <summary>
        /// Enables added entity if world is enabled.
        /// </summary>
        /// <param name="entity">Added entity</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnAdd(E entity)
        {
            if (_enabled) entity.Enable();
        }

        /// <summary>
        /// Disables removed entity if world is enabled. 
        /// </summary>
        /// <param name="entity">Removed entity</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnRemove(E entity)
        {
            if (_enabled) entity.Disable();
        }
    }
}
namespace Atomic.Entities
{
    /// <summary>
    /// Represents a world that manages a collection of entities and controls their lifecycle events.
    /// </summary>
    /// <typeparam name="E">The type of entity managed by this world. Must implement <see cref="IEntity"/>.</typeparam>
    public partial interface IEntityWorld<E> : IEntityCollection<E>, IEnableLifecycle, ITickLifecycle where E : IEntity
    {
        /// <summary>
        /// Gets the name of the entity world.
        /// This can be used for debugging, identification, or UI representation purposes.
        /// </summary>
        string Name { get; set; }
    }
}
namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic version of <see cref="IEntityWorld{E}"/> specialized for <see cref="IEntity"/>.
    /// Provides a convenient base abstraction when a specific entity type is not required.
    /// </summary>
    public interface IEntityWorld : IEntityWorld<IEntity>
    {
    }

    /// <summary>
    /// Represents a world that manages a collection of entities and controls their lifecycle events.
    /// </summary>
    /// <typeparam name="E">The type of entity managed by this world. Must implement <see cref="IEntity"/>.</typeparam>
    public interface IEntityWorld<E> : IEntityCollection<E>, ISpawnable, IUpdatable, IActivatable where E : IEntity
    {
        /// <summary>
        /// Gets the name of the entity world.
        /// This can be used for debugging, identification, or UI representation purposes.
        /// </summary>
        string Name { get; set; }
    }
}
namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic factory interface for creating <see cref="IEntity"/> instances.
    /// </summary>
    public interface IEntityFactory : IEntityFactory<IEntity>
    {
    }

    /// <summary>
    /// Generic factory interface for creating instances of <see cref="IEntity"/> or its derived types.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IEntity"/> that the factory creates.</typeparam>
    public interface IEntityFactory<out T> where T : IEntity
    {
        /// <summary>
        /// Creates and returns a new instance of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        T Create();
    }
}
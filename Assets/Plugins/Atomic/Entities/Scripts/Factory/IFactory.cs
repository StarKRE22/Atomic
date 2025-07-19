namespace Atomic.Entities
{
    /// <summary>
    /// Generic factory interface for creating instances of <see cref="IEntity"/> or its derived types.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IEntity"/> that the factory creates.</typeparam>
    public interface IFactory<out E> where E : IEntity<E>
    {
        /// <summary>
        /// Creates and returns a new instance of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        E Create();
    }
}
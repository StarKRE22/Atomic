namespace Atomic.Entities
{
    /// <summary>
    /// Represents a factory capable of creating <see cref="IEntity"/> instances.
    /// </summary>
    public interface IEntityFactory
    {
        /// <summary>
        /// Creates and returns a new instance of an <see cref="IEntity"/>.
        /// </summary>
        /// <returns>A new <see cref="IEntity"/> instance.</returns>
        IEntity Create();
    }
}
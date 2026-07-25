namespace Atomic.Entities
{
    /// <summary>
    /// Defines a generic factory interface for creating new instances of <see cref="IEntity"/>-based types.
    /// 
    /// This interface is typically implemented by systems or data-driven structures (e.g., ScriptableObjects, MonoBehaviours)
    /// that are responsible for instantiating and configuring entities at runtime.
    /// </summary>
    /// <typeparam name="TEntity">The type of <see cref="IEntity"/> this factory creates.</typeparam>
    public interface IEntityFactory<out TEntity, in TArgs>
        where TEntity : IEntity
        where TArgs : IArgs
    {
        /// <summary>
        /// Creates and returns a new instance of the entity type <typeparamref name="TEntity"/>.
        /// 
        /// Implementations may optionally preconfigure the instance with default tags, values, or behaviors
        /// before returning it.
        /// </summary>
        /// <returns>A new instance of the entity of type <typeparamref name="TEntity"/>.</returns>
        TEntity Create(TArgs args);
    }
}
namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic version of <see cref="IEntityFactory{T}"/> that produces a base <see cref="IEntity"/> instance.
    /// This interface is useful when working with heterogeneous entity types in a shared context, such as registries or catalogs.
    /// </summary>
    public interface IEntityFactory : IEntityFactory<IEntity>
    {
    }
    
    /// <summary>
    /// Defines a generic factory interface for creating new instances of <see cref="IEntity"/>-based types.
    /// 
    /// This interface is typically implemented by systems or data-driven structures (e.g., ScriptableObjects, MonoBehaviours)
    /// that are responsible for instantiating and configuring entities at runtime.
    /// </summary>
    /// <typeparam name="E">The type of <see cref="IEntity"/> this factory creates.</typeparam>
    public interface IEntityFactory<out E> where E : IEntity
    {
        /// <summary>
        /// Creates and returns a new instance of the entity type <typeparamref name="E"/>.
        /// 
        /// Implementations may optionally preconfigure the instance with default tags, values, or behaviors
        /// before returning it.
        /// </summary>
        /// <returns>A new instance of the entity of type <typeparamref name="E"/>.</returns>
        E Create();
    }
}
using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that supports an enable lifecycle event for an <see cref="IEntity"/>.
    /// This is typically called when the entity is activated or enters the active state.
    /// </summary>
    public interface IEnable : IBehaviour
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The entity being enabled.</param>
        void Enable(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IEnable"/> that provides strongly-typed enable logic for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IEnable<in T> : IEnable where T : IEntity
    {
        void IEnable.Enable(IEntity entity) => this.Enable((T) entity);

        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being enabled.</param>
        void Enable(T entity);
    }
    
    /// <summary>
    /// Unsafe generic version of <see cref="IEnable"/> that provides strongly-typed enable logic
    /// for a specific <see cref="IEntity"/> type using low-level casting.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IUnsafeEnable<in T> : IEnable where T : IEntity
    {
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being enabled.</param>
        void Enable(T entity);

        void IEnable.Enable(IEntity entity) => this.Enable(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}
using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that executes logic when an <see cref="IEntity"/> is disabled.
    /// </summary>
    public interface IDisable<E> : IBehaviour<E> where E : IEntity<E>
    {
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        /// <param name="entity">The entity being disabled.</param>
        void Disable(E entity);
    }

    // /// <summary>
    // /// Generic version of <see cref="IDisable"/> that provides strongly-typed disable logic
    // /// for a specific <see cref="IEntity"/> type.
    // /// </summary>
    // /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    // public interface IDisable<in T> : IDisable where T : IEntity
    // {
    //     /// <summary>
    //     /// Called when the entity is disabled.
    //     /// </summary>
    //     /// <param name="entity">The strongly-typed entity being disabled.</param>
    //     void Disable(T entity);
    //
    //     void IDisable.Disable(IEntity entity) => this.Disable((T)entity);
    // }
    //
    // /// <summary>
    // /// Unsafe generic version of <see cref="IDisable"/> that uses low-level casting for performance.
    // /// </summary>
    // /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    // public interface IUnsafeDisable<in T> : IDisable where T : IEntity
    // {
    //     /// <summary>
    //     /// Called when the entity is disabled.
    //     /// </summary>
    //     /// <param name="entity">The strongly-typed entity being disabled.</param>
    //     void Disable(T entity);
    //
    //     void IDisable.Disable(IEntity entity) => this.Disable(UnsafeUtility.As<IEntity, T>(ref entity));
    // }
}
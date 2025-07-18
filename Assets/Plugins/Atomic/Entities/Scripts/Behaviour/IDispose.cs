using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that is called when an <see cref="IEntity"/> is disposed.
    /// </summary>
    public interface IDispose
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The entity being disposed.</param>
        void Dispose(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IDispose"/> that provides strongly-typed dispose logic
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific entity type this logic applies to.</typeparam>
    public interface IDispose<in T> : IDispose where T : IEntity
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disposed.</param>
        void Dispose(T entity);

        void IDispose.Dispose(IEntity entity) => this.Dispose((T)entity);
    }

    /// <summary>
    /// Unsafe generic version of <see cref="IDispose"/> that performs low-level casting for performance.
    /// </summary>
    /// <typeparam name="T">The specific entity type this logic applies to.</typeparam>
    public interface IUnsafeDispose<in T> : IDispose where T : IEntity
    {
        /// <summary>
        /// Called when the entity is being disposed.
        /// </summary>
        /// <param name="entity">The strongly-typed entity being disposed.</param>
        void Dispose(T entity);

        void IDispose.Dispose(IEntity entity) => this.Dispose(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}
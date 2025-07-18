using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that supports initialization logic for an <see cref="IEntity"/>.
    /// </summary>
    public interface IInit : IBehaviour
    {
        /// <summary>
        /// Initializes the behavior with the specified <see cref="IEntity"/>.
        /// </summary>
        /// <param name="entity">The entity to initialize this behavior with.</param>
        void Init(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IInit"/> that provides strongly-typed initialization for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior works with.</typeparam>
    public interface IInit<in T> : IInit where T : IEntity
    {
        /// <summary>
        /// Initializes the behavior with a strongly-typed entity context.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/>.</param>
        void Init(T entity);
        
        void IInit.Init(IEntity entity) => this.Init((T) entity);
    }
    
    /// <summary>
    /// Unsafe generic version of <see cref="IInit"/> that uses low-level casting to initialize a strongly-typed <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior works with.</typeparam>
    public interface IUnsafeInit<in T> : IInit where T : IEntity
    {
        /// <summary>
        /// Initializes the behavior with a strongly-typed entity context.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/>.</param>
        void Init(T entity);

        void IInit.Init(IEntity entity) => this.Init(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}
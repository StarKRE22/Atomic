using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a generic mechanism for configuring or injecting data into an <see cref="IEntity"/> instance.
    /// </summary>
    public interface IEntityInstaller
    {
        /// <summary>
        /// Installs data, configuration, or behaviors into the specified <see cref="IEntity"/>.
        /// </summary>
        /// <param name="entity">The entity to configure or initialize.</param>
        void Install(IEntity entity);
    }

    /// <summary>
    /// Represents a type-safe installer for entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this installer supports.</typeparam>
    /// <remarks>
    /// This interface provides a strongly-typed <see cref="Install(T)"/> method while also implementing the
    /// non-generic <see cref="IEntityInstaller"/> interface. The explicit implementation ensures safe casting.
    /// </remarks>
    public interface IEntityInstaller<in T> : IEntityInstaller where T : IEntity
    {
        /// <summary>
        /// Installs data, configuration, or behaviors into the specified entity of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entity">The strongly-typed entity to configure or initialize.</param>
        void Install(T entity);

        /// <inheritdoc />
        void IEntityInstaller.Install(IEntity entity) => this.Install((T) entity);
    }
    
    /// <summary>
    /// Represents an unsafe, allocation-free variant of <see cref="IEntityInstaller{T}"/>.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this installer supports.</typeparam>
    /// <remarks>
    /// This interface avoids runtime casting and boxing by using <see cref="UnsafeUtility.As{TFrom, TTo}(ref TFrom)"/>.  
    /// Use it only when you are certain that the incoming <see cref="IEntity"/> is of type <typeparamref name="T"/>.
    /// </remarks>
    public interface IEntityInstallerUnsafe<in T> : IEntityInstaller where T : IEntity
    {
        /// <summary>
        /// Installs data, configuration, or behaviors into the specified entity of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entity">The strongly-typed entity to configure or initialize.</param>
        void Install(T entity);

        /// <inheritdoc />
        void IEntityInstaller.Install(IEntity entity) => this.Install(UnsafeUtility.As<IEntity, T>(ref entity));
    }
}
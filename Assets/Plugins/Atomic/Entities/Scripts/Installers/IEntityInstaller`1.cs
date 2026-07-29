using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a type-safe installer for entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The specific type of entity this installer supports.</typeparam>
    /// <remarks>
    /// This interface provides a strongly-typed <see cref="Install(E)"/> method while also implementing the
    /// non-generic <see cref="IEntityInstaller"/> interface. The explicit implementation ensures safe casting.
    /// </remarks>
    public interface IEntityInstaller<in E> : IEntityInstaller where E : IEntity
    {
        /// <summary>
        /// Installs data, configuration, or behaviors into the specified entity of type <typeparamref name="E"/>.
        /// </summary>
        /// <param name="entity">The strongly-typed entity to configure or initialize.</param>
        void Install(E entity);

        /// <inheritdoc />
        void IEntityInstaller.Install(IEntity entity)
        {
            if (entity is not E e)
                throw new InvalidCastException(
                    $"[IEntityInstaller<{typeof(E).Name}>] Invalid entity type for {this.GetType().Name}.\n" +
                    $"Expected: {typeof(E).FullName}\n" +
                    $"Received: {entity?.GetType().FullName ?? "null"}\n" +
                    "Please make sure the correct IEntityInstaller is used for this entity type."
                );
            
            this.Install(e);
        }
    }
}
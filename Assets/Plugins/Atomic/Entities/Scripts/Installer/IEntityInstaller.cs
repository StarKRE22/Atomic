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
        void IEntityInstaller.Install(IEntity entity) => this.Install((E) entity);
    }
}
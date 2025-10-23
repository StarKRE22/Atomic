#if UNITY_5_3_OR_NEWER
namespace Atomic.Entities
{
    /// <summary>
    /// A strongly-typed version of <see cref="SceneEntityInstaller"/> for entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The specific type of <see cref="IEntity"/> this installer operates on.</typeparam>
    /// <remarks>
    /// This variant enforces type safety and eliminates the need for manual casting in derived classes.
    /// </remarks>
    public abstract class SceneEntityInstaller<E> : SceneEntityInstaller, IEntityInstaller<E> where E : class, IEntity
    {
        /// <inheritdoc cref="SceneEntityInstaller.Install" />
        public sealed override void Install(IEntity entity) => this.Install((E) entity);

        /// <inheritdoc cref="SceneEntityInstaller.Uninstall" />
        public sealed override void Uninstall(IEntity entity) => this.Uninstall((E) entity);

        /// <summary>
        /// Installs data or behavior into a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity to install.</param>
        public abstract void Install(E entity);

        /// <summary>
        /// Removes previously installed data or behavior from the specified entity.
        /// </summary>
        /// <param name="entity">
        /// The entity to uninstall configuration, components, or behavior from.
        /// </param>
        /// <remarks>
        /// The default implementation does nothing. Override this method to provide custom uninstall logic.
        /// </remarks>
        public virtual void Uninstall(E entity)
        {
        }
    }
}
#endif
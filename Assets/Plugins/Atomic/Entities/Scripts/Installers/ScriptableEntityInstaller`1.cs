#if UNITY_5_3_OR_NEWER
namespace Atomic.Entities
{
    /// <summary>
    /// A strongly-typed version of <see cref="ScriptableEntityInstaller"/> for installing entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The specific entity type this installer supports.</typeparam>
    /// <remarks>
    /// This class enforces type safety and avoids manual casting in derived implementations.
    /// </remarks>
    public abstract class ScriptableEntityInstaller<E> : ScriptableEntityInstaller, IEntityInstaller<E>
        where E : class, IEntity
    {
        /// <inheritdoc cref="ScriptableEntityInstaller.Install" />
        public sealed override void Install(IEntity entity) => this.Install((E) entity);

        /// <summary>
        /// Applies configuration to a strongly-typed entity instance.
        /// </summary>
        /// <param name="entity">The entity to install.</param>
        public abstract void Install(E entity);

        /// <inheritdoc cref="ScriptableEntityInstaller.Uninstall" />
        public sealed override void Uninstall(IEntity entity) => this.Uninstall((E) entity);

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
#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic base class for implementing <see cref="IEntityViewInstaller"/> using <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// Extend this class to define logic that installs data, behaviors, or UI configuration
    /// into a generic <see cref="EntityView"/> instance.
    /// </remarks>
    public abstract class EntityViewInstaller : EntityViewInstaller<IEntity>
    {
        /// <inheritdoc/>
        public sealed override void Install(EntityView<IEntity> view) => this.Install((EntityView) view);

        /// <inheritdoc cref="Install" />
        protected abstract void Install(EntityView view);
    }

    /// <summary>
    /// Abstract base class for implementing view-specific installation logic for a particular entity type.
    /// </summary>
    /// <typeparam name="E">The type of <see cref="IEntity"/> the view works with.</typeparam>
    /// <remarks>
    /// Use this class to define and inject dependencies, configure visuals, or bind logic
    /// into an <see cref="EntityView{E}"/> component at runtime or during initialization.
    /// </remarks>
    public abstract class EntityViewInstaller<E> : MonoBehaviour, IEntityViewInstaller<E> where E : IEntity
    {
        /// <summary>
        /// Installs configuration, bindings, or logic into the specified <see cref="EntityView{E}"/>.
        /// </summary>
        /// <param name="view">The entity view to install into.</param>
        public abstract void Install(EntityView<E> view);
    }
}
#endif
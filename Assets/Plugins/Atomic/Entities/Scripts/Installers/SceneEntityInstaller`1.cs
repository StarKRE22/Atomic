#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A strongly-typed version of <see cref="SceneEntityInstaller"/> for entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The specific type of <see cref="IEntity"/> this installer operates on.</typeparam>
    /// <remarks>
    /// This variant enforces type safety and eliminates the need for manual casting in derived classes.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Installers/SceneEntityInstaller%601.md")]
    public abstract class SceneEntityInstaller<E> : SceneEntityInstaller, IEntityInstaller<E> where E : class, IEntity
    {
        /// <inheritdoc cref="SceneEntityInstaller.Install" />
        public sealed override void Install(IEntity entity)
        {
            if (entity is not E e)
                throw new InvalidCastException(
                    $"[SceneEntityInstaller<{typeof(E).Name}>] Invalid entity type.\n" +
                    $"Expected: {typeof(E).FullName}\n" +
                    $"Received: {entity?.GetType().FullName ?? "null"}\n" +
                    "Please make sure the correct installer is attached for this entity type."
                );

            this.Install(e);
        }

        /// <inheritdoc cref="SceneEntityInstaller.Uninstall" />
        public sealed override void Uninstall(IEntity entity)
        {
            if (entity is not E e)
                throw new InvalidCastException(
                    $"[SceneEntityInstaller<{typeof(E).Name}>] Invalid entity type.\n" +
                    $"Expected: {typeof(E).FullName}\n" +
                    $"Received: {entity?.GetType().FullName ?? "null"}\n" +
                    "Please connect the correct SceneEntityInstaller for this entity type."
                );

            this.Uninstall(e);
        }

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
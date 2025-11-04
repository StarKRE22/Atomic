#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity <see cref="ScriptableObject"/> that defines reusable logic for installing or configuring an <see cref="IEntity"/>.
    /// </summary>
    /// <remarks>
    /// This is useful for defining shared configuration logic that can be applied to multiple entities,
    /// such as setting default values, tags, or attaching behaviors.
    /// Supports both runtime and edit-time contexts via utility methods.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Installers/ScriptableEntityInstaller.md")]
    public abstract class ScriptableEntityInstaller : ScriptableObject, IEntityInstaller
    {
        /// <summary>
        /// Applies configuration or data to the given entity.
        /// </summary>
        /// <param name="entity">The entity to configure or initialize.</param>
        public abstract void Install(IEntity entity);

        /// <summary>
        /// Removes previously installed data or behavior from the specified entity.
        /// </summary>
        /// <param name="entity">
        /// The entity to uninstall configuration, components, or behavior from.
        /// </param>
        /// <remarks>
        /// The default implementation does nothing. Override this method to provide custom uninstall logic.
        /// </remarks>
        public virtual void Uninstall(IEntity entity)
        {
        }
    }
}
#endif
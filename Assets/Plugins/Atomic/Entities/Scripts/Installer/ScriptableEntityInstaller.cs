using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
    public abstract class ScriptableEntityInstaller : ScriptableObject, IEntityInstaller
    {
        /// <summary>
        /// Applies configuration or data to the given entity.
        /// </summary>
        /// <param name="entity">The entity to configure or initialize.</param>
        public abstract void Install(IEntity entity);

        /// <summary>
        /// Returns <c>true</c> if the application is currently in Play Mode.
        /// </summary>
        protected static bool IsPlayMode()
        {
#if UNITY_EDITOR
            return EditorApplication.isPlaying;
#else
            return true;
#endif
        }

        /// <summary>
        /// Returns <c>true</c> if the application is in Edit Mode and not compiling.
        /// </summary>
        protected static bool IsEditMode()
        {
#if UNITY_EDITOR
            return !EditorApplication.isPlaying && !EditorApplication.isCompiling;
#else
            return false;
#endif
        }
    }

    /// <summary>
    /// A strongly-typed version of <see cref="ScriptableEntityInstaller"/> for installing entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The specific entity type this installer supports.</typeparam>
    /// <remarks>
    /// This class enforces type safety and avoids manual casting in derived implementations.
    /// </remarks>
    public abstract class ScriptableEntityInstaller<T> : ScriptableEntityInstaller where T : class, IEntity
    {
        /// <inheritdoc />
        public sealed override void Install(IEntity entity) => this.Install((T) entity);

        /// <summary>
        /// Applies configuration to a strongly-typed entity instance.
        /// </summary>
        /// <param name="entity">The entity to install.</param>
        protected abstract void Install(T entity);
    }
}
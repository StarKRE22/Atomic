#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity <see cref="MonoBehaviour"/> that can be attached to a GameObject
    /// to perform installation logic on an <see cref="IEntity"/> during runtime or initialization.
    /// </summary>
    /// <remarks>
    /// Used to declaratively configure entities placed in a scene.  
    /// In the Editor, it supports automatic refresh via <c>OnValidate</c>.
    /// </remarks>
    public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
    {
#if UNITY_EDITOR
        /// <summary>
        /// Editor-only callback used to signal the need to refresh editor state when the component is modified.
        /// </summary>
        internal Action refreshCallback;
#endif

        /// <summary>
        /// Installs data or behavior into the specified entity.
        /// </summary>
        /// <param name="entity">The entity to install configuration or components into.</param>
        public abstract void Install(IEntity entity);

        /// <summary>
        /// Called by Unity when the component is modified in the Inspector.
        /// Triggers the <see cref="refreshCallback"/> in Editor to update related systems or previews.
        /// </summary>
        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            try
            {
                if (!EditorApplication.isPlaying && !EditorApplication.isCompiling)
                    refreshCallback?.Invoke();
            }
            catch (Exception)
            {
                // ignored
            }
#endif
        }
    }

    /// <summary>
    /// A strongly-typed version of <see cref="SceneEntityInstaller"/> for entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The specific type of <see cref="IEntity"/> this installer operates on.</typeparam>
    /// <remarks>
    /// This variant enforces type safety and eliminates the need for manual casting in derived classes.
    /// </remarks>
    public abstract class SceneEntityInstaller<T> : SceneEntityInstaller where T : class, IEntity
    {
        /// <inheritdoc/>
        public sealed override void Install(IEntity entity) => this.Install((T) entity);

        /// <summary>
        /// Installs data or behavior into a strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity to install.</param>
        protected abstract void Install(T entity);
    }
}
#endif
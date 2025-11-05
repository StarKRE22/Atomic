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
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Installers/SceneEntityInstaller.md")]
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
}
#endif
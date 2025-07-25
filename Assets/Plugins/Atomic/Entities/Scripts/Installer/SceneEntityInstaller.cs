using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

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

        /// <summary>
        /// Returns <c>true</c> if the application is in Play Mode.
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

    /// <summary>
    /// An unsafe, strongly-typed scene-level installer for entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The concrete type of entity to install. Must be a class implementing <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This abstract base class provides an unsafe override of <see cref="SceneEntityInstaller.Install(IEntity)"/> 
    /// using <see cref="UnsafeUtility.As{TFrom, TTo}(ref TFrom)"/> to avoid runtime casting costs.
    /// <para>
    /// Use this class only when you are certain that the passed <see cref="IEntity"/> is of type <typeparamref name="T"/>, 
    /// as incorrect usage may lead to undefined behavior.
    /// </para>
    /// </remarks>
    public abstract class SceneEntityInstallerUnsafe<T> : SceneEntityInstaller where T : class, IEntity
    {
        /// <summary>
        /// Installs the specified entity by casting it unsafely to <typeparamref name="T"/> and delegating to the type-safe method.
        /// </summary>
        /// <param name="entity">The entity to install. Must be of type <typeparamref name="T"/>.</param>
        public sealed override void Install(IEntity entity) => this.Install(UnsafeUtility.As<IEntity, T>(ref entity));

        /// <summary>
        /// Performs installation logic for the strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/> to configure.</param>
        protected abstract void Install(T entity);
    }
}
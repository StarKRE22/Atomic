using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
    {
#if UNITY_EDITOR
        public Action refreshCallback;
#endif
        public abstract void Install(IEntity entity);

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

        protected static bool IsPlayMode()
        {
#if UNITY_EDITOR
            return EditorApplication.isPlaying;
#else
            return true;
#endif
        }

        protected static bool IsEditMode()
        {
#if UNITY_EDITOR
            return !EditorApplication.isPlaying && !EditorApplication.isCompiling;
#else
            return false;
#endif
        }
    }

    public abstract class SceneEntityInstaller<T> : SceneEntityInstaller where T : class, IEntity
    {
        public sealed override void Install(IEntity entity) => this.Install((T) entity);
        protected abstract void Install(T entity);
    }
}
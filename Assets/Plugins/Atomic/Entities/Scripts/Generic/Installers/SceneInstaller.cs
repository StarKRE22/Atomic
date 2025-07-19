using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class SceneInstaller<E> : MonoBehaviour, IInstaller<E> where E : IEntity<E>
    {
#if UNITY_EDITOR
        public Action refreshCallback;
#endif
        public abstract void Install(E entity);

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
}
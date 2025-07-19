using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    public abstract class ScriptableInstaller<E> : ScriptableObject, IInstaller<E> where E : IEntity<E>
    {
        public abstract void Install(E entity);

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
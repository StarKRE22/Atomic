using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    public abstract class ScriptableEntityInstaller : ScriptableObject, IEntityInstaller
    {
        public abstract void Install(IEntity entity);
        
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
    
    public abstract class ScriptableEntityInstaller<T> : ScriptableEntityInstaller where T : class, IEntity
    {
        public sealed override void Install(IEntity entity) => this.Install((T) entity);
        protected abstract void Install(T entity);
    }
}
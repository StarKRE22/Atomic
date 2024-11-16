using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Atomic.Entities
{
    public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
    {
        [SerializeField]
        private bool autoRefresh = true;

#if UNITY_EDITOR
        internal Action m_refreshCallback;
#endif
        public abstract void Install(IEntity entity);

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            try
            {
                if (this.autoRefresh && !EditorApplication.isPlaying && !EditorApplication.isCompiling)
                {
                    m_refreshCallback?.Invoke();
                }
            }
            catch (Exception)
            {
                // ignored
            }
#endif
        }
    }

    public abstract class SceneEntityInstaller<T> : SceneEntityInstaller where T : class, IEntity
    {
        public sealed override void Install(IEntity entity)
        {
            T tEntity = entity as T;
            
            Assert.IsNotNull(tEntity, $"Mismatch Entity type! Expected: {typeof(T).Name}");
            this.Install(tEntity);
        }

        protected abstract void Install(T entity);
    }
}
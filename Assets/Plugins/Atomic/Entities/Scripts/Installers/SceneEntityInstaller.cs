using System;
using UnityEditor;
using UnityEngine;

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
}
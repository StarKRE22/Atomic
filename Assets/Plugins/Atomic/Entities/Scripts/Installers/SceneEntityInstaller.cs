#if ODIN_INSPECTOR
using System;
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity Installer")]
    public class SceneEntityInstaller : SceneEntityInstallerBase
    {
        [SerializeField]
        private bool autoRefresh = true;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Header("Installers")]
        [SerializeReference]
        protected IEntityInstaller[] installers = default;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Header("Behaviours")]
        [SerializeReference]
        protected IEntityBehaviour[] logics = default;

        public override void Install(IEntity entity)
        {
            if (this.installers is {Length: > 0})
            {
                for (int i = 0, count = this.installers.Length; i < count; i++)
                {
                    IEntityInstaller installer = this.installers[i];
                    if (installer != null)
                    {
                        installer.Install(entity);
                    }
                }
            }

            if (this.logics is {Length: > 0})
            {
                for (int i = 0, count = this.logics.Length; i < count; i++)
                {
                    IEntityBehaviour behaviour = this.logics[i];
                    if (behaviour != null)
                    {
                        entity.AddBehaviour(behaviour);
                    }
                }
            }
        }


        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            try
            {
                if (this.autoRefresh && !EditorApplication.isPlaying && !EditorApplication.isCompiling)
                {
                    mRefreshCallback?.Invoke();
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
#endif
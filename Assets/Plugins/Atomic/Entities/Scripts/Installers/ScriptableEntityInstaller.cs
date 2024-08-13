#if ODIN_INSPECTOR
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [CreateAssetMenu(
        fileName = "ScriptableEntityInstaller",
        menuName = "Atomic/Entitites/New ScriptableEntityInstaller"
    )]
    public class ScriptableEntityInstaller : ScriptableEntityInstallerBase
    {
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
    }
}
#endif
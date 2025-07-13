#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Entities
{
    [CreateAssetMenu(
        fileName = "Entity Installer",
        menuName = "Atomic/Entities/Scriptable Entity Installer"
    )]
    public class ScriptableEntityInstallerDefault : ScriptableEntityInstaller
    {
        [HideInPlayMode]
        [Header("Installers")]
        [SerializeReference]
        protected IEntityInstaller[] installers = default;

        [HideInPlayMode]
        [Header("Behaviours")]
        [SerializeReference]
        protected IBehaviour[] logics = default;

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
                    IBehaviour behaviour = this.logics[i];
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
#if ODIN_INSPECTOR
using UnityEngine;

namespace Atomic.Contexts
{
    [CreateAssetMenu(
        fileName = "ContextInstaller",
        menuName = "Atomic/Context/Context Installer"
    )]
    public sealed class ScriptableContextInstaller : ScriptableContextInstallerBase
    {
        [Space(12)]
        [SerializeReference]
        private IContextInstaller[] installers;

        [Space(12)]
        [SerializeReference]
        private IContextSystem[] systems;
        
        public override void Install(IContext context)
        {
            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Length; i < count; i++)
                {
                    IContextInstaller installer = this.installers[i];
                    installer?.Install(context);
                }
            }

            if (this.systems != null)
            {
                for (int i = 0, count = this.systems.Length; i < count; i++)
                {
                    IContextSystem system = this.systems[i];
                    if (system != null)
                    {
                        context.AddSystem(system);
                    }
                }
            }
        }
    }
}
#endif
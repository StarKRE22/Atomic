using System;
using UnityEngine;
// ReSharper disable NotAccessedField.Local

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Contexts
{
    [Serializable]
    public sealed class GroupInstaller : IContextInstaller
    {
#if ODIN_INSPECTOR
        [GUIColor(1f, 0.92156863f, 0.015686275f)]
#endif
        [SerializeField]
        private string name;
        
        [Space(12)]
        [SerializeReference] 
        private IContextInstaller[] installers = default;

        [Space(12)]
        [SerializeReference]
        private IContextSystem[] systems = default;
        
        public void Install(IContext context)
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
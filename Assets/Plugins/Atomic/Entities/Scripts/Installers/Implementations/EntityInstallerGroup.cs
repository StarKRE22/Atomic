#if ODIN_INSPECTOR
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class EntityInstallerGroup : IEntityInstaller
    {
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f), HideLabel]
#endif
        public string name;

        [SerializeReference]
        protected IEntityInstaller[] installers = default;

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 8)]
#endif
        [SerializeReference]
        protected IEntityBehaviour[] behaviours = default;

        public virtual void Install(IEntity entity)
        {
            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Length; i < count; i++)
                {
                    IEntityInstaller installer = this.installers[i];
                    installer?.Install(entity);
                }
            }

            if (this.behaviours != null)
            {
                for (int i = 0, count = this.behaviours.Length; i < count; i++)
                {
                    IEntityBehaviour behaviour = this.behaviours[i];
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
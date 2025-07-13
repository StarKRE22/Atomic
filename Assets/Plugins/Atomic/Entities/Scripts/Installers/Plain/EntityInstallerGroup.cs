#if ODIN_INSPECTOR
using System;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Atomic.Entities
{
    [Serializable]
    [InlineProperty]
    public class EntityInstallerGroup : IEntityInstaller
    {
        [GUIColor(0f, 0.83f, 1f), HideLabel]
        public string name;

        [SerializeReference]
        protected IEntityInstaller[] installers = default;

        [PropertySpace(SpaceBefore = 0, SpaceAfter = 8)]
        [SerializeReference]
        protected IBehaviour[] behaviours = default;

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
                    IBehaviour behaviour = this.behaviours[i];
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
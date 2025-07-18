#if ODIN_INSPECTOR
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Atomic.Entities
{
    [InlineProperty]
    [Serializable]
    public class EntityInstallerBehaviour<T> : IEntityInstaller where T : IBehaviour
    {
        [SerializeField]
        protected T value;

        public EntityInstallerBehaviour()
        {
        }

        public EntityInstallerBehaviour(T value)
        {
            this.value = value;
        }

        public virtual void Install(IEntity entity)
        {
            entity.AddBehaviour(this.value);
        }
    }
}
#endif
#if ODIN_INSPECTOR
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Atomic.Entities
{
    [InlineProperty]
    [Serializable]
    public class BehaviourEntityInstaller<T> : IEntityInstaller where T : IBehaviour
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public BehaviourEntityInstaller()
        {
        }

        public BehaviourEntityInstaller(T value)
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
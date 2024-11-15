#if ODIN_INSPECTOR
using System;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [Serializable]
    public sealed class ValueEntityInstaller : IEntityInstaller
    {
        [EntityValue]
        [SerializeField]
        private int key;
        
        [SerializeReference]
        private object value = default;
        
        public void Install(IEntity entity)
        {
            if (this.value != null)
            {
                entity.AddValue(this.key, this.value);
            }
        }
    }

    [Serializable]

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class ValueEntityInstaller<T> : IEntityInstaller
    {
        [EntityValue]
#if ODIN_INSPECTOR
        [HorizontalGroup]
#endif
        [SerializeField]
        private int id = -1;

#if ODIN_INSPECTOR
        [HideLabel]
        [HorizontalGroup]
#endif
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public ValueEntityInstaller()
        {
        }

        public ValueEntityInstaller(T value)
        {
            this.value = value;
        }

        public virtual void Install(IEntity entity)
        {
            entity.AddValue(this.id, this.value);
        }
    }
}
#endif
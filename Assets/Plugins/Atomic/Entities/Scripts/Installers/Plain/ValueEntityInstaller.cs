#if ODIN_INSPECTOR
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Atomic.Entities
{
    [Serializable]
    public sealed class ValueEntityInstaller : IEntityInstaller
    {
        [EntityValue]
        [SerializeField]
        private string key;

        [SerializeReference]
        private object value = default;

        public void Install(IEntity entity)
        {
            if (this.value != null) 
                entity.AddValue(this.key, this.value);
        }
    }

    [Serializable]

    [InlineProperty]
    public class ValueEntityInstaller<T> : IEntityInstaller
    {
        [EntityValue]
        [HorizontalGroup]
        [SerializeField]
        private string key;

        [HideLabel]
        [HorizontalGroup]
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
            entity.AddValue(this.key, this.value);
        }
    }
}
#endif
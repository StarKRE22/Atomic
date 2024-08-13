#if UNITY_MATHEMATICS
using System;
using Atomic.Elements;
using Unity.Mathematics;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
    [Serializable]
    public class quaternionReactive : IReactiveVariable<quaternion>
    {
        public event Action<quaternion> OnValueChanged;
        
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private quaternion value;

        public quaternion Value
        {
            get { return this.value; }
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        public quaternionReactive()
        {
            this.value = default;
        }

        public quaternionReactive(quaternion value)
        {
            this.value = value;
        }
        
        public static implicit operator quaternionReactive(quaternion value)
        {
            return new quaternionReactive(value);
        }

        public Action<quaternion> Subscribe(Action<quaternion> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }

        public void Unsubscribe(Action<quaternion> listener)
        {
            this.OnValueChanged -= listener;
        }

        private void InvokeEvent(quaternion value)
        {
            this.OnValueChanged?.Invoke(value);
        }
    }
}
#endif
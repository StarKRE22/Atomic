using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveQuaternion : IReactiveVariable<Quaternion>, IDisposable
    {
        public event System.Action<Quaternion> OnValueChanged;
        
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Quaternion value;

        public Quaternion Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        public ReactiveQuaternion()
        {
            this.value = default;
        }

        public ReactiveQuaternion(Quaternion value)
        {
            this.value = value;
        }
        
        public static implicit operator ReactiveQuaternion(Quaternion value)
        {
            return new ReactiveQuaternion(value);
        }

        public System.Action<Quaternion> Subscribe(System.Action<Quaternion> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }

        public void Unsubscribe(System.Action<Quaternion> listener)
        {
            this.OnValueChanged -= listener;
        }

        private void InvokeEvent(Quaternion value)
        {
            this.OnValueChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnValueChanged);
        }
    }
}
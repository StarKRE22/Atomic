using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class ReactiveFloat : IReactiveVariable<float>, IDisposable
    {
        public event System.Action<float> OnValueChanged;
        
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private float value;

        public float Value
        {
            get { return this.value; }
            set
            {
                if (Math.Abs(this.value - value) > float.Epsilon)
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        public ReactiveFloat()
        {
            this.value = default;
        }

        public ReactiveFloat(float value)
        {
            this.value = value;
        }

        public static implicit operator ReactiveFloat(float value)
        {
            return new ReactiveFloat(value);
        }

        public System.Action<float> Subscribe(System.Action<float> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }

        public void Unsubscribe(System.Action<float> listener)
        {
            this.OnValueChanged -= listener;
        }

        private void InvokeEvent(float value)
        {
            this.OnValueChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnValueChanged);
        }
    }
}
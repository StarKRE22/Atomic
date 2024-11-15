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
    public class ReactiveInt : IReactiveVariable<int>, IDisposable
    {
        public event System.Action<int> OnValueChanged;
        
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private int value;

        public int Value
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

        public ReactiveInt()
        {
            this.value = default;
        }

        public ReactiveInt(int value)
        {
            this.value = value;
        }
        
        public static implicit operator ReactiveInt(int value)
        {
            return new ReactiveInt(value);
        }

        public System.Action<int> Subscribe(System.Action<int> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }

        public void Unsubscribe(System.Action<int> listener)
        {
            this.OnValueChanged -= listener;
        }
        
        private void InvokeEvent(int value)
        {
            this.OnValueChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnValueChanged);
        }
    }
}

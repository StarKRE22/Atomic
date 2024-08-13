using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveVector2 : IReactiveVariable<Vector2>, IDisposable
    {
        public event System.Action<Vector2> OnValueChanged;
        
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif      
        [SerializeField]
        private Vector2 value;

        public Vector2 Value
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

        public ReactiveVector2()
        {
            this.value = default;
        }

        public ReactiveVector2(Vector2 value)
        {
            this.value = value;
        }
        
        public static implicit operator ReactiveVector2(Vector2 value)
        {
            return new ReactiveVector2(value);
        }

        public System.Action<Vector2> Subscribe(System.Action<Vector2> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }

        public void Unsubscribe(System.Action<Vector2> listener)
        {
            this.OnValueChanged -= listener;
        }
        
        private void InvokeEvent(Vector2 value)
        {
            this.OnValueChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnValueChanged);
        }
    }
}
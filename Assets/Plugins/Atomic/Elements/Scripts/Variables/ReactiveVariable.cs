using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents a serialized read-write reactive property.
    [Serializable]
    public class ReactiveVariable<T> : IReactiveVariable<T>, IDisposable
    {
        private static readonly IEqualityComparer<T> equalityComparer = EqualityComparer.GetDefault<T>();

        public event System.Action<T> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private T value;
        
        public T Value
        {
            get { return this.value; }
            set
            {
                if (!equalityComparer.Equals(this.value, value))
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        public T Invoke()
        {
            return this.value;
        }

        public ReactiveVariable()
        {
            this.value = default;
        }

        public ReactiveVariable(T value)
        {
            this.value = value;
        }

        public static implicit operator ReactiveVariable<T>(T value)
        {
            return new ReactiveVariable<T>(value);
        }
        
        public System.Action<T> Subscribe(System.Action<T> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }

        public void Unsubscribe(System.Action<T> listener)
        {
            this.OnValueChanged -= listener;
        }

        private void InvokeEvent(T value)
        {
            this.OnValueChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnValueChanged);
        }
    }
}

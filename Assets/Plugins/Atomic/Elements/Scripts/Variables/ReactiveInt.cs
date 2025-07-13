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
        public event Action<int> OnValueChanged;
        
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

        public ReactiveInt() => this.value = default;

        public ReactiveInt(int value) => this.value = value;

        public static implicit operator ReactiveInt(int value) => new(value);

        public void Subscribe(Action<int> listener) => this.OnValueChanged += listener;

        public void Unsubscribe(Action<int> listener) => this.OnValueChanged -= listener;

        private void InvokeEvent(int value) => this.OnValueChanged?.Invoke(value);

        public void Dispose() => AtomicHelper.Dispose(ref this.OnValueChanged);

        public override string ToString() => this.Value.ToString();
    }
}

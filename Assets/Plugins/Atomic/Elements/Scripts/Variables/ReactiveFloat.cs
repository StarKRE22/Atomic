using System;
using System.Globalization;
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
        public event Action<float> OnValueChanged;

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

        public ReactiveFloat() => this.value = default;

        public ReactiveFloat(float value) => this.value = value;

        public static implicit operator ReactiveFloat(float value) => new(value);

        public void Subscribe(Action<float> listener) => this.OnValueChanged += listener;

        public void Unsubscribe(Action<float> listener) => this.OnValueChanged -= listener;

        private void InvokeEvent(float value) => this.OnValueChanged?.Invoke(value);

        public void Dispose() => AtomicHelper.Dispose(ref this.OnValueChanged);

        public override string ToString() => this.value.ToString(CultureInfo.InvariantCulture);
    }
}
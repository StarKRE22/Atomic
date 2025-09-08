#if UNITY_MATHEMATICS

using System;
using Atomic.Elements;
using Unity.Mathematics;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper for a <see cref="float4"/> value.
    /// Invokes <see cref="OnValueChanged"/> whenever the value changes.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class reactive_float4 : IReactiveVariable<float4>
    {
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<float4> OnValueChanged;

        /// <summary>
        /// The wrapped <see cref="float4"/> value.
        /// Triggers <see cref="OnValueChanged"/> when changed to a different value.
        /// </summary>
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private float4 value;

        /// <summary>
        /// Gets or sets the current value.
        /// Setting a new value will trigger <see cref="OnValueChanged"/> if the value differs.
        /// </summary>
        public float4 Value
        {
            get => this.value;
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="reactive_float4"/> with the default value.
        /// </summary>
        public reactive_float4() => this.value = default;

        /// <summary>
        /// Creates a new <see cref="reactive_float4"/> with the specified initial value.
        /// </summary>
        /// <param name="value">Initial float4 value.</param>
        public reactive_float4(float4 value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="float4"/> to a <see cref="reactive_float4"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator reactive_float4(float4 value) => new(value);

        /// <summary>
        /// Subscribes to value changes.
        /// </summary>
        /// <param name="action">Callback to invoke on value change.</param>
        /// <returns>A subscription object for unsubscribing.</returns>
        public Subscription<float4> Subscribe(Action<float4> action)
        {
            this.OnValueChanged += action;
            return new Subscription<float4>(this, action);
        }

        /// <summary>
        /// Unsubscribes a listener from the value change event.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<float4> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Invokes the value changed event manually.
        /// Used internally by the Odin Inspector callback.
        /// </summary>
        /// <param name="value">The value to broadcast.</param>
        private void InvokeEvent(float4 value) => this.OnValueChanged?.Invoke(value);
    }
}
#endif

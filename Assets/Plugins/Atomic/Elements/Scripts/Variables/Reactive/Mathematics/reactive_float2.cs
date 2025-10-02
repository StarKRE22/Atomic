#if UNITY_MATHEMATICS

using System;
using Unity.Mathematics;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper for a <see cref="float2"/> value.
    /// Invokes <see cref="OnEvent"/> whenever the value changes.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class reactive_float2 : IReactiveVariable<float2>
    {
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<float2> OnEvent;

        /// <summary>
        /// The wrapped <see cref="float2"/> value.
        /// Triggers <see cref="OnEvent"/> when changed to a different value.
        /// </summary>
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private float2 value;

        /// <summary>
        /// Gets or sets the current value.
        /// Setting a new value will trigger <see cref="OnEvent"/> if the value differs.
        /// </summary>
        public float2 Value
        {
            get => this.value;
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    this.OnEvent?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="reactive_float2"/> with the default value.
        /// </summary>
        public reactive_float2() => this.value = default;

        /// <summary>
        /// Creates a new <see cref="reactive_float2"/> with the specified initial value.
        /// </summary>
        /// <param name="value">Initial float2 value.</param>
        public reactive_float2(float2 value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="float2"/> to a <see cref="reactive_float2"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator reactive_float2(float2 value) => new(value);
        
        /// <summary>
        /// Invokes the value changed event manually.
        /// Used internally by the Odin Inspector callback.
        /// </summary>
        /// <param name="value">The value to broadcast.</param>
        private void InvokeEvent(float2 value) => this.OnEvent?.Invoke(value);
    }
}
#endif

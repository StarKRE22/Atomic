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
    /// A reactive wrapper for a <see cref="int3"/> value.
    /// Invokes <see cref="OnValueChanged"/> whenever the value changes.
    /// Implements <see cref="IReactiveVariable{int3}"/>.
    /// </summary>
    [Serializable]
    public sealed class reactive_int3 : IReactiveVariable<int3>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<int3> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private int3 value;

        /// <summary>
        /// Gets or sets the current <see cref="int3"/> value.
        /// Triggers <see cref="OnValueChanged"/> when changed.
        /// </summary>
        public int3 Value
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
        /// Creates a new <see cref="reactive_int3"/> with the default value (0,0,0).
        /// </summary>
        public reactive_int3() => this.value = int3.zero;

        /// <summary>
        /// Creates a new <see cref="reactive_int3"/> with a specified initial value.
        /// </summary>
        /// <param name="value">Initial int3 value.</param>
        public reactive_int3(int3 value) => this.value = value;

        /// <summary>
        /// Implicitly converts an <see cref="int3"/> to a <see cref="reactive_int3"/>.
        /// </summary>
        public static implicit operator reactive_int3(int3 value) => new(value);

        /// <summary>
        /// Subscribes to value changes.
        /// </summary>
        /// <param name="action">Callback to invoke on value change.</param>
        /// <returns>A subscription object for unsubscribing.</returns>
        public Subscription<int3> Subscribe(Action<int3> action)
        {
            this.OnValueChanged += action;
            return new Subscription<int3>(this, action);
        }

        /// <summary>
        /// Unsubscribes a listener from the value change event.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<int3> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Invokes the value changed event manually (used by Odin Inspector).
        /// </summary>
        /// <param name="value">The value to broadcast.</param>
        private void InvokeEvent(int3 value) => this.OnValueChanged?.Invoke(value);

        /// <summary>
        /// Disposes the reactive variable by clearing all listeners.
        /// </summary>
        public void Dispose() => this.OnValueChanged = null;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif

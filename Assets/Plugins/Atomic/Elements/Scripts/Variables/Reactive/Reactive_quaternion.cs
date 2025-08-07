#if UNITY_MATHEMATICS
using System;
using Atomic.Elements;
using Unity.Mathematics;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
    /// <summary>
    /// A reactive wrapper for a <see cref="quaternion"/> value.
    /// Notifies subscribers whenever the value changes.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class Reactive_quaternion : IReactiveVariable<quaternion>
    {
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<quaternion> OnValueChanged;

        /// <summary>
        /// The internal quaternion value.
        /// Triggers <see cref="OnValueChanged"/> when set to a new value.
        /// </summary>
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private quaternion value;

        /// <summary>
        /// Gets or sets the current quaternion value.
        /// Setting a new value invokes <see cref="OnValueChanged"/> if it differs from the previous value.
        /// </summary>
        public quaternion Value
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
        /// Creates a new <see cref="Reactive_quaternion"/> with a default value.
        /// </summary>
        public Reactive_quaternion() => this.value = default;

        /// <summary>
        /// Creates a new <see cref="Reactive_quaternion"/> with the specified value.
        /// </summary>
        /// <param name="value">The initial quaternion value.</param>
        public Reactive_quaternion(quaternion value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="quaternion"/> to a <see cref="Reactive_quaternion"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator Reactive_quaternion(quaternion value) => new(value);

        /// <summary>
        /// Subscribes to value change events.
        /// </summary>
        /// <param name="listener">Callback to invoke when the value changes.</param>
        /// <returns>A subscription object for managing the listener.</returns>
        public Subscription<quaternion> Subscribe(Action<quaternion> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<quaternion>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a listener from the value change event.
        /// </summary>
        /// <param name="listener">The callback to remove.</param>
        public void Unsubscribe(Action<quaternion> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Manually invokes the <see cref="OnValueChanged"/> event with the given value.
        /// Used internally by the inspector.
        /// </summary>
        /// <param name="value">The value to invoke with.</param>
        private void InvokeEvent(quaternion value) => this.OnValueChanged?.Invoke(value);
    }
}
#endif

#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive quaternion variable that raises events when its value changes.
    /// Implements <see cref="IReactiveVariable{Quaternion}"/> and <see cref="IDisposable"/>.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveQuaternion : IReactiveVariable<Quaternion>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<Quaternion> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Quaternion value;

        /// <summary>
        /// Gets or sets the quaternion value. Invokes <see cref="OnValueChanged"/> when the value changes.
        /// </summary>
        public Quaternion Value
        {
            get => this.value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ReactiveQuaternion"/> with default value (identity).
        /// </summary>
        public ReactiveQuaternion() => this.value = default;

        /// <summary>
        /// Initializes a new instance of <see cref="ReactiveQuaternion"/> with a specific quaternion.
        /// </summary>
        /// <param name="value">Initial value of the quaternion.</param>
        public ReactiveQuaternion(Quaternion value) => this.value = value;

        /// <summary>
        /// Initializes a new instance of <see cref="ReactiveQuaternion"/> using quaternion components.
        /// </summary>
        /// <param name="x">X component.</param>
        /// <param name="y">Y component.</param>
        /// <param name="z">Z component.</param>
        /// <param name="w">W component.</param>
        public ReactiveQuaternion(float x, float y, float z, float w) => this.value = new Quaternion(x, y, z, w);

        /// <summary>
        /// Implicitly converts a quaternion to a <see cref="ReactiveQuaternion"/>.
        /// </summary>
        public static implicit operator ReactiveQuaternion(Quaternion value) => new(value);

        /// <summary>
        /// Subscribes to value change notifications.
        /// </summary>
        /// <param name="listener">Callback to invoke on value change.</param>
        /// <returns>A subscription object to manage the listener.</returns>
        public Subscription<Quaternion> Subscribe(Action<Quaternion> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<Quaternion>(this, listener);
        }

        /// <summary>
        /// Unsubscribes from value change notifications.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<Quaternion> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Manually invokes the value change event.
        /// </summary>
        private void InvokeEvent(Quaternion value) => this.OnValueChanged?.Invoke(value);

        /// <summary>
        /// Disposes the object by clearing all listeners.
        /// </summary>
        public void Dispose() => this.OnValueChanged = null;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.Value.ToString();
    }
}
#endif
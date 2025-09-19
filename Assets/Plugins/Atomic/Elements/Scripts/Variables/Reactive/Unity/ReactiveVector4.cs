#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper for a <see cref="Vector4"/> that notifies listeners when the value changes.
    /// Implements <see cref="IReactiveVariable{Vector4}"/> and <see cref="IDisposable"/>.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveVector4 : IReactiveVariable<Vector4>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<Vector4> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Vector4 value;

        /// <summary>
        /// Gets or sets the current <see cref="Vector4"/> value.
        /// Invokes <see cref="OnValueChanged"/> if the new value is different.
        /// </summary>
        public Vector4 Value
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
        /// Initializes a new instance of the <see cref="ReactiveVector4"/> class with the default value.
        /// </summary>
        public ReactiveVector4() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveVector4"/> class with the specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Vector4"/> value.</param>
        public ReactiveVector4(Vector4 value) => this.value = value;

        /// <summary>
        /// Implicitly wraps a <see cref="Vector4"/> value in a <see cref="ReactiveVector4"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator ReactiveVector4(Vector4 value) => new(value);

        /// <summary>
        /// Subscribes a listener to value change notifications.
        /// </summary>
        /// <param name="listener">The callback to invoke when the value changes.</param>
        /// <returns>A subscription token for later unsubscription.</returns>
        public Subscription<Vector4> Subscribe(Action<Vector4> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<Vector4>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a previously registered listener.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<Vector4> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Manually invokes the <see cref="OnValueChanged"/> event with the given value.
        /// </summary>
        private void InvokeEvent(Vector4 value) => this.OnValueChanged?.Invoke(value);

        /// <summary>
        /// Disposes the variable by removing all event listeners.
        /// </summary>
        public void Dispose() => this.OnValueChanged = null;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.Value.ToString();
    }
}
#endif

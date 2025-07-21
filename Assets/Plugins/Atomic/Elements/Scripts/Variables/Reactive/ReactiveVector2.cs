#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper for <see cref="Vector2"/> that notifies listeners when the value changes.
    /// Implements <see cref="IReactiveVariable{Vector2}"/> and <see cref="IDisposable"/>.
    /// </summary>
    [Serializable]
    public class ReactiveVector2 : IReactiveVariable<Vector2>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<Vector2> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Vector2 value;

        /// <summary>
        /// Gets or sets the current <see cref="Vector2"/> value.
        /// Invokes <see cref="OnValueChanged"/> if the new value is different.
        /// </summary>
        public Vector2 Value
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
        /// Initializes a new instance of the <see cref="ReactiveVector2"/> class with a default value.
        /// </summary>
        public ReactiveVector2() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveVector2"/> class with the specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Vector2"/> value.</param>
        public ReactiveVector2(Vector2 value) => this.value = value;

        /// <summary>
        /// Implicitly wraps a <see cref="Vector2"/> value in a <see cref="ReactiveVector2"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator ReactiveVector2(Vector2 value) => new(value);

        /// <summary>
        /// Subscribes a listener to value change notifications.
        /// </summary>
        /// <param name="listener">The callback to invoke when the value changes.</param>
        /// <returns>A subscription token for later unsubscription.</returns>
        public Subscription<Vector2> Subscribe(Action<Vector2> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<Vector2>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a previously registered listener.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<Vector2> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Manually invokes the <see cref="OnValueChanged"/> event with the given value.
        /// </summary>
        private void InvokeEvent(Vector2 value) => this.OnValueChanged?.Invoke(value);

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
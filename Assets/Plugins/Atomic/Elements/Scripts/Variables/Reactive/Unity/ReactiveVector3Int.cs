#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper for <see cref="Vector3Int"/> that notifies listeners when the value changes.
    /// Implements <see cref="IReactiveVariable{Vector3Int}"/> and <see cref="IDisposable"/>.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveVector3Int : IReactiveVariable<Vector3Int>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<Vector3Int> OnEvent;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Vector3Int value;

        /// <summary>
        /// Gets or sets the current <see cref="Vector3Int"/> value.
        /// Invokes <see cref="OnEvent"/> if the new value is different.
        /// </summary>
        public Vector3Int Value
        {
            get => this.value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnEvent?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveVector3Int"/> class with a default value.
        /// </summary>
        public ReactiveVector3Int() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveVector3Int"/> class with the specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Vector3Int"/> value.</param>
        public ReactiveVector3Int(Vector3Int value) => this.value = value;

        /// <summary>
        /// Implicitly wraps a <see cref="Vector3Int"/> value in a <see cref="ReactiveVector3Int"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator ReactiveVector3Int(Vector3Int value) => new(value);

        /// <summary>
        /// Subscribes a listener to value change notifications.
        /// </summary>
        /// <param name="listener">The callback to invoke when the value changes.</param>
        /// <returns>A subscription token for later unsubscription.</returns>
        public Subscription<Vector3Int> Subscribe(Action<Vector3Int> listener)
        {
            this.OnEvent += listener;
            return new Subscription<Vector3Int>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a previously registered listener.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<Vector3Int> listener) => this.OnEvent -= listener;

        /// <summary>
        /// Manually invokes the <see cref="OnEvent"/> event with the given value.
        /// </summary>
        private void InvokeEvent(Vector3Int value) => this.OnEvent?.Invoke(value);

        /// <summary>
        /// Disposes the variable by removing all event listeners.
        /// </summary>
        public void Dispose() => this.OnEvent = null;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.Value.ToString();
    }
}
#endif

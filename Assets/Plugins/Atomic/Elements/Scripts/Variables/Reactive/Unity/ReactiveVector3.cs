#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper for <see cref="Vector3"/> that notifies listeners when the value changes.
    /// Implements <see cref="IReactiveVariable{Vector3}"/> and <see cref="IDisposable"/>.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveVector3 : IReactiveVariable<Vector3>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<Vector3> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Vector3 value;

        /// <summary>
        /// Gets or sets the current <see cref="Vector3"/> value.
        /// Notifies listeners via <see cref="OnValueChanged"/> if the value changes.
        /// </summary>
        public Vector3 Value
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
        /// Initializes a new instance of the <see cref="ReactiveVector3"/> class with the default value.
        /// </summary>
        public ReactiveVector3() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveVector3"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Vector3"/> value.</param>
        public ReactiveVector3(Vector3 value) => this.value = value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveVector3"/> class with specified components.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        /// <param name="z">The Z component.</param>
        public ReactiveVector3(float x, float y, float z) => this.value = new Vector3(x, y, z);

        /// <summary>
        /// Implicitly wraps a <see cref="Vector3"/> value in a <see cref="ReactiveVector3"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator ReactiveVector3(Vector3 value) => new(value);

        /// <summary>
        /// Subscribes to value change events.
        /// </summary>
        /// <param name="listener">The listener to invoke when the value changes.</param>
        /// <returns>A subscription that can be disposed to unsubscribe.</returns>
        public Subscription<Vector3> Subscribe(Action<Vector3> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<Vector3>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a previously registered listener.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<Vector3> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Manually invokes the <see cref="OnValueChanged"/> event.
        /// </summary>
        private void InvokeEvent(Vector3 value) => this.OnValueChanged?.Invoke(value);

        /// <summary>
        /// Removes all listeners and releases references.
        /// </summary>
        public void Dispose() => this.OnValueChanged = null;

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.Value.ToString();
    }
}
#endif
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive integer variable that invokes events on value change.
    /// Implements <see cref="IReactiveVariable{int}"/> and <see cref="IDisposable"/>.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveInt : IReactiveVariable<int>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<int> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private int value;

        /// <summary>
        /// Gets or sets the integer value. Triggers <see cref="OnValueChanged"/> when the value changes.
        /// </summary>
        public int Value
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
        /// Initializes a new instance of <see cref="ReactiveInt"/> with a default value of 0.
        /// </summary>
        public ReactiveInt() => this.value = default;

        /// <summary>
        /// Initializes a new instance of <see cref="ReactiveInt"/> with a specific value.
        /// </summary>
        /// <param name="value">Initial integer value.</param>
        public ReactiveInt(int value) => this.value = value;

        /// <summary>
        /// Implicitly converts an integer to a <see cref="ReactiveInt"/>.
        /// </summary>
        public static implicit operator ReactiveInt(int value) => new(value);

        /// <summary>
        /// Subscribes to value change events.
        /// </summary>
        /// <param name="listener">Callback invoked when the value changes.</param>
        /// <returns>A subscription object for managing the listener.</returns>
        public Subscription<int> Subscribe(Action<int> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<int>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a listener from value change events.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<int> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Invokes the <see cref="OnValueChanged"/> event manually with the given value.
        /// </summary>
        private void InvokeEvent(int value) => this.OnValueChanged?.Invoke(value);

        /// <summary>
        /// Disposes the variable by clearing all listeners from <see cref="OnValueChanged"/>.
        /// </summary>
        public void Dispose() => InternalUtils.Dispose(ref this.OnValueChanged);

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.Value.ToString();
    }
}
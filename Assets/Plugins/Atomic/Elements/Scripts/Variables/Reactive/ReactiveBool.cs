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
    /// A reactive boolean variable that notifies subscribers when its value changes.
    /// Implements <see cref="IReactiveVariable{Boolean}"/> and <see cref="IDisposable"/>.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveBool : IReactiveVariable<bool>, IDisposable
    {
        /// <inheritdoc />
        public event Action<bool> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool value;

        /// <summary>
        /// Gets or sets the current value.
        /// Triggers <see cref="OnValueChanged"/> if the value is different from the previous.
        /// </summary>
        public bool Value
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
        /// Creates a new instance of <see cref="ReactiveBool"/> with the default value (false).
        /// </summary>
        public ReactiveBool() => this.value = default;

        /// <summary>
        /// Creates a new instance of <see cref="ReactiveBool"/> with the specified value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public ReactiveBool(bool value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="bool"/> to a <see cref="ReactiveBool"/>.
        /// </summary>
        /// <param name="value">The boolean value to wrap.</param>
        public static implicit operator ReactiveBool(bool value) => new(value);

        /// <summary>
        /// Subscribes a listener to the <see cref="OnValueChanged"/> event.
        /// </summary>
        /// <param name="listener">The callback to invoke on value changes.</param>
        /// <returns>A disposable subscription object.</returns>
        public Subscription<bool> Subscribe(Action<bool> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<bool>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a listener from the <see cref="OnValueChanged"/> event.
        /// </summary>
        /// <param name="listener">The callback to remove.</param>
        public void Unsubscribe(Action<bool> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Manually triggers the <see cref="OnValueChanged"/> event with the current value.
        /// </summary>
        /// <param name="value">The value to invoke with.</param>
        private void InvokeEvent(bool value) => this.OnValueChanged?.Invoke(value);

        /// <summary>
        /// Disposes the instance and clears all subscriptions.
        /// </summary>
        public void Dispose() => this.OnValueChanged = null;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.Value.ToString();
    }
}
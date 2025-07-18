using System;
using System.Globalization;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive float variable that triggers events on value changes.
    /// Implements <see cref="IReactiveVariable{float}"/> and <see cref="IDisposable"/>.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveFloat : IReactiveVariable<float>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<float> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private float value;

        /// <summary>
        /// Gets or sets the float value. Raises <see cref="OnValueChanged"/> if the value is different.
        /// </summary>
        public float Value
        {
            get => this.value;
            set
            {
                if (Math.Abs(this.value - value) > float.Epsilon)
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveFloat"/> class with default value (0).
        /// </summary>
        public ReactiveFloat() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveFloat"/> class with a specified value.
        /// </summary>
        /// <param name="value">Initial float value.</param>
        public ReactiveFloat(float value) => this.value = value;

        /// <summary>
        /// Implicitly converts a float to a <see cref="ReactiveFloat"/>.
        /// </summary>
        public static implicit operator ReactiveFloat(float value) => new(value);

        /// <summary>
        /// Subscribes to the <see cref="OnValueChanged"/> event.
        /// </summary>
        /// <param name="listener">Callback to invoke when the value changes.</param>
        /// <returns>A subscription that can be used for later unsubscription.</returns>
        public Subscription<float> Subscribe(Action<float> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<float>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a listener from the <see cref="OnValueChanged"/> event.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<float> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Manually invokes the <see cref="OnValueChanged"/> event.
        /// </summary>
        /// <param name="value">The value to broadcast.</param>
        private void InvokeEvent(float value) => this.OnValueChanged?.Invoke(value);

        /// <summary>
        /// Disposes the instance and removes all listeners from the <see cref="OnValueChanged"/> event.
        /// </summary>
        public void Dispose() => InternalUtils.Dispose(ref this.OnValueChanged);

        /// <summary>
        /// Returns a string representation of the current value using invariant culture.
        /// </summary>
        public override string ToString() => this.value.ToString(CultureInfo.InvariantCulture);
    }
}
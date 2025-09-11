using System;
using System.Collections.Generic;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a serialized reactive variable that raises events when its value changes.
    /// Implements <see cref="IReactiveVariable{T}"/> and <see cref="IDisposable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveVariable<T> : IReactiveVariable<T>, IDisposable
    {
        private static readonly IEqualityComparer<T> s_equalityComparer = EqualityComparer.GetDefault<T>();

        public event Action<T> OnValueChanged;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeValueChanged))]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T value;

        /// <summary>
        /// Gets or sets the current value.
        /// When a new value is assigned that differs from the previous one, the <see cref="OnValueChanged"/> event is triggered.
        /// </summary>
        public T Value
        {
            get => this.value;
            set
            {
                if (!s_equalityComparer.Equals(this.value, value))
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="ReactiveVariable{T}"/> with the default value.
        /// </summary>
        public ReactiveVariable() => this.value = default;

        /// <summary>
        /// Creates a new instance of <see cref="ReactiveVariable{T}"/> with the specified initial value.
        /// </summary>
        /// <param name="value">Initial value to assign.</param>
        public ReactiveVariable(T value) => this.value = value;

        /// <summary>
        /// Implicitly converts a value to a <see cref="ReactiveVariable{T}"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator ReactiveVariable<T>(T value) => new(value);

        /// <summary>
        /// Returns the stored value. This is useful for functional-style invocation.
        /// </summary>
        public T Invoke() => this.value;

        /// <summary>
        /// Subscribes a listener to be notified when the value changes.
        /// </summary>
        /// <param name="listener">The callback to invoke on value change.</param>
        /// <returns>A subscription that can be used to unsubscribe later.</returns>
        public Subscription<T> Subscribe(Action<T> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<T>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a previously registered listener from value change notifications.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void Unsubscribe(Action<T> listener) => this.OnValueChanged -= listener;

        /// <summary>
        /// Manually triggers the <see cref="OnValueChanged"/> event with the given value.
        /// </summary>
        private void InvokeValueChanged(T value) => this.OnValueChanged?.Invoke(value);

        /// <summary>
        /// Disposes the object by clearing all subscribed listeners.
        /// </summary>
        public void Dispose() => this.OnValueChanged = null;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value?.ToString();
    }
}
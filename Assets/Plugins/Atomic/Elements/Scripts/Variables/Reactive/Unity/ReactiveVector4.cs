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
        public event Action<Vector4> OnEvent;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Vector4 value;

        /// <summary>
        /// Gets or sets the current <see cref="Vector4"/> value.
        /// Invokes <see cref="OnEvent"/> if the new value is different.
        /// </summary>
        public Vector4 Value
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
        /// Manually invokes the <see cref="OnEvent"/> event with the given value.
        /// </summary>
        private void InvokeEvent(Vector4 value) => this.OnEvent?.Invoke(value);

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

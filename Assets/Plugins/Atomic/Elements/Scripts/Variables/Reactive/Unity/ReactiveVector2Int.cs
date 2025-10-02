#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper for <see cref="Vector2Int"/> that notifies listeners when the value changes.
    /// Implements <see cref="IReactiveVariable{Vector2Int}"/> and <see cref="IDisposable"/>.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class ReactiveVector2Int : IReactiveVariable<Vector2Int>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<Vector2Int> OnEvent;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Vector2Int value;

        /// <summary>
        /// Gets or sets the current <see cref="Vector2Int"/> value.
        /// Invokes <see cref="OnEvent"/> if the new value is different.
        /// </summary>
        public Vector2Int Value
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
        /// Initializes a new instance of the <see cref="ReactiveVector2Int"/> class with a default value.
        /// </summary>
        public ReactiveVector2Int() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveVector2Int"/> class with the specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Vector2Int"/> value.</param>
        public ReactiveVector2Int(Vector2Int value) => this.value = value;

        /// <summary>
        /// Implicitly wraps a <see cref="Vector2Int"/> value in a <see cref="ReactiveVector2Int"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator ReactiveVector2Int(Vector2Int value) => new(value);
        
        /// <summary>
        /// Manually invokes the <see cref="OnEvent"/> event with the given value.
        /// </summary>
        private void InvokeEvent(Vector2Int value) => this.OnEvent?.Invoke(value);

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

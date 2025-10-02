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
        public event Action<int> OnEvent;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private int value;

        /// <summary>
        /// Gets or sets the integer value. Triggers <see cref="OnEvent"/> when the value changes.
        /// </summary>
        public int Value
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
        /// Initializes a new instance of <see cref="ReactiveInt"/> with a default value of 0.
        /// </summary>
        public ReactiveInt() => this.value = 0;

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
        /// Invokes the <see cref="OnEvent"/> event manually with the given value.
        /// </summary>
        private void InvokeEvent(int value) => this.OnEvent?.Invoke(value);

        /// <summary>
        /// Disposes the variable by clearing all listeners from <see cref="OnEvent"/>.
        /// </summary>
        public void Dispose() => this.OnEvent = null;

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.Value.ToString();
    }
}
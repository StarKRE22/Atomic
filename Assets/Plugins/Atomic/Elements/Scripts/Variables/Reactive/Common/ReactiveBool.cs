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
        public event Action<bool> OnEvent;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool value;

        /// <summary>
        /// Gets or sets the current value.
        /// Triggers <see cref="OnEvent"/> if the value is different from the previous.
        /// </summary>
        public bool Value
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
        /// Creates a new instance of <see cref="ReactiveBool"/> with the default value (false).
        /// </summary>
        public ReactiveBool() => this.value = false;

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
        /// Manually triggers the <see cref="OnEvent"/> event with the current value.
        /// </summary>
        /// <param name="value">The value to invoke with.</param>
        private void InvokeEvent(bool value) => this.OnEvent?.Invoke(value);

        /// <summary>
        /// Disposes the instance and clears all subscriptions.
        /// </summary>
        public void Dispose() => this.OnEvent = null;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.Value.ToString();
    }
}
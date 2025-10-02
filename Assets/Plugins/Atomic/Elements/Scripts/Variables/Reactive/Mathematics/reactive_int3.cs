#if UNITY_MATHEMATICS
using System;
using Atomic.Elements;
using Unity.Mathematics;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper for a <see cref="int3"/> value.
    /// Invokes <see cref="OnEvent"/> whenever the value changes.
    /// Implements <see cref="IReactiveVariable{int3}"/>.
    /// </summary>
    [Serializable]
    public sealed class reactive_int3 : IReactiveVariable<int3>, IDisposable
    {
        /// <inheritdoc/>
        public event Action<int3> OnEvent;

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private int3 value;

        /// <summary>
        /// Gets or sets the current <see cref="int3"/> value.
        /// Triggers <see cref="OnEvent"/> when changed.
        /// </summary>
        public int3 Value
        {
            get => this.value;
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    this.OnEvent?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="reactive_int3"/> with the default value (0,0,0).
        /// </summary>
        public reactive_int3() => this.value = int3.zero;

        /// <summary>
        /// Creates a new <see cref="reactive_int3"/> with a specified initial value.
        /// </summary>
        /// <param name="value">Initial int3 value.</param>
        public reactive_int3(int3 value) => this.value = value;

        /// <summary>
        /// Implicitly converts an <see cref="int3"/> to a <see cref="reactive_int3"/>.
        /// </summary>
        public static implicit operator reactive_int3(int3 value) => new(value);
        
        /// <summary>
        /// Invokes the value changed event manually (used by Odin Inspector).
        /// </summary>
        /// <param name="value">The value to broadcast.</param>
        private void InvokeEvent(int3 value) => this.OnEvent?.Invoke(value);

        /// <summary>
        /// Disposes the reactive variable by clearing all listeners.
        /// </summary>
        public void Dispose() => this.OnEvent = null;

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif

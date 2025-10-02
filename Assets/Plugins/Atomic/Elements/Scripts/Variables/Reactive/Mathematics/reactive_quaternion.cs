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
    /// A reactive wrapper for a <see cref="quaternion"/> value.
    /// Notifies subscribers whenever the value changes.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class reactive_quaternion : IReactiveVariable<quaternion>
    {
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<quaternion> OnEvent;

        /// <summary>
        /// The internal quaternion value.
        /// Triggers <see cref="OnEvent"/> when set to a new value.
        /// </summary>
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private quaternion value;

        /// <summary>
        /// Gets or sets the current quaternion value.
        /// Setting a new value invokes <see cref="OnEvent"/> if it differs from the previous value.
        /// </summary>
        public quaternion Value
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
        /// Creates a new <see cref="reactive_quaternion"/> with a default value.
        /// </summary>
        public reactive_quaternion() => this.value = default;

        /// <summary>
        /// Creates a new <see cref="reactive_quaternion"/> with the specified value.
        /// </summary>
        /// <param name="value">The initial quaternion value.</param>
        public reactive_quaternion(quaternion value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="quaternion"/> to a <see cref="reactive_quaternion"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator reactive_quaternion(quaternion value) => new(value);

        /// <summary>
        /// Manually invokes the <see cref="OnEvent"/> event with the given value.
        /// Used internally by the inspector.
        /// </summary>
        /// <param name="value">The value to invoke with.</param>
        private void InvokeEvent(quaternion value) => this.OnEvent?.Invoke(value);
    }
}
#endif

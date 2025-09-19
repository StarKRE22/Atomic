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
    /// A simple serialized variable for <see cref="float2"/> values.
    /// Implements <see cref="IVariable{float2}"/>.
    /// </summary>
    [Serializable]
    public sealed class float2_variable : IVariable<float2>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private float2 value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public float2 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (0,0).
        /// </summary>
        public float2_variable() => this.value = float2.zero;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial float2 value.</param>
        public float2_variable(float2 value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public float2 Invoke() => this.value;

        /// <summary>
        /// Implicitly converts a <see cref="float2"/> to a <see cref="float2_variable"/>.
        /// </summary>
        public static implicit operator float2_variable(float2 value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
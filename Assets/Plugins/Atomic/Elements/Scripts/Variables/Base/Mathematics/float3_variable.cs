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
    /// A simple serialized variable for <see cref="float3"/> values.
    /// Implements <see cref="IVariable{float3}"/>.
    /// </summary>
    [Serializable]
    public sealed class float3_variable : IVariable<float3>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private float3 value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public float3 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (0,0,0).
        /// </summary>
        public float3_variable() => this.value = float3.zero;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial float3 value.</param>
        public float3_variable(float3 value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public float3 Invoke() => this.value;

        /// <summary>
        /// Implicitly converts a <see cref="float3"/> to a <see cref="float3_variable"/>.
        /// </summary>
        public static implicit operator float3_variable(float3 value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
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
    /// A simple serialized variable for <see cref="float4"/> values.
    /// Implements <see cref="IVariable{float4}"/>.
    /// </summary>
    [Serializable]
    public sealed class Float4Variable : IVariable<float4>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private float4 value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public float4 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (0,0,0,0).
        /// </summary>
        public Float4Variable() => this.value = float4.zero;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial float4 value.</param>
        public Float4Variable(float4 value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public float4 Invoke() => this.value;

        /// <summary>
        /// Implicitly converts a <see cref="float4"/> to a <see cref="Float4Variable"/>.
        /// </summary>
        public static implicit operator Float4Variable(float4 value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
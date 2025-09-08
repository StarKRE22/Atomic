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
    /// A simple serialized variable for <see cref="int2"/> values.
    /// Implements <see cref="IVariable{int2}"/>.
    /// </summary>
    [Serializable]
    public sealed class int2_variable : IVariable<int2>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private int2 value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public int2 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (0,0).
        /// </summary>
        public int2_variable() => this.value = int2.zero;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial int2 value.</param>
        public int2_variable(int2 value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public int2 Invoke() => this.value;

        /// <summary>
        /// Implicitly converts an <see cref="int2"/> to an <see cref="int2_variable"/>.
        /// </summary>
        public static implicit operator int2_variable(int2 value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
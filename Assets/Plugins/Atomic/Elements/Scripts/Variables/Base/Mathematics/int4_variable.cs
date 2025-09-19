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
    /// A simple serialized variable for <see cref="int4"/> values.
    /// Implements <see cref="IVariable{int4}"/>.
    /// </summary>
    [Serializable]
    public sealed class int4_variable : IVariable<int4>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private int4 value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public int4 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (0,0,0,0).
        /// </summary>
        public int4_variable() => this.value = int4.zero;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial int4 value.</param>
        public int4_variable(int4 value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public int4 Invoke() => this.value;

        /// <summary>
        /// Implicitly converts an <see cref="int4"/> to an <see cref="int4_variable"/>.
        /// </summary>
        public static implicit operator int4_variable(int4 value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
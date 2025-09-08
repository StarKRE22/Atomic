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
    /// A simple serialized variable for <see cref="int3"/> values.
    /// Implements <see cref="IVariable{int3}"/>.
    /// </summary>
    [Serializable]
    public sealed class int3_variable : IVariable<int3>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private int3 value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public int3 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (0,0,0).
        /// </summary>
        public int3_variable() => this.value = int3.zero;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial int3 value.</param>
        public int3_variable(int3 value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public int3 Invoke() => this.value;

        /// <summary>
        /// Implicitly converts an <see cref="int3"/> to an <see cref="int3_variable"/>.
        /// </summary>
        public static implicit operator int3_variable(int3 value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
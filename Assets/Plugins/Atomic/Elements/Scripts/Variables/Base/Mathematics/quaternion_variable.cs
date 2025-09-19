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
    /// A simple serialized variable that stores a <see cref="quaternion"/> value.
    /// Implements <see cref="IVariable{quaternion}"/>.
    /// </summary>
    [Serializable]
    public sealed class quaternion_variable : IVariable<quaternion>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private quaternion value;

        /// <summary>
        /// Gets or sets the current <see cref="quaternion"/> value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public quaternion Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (<see cref="quaternion.identity"/>).
        /// </summary>
        public quaternion_variable() => this.value = quaternion.identity;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="quaternion"/> value.</param>
        public quaternion_variable(quaternion value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public quaternion Invoke() => this.value;

        /// <summary>
        /// Implicitly converts a <see cref="quaternion"/> to a <see cref="quaternion_variable"/>.
        /// </summary>
        public static implicit operator quaternion_variable(quaternion value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.value.ToString();
    }
}
#endif
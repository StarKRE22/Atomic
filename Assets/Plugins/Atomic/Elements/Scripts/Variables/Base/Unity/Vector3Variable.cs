#if UNITY_5_3_OR_NEWER
using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A simple serialized container for a <see cref="Vector3"/> value.
    /// Implements <see cref="IVariable{Vector3}"/>.
    /// </summary>
    [Serializable]
    public sealed class Vector3Variable : IVariable<Vector3>
    {
        /// <summary>
        /// Gets or sets the stored <see cref="Vector3"/> value.
        /// </summary>
        public Vector3 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// The serialized <see cref="Vector3"/> value.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private Vector3 value;

        /// <summary>
        /// Returns the stored value.
        /// </summary>
        public Vector3 Invoke() => this.value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3Variable"/> class with the default value.
        /// </summary>
        public Vector3Variable() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseVector3"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Vector3"/> value.</param>
        public Vector3Variable(Vector3 value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="Vector3"/> to a <see cref="Vector3Variable"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator Vector3Variable(Vector3 value) => new(value);

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
#if UNITY_5_3_OR_NEWER
using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A simple serialized container for a <see cref="Vector4"/> value.
    /// Implements <see cref="IVariable{Vector4}"/>.
    /// </summary>
    [Serializable]
    public sealed class Vector4Variable : IVariable<Vector4>
    {
        /// <summary>
        /// Gets or sets the stored <see cref="Vector4"/> value.
        /// </summary>
        public Vector4 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// The serialized <see cref="Vector4"/> value.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private Vector4 value;

        /// <summary>
        /// Returns the stored value.
        /// </summary>
        public Vector4 Invoke() => this.value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4Variable"/> class with the default value.
        /// </summary>
        public Vector4Variable() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4Variable"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Vector4"/> value.</param>
        public Vector4Variable(Vector4 value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="Vector4"/> to a <see cref="Vector4Variable"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator Vector4Variable(Vector4 value) => new(value);

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
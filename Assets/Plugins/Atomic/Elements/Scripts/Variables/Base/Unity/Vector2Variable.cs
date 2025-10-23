#if UNITY_5_3_OR_NEWER
using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A simple serialized container for a <see cref="Vector2"/> value.
    /// Implements <see cref="IVariable{Vector2}"/>.
    /// </summary>
    [Serializable]
    public sealed class Vector2Variable : IVariable<Vector2>
    {
        /// <summary>
        /// Gets or sets the stored <see cref="Vector2"/> value.
        /// </summary>
        public Vector2 Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// The serialized <see cref="Vector2"/> value.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private Vector2 value;

        /// <summary>
        /// Returns the stored value.
        /// </summary>
        public Vector2 Invoke() => this.value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2Variable"/> class with the default value.
        /// </summary>
        public Vector2Variable() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2Variable"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Vector2"/> value.</param>
        public Vector2Variable(Vector2 value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="Vector2"/> to a <see cref="Vector2Variable"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator Vector2Variable(Vector2 value) => new(value);

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
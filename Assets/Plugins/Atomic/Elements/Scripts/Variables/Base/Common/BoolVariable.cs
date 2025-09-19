using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A simple serialized container for a <see cref="bool"/> value.
    /// Implements <see cref="IVariable{bool}"/>.
    /// </summary>
    [Serializable]
    public sealed class BoolVariable : IVariable<bool>
    {
        /// <summary>
        /// Gets or sets the stored <see cref="bool"/> value.
        /// </summary>
        public bool Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// The serialized <see cref="bool"/> value.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool value;

        /// <summary>
        /// Returns the stored value.
        /// </summary>
        public bool Invoke() => this.value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolVariable"/> class with the default value.
        /// </summary>
        public BoolVariable() => this.value = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolVariable"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="bool"/> value.</param>
        public BoolVariable(bool value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="bool"/> to a <see cref="BoolVariable"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator BoolVariable(bool value) => new(value);

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
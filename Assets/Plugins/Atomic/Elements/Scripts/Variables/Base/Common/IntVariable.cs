using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A simple serialized container for an <see cref="int"/> value.
    /// Implements <see cref="IVariable{int}"/>.
    /// </summary>
    [Serializable]
    public sealed class IntVariable : IVariable<int>
    {
        /// <summary>
        /// Gets or sets the stored <see cref="int"/> value.
        /// </summary>
        public int Value
        {
            get => this.value;
            set => this.value = value;
        }

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private int value;

        /// <summary>
        /// Returns the stored value.
        /// </summary>
        public int Invoke() => this.value;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class with the default value.
        /// </summary>
        public IntVariable() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVariable"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="int"/> value.</param>
        public IntVariable(int value) => this.value = value;

        /// <summary>
        /// Implicitly converts an <see cref="int"/> to a <see cref="IntVariable"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator IntVariable(int value) => new(value);

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}

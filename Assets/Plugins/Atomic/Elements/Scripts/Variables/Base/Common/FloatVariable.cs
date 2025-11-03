using System;
using System.Globalization;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A simple serialized container for a <see cref="float"/> value.
    /// Implements <see cref="IVariable{float}"/>.
    /// </summary>
    [Serializable]
    public sealed class FloatVariable : IVariable<float>
    {
        /// <summary>
        /// Gets or sets the stored <see cref="float"/> value.
        /// </summary>
        public float Value
        {
            get => this.value;
            set => this.value = value;
        }

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private float value;

        /// <summary>
        /// Returns the stored value.
        /// </summary>
        public float Invoke() => this.value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class with the default value.
        /// </summary>
        public FloatVariable() => this.value = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatVariable"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="float"/> value.</param>
        public FloatVariable(float value) => this.value = value;

        /// <summary>
        /// Implicitly converts a <see cref="float"/> to a <see cref="FloatVariable"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator FloatVariable(float value) => new(value);

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.value.ToString(CultureInfo.InvariantCulture);
    }
}
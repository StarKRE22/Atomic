using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A simple serialized container for a value of type <typeparamref name="T"/>.
    /// Implements <see cref="IVariable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    [Serializable]
    public class Variable<T> : IVariable<T>
    {
        /// <summary>
        /// Gets or sets the stored value.
        /// </summary>
        public T Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// The serialized value.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T value;

        /// <summary>
        /// Returns the stored value.
        /// </summary>
        public T Invoke() => this.value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Variable{T}"/> class with the default value.
        /// </summary>
        public Variable() => this.value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="Variable{T}"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public Variable(T value) => this.value = value;

        /// <summary>
        /// Implicitly converts a value of type <typeparamref name="T"/> to a <see cref="Variable{T}"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator Variable<T>(T value) => new(value);

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString() => this.value?.ToString();
    }
}
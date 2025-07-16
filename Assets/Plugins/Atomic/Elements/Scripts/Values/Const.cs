using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a serialized, immutable (read-only) constant value wrapper.
    /// Implements <see cref="IValue{T}"/> and supports implicit conversions.
    /// Useful in systems where values must be serialized or treated as data sources.
    /// </summary>
    /// <typeparam name="T">The type of the wrapped constant value.</typeparam>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class Const<T> : IValue<T>
    {
        /// <summary>
        /// Invokes the constant value as a function (from <see cref="IValue{T}"/>).
        /// </summary>
        /// <returns>The wrapped constant value.</returns>
        public T Invoke() => this.value;

        /// <summary>
        /// Gets the wrapped constant value.
        /// </summary>
        public T Value => this.value;

#if ODIN_INSPECTOR
        [HideLabel]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Const{T}"/> class with the default value of <typeparamref name="T"/>.
        /// </summary>
        public Const()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Const{T}"/> class with a specified value.
        /// </summary>
        /// <param name="value">The constant value to wrap.</param>
        public Const(T value) => this.value = value;

        /// <summary>
        /// Implicitly converts a value of type <typeparamref name="T"/> to a <see cref="Const{T}"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        /// <returns>A new <see cref="Const{T}"/> containing the value.</returns>
        public static implicit operator Const<T>(T value) => new(value);

        /// <summary>
        /// Implicitly converts a <see cref="Const{T}"/> to its underlying value of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">The constant wrapper to extract the value from.</param>
        /// <returns>The underlying constant value.</returns>
        public static implicit operator T(Const<T> value) => value.value;

        /// <summary>
        /// Returns a string that represents the wrapped constant value.
        /// </summary>
        /// <returns>A string representation of the value.</returns>
        public override string ToString() => this.value?.ToString();
    }
}
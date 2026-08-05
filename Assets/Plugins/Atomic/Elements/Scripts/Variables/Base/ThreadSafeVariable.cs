using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a thread-safe implementation of <see cref="IVariable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    public class ThreadSafeVariable<T> : IVariable<T>
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer<T>.Default;
        private readonly object _lock = new();

        private T _value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>
        /// The stored value. Access is synchronized to ensure thread safety.
        /// </value>
        public T Value
        {
            get
            {
                lock (_lock)
                    return _value;
            }
            set
            {
                lock (_lock)
                {
                    if (s_comparer.Equals(_value, value))
                        return;

                    _value = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeVariable{T}"/> class
        /// with the default value of <typeparamref name="T"/>.
        /// </summary>
        public ThreadSafeVariable() => _value = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeVariable{T}"/> class
        /// with the specified initial value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public ThreadSafeVariable(T value) => _value = value;

        /// <summary>
        /// Returns the string representation of the current value.
        /// </summary>
        /// <returns>
        /// The string representation of the stored value, or <see langword="null"/>
        /// if the value is <see langword="null"/>.
        /// </returns>
        public override string ToString()
        {
            lock (_lock)
                return _value?.ToString();
        }
    }
}

using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a parameterless boolean predicate.
    /// Wraps a <see cref="System.Func{Boolean}"/> delegate and implements <see cref="IPredicate"/>.
    /// </summary>
    public class InlinePredicate : InlineFunction<bool>, IPredicate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlinePredicate"/> class with the specified function.
        /// </summary>
        /// <param name="func">The boolean-returning function to wrap.</param>
        public InlinePredicate(Func<bool> func) : base(func)
        {
        }
        
        /// <summary>
        /// Implicit conversion from a <see cref="Func{bool}"/> to <see cref="InlinePredicate"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator InlinePredicate(Func<bool> value) => new(value);
    }

    /// <summary>
    /// Represents a predicate with one input parameter.
    /// Wraps a <see cref="System.Func{T, Boolean}"/> delegate and implements <see cref="IPredicate{T}"/>.
    /// </summary>
    /// <typeparam name="T">The input type of the predicate.</typeparam>
    public class InlinePredicate<T> : InlineFunction<T, bool>, IPredicate<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlinePredicate{T}"/> class with the specified function.
        /// </summary>
        /// <param name="func">The function that takes a <typeparamref name="T"/> and returns a boolean.</param>
        public InlinePredicate(Func<T, bool> func) : base(func)
        {
        }
        
        /// <summary>
        /// Implicit conversion from a <see cref="Func{T, bool}"/> to <see cref="InlinePredicate{T}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator InlinePredicate<T>(Func<T, bool> value) => new(value);
    }

    /// <summary>
    /// Represents a predicate with two input parameters.
    /// Wraps a <see cref="System.Func{T1, T2, Boolean}"/> delegate and implements <see cref="IPredicate{T1, T2}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    public class InlinePredicate<T1, T2> : InlineFunction<T1, T2, bool>, IPredicate<T1, T2>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlinePredicate{T1,T2}"/> class with the specified function.
        /// </summary>
        /// <param name="func">The function that takes <typeparamref name="T1"/> and <typeparamref name="T2"/> and returns a boolean.</param>
        public InlinePredicate(Func<T1, T2, bool> func) : base(func)
        {
        }
        
        /// <summary>
        /// Implicit conversion from a <see cref="Func{T1, T2, bool}"/> to <see cref="InlinePredicate{T1, T2}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator InlinePredicate<T1, T2>(Func<T1, T2, bool> value) => new(value);
    }
}
using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods to convert delegates and reactive values into function wrappers.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Wraps a <see cref="Func{R}"/> into a <see cref="BasicFunction{T}"/>.
        /// </summary>
        /// <typeparam name="R">The return type of the function.</typeparam>
        /// <param name="func">The function to wrap.</param>
        /// <returns>A <see cref="BasicFunction{T}"/> that wraps the provided delegate.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasicFunction<R> AsFunction<R>(this Func<R> func) => new(func);

        /// <summary>
        /// Wraps a function with one parameter and a context object into a <see cref="BasicFunction{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the context object.</typeparam>
        /// <typeparam name="R">The return type of the function.</typeparam>
        /// <param name="it">The context object to pass to the function.</param>
        /// <param name="func">The function that accepts the context object.</param>
        /// <returns>A <see cref="BasicFunction{T}"/> that wraps the contextual invocation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasicFunction<R> AsFunction<T, R>(this T it, Func<T, R> func) => new(() => func.Invoke(it));

        /// <summary>
        /// Creates a new function that returns the negation of the current <see cref="IFunction{T}"/> boolean value.
        /// </summary>
        /// <param name="it">The reactive boolean value to negate.</param>
        /// <returns>A <see cref="BasicFunction{T}"/> that returns the inverse of the current value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasicFunction<bool> AsNot(this IFunction<bool> it) => new(() => !it.Invoke());
    }
}

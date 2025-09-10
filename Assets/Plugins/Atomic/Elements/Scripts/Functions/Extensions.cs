using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods to convert delegates and reactive values into function wrappers.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a new function that returns the negation of the current <see cref="IFunction{bool}"/> value.
        /// </summary>
        /// <param name="it">The reactive boolean value to negate.</param>
        /// <returns>A <see cref="InlineFunction{bool}"/> that returns the inverse of the current value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InlineFunction<bool> Invert(this IFunction<bool> it) => new(() => 
            !it.Invoke());

        /// <summary>
        /// Creates a new function that returns the negation of the current <see cref="IFunction{T, bool}"/> value.
        /// </summary>
        /// <typeparam name="T">The input type of the function.</typeparam>
        /// <param name="it">The reactive boolean function to negate.</param>
        /// <returns>An <see cref="InlineFunction{T, bool}"/> that returns the inverse of the current value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InlineFunction<T, bool> Invert<T>(this IFunction<T, bool> it) => 
            new(arg => !it.Invoke(arg));

        /// <summary>
        /// Creates a new function that returns the negation of the current <see cref="IFunction{T1, T2, bool}"/> value.
        /// </summary>
        /// <typeparam name="T1">The first input type of the function.</typeparam>
        /// <typeparam name="T2">The second input type of the function.</typeparam>
        /// <param name="it">The reactive boolean function to negate.</param>
        /// <returns>An <see cref="InlineFunction{T1, T2, bool}"/> that returns the inverse of the current value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InlineFunction<T1, T2, bool> Invert<T1, T2>(this IFunction<T1, T2, bool> it) =>
            new((arg1, arg2) => !it.Invoke(arg1, arg2));
    }
}
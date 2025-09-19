using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods to convert delegates and reactive values into function wrappers.
    /// </summary>
    public static partial class Extensions
    {
        #region Invert

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

        #endregion

        #region Collections
        
        /// <summary>
        /// Adds a parameterless function to a collection of <see cref="Func{R}"/> delegates.
        /// </summary>
        /// <typeparam name="R">The return type of the function.</typeparam>
        /// <param name="it">The collection to which the function will be added.</param>
        /// <param name="member">The <see cref="IFunction{R}"/> object whose <see cref="IFunction{R}.Invoke"/> method will be added to the collection.</param>
        /// <remarks>
        /// This method wraps the <see cref="IFunction{R}.Invoke"/> method into a <see cref="Func{R}"/> delegate.
        /// The method is aggressively inlined for performance.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<R>(this ICollection<Func<R>> it, IFunction<R> member) => it.Add(member.Invoke);

        /// <summary>
        /// Removes a parameterless function from a collection of <see cref="Func{R}"/> delegates.
        /// </summary>
        /// <typeparam name="R">The return type of the function.</typeparam>
        /// <param name="it">The collection from which the function will be removed.</param>
        /// <param name="member">The <see cref="IFunction{R}"/> object whose <see cref="IFunction{R}.Invoke"/> method will be removed from the collection.</param>
        /// <remarks>
        /// This method wraps the <see cref="IFunction{R}.Invoke"/> method into a <see cref="Func{R}"/> delegate.
        /// The method is aggressively inlined for performance.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<R>(this ICollection<Func<R>> it, IFunction<R> member) => it.Remove(member.Invoke);

        #endregion
    }
}
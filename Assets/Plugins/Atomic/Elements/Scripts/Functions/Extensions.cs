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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool InvokeNot(this IFunction<bool> it) => 
            !it.Invoke();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool InvokeNot<T>(this IFunction<T, bool> it, T arg) =>
            !it.Invoke(arg);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool InvokeNot<T1, T2>(this IFunction<T1, T2, bool> it, T1 arg1, T2 arg2) =>
            !it.Invoke(arg1, arg2);

        #region Invert

        /// <summary>
        /// Creates a new function that returns the negation of the current <see cref="IFunction{bool}"/> value.
        /// </summary>
        /// <param name="it">The reactive boolean value to negate.</param>
        /// <returns>A <see cref="InlineFunction{bool}"/> that returns the inverse of the current value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InlineFunction<bool> Negate(this IFunction<bool> it) => new(() => !it.Invoke());

        /// <summary>
        /// Creates a new function that returns the negation of the current <see cref="IFunction{T, bool}"/> value.
        /// </summary>
        /// <typeparam name="T">The input type of the function.</typeparam>
        /// <param name="it">The reactive boolean function to negate.</param>
        /// <returns>An <see cref="InlineFunction{T, bool}"/> that returns the inverse of the current value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InlineFunction<T, bool> Negate<T>(this IFunction<T, bool> it) => new(arg => !it.Invoke(arg));

        /// <summary>
        /// Creates a new function that returns the negation of the current <see cref="IFunction{T1, T2, bool}"/> value.
        /// </summary>
        /// <typeparam name="T1">The first input type of the function.</typeparam>
        /// <typeparam name="T2">The second input type of the function.</typeparam>
        /// <param name="it">The reactive boolean function to negate.</param>
        /// <returns>An <see cref="InlineFunction{T1, T2, bool}"/> that returns the inverse of the current value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InlineFunction<T1, T2, bool> Negate<T1, T2>(this IFunction<T1, T2, bool> it) =>
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
        public static void Add<R>(this ICollection<Func<R>> it, IFunction<R> member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            it.Add(member.Invoke);
        }

        /// <summary>
        /// Adds a single-argument function to a collection of <see cref="Func{T, R}"/> delegates.
        /// </summary>
        /// <typeparam name="T">The type of the input argument of the function.</typeparam>
        /// <typeparam name="R">The return type of the function.</typeparam>
        /// <param name="it">The collection to which the function will be added.</param>
        /// <param name="member">
        /// The <see cref="IFunction{T, R}"/> object whose <see cref="IFunction{T, R}.Invoke"/> 
        /// method will be wrapped and added to the collection.
        /// </param>
        /// <remarks>
        /// This method wraps the <see cref="IFunction{T, R}.Invoke"/> method into a 
        /// <see cref="Func{T, R}"/> delegate.
        /// The method is aggressively inlined for performance.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T, R>(
            this ICollection<Func<T, R>> it,
            IFunction<T, R> member
        )
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            it.Add(member.Invoke);
        }

        /// <summary>
        /// Adds a two-argument function to a collection of <see cref="Func{T1, T2, R}"/> delegates.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T1, T2, R>(
            this ICollection<Func<T1, T2, R>> it,
            IFunction<T1, T2, R> member
        )
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            it.Add(member.Invoke);
        }

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
        public static void Remove<R>(this ICollection<Func<R>> it, IFunction<R> member)
        {
            if (member != null)
                it.Remove(member.Invoke);
        }


        /// <summary>
        /// Removes a two-argument function from a collection of <see cref="Func{T1, T2, R}"/> delegates.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<T1, T2, R>(
            this ICollection<Func<T1, T2, R>> it,
            IFunction<T1, T2, R> member
        )
        {
            if (member != null)
                it.Remove(member.Invoke);
        }

        #endregion
    }
}
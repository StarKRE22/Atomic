using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for creating variable wrappers.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Wraps a value in a <see cref="Variable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="it">The value to wrap.</param>
        /// <returns>A <see cref="Variable{T}"/> containing the given value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Variable<T> AsVariable<T>(this T it) => new(it);

        /// <summary>
        /// Wraps a value in a <see cref="ReactiveVariable{T}"/> for reactive subscriptions.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="it">The value to wrap.</param>
        /// <returns>A <see cref="ReactiveVariable{T}"/> containing the given value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<T> AsReactiveVariable<T>(this T it) => new(it);

        /// <summary>
        /// Creates a <see cref="InlineVariable{T}"/> that wraps access to a field or property of an object.
        /// </summary>
        /// <typeparam name="T">The type of the object that contains the value.</typeparam>
        /// <typeparam name="R">The type of the value being proxied.</typeparam>
        /// <param name="it">The source object.</param>
        /// <param name="getter">A function to retrieve the value from the object.</param>
        /// <param name="setter">An action to set the value on the object.</param>
        /// <returns>A proxy variable that reflects the value through the provided accessors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InlineVariable<R> AsInlineVariable<T, R>(this T it, Func<T, R> getter, Action<T, R> setter) =>
            new(() => getter.Invoke(it), value => setter.Invoke(it, value));
    }
}
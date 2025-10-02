using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for working with reactive values and constants.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Wraps a value in a <see cref="Const{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of the value to wrap.</typeparam>
        /// <param name="it">The value to wrap.</param>
        /// <returns>A new <see cref="Const{T}"/> instance containing the value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<T> AsСonst<T>(this T it) => new(it);

        /// <summary>
        /// Subscribes to a reactive value and immediately invokes the callback with the current value.
        /// </summary>
        /// <typeparam name="T">The type of the reactive value.</typeparam>
        /// <param name="it">The reactive value to observe.</param>
        /// <param name="action">The callback to invoke on value changes and immediately with the current value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T> Observe<T>(this IReactiveValue<T> it, Action<T> action)
        {
            action.Invoke(it.Value);
            return new Subscription<T>(it, action);
        }
    }
}
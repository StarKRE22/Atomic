using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods to simplify subscribing and unsubscribing <see cref="IAction"/> instances
    /// to and from <see cref="IReactive"/> sources.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Subscribes an <see cref="IAction"/> to a non-generic <see cref="IReactive"/>.
        /// </summary>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription"/> instance representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription Subscribe(this IReactive it, IAction action) => it.Subscribe(action.Invoke);

        /// <summary>
        /// Unsubscribes an <see cref="IAction"/> from a non-generic <see cref="IReactive"/>.
        /// </summary>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe(this IReactive it, IAction action) => it.Unsubscribe(action.Invoke);

        /// <summary>
        /// Subscribes an <see cref="IAction{T}"/> to a generic <see cref="IReactive{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription{T}"/> representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T> Subscribe<T>(this IReactive<T> it, IAction<T> action) =>
            it.Subscribe(action.Invoke);

        /// <summary>
        /// Unsubscribes an <see cref="IAction{T}"/> from a generic <see cref="IReactive{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T>(this IReactive<T> it, IAction<T> action) => it.Unsubscribe(action.Invoke);

        /// <summary>
        /// Subscribes an <see cref="IAction{T1,T2}"/> to a two-parameter <see cref="IReactive{T1,T2}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription{T1,T2}"/> representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T1, T2> Subscribe<T1, T2>(this IReactive<T1, T2> it, IAction<T1, T2> action) =>
            it.Subscribe(action.Invoke);

        /// <summary>
        /// Unsubscribes an <see cref="IAction{T1,T2}"/> from a two-parameter <see cref="IReactive{T1,T2}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2>(this IReactive<T1, T2> it, IAction<T1, T2> action) =>
            it.Unsubscribe(action.Invoke);

        /// <summary>
        /// Subscribes an <see cref="IAction{T1,T2,T3}"/> to a three-parameter <see cref="IReactive{T1,T2,T3}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <typeparam name="T3">The type of the third emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription{T1,T2,T3}"/> representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(this IReactive<T1, T2, T3> it,
            IAction<T1, T2, T3> action) => it.Subscribe(action.Invoke);

        /// <summary>
        /// Unsubscribes an <see cref="IAction{T1,T2,T3}"/> from a three-parameter <see cref="IReactive{T1,T2,T3}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <typeparam name="T3">The type of the third emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2, T3>(this IReactive<T1, T2, T3> it, IAction<T1, T2, T3> action) =>
            it.Unsubscribe(action.Invoke);

        /// <summary>
        /// Subscribes a range of <see cref="IAction{T}"/> actions to a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        /// <typeparam name="T">The type of the emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="actions">The collection of actions to subscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubscribeRange<T>(this IReactive<T> it, IEnumerable<IAction<T>> actions)
        {
            if (actions != null)
                foreach (IAction<T> action in actions)
                    if (action != null)
                        it.Subscribe(action.Invoke);
        }
    }
}
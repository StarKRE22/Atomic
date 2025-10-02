using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods to simplify subscribing and unsubscribing <see cref="IAction"/> instances
    /// to and from <see cref="ISignal"/> sources.
    /// </summary>
    public static partial class Extensions
    {
        #region Subscribe

        /// <summary>
        /// Subscribes an action to be invoked when the signal is triggered.
        /// </summary>
        /// <param name="action">The action to invoke when the signal is triggered.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription Subscribe(this ISignal it, Action action) => new(it, action);


        /// <summary>
        /// Subscribes an action to be invoked when the reactive source emits a value.
        /// </summary>
        /// <param name="action">The action to invoke with the emitted value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T> Subscribe<T>(this ISignal<T> it, Action<T> action) => new(it, action);

        /// <summary>
        /// Subscribes an action to be invoked when the reactive source emits four values.
        /// </summary>
        /// <param name="action">The action to invoke with the emitted values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T1, T2, T3, T4> Subscribe<T1, T2, T3, T4>(
            this ISignal<T1, T2, T3, T4> it,
            Action<T1, T2, T3, T4> action
        ) => new(it, action);


        /// <summary>
        /// Subscribes an action to be invoked when the reactive source emits two values.
        /// </summary>
        /// <param name="action">The action to invoke with the emitted values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T1, T2> Subscribe<T1, T2>(this ISignal<T1, T2> it, Action<T1, T2> action)
            => new(it, action);


        /// <summary>
        /// Subscribes an action to be invoked when the reactive source emits three values.
        /// </summary>
        /// <param name="action">The action to invoke with the emitted values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(
            this ISignal<T1, T2, T3> it,
            Action<T1, T2, T3> action
        ) => new(it, action);

        /// <summary>
        /// Subscribes an <see cref="IAction"/> to a non-generic <see cref="ISignal"/>.
        /// </summary>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription"/> instance representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription Subscribe(this ISignal it, IAction action) => new(it, action.Invoke);

        /// <summary>
        /// Subscribes an <see cref="IAction{T}"/> to a generic <see cref="ISignal{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription{T}"/> representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T> Subscribe<T>(this ISignal<T> it, IAction<T> action) => new(it, action.Invoke);

        /// <summary>
        /// Subscribes an <see cref="IAction{T1,T2}"/> to a two-parameter <see cref="ISignal{T1,T2}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription{T1,T2}"/> representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T1, T2> Subscribe<T1, T2>(this ISignal<T1, T2> it, IAction<T1, T2> action) =>
            new(it, action.Invoke);

        /// <summary>
        /// Subscribes an <see cref="IAction{T1,T2,T3}"/> to a three-parameter <see cref="ISignal{T1,T2,T3}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <typeparam name="T3">The type of the third emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription{T1,T2,T3}"/> representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(
            this ISignal<T1, T2, T3> it,
            IAction<T1, T2, T3> action
        ) => new(it, action.Invoke);

        /// <summary>
        /// Subscribes an <see cref="IAction{T1,T2,T3,T4}"/> to a four-parameter <see cref="ISignal{T1,T2,T3,T4}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <typeparam name="T3">The type of the third emitted value.</typeparam>
        /// <typeparam name="T4">The type of the fourth emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to subscribe.</param>
        /// <returns>A <see cref="Subscription{T1,T2,T3,T4}"/> representing the subscription.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subscription<T1, T2, T3, T4> Subscribe<T1, T2, T3, T4>(
            this ISignal<T1, T2, T3, T4> it,
            IAction<T1, T2, T3, T4> action
        ) => new(it, action.Invoke);

        #endregion

        #region Unsubscribe

        /// <summary>
        /// Unsubscribes a previously registered action from the signal.
        /// </summary>
        /// <param name="action">The action to remove from the list of subscribers.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe(this ISignal it, Action action) => it.OnEvent -= action;

        /// <summary>
        /// Unsubscribes a previously registered action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove from the subscription list.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T>(this ISignal<T> it, Action<T> action) => it.OnEvent -= action;

        /// <summary>
        /// Unsubscribes a previously registered action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove from the subscription list.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2>(this ISignal<T1, T2> it, Action<T1, T2> action) => it.OnEvent -= action;

        /// <summary>
        /// Unsubscribes a previously registered action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove from the subscription list.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2, T3>(this ISignal<T1, T2, T3> it, Action<T1, T2, T3> action) =>
            it.OnEvent -= action;

        /// <summary>
        /// Unsubscribes a previously registered action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove from the subscription list.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2, T3, T4>(
            this ISignal<T1, T2, T3, T4> it,
            Action<T1, T2, T3, T4> action
        ) => it.OnEvent -= action;

        /// <summary>
        /// Unsubscribes an <see cref="IAction"/> from a non-generic <see cref="ISignal"/>.
        /// </summary>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe(this ISignal it, IAction action) => it.OnEvent -= action.Invoke;

        /// <summary>
        /// Unsubscribes an <see cref="IAction{T}"/> from a generic <see cref="ISignal{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T>(this ISignal<T> it, IAction<T> action) => it.OnEvent -= action.Invoke;

        /// <summary>
        /// Unsubscribes an <see cref="IAction{T1,T2}"/> from a two-parameter <see cref="ISignal{T1,T2}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2>(this ISignal<T1, T2> it, IAction<T1, T2> action) =>
            it.OnEvent -= action.Invoke;

        /// <summary>
        /// Unsubscribes an <see cref="IAction{T1,T2,T3}"/> from a three-parameter <see cref="ISignal{T1,T2,T3}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <typeparam name="T3">The type of the third emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2, T3>(this ISignal<T1, T2, T3> it, IAction<T1, T2, T3> action) =>
            it.OnEvent -= action.Invoke;

        /// <summary>
        /// Unsubscribes an <see cref="IAction{T1,T2,T3,T4}"/> from a four-parameter <see cref="ISignal{T1,T2,T3,T4}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first emitted value.</typeparam>
        /// <typeparam name="T2">The type of the second emitted value.</typeparam>
        /// <typeparam name="T3">The type of the third emitted value.</typeparam>
        /// <typeparam name="T4">The type of the fourth emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="action">The action to unsubscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2, T3, T4>(
            this ISignal<T1, T2, T3, T4> it,
            IAction<T1, T2, T3, T4> action
        ) => it.OnEvent -= action.Invoke;

        #endregion

        #region SubscribeRange

        /// <summary>
        /// Subscribes a range of <see cref="IAction"/> actions to a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        /// <param name="it">The reactive source.</param>
        /// <param name="actions">The collection of actions to subscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubscribeRange(this ISignal it, IEnumerable<IAction> actions)
        {
            if (actions != null)
                foreach (IAction action in actions)
                    if (action != null)
                        it.OnEvent += action.Invoke;
        }

        /// <summary>
        /// Subscribes a range of <see cref="IAction{T}"/> actions to a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        /// <typeparam name="T">The type of the emitted value.</typeparam>
        /// <param name="it">The reactive source.</param>
        /// <param name="actions">The collection of actions to subscribe.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubscribeRange<T>(this ISignal<T> it, IEnumerable<IAction<T>> actions)
        {
            if (actions != null)
                foreach (IAction<T> action in actions)
                    if (action != null)
                        it.OnEvent += action.Invoke;
        }

        /// <summary>
        /// Subscribes a range of <see cref="IAction{T1,T2}"/> actions to a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubscribeRange<T1, T2>(this ISignal<T1, T2> it, IEnumerable<IAction<T1, T2>> actions)
        {
            if (actions != null)
                foreach (var action in actions)
                    if (action != null)
                        it.OnEvent += action.Invoke;
        }

        /// <summary>
        /// Subscribes a range of <see cref="IAction{T1,T2,T3}"/> actions to a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubscribeRange<T1, T2, T3>(this ISignal<T1, T2, T3> it,
            IEnumerable<IAction<T1, T2, T3>> actions)
        {
            if (actions != null)
                foreach (var action in actions)
                    if (action != null)
                        it.OnEvent += action.Invoke;
        }

        /// <summary>
        /// Subscribes a range of <see cref="IAction{T1,T2,T3,T4}"/> actions to a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubscribeRange<T1, T2, T3, T4>(this ISignal<T1, T2, T3, T4> it,
            IEnumerable<IAction<T1, T2, T3, T4>> actions)
        {
            if (actions != null)
                foreach (var action in actions)
                    if (action != null)
                        it.OnEvent += action.Invoke;
        }

        #endregion

        #region UnsubscribeRange

        /// <summary>
        /// Unsubscribes a range of <see cref="IAction"/> actions from a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        public static void UnsubscribeRange(this ISignal it, IEnumerable<IAction> actions)
        {
            if (actions != null)
                foreach (IAction action in actions)
                    if (action != null)
                        it.OnEvent -= action.Invoke;
        }

        /// <summary>
        /// Unsubscribes a range of <see cref="IAction{T}"/> actions from a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        public static void UnsubscribeRange<T>(this ISignal<T> it, IEnumerable<IAction<T>> actions)
        {
            if (actions != null)
                foreach (IAction<T> action in actions)
                    if (action != null)
                        it.OnEvent -= action.Invoke;
        }

        /// <summary>
        /// Unsubscribes a range of <see cref="IAction{T1,T2}"/> actions from a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        public static void UnsubscribeRange<T1, T2>(this ISignal<T1, T2> it, IEnumerable<IAction<T1, T2>> actions)
        {
            if (actions != null)
                foreach (IAction<T1, T2> action in actions)
                    if (action != null)
                        it.OnEvent -= action.Invoke;
        }

        /// <summary>
        /// Unsubscribes a range of <see cref="IAction{T1,T2,T3}"/> actions from a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        public static void UnsubscribeRange<T1, T2, T3>(this ISignal<T1, T2, T3> it,
            IEnumerable<IAction<T1, T2, T3>> actions)
        {
            if (actions != null)
                foreach (IAction<T1, T2, T3> action in actions)
                    if (action != null)
                        it.OnEvent -= action.Invoke;
        }

        /// <summary>
        /// Unsubscribes a range of <see cref="IAction{T1,T2,T3,T4}"/> actions from a reactive source.
        /// Null actions in the collection are ignored.
        /// </summary>
        public static void UnsubscribeRange<T1, T2, T3, T4>(this ISignal<T1, T2, T3, T4> it,
            IEnumerable<IAction<T1, T2, T3, T4>> actions)
        {
            if (actions != null)
                foreach (IAction<T1, T2, T3, T4> action in actions)
                    if (action != null)
                        it.OnEvent -= action.Invoke;
        }

        #endregion
    }
}
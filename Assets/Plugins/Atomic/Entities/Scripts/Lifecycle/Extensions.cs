using System;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides extension methods for event-based subscriptions to lifecycle interfaces.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Subscribes to the <see cref="IInitLifecycle.OnInitialized"/> event.
        /// </summary>
        /// <param name="source">The spawnable object.</param>
        /// <param name="action">The action to invoke on spawn.</param>
        /// <returns>A disposable <see cref="InitSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InitSubscription WhenInit(this IInitLifecycle source, Action action) =>
            new(source, action);

        /// <summary>
        /// Subscribes to the <see cref="IInitLifecycle.OnDisposed"/> event.
        /// </summary>
        /// <param name="source">The spawnable object.</param>
        /// <param name="action">The action to invoke on despawn.</param>
        /// <returns>A disposable <see cref="DisposeSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisposeSubscription WhenDispose(this IInitLifecycle source, Action action) =>
            new(source, action);

        /// <summary>
        /// Subscribes to the <see cref="IEnableLifecycle.OnEnabled"/> event.
        /// </summary>
        /// <param name="source">The activatable object.</param>
        /// <param name="action">The action to invoke when enabled.</param>
        /// <returns>A disposable <see cref="EnableSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnableSubscription WhenEnable(this IEnableLifecycle source, Action action) =>
            new(source, action);

        /// <summary>
        /// Subscribes to the <see cref="IEnableLifecycle.OnDisables"/> event.
        /// </summary>
        /// <param name="source">The activatable object.</param>
        /// <param name="action">The action to invoke when disabled.</param>
        /// <returns>A disposable <see cref="DisableSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisableSubscription WhenDisable(this IEnableLifecycle source, Action action) =>
            new(source, action);

        /// <summary>
        /// Subscribes to the <see cref="ITickLifecycle.OnTicked"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each frame update.</param>
        /// <returns>A disposable <see cref="TickSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TickSubscription WhenTick(this ITickLifecycle source, Action<float> action) =>
            new(source, action);

        /// <summary>
        /// Subscribes to the <see cref="ITickLifecycle.OnFixedTicked"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each fixed update frame.</param>
        /// <returns>A disposable <see cref="FixedTickSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedTickSubscription WhenFixedTick(this ITickLifecycle source, Action<float> action) =>
            new(source, action);

        /// <summary>
        /// Subscribes to the <see cref="ITickLifecycle.OnLateTicked"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each late update frame.</param>
        /// <returns>A disposable <see cref="LateTickSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LateTickSubscription WhenLateTick(this ITickLifecycle source, Action<float> action) =>
            new(source, action);
    }
}
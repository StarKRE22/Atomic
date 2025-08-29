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
        /// Subscribes to the <see cref="IInitSource.OnInitialized"/> event.
        /// </summary>
        /// <param name="source">The spawnable object.</param>
        /// <param name="action">The action to invoke on spawn.</param>
        /// <returns>A disposable <see cref="InitSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InitSubscription WhenInit(this IInitSource source, Action action)
        {
            source.OnInitialized += action;
            return new InitSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IInitSource.OnDisposed"/> event.
        /// </summary>
        /// <param name="source">The spawnable object.</param>
        /// <param name="action">The action to invoke on despawn.</param>
        /// <returns>A disposable <see cref="DisposeSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisposeSubscription WhenDispose(this IInitSource source, Action action)
        {
            source.OnDisposed += action;
            return new DisposeSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IIActivatable.OnActive/> event.
        /// </summary>
        /// <param name="source">The activatable object.</param>
        /// <param name="action">The action to invoke when enabled.</param>
        /// <returns>A disposable <see cref="EnableSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnableSubscription WhenEnable(this IEnableSource source, Action action)
        {
            source.OnEnabled += action;
            return new EnableSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IIActivatable.OnInactive/> event.
        /// </summary>
        /// <param name="source">The activatable object.</param>
        /// <param name="action">The action to invoke when disabled.</param>
        /// <returns>A disposable <see cref="DisableSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisableSubscription WhenDisable(this IEnableSource source, Action action)
        {
            source.OnDisabled += action;
            return new DisableSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IUpdateSource.OnUpdated"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each frame update.</param>
        /// <returns>A disposable <see cref="UpdateSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UpdateSubscription WhenUpdate(this IUpdateSource source, Action<float> action)
        {
            source.OnUpdated += action;
            return new UpdateSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IUpdateSource.OnFixedUpdated"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each fixed update frame.</param>
        /// <returns>A disposable <see cref="FixedUpdateSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedUpdateSubscription WhenFixedUpdate(this IUpdateSource source, Action<float> action)
        {
            source.OnFixedUpdated += action;
            return new FixedUpdateSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IUpdateSource.OnLateUpdated"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each late update frame.</param>
        /// <returns>A disposable <see cref="LateUpdateSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LateUpdateSubscription WhenLateUpdate(this IUpdateSource source, Action<float> action)
        {
            source.OnLateUpdated += action;
            return new LateUpdateSubscription(source, action);
        }
    }
}
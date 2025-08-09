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
        /// Subscribes to the <see cref="ISpawnable.OnSpawned"/> event.
        /// </summary>
        /// <param name="source">The spawnable object.</param>
        /// <param name="action">The action to invoke on spawn.</param>
        /// <returns>A disposable <see cref="SpawnSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpawnSubscription WhenSpawn(this ISpawnable source, Action action)
        {
            source.OnSpawned += action;
            return new SpawnSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="ISpawnable.OnDespawned"/> event.
        /// </summary>
        /// <param name="source">The spawnable object.</param>
        /// <param name="action">The action to invoke on despawn.</param>
        /// <returns>A disposable <see cref="DespawnSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DespawnSubscription WhenDespawn(this ISpawnable source, Action action)
        {
            source.OnDespawned += action;
            return new DespawnSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IIActivatable.OnActive/> event.
        /// </summary>
        /// <param name="source">The activatable object.</param>
        /// <param name="action">The action to invoke when enabled.</param>
        /// <returns>A disposable <see cref="ActivateSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActivateSubscription WhenActivate(this IActivatable source, Action action)
        {
            source.OnActivated += action;
            return new ActivateSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IIActivatable.OnInactive/> event.
        /// </summary>
        /// <param name="source">The activatable object.</param>
        /// <param name="action">The action to invoke when disabled.</param>
        /// <returns>A disposable <see cref="DeactivateSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DeactivateSubscription WhenDeactivate(this IActivatable source, Action action)
        {
            source.OnDeactivated += action;
            return new DeactivateSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IUpdatable.OnUpdated"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each frame update.</param>
        /// <returns>A disposable <see cref="UpdateSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UpdateSubscription WhenUpdate(this IUpdatable source, Action<float> action)
        {
            source.OnUpdated += action;
            return new UpdateSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IUpdatable.OnFixedUpdated"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each fixed update frame.</param>
        /// <returns>A disposable <see cref="FixedUpdateSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedUpdateSubscription WhenFixedUpdate(this IUpdatable source, Action<float> action)
        {
            source.OnFixedUpdated += action;
            return new FixedUpdateSubscription(source, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IUpdatable.OnLateUpdated"/> event.
        /// </summary>
        /// <param name="source">The updatable object.</param>
        /// <param name="action">The action to invoke each late update frame.</param>
        /// <returns>A disposable <see cref="LateUpdateSubscription"/> that unsubscribes when disposed.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LateUpdateSubscription WhenLateUpdate(this IUpdatable source, Action<float> action)
        {
            source.OnLateUpdated += action;
            return new LateUpdateSubscription(source, action);
        }
    }
}
using System;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnInitialized"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is initialized.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InitSubscription<E> WhenInit<E>(this IEntity<E> entity, Action action) 
            where E : IEntity<E>
        {
            entity.OnInitialized += action;
            return new InitSubscription<E>(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnEnabled"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is enabled.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnableSubscription<E> WhenEnable<E>(this IEntity<E> entity, Action action) 
            where E : IEntity<E>
        {
            entity.OnEnabled += action;
            return new EnableSubscription<E>(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnDisabled"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is disabled.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisableSubscription<E> WhenDisable<E>(this IEntity<E> entity, Action action)
            where E : IEntity<E>
        {
            entity.OnDisabled += action;
            return new DisableSubscription<E>(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnDisposed"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is disposed.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisposeSubscription<E> WhenDispose<E>(this IEntity<E> entity, Action action)
            where E : IEntity<E>
        {
            entity.OnDisposed += action;
            return new DisposeSubscription<E>(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the Update cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UpdateSubscription<E> WhenUpdate<E>(this IEntity<E> entity, Action<float> action)
            where E : IEntity<E>
        {
            entity.OnUpdated += action;
            return new UpdateSubscription<E>(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnFixedUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the FixedUpdate cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedUpdateSubscription<E> WhenFixedUpdate<E>(this IEntity<E> entity, Action<float> action)
            where E : IEntity<E>
        {
            entity.OnFixedUpdated += action;
            return new FixedUpdateSubscription<E>(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnLateUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the LateUpdate cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LateUpdateSubscription<E> WhenLateUpdate<E>(this IEntity<E> entity, Action<float> action)
            where E : IEntity<E>
        {
            entity.OnLateUpdated += action;
            return new LateUpdateSubscription<E>(entity, action);
        }
    }
}
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
        public static InitSubscription WhenInit(this IEntity entity, Action action)
        {
            entity.OnInitialized += action;
            return new InitSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnEnabled"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is enabled.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnableSubscription WhenEnable(this IEntity entity, Action action)
        {
            entity.OnEnabled += action;
            return new EnableSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnDisabled"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is disabled.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisableSubscription WhenDisable(this IEntity entity, Action action)
        {
            entity.OnDisabled += action;
            return new DisableSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnDisposed"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is disposed.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisposeSubscription WhenDispose(this IEntity entity, Action action)
        {
            entity.OnDisposed += action;
            return new DisposeSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the Update cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UpdateSubscription WhenUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnUpdated += action;
            return new UpdateSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnFixedUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the FixedUpdate cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedUpdateSubscription WhenFixedUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnFixedUpdated += action;
            return new FixedUpdateSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnLateUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the LateUpdate cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        /// 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LateUpdateSubscription WhenLateUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnLateUpdated += action;
            return new LateUpdateSubscription(entity, action);
        }
    }
}
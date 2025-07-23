using System;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnSpawned"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is initialized.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntitySpawnSubscription WhenSpawn(this IEntity entity, Action action)
        {
            entity.OnSpawned += action;
            return new EntitySpawnSubscription(entity, action);
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
        public static EntityDisableSubscription WhenDisable(this IEntity entity, Action action)
        {
            entity.OnDisabled += action;
            return new EntityDisableSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnDespawned"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke when the entity is disposed.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DespawnSubscription WhenDespawn(this IEntity entity, Action action)
        {
            entity.OnDespawned += action;
            return new DespawnSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the Update cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntityUpdateSubscription WhenUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnUpdated += action;
            return new EntityUpdateSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnFixedUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the FixedUpdate cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntityFixedUpdateSubscription WhenFixedUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnFixedUpdated += action;
            return new EntityFixedUpdateSubscription(entity, action);
        }

        /// <summary>
        /// Subscribes to the <see cref="IEntity.OnLateUpdated"/> event and returns a disposable subscription.
        /// </summary>
        /// <param name="entity">The entity to observe.</param>
        /// <param name="action">The callback to invoke during the LateUpdate cycle.</param>
        /// <returns>A disposable subscription object.</returns>
        /// 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntityLateUpdateSubscription WhenLateUpdate(this IEntity entity, Action<float> action)
        {
            entity.OnLateUpdated += action;
            return new EntityLateUpdateSubscription(entity, action);
        }
    }
}
using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Subscription for the <see cref="IEntity.OnInitialized"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct EntityInitSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action _callback;

        /// <summary>
        /// Creates a new subscription for the <see cref="IEntity.OnInitialized"/> event.
        /// </summary>
        internal EntityInitSubscription(IEntity entity, Action callback)
        {
            _entity = entity;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="IEntity.OnInitialized"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_entity != null && _callback != null)
                _entity.OnInitialized -= _callback;
        }
    }

    /// <summary>
    /// Subscription for the <see cref="IEntity.OnEnabled"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct EntityEnableSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action _callback;

        internal EntityEnableSubscription(IEntity entity, Action callback)
        {
            _entity = entity;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="IEntity.OnEnabled"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_entity != null && _callback != null)
                _entity.OnEnabled -= _callback;
        }
    }

    /// <summary>
    /// Subscription for the <see cref="IEntity.OnDisabled"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct EntityDisableSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action _callback;

        internal EntityDisableSubscription(IEntity entity, Action callback)
        {
            _entity = entity;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="IEntity.OnDisabled"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_entity != null && _callback != null)
                _entity.OnDisabled -= _callback;
        }
    }

    /// <summary>
    /// Subscription for the <see cref="IEntity.OnDisposed"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct EntityDisposeSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action _callback;

        internal EntityDisposeSubscription(IEntity entity, Action callback)
        {
            _entity = entity;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="IEntity.OnDisposed"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_entity != null && _callback != null)
                _entity.OnDisposed -= _callback;
        }
    }

    /// <summary>
    /// Subscription for the <see cref="IEntity.OnUpdated"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct EntityUpdateSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action<float> _callback;

        internal EntityUpdateSubscription(IEntity entity, Action<float> callback)
        {
            _entity = entity;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="IEntity.OnUpdated"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_entity != null && _callback != null)
                _entity.OnUpdated -= _callback;
        }
    }

    /// <summary>
    /// Subscription for the <see cref="IEntity.OnFixedUpdated"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct EntityFixedUpdateSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action<float> _callback;

        internal EntityFixedUpdateSubscription(IEntity entity, Action<float> callback)
        {
            _entity = entity;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="IEntity.OnFixedUpdated"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_entity != null && _callback != null)
                _entity.OnFixedUpdated -= _callback;
        }
    }

    /// <summary>
    /// Subscription for the <see cref="IEntity.OnLateUpdated"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct EntityLateUpdateSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action<float> _callback;

        internal EntityLateUpdateSubscription(IEntity entity, Action<float> callback)
        {
            _entity = entity;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="IEntity.OnLateUpdated"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_entity != null && _callback != null)
                _entity.OnLateUpdated -= _callback;
        }
    }
}
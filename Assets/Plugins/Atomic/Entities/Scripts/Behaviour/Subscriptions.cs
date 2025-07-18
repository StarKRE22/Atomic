using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Subscription for the <see cref="IEntity.OnInitialized"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct InitSubscription<E> : IDisposable where E : IEntity<E>
    {
        private readonly IEntity<E> _entity;
        private readonly Action _callback;

        /// <summary>
        /// Creates a new subscription for the <see cref="IEntity.OnInitialized"/> event.
        /// </summary>
        internal InitSubscription(IEntity<E> entity, Action callback)
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
    public readonly struct EnableSubscription<E> : IDisposable where E : IEntity<E>
    {
        private readonly IEntity<E> _entity;
        private readonly Action _callback;

        internal EnableSubscription(IEntity<E> entity, Action callback)
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
    public readonly struct DisableSubscription<E> : IDisposable where E : IEntity<E>
    {
        private readonly IEntity<E> _entity;
        private readonly Action _callback;

        internal DisableSubscription(IEntity<E> entity, Action callback)
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
    public readonly struct DisposeSubscription<E> : IDisposable where E : IEntity<E>
    {
        private readonly IEntity<E> _entity;
        private readonly Action _callback;

        internal DisposeSubscription(IEntity<E> entity, Action callback)
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
    public readonly struct UpdateSubscription<E> : IDisposable where E : IEntity<E>
    {
        private readonly IEntity<E> _entity;
        private readonly Action<float> _callback;

        internal UpdateSubscription(IEntity<E> entity, Action<float> callback)
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
    public readonly struct FixedUpdateSubscription<E> : IDisposable where E : IEntity<E>
    {
        private readonly IEntity<E> _entity;
        private readonly Action<float> _callback;

        internal FixedUpdateSubscription(IEntity<E> entity, Action<float> callback)
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
    public readonly struct LateUpdateSubscription<E> : IDisposable where E : IEntity<E>
    {
        private readonly IEntity<E> _entity;
        private readonly Action<float> _callback;

        internal LateUpdateSubscription(IEntity<E> entity, Action<float> callback)
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
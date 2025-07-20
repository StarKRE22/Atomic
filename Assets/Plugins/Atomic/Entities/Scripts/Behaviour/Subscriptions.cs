using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Subscription for the <see cref="IEntity.OnInitialized"/> event.
    /// Automatically unsubscribes when disposed.
    /// </summary>
    public readonly struct InitSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action _callback;

        /// <summary>
        /// Creates a new subscription for the <see cref="IEntity.OnInitialized"/> event.
        /// </summary>
        internal InitSubscription(IEntity entity, Action callback)
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
    public readonly struct EnableSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action _callback;

        internal EnableSubscription(IEntity entity, Action callback)
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
    public readonly struct DisableSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action _callback;

        internal DisableSubscription(IEntity entity, Action callback)
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
    public readonly struct DisposeSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action _callback;

        internal DisposeSubscription(IEntity entity, Action callback)
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
    public readonly struct UpdateSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action<float> _callback;

        internal UpdateSubscription(IEntity entity, Action<float> callback)
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
    public readonly struct FixedUpdateSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action<float> _callback;

        internal FixedUpdateSubscription(IEntity entity, Action<float> callback)
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
    public readonly struct LateUpdateSubscription : IDisposable
    {
        private readonly IEntity _entity;
        private readonly Action<float> _callback;

        internal LateUpdateSubscription(IEntity entity, Action<float> callback)
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
using System;
using System.Collections.Generic;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    public partial class Entity<E>
    {
        /// <summary>
        /// Equality comparer for IUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IUpdate> s_updateComparer =
            EqualityComparer<IUpdate>.Default;

        /// <summary>
        /// Equality comparer for IFixedUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IFixedUpdate> s_fixedUpdateComparer =
            EqualityComparer<IFixedUpdate>.Default;
        
        /// <summary>
        /// Equality comparer for ILateUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<ILateUpdate> s_lateUpdateComparer =
            EqualityComparer<ILateUpdate>.Default;

        /// <summary>
        /// Called when the entity has been initialized.
        /// </summary>
        public event Action OnInitialized;
        
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        public event Action OnEnabled;
     
        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        public event Action OnDisabled;
        
        /// <summary>
        /// Called when the entity is disposed.
        /// </summary>
        public event Action OnDisposed;

        /// <summary>
        /// Called every frame while the entity is enabled.
        /// </summary>
        public event Action<float> OnUpdated;
        
        /// <summary>
        /// Called every fixed frame while the entity is enabled.
        /// </summary>
        public event Action<float> OnFixedUpdated;
        
        /// <summary>
        /// Called every late frame while the entity is enabled.
        /// </summary>
        public event Action<float> OnLateUpdated;

        /// <summary>
        /// Indicates whether the entity has been initialized.
        /// </summary>
        public bool Initialized => this.initialized;
        
        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
        public bool Enabled => this.enabled;

        private bool initialized;
        private bool enabled;

        private IUpdate[] updates;
        private IFixedUpdate[] fixedUpdates;
        private ILateUpdate[] lateUpdates;

        private int updateCount;
        private int fixedUpdateCount;
        private int lateUpdateCount;

        /// <summary>
        /// Initializes the entity and all IInit behaviours.
        /// </summary>
        public void Init()
        {
            if (this.initialized)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IInit initBehaviour)
                    initBehaviour.Init(this.owner);

            this.initialized = true;
            this.OnInitialized?.Invoke();
        }

        /// <summary>
        /// Disposes the entity and all IDispose behaviours.
        /// </summary>
        public void Dispose()
        {
            if (!this.initialized)
                return;

            if (this.enabled)
                this.Disable();

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IDispose disposeBehaviour)
                    disposeBehaviour.Dispose(this.owner);

            this.initialized = false;
            this.OnDisposed?.Invoke();
        }

        /// <summary>
        /// Enables the entity and registers update behaviours.
        /// </summary>
        public void Enable()
        {
            if (!this.initialized)
                this.Init();

            if (this.enabled)
                return;

            this.enabled = true;

            for (int i = 0; i < _behaviourCount; i++)
                this.EnableBehaviour(in _behaviours[i]);

            this.OnEnabled?.Invoke();
        }

        /// <summary>
        /// Disables the entity and unregisters update behaviours.
        /// </summary>
        public void Disable()
        {
            if (!this.enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                this.DisableBehaviour(in _behaviours[i]);

            this.enabled = false;
            this.OnDisabled?.Invoke();
        }

        /// <summary>
        /// Invokes OnUpdate for all registered IUpdate behaviours.
        /// </summary>
        public void OnUpdate(float deltaTime)
        {
            if (!this.enabled)
                return;

            for (int i = 0; i < this.updateCount && this.enabled; i++)
                this.updates[i].OnUpdate(this.owner, deltaTime);

            this.OnUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnFixedUpdate for all registered IFixedUpdate behaviours.
        /// </summary>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!this.enabled)
                return;

            for (int i = 0; i < this.fixedUpdateCount && this.enabled; i++)
                this.fixedUpdates[i].OnFixedUpdate(this.owner, deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnLateUpdate for all registered ILateUpdate behaviours.
        /// </summary>
        public void OnLateUpdate(float deltaTime)
        {
            if (!this.enabled)
                return;

            for (int i = 0; i < this.lateUpdateCount && this.enabled; i++)
                this.lateUpdates[i].OnLateUpdate(this.owner, deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        private void EnableBehaviour(in IBehaviour<E> behaviour)
        {
            if (behaviour is IEnable entityEnable)
                entityEnable.Enable(this.owner);

            if (behaviour is IUpdate update)
                Add(ref this.updates, ref this.updateCount, in update);

            if (behaviour is IFixedUpdate fixedUpdate)
                Add(ref this.fixedUpdates, ref this.fixedUpdateCount, fixedUpdate);

            if (behaviour is ILateUpdate lateUpdate)
                Add(ref this.lateUpdates, ref this.lateUpdateCount, lateUpdate);
        }

        private void DisableBehaviour(in IBehaviour<E> behaviour)
        {
            if (behaviour is IDisable entityDisable)
                entityDisable.Disable(this.owner);

            if (behaviour is IUpdate update)
                Remove(ref this.updates, ref this.updateCount, update, s_updateComparer);

            if (behaviour is IFixedUpdate fixedUpdate)
                Remove(ref this.fixedUpdates, ref this.fixedUpdateCount, fixedUpdate, s_fixedUpdateComparer);

            if (behaviour is ILateUpdate lateUpdate)
                Remove(ref this.lateUpdates, ref this.lateUpdateCount, lateUpdate, s_lateUpdateComparer);
        }
    }
}
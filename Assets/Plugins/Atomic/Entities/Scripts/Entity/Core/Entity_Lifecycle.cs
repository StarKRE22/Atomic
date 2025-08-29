using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Entities.EntityUtils;

namespace Atomic.Entities
{
    public partial class Entity
    {
        /// <summary>
        /// Equality comparer for IUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityUpdate> s_updateComparer =
            EqualityComparer<IEntityUpdate>.Default;

        /// <summary>
        /// Equality comparer for IFixedUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityFixedUpdate> s_fixedUpdateComparer =
            EqualityComparer<IEntityFixedUpdate>.Default;

        /// <summary>
        /// Equality comparer for ILateUpdate behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityLateUpdate> s_lateUpdateComparer =
            EqualityComparer<IEntityLateUpdate>.Default;
        
        /// Called when the entity is initialized.
        /// </summary>
        public event Action OnInitialized;
       
        /// Called when the entity is disposed.
        /// </summary>
        public event Action OnDisposed;
        
        /// <summary>
        /// Called when the entity is enabled.
        /// </summary>
        public event Action OnEnabled;

        /// <summary>
        /// Called when the entity is disabled.
        /// </summary>
        public event Action OnDisabled;

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
        /// Indicates whether the entity has been Initialized.
        /// </summary>
        public bool Initialized => _initialized;

        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
        public bool Enabled => _enabled;

        private bool _initialized;
        private bool _enabled;

        private IEntityUpdate[] _updates;
        private IEntityFixedUpdate[] _fixedUpdates;
        private IEntityLateUpdate[] _lateUpdates;

        private int _updateCount;
        private int _fixedUpdateCount;
        private int _lateUpdateCount;

        /// <summary>
        /// Initializes the entity.
        /// </summary>
        public void Init()
        {
            if (_initialized)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityInit behaviour)
                    behaviour.Init(this);
            
            _initialized = true;
            
            this.OnStateChanged?.Invoke();
            this.OnInitialized?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Deinitialize()
        {
            if (!_initialized) 
                return;
            
            if (_enabled)
                this.Disable();

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityDispose behaviour)
                    behaviour.Dispose(this);
            
            _initialized = false;
        }
        
        /// <summary>
        /// Enables the entity and registers update behaviours.
        /// </summary>
        public void Enable()
        {
            if (_enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                this.EnableBehaviour(_behaviours[i]);
            
            _enabled = true;

            this.OnEnabled?.Invoke();
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Disables the entity and unregisters update behaviours.
        /// </summary>
        public void Disable()
        {
            if (!_enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                this.DisableBehaviour(_behaviours[i]);
            
            _enabled = false;

            this.OnStateChanged?.Invoke();
            this.OnDisabled?.Invoke();
        }

        /// <summary>
        /// Invokes OnUpdate for all registered IUpdate behaviours.
        /// </summary>
        public void OnUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < _updateCount; i++)
                _updates[i].Update(this, deltaTime);
            
            this.OnUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnFixedUpdate for all registered IFixedUpdate behaviours.
        /// </summary>
        public void OnFixedUpdate(float deltaTime)
        {
            if (!_enabled)
                return;
            
            for (int i = 0; i < _fixedUpdateCount; i++)
                _fixedUpdates[i].FixedUpdate(this, deltaTime);
            
            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        /// <summary>
        /// Invokes OnLateUpdate for all registered ILateUpdate behaviours.
        /// </summary>
        public void OnLateUpdate(float deltaTime)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < _lateUpdateCount && _enabled; i++)
                _lateUpdates[i].LateUpdate(this, deltaTime);
            
            this.OnLateUpdated?.Invoke(deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityEnable enable)
                enable.Enable(this);

            if (behaviour is IEntityUpdate update)
                Add(ref _updates, ref _updateCount, update);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Add(ref _fixedUpdates, ref _fixedUpdateCount, fixedUpdate);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Add(ref _lateUpdates, ref _lateUpdateCount, lateUpdate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityDisable disable)
                disable.Disable(this);

            if (behaviour is IEntityUpdate update)
                Remove(ref _updates, ref _updateCount, update, s_updateComparer);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Remove(ref _fixedUpdates, ref _fixedUpdateCount, fixedUpdate, s_fixedUpdateComparer);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Remove(ref _lateUpdates, ref _lateUpdateCount, lateUpdate, s_lateUpdateComparer);
        }
    }
}
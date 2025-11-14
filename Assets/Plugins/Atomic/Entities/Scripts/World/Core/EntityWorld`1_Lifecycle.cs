using System;

namespace Atomic.Entities
{
    public partial class EntityWorld<E>
    {
        /// <inheritdoc/>
        public event Action OnEnabled;

        /// <inheritdoc/>
        public event Action OnDisabled;

        /// <inheritdoc/>
        public event Action<float> OnTicked;

        /// <inheritdoc/>
        public event Action<float> OnFixedTicked;

        /// <inheritdoc/>
        public event Action<float> OnLateTicked;

        /// <summary>
        /// Indicates whether the world is enabled.
        /// </summary>
        public bool Enabled => _enabled;

        private bool _enabled;

        /// <inheritdoc/>
        public void Enable()
        {
            if (_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Enable();
                currentIndex = slot.right;
            }

            _enabled = true;

            this.NotifyAboutStateChanged();
            this.OnEnabled?.Invoke();
        }

        /// <inheritdoc/>
        public void Disable()
        {
            if (!_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Disable();
                currentIndex = slot.right;
            }

            _enabled = false;

            this.NotifyAboutStateChanged();
            this.OnDisabled?.Invoke();
        }

        /// <inheritdoc/>
        public void Tick(float deltaTime)
        {
            if (!_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.Tick(deltaTime);
                currentIndex = slot.right;
            }

            this.OnTicked?.Invoke(deltaTime);
        }

        /// <inheritdoc/>
        public void FixedTick(float deltaTime)
        {
            if (!_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.FixedTick(deltaTime);
                currentIndex = slot.right;
            }

            this.OnFixedTicked?.Invoke(deltaTime);
        }

        /// <inheritdoc/>
        public void LateTick(float deltaTime)
        {
            if (!_enabled)
                return;

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                slot.value.LateTick(deltaTime);
                currentIndex = slot.right;
            }

            this.OnLateTicked?.Invoke(deltaTime);
        }

        /// <summary>
        /// Disables this state, clear all entities and release all events
        /// </summary>
        public override void Dispose()
        {
            if (_enabled)
                this.Disable();

            base.Dispose();

            //Unsubscribe events:
            this.OnEnabled = null;
            this.OnDisabled = null;
            this.OnTicked = null;
            this.OnFixedTicked = null;
            this.OnLateTicked = null;
        }
    }
}
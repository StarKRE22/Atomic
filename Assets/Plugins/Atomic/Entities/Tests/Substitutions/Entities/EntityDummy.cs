namespace Atomic.Entities
{
    public class EntityDummy : Entity
    {
        public bool WasInitialized { get; private set; }
        public bool WasDisposed { get; private set; }
        public bool WasEnabled { get; private set; }
        public bool WasDisabled { get; private set; }
        public bool WasUpdated { get; private set; }
        public bool WasFixedUpdated { get; private set; }
        public bool WasLateUpdated { get; private set; }

        public float LastDeltaTime { get; private set; }
        public float LastFixedDeltaTime { get; private set; }
        public float LastLateDeltaTime { get; private set; }

        public EntityDummy()
        {
            this.OnInitialized += () => WasInitialized = true;
            this.OnDisposed += () => WasDisposed = true;
            this.OnEnabled += () => WasEnabled = true;
            this.OnDisabled += () =>  WasDisabled = true;
            this.OnTicked += deltaTime =>
            {
                LastDeltaTime = deltaTime;
                WasUpdated = true;
            };

            this.OnFixedTicked += deltaTime =>
            {
                LastFixedDeltaTime = deltaTime;
                WasFixedUpdated = true;
            };

            this.OnLateTicked += deltaTime =>
            {
                LastLateDeltaTime = deltaTime;
                WasLateUpdated = true;
            };
        }
    }
}
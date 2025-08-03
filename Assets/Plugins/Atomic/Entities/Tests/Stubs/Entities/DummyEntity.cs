namespace Atomic.Entities
{
    public class DummyEntity : Entity
    {
        public bool WasSpawned { get; private set; }
        public bool WasDespawned { get; private set; }
        public bool WasActivated { get; private set; }
        public bool WasDeactivated { get; private set; }
        public bool WasUpdated { get; private set; }
        public bool WasFixedUpdated { get; private set; }
        public bool WasLateUpdated { get; private set; }
        
        public float LastDeltaTime { get; private set; }
        public float LastFixedDeltaTime { get; private set; }
        public float LastLateDeltaTime { get; private set; }
        
        protected override void ProcessSpawn()
        {
            base.ProcessSpawn();
            WasSpawned = true;
        }

        protected override void ProcessDespawn()
        {
            base.ProcessDespawn();
            WasDespawned = true;
        }

        protected override void ProcessActivate()
        {
            base.ProcessActivate();
            WasActivated = true;
        }

        protected override void ProcessInactivate()
        {
            base.ProcessInactivate();
            WasDeactivated = true;
        }

        protected override void ProcessUpdate(float deltaTime)
        {
            base.ProcessUpdate(deltaTime);
            LastDeltaTime = deltaTime;
            WasUpdated = true;
        }

        protected override void ProcessFixedUpdate(float deltaTime)
        {
            base.ProcessFixedUpdate(deltaTime);
            LastFixedDeltaTime = deltaTime;
            WasFixedUpdated = true;
        }

        protected override void ProcessLateUpdate(float deltaTime)
        {
            base.ProcessLateUpdate(deltaTime);
            LastLateDeltaTime = deltaTime;
            WasLateUpdated = true;
        }
    }
}
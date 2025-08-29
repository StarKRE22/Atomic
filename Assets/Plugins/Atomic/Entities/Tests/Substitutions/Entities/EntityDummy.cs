namespace Atomic.Entities
{
    public class EntityDummy : Entity
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
        
        protected override void ProcessInit()
        {
            base.ProcessInit();
            WasSpawned = true;
        }

        protected override void ProcessDispose()
        {
            base.ProcessDispose();
            WasDespawned = true;
        }

        protected override void ProcessEnable()
        {
            base.ProcessEnable();
            WasActivated = true;
        }

        protected override void ProcessDisable()
        {
            base.ProcessDisable();
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
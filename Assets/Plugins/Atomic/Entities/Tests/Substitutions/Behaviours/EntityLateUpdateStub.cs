namespace Atomic.Entities
{
    public class EntityLateUpdateStub : IEntityLateUpdate
    {
        public bool WasCalled { get; private set; }
        public float LastDeltaTime { get; private set; }

        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            LastDeltaTime = deltaTime;
        }
    }
}
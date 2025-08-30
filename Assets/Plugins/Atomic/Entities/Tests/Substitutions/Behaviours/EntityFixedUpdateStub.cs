namespace Atomic.Entities
{
    public class EntityFixedUpdateStub : IEntityFixedUpdate
    {
        public bool WasCalled { get; private set; }
        public float LastDeltaTime { get; private set; }

        public void FixedUpdate(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            LastDeltaTime = deltaTime;
        }
    }
}
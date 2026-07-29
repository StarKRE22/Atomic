namespace Atomic.Entities
{
    public class EntityFixedTickSpy : IEntityFixedTick
    {
        public bool WasCalled { get; private set; }
        public float LastDeltaTime { get; private set; }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            LastDeltaTime = deltaTime;
        }
    }
}
namespace Atomic.Entities
{
    public class DisableDuringFixedTickStub : IEntityFixedTick
    {
        public bool WasCalled { get; private set; }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            entity.Disable();
        }
    }
}
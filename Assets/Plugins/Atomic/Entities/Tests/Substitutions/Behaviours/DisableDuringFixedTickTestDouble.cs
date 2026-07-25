namespace Atomic.Entities
{
    public class DisableDuringFixedTickTestDouble : IEntityFixedTick
    {
        public bool WasCalled { get; private set; }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            entity.Disable();
        }
    }
}
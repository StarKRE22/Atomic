namespace Atomic.Entities
{
    public class DisableDuringLateTickStub : IEntityLateTick
    {
        public bool WasCalled { get; private set; }

        public void LateTick(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            entity.Disable(); // отключает во время обновления
        }
    }
}
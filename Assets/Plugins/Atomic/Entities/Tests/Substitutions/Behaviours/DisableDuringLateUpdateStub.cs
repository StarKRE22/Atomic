namespace Atomic.Entities
{
    public class DisableDuringLateUpdateStub : IEntityLateUpdate
    {
        public bool WasCalled { get; private set; }

        public void LateUpdate(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            entity.Disable(); // отключает во время обновления
        }
    }
}
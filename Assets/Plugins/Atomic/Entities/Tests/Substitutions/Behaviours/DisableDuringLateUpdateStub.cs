namespace Atomic.Entities
{
    public class DisableDuringLateUpdateStub : IEntityLateUpdate
    {
        public bool WasCalled { get; private set; }

        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            entity.Deactivate(); // отключает во время обновления
        }
    }
}
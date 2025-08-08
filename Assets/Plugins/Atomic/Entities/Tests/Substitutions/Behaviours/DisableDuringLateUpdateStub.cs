namespace Atomic.Entities
{
    public class DisableDuringLateUpdateStub : IEntityLateUpdate
    {
        public bool WasCalled { get; private set; }

        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            entity.Inactivate(); // отключает во время обновления
        }
    }
}
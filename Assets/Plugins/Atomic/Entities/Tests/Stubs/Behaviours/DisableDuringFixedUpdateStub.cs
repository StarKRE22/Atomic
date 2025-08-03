namespace Atomic.Entities
{
    public class DisableDuringFixedUpdateStub : IEntityFixedUpdate
    {
        public bool WasCalled { get; private set; }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            entity.Inactivate();
        }
    }
}
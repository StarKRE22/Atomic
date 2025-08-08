namespace Atomic.Entities
{
    public class DisableDuringUpdateStub : IEntityUpdate
    {
        public bool WasUpdated { get; private set; }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            WasUpdated = true;
            entity.Inactivate(); // отключает себя => следующий не будет вызван
        }
    }
}
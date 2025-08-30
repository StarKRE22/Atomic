namespace Atomic.Entities
{
    public class DisableDuringUpdateStub : IEntityUpdate
    {
        public bool WasUpdated { get; private set; }

        public void Update(IEntity entity, float deltaTime)
        {
            WasUpdated = true;
            entity.Disable(); // отключает себя => следующий не будет вызван
        }
    }
}
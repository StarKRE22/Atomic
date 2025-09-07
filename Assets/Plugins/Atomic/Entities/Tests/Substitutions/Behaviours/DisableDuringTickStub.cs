namespace Atomic.Entities
{
    public class DisableDuringTickStub : IEntityTick
    {
        public bool WasUpdated { get; private set; }

        public void Tick(IEntity entity, float deltaTime)
        {
            WasUpdated = true;
            entity.Disable(); // отключает себя => следующий не будет вызван
        }
    }
}
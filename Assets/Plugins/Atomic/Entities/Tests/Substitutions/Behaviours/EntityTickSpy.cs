namespace Atomic.Entities
{
    public sealed class EntityTickSpy : IEntityTick
    {
        public bool WasUpdated;
        public float LastDeltaTime;
        
        public void Tick(IEntity entity, float deltaTime)
        {
            WasUpdated = true;
            LastDeltaTime = deltaTime;
        }
    }
}
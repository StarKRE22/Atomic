namespace Atomic.Entities
{
    public sealed class EntityUpdateStub : IEntityUpdate
    {
        public bool WasUpdated;
        public float LastDeltaTime;
        
        public void Update(IEntity entity, float deltaTime)
        {
            WasUpdated = true;
            LastDeltaTime = deltaTime;
        }
    }
}
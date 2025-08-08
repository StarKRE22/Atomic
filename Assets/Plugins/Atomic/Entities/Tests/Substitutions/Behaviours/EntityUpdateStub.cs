namespace Atomic.Entities
{
    public sealed class EntityUpdateStub : IEntityUpdate
    {
        public bool WasUpdated;
        public float LastDeltaTime;
        
        public void OnUpdate(IEntity entity, float deltaTime)
        {
            WasUpdated = true;
            LastDeltaTime = deltaTime;
        }
    }
}
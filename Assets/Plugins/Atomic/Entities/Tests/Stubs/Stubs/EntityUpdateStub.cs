namespace Atomic.Entities
{
    public sealed class EntityUpdateStub : IEntityUpdate
    {
        public void OnUpdate(IEntity entity, float deltaTime)
        {
        }
    }
}
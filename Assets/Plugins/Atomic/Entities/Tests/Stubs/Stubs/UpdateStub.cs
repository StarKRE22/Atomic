namespace Atomic.Entities
{
    public sealed class UpdateStub : IUpdate
    {
        public void OnUpdate(in IEntity entity, in float deltaTime)
        {
        }
    }
}
namespace Atomic.Entities
{
    public interface IEntityFixedUpdate : IEntityBehaviour
    {
        void OnFixedUpdate(IEntity entity, float deltaTime);
    }
}
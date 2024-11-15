namespace Atomic.Entities
{
    public interface IEntityLateUpdate : IEntityBehaviour
    {
        void OnLateUpdate(IEntity entity, float deltaTime);
    }
}
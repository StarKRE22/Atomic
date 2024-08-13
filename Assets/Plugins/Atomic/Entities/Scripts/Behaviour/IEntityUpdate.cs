
namespace Atomic.Entities
{
    public interface IEntityUpdate : IEntityBehaviour
    {
        void OnUpdate(IEntity entity, float deltaTime);
    }
}
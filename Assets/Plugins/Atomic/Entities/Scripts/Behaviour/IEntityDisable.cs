namespace Atomic.Entities
{
    public interface IEntityDisable : IEntityBehaviour
    {
        void Disable(IEntity entity);
    }
}
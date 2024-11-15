namespace Atomic.Entities
{
    public interface IEntityInit : IEntityBehaviour
    {
        void Init(IEntity entity);
    }
}
namespace Atomic.Entities
{
    public interface IEntityPool
    {
        IEntity Rent();
        void Return(IEntity entity);
        
        void Init(int initialCoint);
        void Clear();
    }
}
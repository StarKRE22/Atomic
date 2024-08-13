using Atomic.Entities;

namespace GameExample.Engine
{
    public interface IEntityPool
    {
        public IEntity Rent();
        public void Return(IEntity entity);
    }
}
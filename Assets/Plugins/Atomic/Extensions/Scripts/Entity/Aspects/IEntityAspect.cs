using Atomic.Entities;

namespace Atomic.Extensions
{
    public interface IEntityAspect
    {
        void Apply(IEntity entity);
        void Discard(IEntity entity);
    }
}
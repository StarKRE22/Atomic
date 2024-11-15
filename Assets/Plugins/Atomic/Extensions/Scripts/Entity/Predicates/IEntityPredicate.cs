using Atomic.Elements;
using Atomic.Entities;

namespace Atomic.Extensions
{
    public interface IEntityPredicate : IPredicate<IEntity>
    {
    }

    public interface IEntityPredicate_Entity : IPredicate<IEntity, IEntity>
    {
    }
}
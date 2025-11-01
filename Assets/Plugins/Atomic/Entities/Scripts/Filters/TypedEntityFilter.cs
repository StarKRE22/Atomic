using System;

namespace Atomic.Entities
{
    public class TypedEntityFilter<E> : TypedEntityFilter<E, IEntity> where E : IEntity
    {
        public TypedEntityFilter(IReadOnlyEntityCollection<IEntity> source, Predicate<E> predicate, params IEntityTrigger<E>[] triggers) : base(source, predicate, triggers)
        {
        }
    }
}
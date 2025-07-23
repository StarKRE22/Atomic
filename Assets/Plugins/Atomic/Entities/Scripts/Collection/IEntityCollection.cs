using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public interface IEntityCollection : IEntityCollection<IEntity>, IReadOnlyEntityCollection
    {
    }

    public interface IEntityCollection<E> : IReadOnlyEntityCollection<E>, ICollection<E>, IDisposable where E : IEntity
    {
        new int Count { get; }

        new bool Contains(E entity);

        new bool Add(E entity);

        new void CopyTo(E[] array, int arrayIndex);
    }
}
using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IWorld<E> : IReadOnlyCollection<E> where E : IEntity<E>
    {
        event Action OnStateChanged;
        event Action<E> OnAdded;
        event Action<E> OnDeleted;

        string Name { get; }

        bool Add(E entity);
        bool Has(E entity);
        bool Del(E entity);
        void Clear();

        E[] GetAll();
        int GetAll(E[] results);
        void CopyTo(ICollection<E> results);
    }
}
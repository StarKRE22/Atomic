using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IEntityWorld<E> : IReadOnlyCollection<E> where E : IEntity<E>
    {
        event Action OnStateChanged;
        event Action<E> OnAdded;
        event Action<E> OnDeleted;

        string Name { get; }

        bool Add(in IEntity entity);
        bool Has(in IEntity entity);
        bool Del(in IEntity entity);
        void Clear();

        IEntity[] GetAll();
        int GetAll(in IEntity[] results);
        void CopyTo(ICollection<IEntity> results);
    }
}
using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IEntityWorld<T> : IReadOnlyCollection<T> where T : IEntity
    {
        event Action OnStateChanged;
        event Action<IEntity> OnAdded;
        event Action<IEntity> OnDeleted;

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
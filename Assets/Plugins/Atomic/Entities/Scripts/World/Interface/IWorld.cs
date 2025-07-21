using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IWorld<E> : IReadOnlyEntityCollection<E> where E : IEntity
    {
        event Action OnStateChanged;

        string Name { get; }

        bool Add(E entity);
        bool Del(E entity);
        void Clear();
    }
}
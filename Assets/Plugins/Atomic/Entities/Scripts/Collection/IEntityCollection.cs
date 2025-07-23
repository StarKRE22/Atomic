using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a mutable collection of <see cref="IEntity"/> instances.
    /// Inherits from both <see cref="IEntityCollection{E}"/> and <see cref="IReadOnlyEntityCollection"/>.
    /// </summary>
    public interface IEntityCollection : IEntityCollection<IEntity>, IReadOnlyEntityCollection
    {
    }

    /// <summary>
    /// Represents a generic mutable collection of entities.
    /// Provides methods for adding, removing, and clearing entities.
    /// Inherits from <see cref="IReadOnlyEntityCollection{E}"/>.
    /// </summary>
    /// <typeparam name="E">The type of entity, which must implement <see cref="IEntity"/>.</typeparam>
    public interface IEntityCollection<E> : IReadOnlyEntityCollection<E>, ICollection<E>, IDisposable where E : IEntity
    {
        new int Count { get; }

        new bool Contains(E entity);

        new bool Add(E entity);

        new void CopyTo(E[] array, int arrayIndex);
    }
}
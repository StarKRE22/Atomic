using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a mutable collection of entities of type <typeparamref name="E"/>.
    /// Supports standard collection operations and provides utility methods for working with entity lifecycles.
    /// </summary>
    /// <typeparam name="E">The type of entities stored in the collection. Must implement <see cref="IEntity"/>.</typeparam>
    public interface IEntityCollection<E> : IReadOnlyEntityCollection<E>, ICollection<E>, IDisposable where E : IEntity
    {
        /// <summary>
        /// Gets the number of entities contained in the collection.
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// Determines whether the collection contains the specified entity.
        /// </summary>
        /// <param name="entity">The entity to locate in the collection.</param>
        /// <returns><c>true</c> if the entity is found; otherwise, <c>false</c>.</returns>
        new bool Contains(E entity);

        /// <summary>
        /// Adds the specified entity to the collection.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns><c>true</c> if the entity was successfully added; <c>false</c> if it already existed.</returns>
        new bool Add(E entity);

        /// <summary>
        /// Copies the elements of the collection to the specified array, starting at the specified index.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        new void CopyTo(E[] array, int arrayIndex);
    }
}
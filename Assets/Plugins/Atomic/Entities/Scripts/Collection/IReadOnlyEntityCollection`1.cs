using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a read-only, observable collection of entities of type <typeparamref name="E"/>.
    /// Provides access to entity presence, enumeration, and change events.
    /// </summary>
    /// <typeparam name="E">The type of entity contained in the collection. Must implement <see cref="IEntity"/>.</typeparam>
    public interface IReadOnlyEntityCollection<E> : IReadOnlyCollection<E> where E : IEntity
    {
        /// <summary>
        /// Occurs when collection state changed.
        /// </summary>
        event Action OnStateChanged;

        /// <summary>
        /// Occurs when an entity is added to the collection.
        /// </summary>
        event Action<E> OnAdded;

        /// <summary>
        /// Occurs when an entity is removed from the collection.
        /// </summary>
        event Action<E> OnRemoved;

        /// <summary>
        /// Determines whether the specified entity is currently present in the collection.
        /// </summary>
        /// <param name="entity">The entity to check for presence.</param>
        /// <returns><c>true</c> if the entity is in the collection; otherwise, <c>false</c>.</returns>
        bool Contains(E entity);

        /// <summary>
        /// Copies all entities into the specified collection.
        /// </summary>
        /// <param name="results">The collection to populate with entities.</param>
        void CopyTo(ICollection<E> results);

        /// <summary>
        /// Copies all entities into the specified collection.
        /// </summary>
        void CopyTo(E[] array, int arrayIndex);
    }
}
using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="IReadOnlyEntityCollection{E}"/> for collections of <see cref="IEntity"/>.
    /// </summary>
    public interface IReadOnlyEntityCollection : IReadOnlyEntityCollection<IEntity>
    {
    }
    
    /// <summary>
    /// Represents a read-only, observable collection of entities of type <typeparamref name="E"/>.
    /// Provides access to entity presence, enumeration, and change events.
    /// </summary>
    /// <typeparam name="E">The type of entity contained in the collection. Must implement <see cref="IEntity"/>.</typeparam>
    public interface IReadOnlyEntityCollection<E> : IReadOnlyCollection<E> where E : IEntity
    {
        /// <summary>
        /// Occurs when an entity is added to the collection.
        /// </summary>
        event Action<E> OnAdded;

        /// <summary>
        /// Occurs when an entity is removed from the collection.
        /// </summary>
        event Action<E> OnDeleted;

        /// <summary>
        /// Determines whether the specified entity is currently present in the collection.
        /// </summary>
        /// <param name="entity">The entity to check for presence.</param>
        /// <returns><c>true</c> if the entity is in the collection; otherwise, <c>false</c>.</returns>
        bool Has(E entity);

        /// <summary>
        /// Returns a new array containing all entities currently in the collection.
        /// </summary>
        /// <returns>An array of entities.</returns>
        E[] GetAll();

        /// <summary>
        /// Copies all entities into the provided array.
        /// </summary>
        /// <param name="results">The array to populate.</param>
        /// <returns>The number of entities copied.</returns>
        int GetAll(E[] results);

        /// <summary>
        /// Copies all entities into the specified collection.
        /// </summary>
        /// <param name="results">The collection to populate with entities.</param>
        void CopyTo(ICollection<E> results);
    }
}
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
    public interface IEntityCollection<E> : IReadOnlyEntityCollection<E> where E : IEntity
    {
        /// <summary>
        /// Adds the specified entity to the collection.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        /// <c>true</c> if the entity was successfully added;
        /// <c>false</c> if the entity is null or already present.
        /// </returns>
        bool Add(E entity);
        
        /// <summary>
        /// Removes the specified entity from the collection.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>
        /// <c>true</c> if the entity was successfully removed;
        /// <c>false</c> if the entity is null or not found.
        /// </returns>
        bool Del(E entity);
        
        /// <summary>
        /// Removes all entities from the collection and releases associated resources.
        /// </summary>
        void Clear();
    }
}
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic base class for working with general <see cref="IEntity"/> views through pooling.
    /// Inherits from <see cref="EntityViewPoolAbstract{IEntity}"/> for use with non-generic systems.
    /// </summary>
    /// <remarks>
    /// Use this class when the specific type of entity is not needed or when working with a unified base view pool.
    /// </remarks>
    public abstract class EntityViewPoolAbstract : EntityViewPoolAbstract<IEntity>
    {
    }

    /// <summary>
    /// A base implementation for a pool that manages view instances for entities of type <typeparamref name="E"/>.
    /// Responsible for renting, returning, and clearing view instances associated with entity names.
    /// </summary>
    /// <typeparam name="E">The type of entity associated with the view. Must implement <see cref="IEntity"/>.</typeparam>
    public abstract class EntityViewPoolAbstract<E> : MonoBehaviour where E : IEntity
    {
        /// <summary>
        /// Retrieves a view instance for the specified prefab name.
        /// If the view does not exist in the pool, a new one should be created.
        /// </summary>
        /// <param name="name">The name of the entity or prefab used as a key for pooling.</param>
        /// <returns>An instance of <see cref="EntityViewBase{E}"/> corresponding to the specified name.</returns>
        public abstract EntityViewBase<E> Rent(string name);

        /// <summary>
        /// Returns the specified view to the pool for reuse.
        /// </summary>
        /// <param name="name">The name of the entity or prefab used as a key for pooling.</param>
        /// <param name="view">The view instance to return to the pool.</param>
        public abstract void Return(string name, EntityViewBase<E> view);

        /// <summary>
        /// Clears all internal pools, disposing or recycling all currently stored views.
        /// </summary>
        public abstract void Clear();
    }
}
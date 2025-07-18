using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Defines a filtered view over a collection of entities,
    /// supporting change tracking, querying, and data copying.
    /// </summary>
    public interface IEntityFilter : IDisposable, IEnumerable<IEntity>
    {
        /// <summary>
        /// Occurs when an entity enters the filter (i.e., matches the filter criteria).
        /// </summary>
        event Action<IEntity> OnAdded;

        /// <summary>
        /// Occurs when an entity is removed from the filter (i.e., no longer matches the criteria).
        /// </summary>
        event Action<IEntity> OnDeleted;

        /// <summary>
        /// Gets the number of entities currently included in the filter.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines whether the specified entity is currently present in the filter.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <returns><c>true</c> if the entity is contained in the filter; otherwise, <c>false</c>.</returns>
        bool Has(IEntity entity);

        /// <summary>
        /// Returns a new array containing all entities in the filter.
        /// </summary>
        /// <returns>An array of all filtered entities.</returns>
        IEntity[] GetAll();

        /// <summary>
        /// Copies all filtered entities into the provided array.
        /// </summary>
        /// <param name="results">The array to populate.</param>
        /// <returns>The number of entities copied.</returns>
        int GetAll(IEntity[] results);

        /// <summary>
        /// Copies all filtered entities into the specified collection.
        /// </summary>
        /// <param name="results">The collection to populate.</param>
        void CopyTo(ICollection<IEntity> results);

        /// <summary>
        /// Provides a mechanism for reacting to per-entity changes that might affect filter membership.
        /// </summary>
        public interface ITrigger
        {
            /// <summary>
            /// Delegate used for synchronizing entity state within the filter.
            /// </summary>
            /// <param name="entity">The affected entity.</param>
            public delegate void SyncAction(IEntity entity);
            
            /// <summary>
            /// Subscribes to entity-specific events to track relevant changes.
            /// </summary>
            /// <param name="entity">The entity to observe.</param>
            /// <param name="syncAction">The callback to invoke when a tracked change occurs.</param>
            void Subscribe(IEntity entity, SyncAction syncAction);

            /// <summary>
            /// Unsubscribes from previously tracked entity-specific changes.
            /// </summary>
            /// <param name="entity">The entity that was being observed.</param>
            /// <param name="syncAction">The callback to invoke when a tracked change occurs.</param>
            void Unsubscribe(IEntity entity, SyncAction syncAction);
        }
    }
}
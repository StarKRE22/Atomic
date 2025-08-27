using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a read-only view of an entity collection. 
    /// Provides notifications when entity views are added or removed and allows retrieving the view associated with a specific entity.
    /// </summary>
    public interface IReadOnlyEntityCollectionView : IReadOnlyCollection<KeyValuePair<IEntity, IReadOnlyEntityView>>
    {
        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// Subscribers can use this event to react to entity instantiation in the view.
        /// </summary>
        event Action<IEntity, IReadOnlyEntityView> OnAdded;

        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// Subscribers can use this event to react to entity destruction or removal from the view.
        /// </summary>
        event Action<IEntity, IReadOnlyEntityView> OnRemoved;

        /// <summary>
        /// Gets the view instance associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <returns>
        /// The active <see cref="IReadOnlyEntityView"/> instance associated with the entity, or <c>null</c> if no view exists.
        /// </returns>
        IReadOnlyEntityView GetView(IEntity entity);
    }
}
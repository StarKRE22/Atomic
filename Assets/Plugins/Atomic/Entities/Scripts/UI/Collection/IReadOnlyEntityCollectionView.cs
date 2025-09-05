using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a read-only view of an entity collection. 
    /// Provides notifications when entity views are added or removed and allows retrieving the view associated with a specific entity.
    /// </summary>
    public interface IReadOnlyEntityCollectionView : IReadOnlyDictionary<IEntity, IEntityView>
    {
        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// Subscribers can use this event to react to entity instantiation in the view.
        /// </summary>
        event Action<IEntity, IEntityView> OnAdded;

        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// Subscribers can use this event to react to entity destruction or removal from the view.
        /// </summary>
        event Action<IEntity, IEntityView> OnRemoved;
    }
    
    /// <summary>
    /// Represents a read-only view of an entity collection. 
    /// Provides notifications when entity views are added or removed and allows retrieving the view associated with a specific entity.
    /// </summary>
    public interface IReadOnlyEntityCollectionView : IReadOnlyDictionary<IEntity, IEntityView>
    {
        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// Subscribers can use this event to react to entity instantiation in the view.
        /// </summary>
        event Action<IEntity, E> OnAdded;

        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// Subscribers can use this event to react to entity destruction or removal from the view.
        /// </summary>
        event Action<IEntity, E> OnRemoved;
    }
}
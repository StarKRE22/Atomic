using System;

namespace Atomic.Entities
{
    public interface IReadOnlyEntityCollectionView
    {
        /// <summary>
        /// Raised when a view is spawned for a newly added entity.
        /// </summary>
        event Action<IEntity, EntityViewBase> OnAdded;
        /// <summary>
        /// Raised when a view is removed for a despawned or removed entity.
        /// </summary>
        event Action<IEntity, EntityViewBase> OnRemoved;

        /// <summary>
        /// Gets the view instance associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view is requested.</param>
        /// <returns>The active <see cref="IReadOnlyEntityView"/> instance.</returns>
        IReadOnlyEntityView GetView(IEntity entity);
    }
}
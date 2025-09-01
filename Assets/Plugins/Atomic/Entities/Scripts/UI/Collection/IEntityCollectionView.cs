namespace Atomic.Entities
{
    /// <summary>
    /// Represents a modifiable view of an entity collection. 
    /// Extends <see cref="IReadOnlyEntityCollectionView"/> by allowing adding, removing, and clearing entity views.
    /// </summary>
    public interface IEntityCollectionView : IReadOnlyEntityCollectionView
    {
        /// <summary>
        /// Adds a view for the specified entity to the collection.
        /// </summary>
        /// <param name="entity">The entity for which a view should be added.</param>
        void AddView(IEntity entity);

        /// <summary>
        /// Removes the view associated with the specified entity from the collection.
        /// </summary>
        /// <param name="entity">The entity whose view should be removed.</param>
        void RemoveView(IEntity entity);

        /// <summary>
        /// Removes all views from the collection, clearing all associated entity views.
        /// </summary>
        void ClearViews();
    }
}

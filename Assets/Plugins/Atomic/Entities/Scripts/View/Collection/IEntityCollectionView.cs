namespace Atomic.Entities
{
    public interface IEntityCollectionView : IReadOnlyEntityCollectionView
    {
        /// <summary>
        /// Creates and shows a view for the specified entity, if the spawn condition is met.
        /// </summary>
        /// <param name="entity">The entity to visualize.</param>
        void AddView(IEntity entity);

        /// <summary>
        /// Hides and returns the view associated with the specified entity.
        /// </summary>
        /// <param name="entity">The entity whose view should be removed.</param>
        void RemoveView(IEntity entity);
    }
}

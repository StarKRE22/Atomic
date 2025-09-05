namespace Atomic.Entities
{
    /// <summary>
    /// Represents a modifiable view of an entity collection. 
    /// Extends <see cref="IReadOnlyEntityCollectionView"/> by allowing adding, removing, and clearing entity views.
    /// </summary>
    public interface IEntityCollectionView<E> : IReadOnlyEntityCollectionView<E>
    {
        /// <summary>
        /// Gets the display name or identifier of the view.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets a value indicating whether the view is currently visible (e.g., active in scene or UI).
        /// </summary>
        bool IsVisible { get; }

        void Show(IEntityCollection<E> collection);

        void Hide(IEntityCollection<E> collection);
    }
}

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a modifiable visual view of an <see cref="IEntity"/>.
    /// Extends <see cref="IReadOnlyEntityView"/> by allowing the view to be shown or hidden.
    /// </summary>
    public interface IEntityView : IReadOnlyEntityView
    {
        /// <summary>
        /// Displays the view for the specified entity and associates it with this view instance.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        void Show(IEntity entity);

        /// <summary>
        /// Hides or deactivates the current view, removing its association with the entity.
        /// </summary>
        void Hide();
    }
}
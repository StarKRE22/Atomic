namespace Atomic.Entities
{
    public interface IEntityView : IReadOnlyEntityView
    {
        /// <summary>
        /// Displays the view for the specified entity.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        void Show(IEntity entity);

        /// <summary>
        /// Hides or deactivates the current view.
        /// </summary>
        void Hide();
    }
}
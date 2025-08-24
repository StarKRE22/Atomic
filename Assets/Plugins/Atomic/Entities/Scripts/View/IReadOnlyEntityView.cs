namespace Atomic.Entities
{
    public interface IReadOnlyEntityView
    {
        /// <summary>
        /// Gets the display name or identifier of the view.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the entity instance currently associated with this view.
        /// </summary>
        IEntity Entity { get; }

        /// <summary>
        /// Gets a value indicating whether the view is currently visible (e.g., active in scene or UI).
        /// </summary>
        bool IsVisible { get; }
    }
}
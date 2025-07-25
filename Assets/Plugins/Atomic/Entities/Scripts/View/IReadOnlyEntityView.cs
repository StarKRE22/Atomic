namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="IReadOnlyEntityView{IEntity}"/>.
    /// Represents a read-only interface for inspecting entity views without mutation capabilities.
    /// </summary>
    public interface IReadOnlyEntityView : IReadOnlyEntityView<IEntity>
    {
    }

    /// <summary>
    /// Defines a read-only contract for inspecting a view associated with an entity of type <typeparamref name="E"/>.
    /// Useful when the consumer only needs to query the view's state without modifying it.
    /// </summary>
    /// <typeparam name="E">
    /// The type of entity represented by the view. Must implement <see cref="IEntity"/>.
    /// </typeparam>
    public interface IReadOnlyEntityView<out E> where E : IEntity
    {
        /// <summary>
        /// Gets the display name or identifier of the view.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the entity instance currently associated with this view.
        /// </summary>
        E Entity { get; }

        /// <summary>
        /// Gets a value indicating whether the view is currently visible (e.g., active in scene or UI).
        /// </summary>
        bool IsVisible { get; }
    }

}
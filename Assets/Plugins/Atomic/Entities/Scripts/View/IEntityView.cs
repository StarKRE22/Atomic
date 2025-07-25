namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic shortcut for <see cref="IEntityView{IEntity}"/>.
    /// Represents a view that is bound to a general <see cref="IEntity"/> instance.
    /// </summary>
    public interface IEntityView : IEntityView<IEntity>, IReadOnlyEntityView
    {
    }

    /// <summary>
    /// Represents a visual or logical view for an entity of type <typeparamref name="E"/>.
    /// Provides metadata such as name and visibility state, and allows showing or hiding the associated entity.
    /// </summary>
    /// <typeparam name="E">
    /// The type of entity this view represents. Must implement <see cref="IEntity"/>.
    /// </typeparam>
    public interface IEntityView<E> : IReadOnlyEntityView<E> where E : IEntity
    {
        /// <summary>
        /// Displays the view for the specified entity.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        void Show(E entity);

        /// <summary>
        /// Hides or deactivates the current view.
        /// </summary>
        void Hide();
    }
}
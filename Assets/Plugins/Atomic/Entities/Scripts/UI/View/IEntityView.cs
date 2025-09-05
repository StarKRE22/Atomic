using System;

namespace Atomic.Entities
{
    public interface IEntityView : IEntityView<IEntity>
    {
    }

    public interface IEntityView<E> : IDisposable where E : IEntity
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

        void Init();
        
        /// <summary>
        /// Displays the view for the specified entity and associates it with this view instance.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        void Show(E entity);

        /// <summary>
        /// Hides the current view, removing its association with the entity.
        /// </summary>
        void Hide();
    }
}
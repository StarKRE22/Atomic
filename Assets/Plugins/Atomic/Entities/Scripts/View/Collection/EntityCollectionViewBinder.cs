using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Binds a collection of IEntity objects to a view, allowing the view to reflect changes in the model automatically.
    /// </summary>
    public class EntityCollectionViewBinder : EntityCollectionViewBinder<IEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionViewBinder"/> class.
        /// </summary>
        /// <param name="model">The entity collection to bind.</param>
        /// <param name="view">The view that will display the entities.</param>
        public EntityCollectionViewBinder(IEntityCollection<IEntity> model, IEntityCollectionView view) :
            base(model, view)
        {
        }
    }

    /// <summary>
    /// Binds a collection of entities of type <typeparamref name="E"/> to a view. 
    /// Automatically updates the view when entities are added or removed from the model.
    /// </summary>
    /// <typeparam name="E">The type of entity in the collection.</typeparam>
    public class EntityCollectionViewBinder<E> : IDisposable where E : IEntity
    {
        private readonly IEntityCollection<E> _model;
        private readonly IEntityCollectionView _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionViewBinder{E}"/> class.
        /// Subscribes to the model's events and populates the view with existing entities.
        /// </summary>
        /// <param name="model">The entity collection to bind.</param>
        /// <param name="view">The view that will display the entities.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="model"/> or <paramref name="view"/> is null.</exception>
        public EntityCollectionViewBinder(IEntityCollection<E> model, IEntityCollectionView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _model.OnAdded += this.OnEntityAdded;
            _model.OnRemoved += this.OnEntityRemoved;

            foreach (E entity in _model)
                _view.AddView(entity);
        }

        /// <summary>
        /// Unsubscribes from the model's events and removes all entities from the view.
        /// </summary>
        public void Dispose()
        {
            _model.OnAdded -= this.OnEntityAdded;
            _model.OnRemoved -= this.OnEntityRemoved;

            foreach (E entity in _model)
                _view.RemoveView(entity);
        }

        /// <summary>
        /// Called when an entity is removed from the model. Removes the entity from the view.
        /// </summary>
        /// <param name="entity">The entity that was removed.</param>
        private void OnEntityRemoved(E entity) => _view.RemoveView(entity);

        /// <summary>
        /// Called when an entity is added to the model. Adds the entity to the view.
        /// </summary>
        /// <param name="entity">The entity that was added.</param>
        private void OnEntityAdded(E entity) => _view.AddView(entity);
    }
}
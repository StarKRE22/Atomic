using System;

namespace Atomic.Entities
{
    public class EntityCollectionViewBinder : EntityCollectionViewBinder<IEntity>
    {
        public EntityCollectionViewBinder(IEntityCollection<IEntity> model, IEntityCollectionView view) : 
            base(model, view)
        {
        }
    }

    public class EntityCollectionViewBinder<E> : IDisposable where E : IEntity
    {
        private readonly IEntityCollection<E> _model;
        private readonly IEntityCollectionView _view;

        public EntityCollectionViewBinder(IEntityCollection<E> model, IEntityCollectionView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            
            _model = model;
            _model.OnAdded += this.OnEntityAdded;
            _model.OnRemoved += this.OnEntityRemoved;

            foreach (E entity in _model)
                _view.AddView(entity);
        }
        
        public void Dispose()
        {
            _model.OnAdded -= this.OnEntityAdded;
            _model.OnRemoved -= this.OnEntityRemoved;

            foreach (E entity in _model)
                _view.RemoveView(entity);
        }
        
        private void OnEntityRemoved(E obj) => _view.RemoveView(obj);

        private void OnEntityAdded(E obj) => _view.AddView(obj);
    }
}
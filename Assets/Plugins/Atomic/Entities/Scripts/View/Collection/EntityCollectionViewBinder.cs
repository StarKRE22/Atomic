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
        private readonly IEntityCollection<IEntity> _model;
        private readonly IEntityCollectionView _view;

        public EntityCollectionViewBinder(IEntityCollection<IEntity> model, IEntityCollectionView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            
            _model = model;
            _model.OnAdded += _view.AddView;
            _model.OnRemoved += _view.RemoveView;

            foreach (E entity in _model)
                _view.AddView(entity);
        }

        public void Dispose()
        {
            _model.OnAdded -= _view.AddView;
            _model.OnRemoved -= _view.RemoveView;

            foreach (E entity in _model)
                _view.RemoveView(entity);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public class EntityCollectionView : EntityCollectionView<IEntity>
    {
    }

    public abstract class EntityCollectionView<E> : MonoBehaviour where E : IEntity
    {
        public event Action<E, EntityViewAbstract<E>> OnViewAdded;
        public event Action<E, EntityViewAbstract<E>> OnViewRemoved;

        private readonly Dictionary<E, EntityViewAbstract<E>> _activeViews = new();

        [SerializeField]
        private Transform _viewport;

        [SerializeField]
        private EntityViewPool<E> _viewPool;

        private IEntityCollection<E> _collection;

        public IEntityView<E> GetView(E entity) => _activeViews[entity];

        public void Show(IEntityWorld<E> world)
        {
            this.Hide();

            _collection = world;

            if (_collection == null)
                return;

            _collection.OnAdded += this.SpawnView;
            _collection.OnRemoved += this.UnspawnView;

            foreach (E entity in _collection)
                this.SpawnView(entity);
        }

        public void Hide()
        {
            if (_collection == null)
                return;

            _collection.OnAdded -= this.SpawnView;
            _collection.OnRemoved -= this.UnspawnView;

            foreach (E entity in _collection)
                this.UnspawnView(entity);
        }

        protected virtual string GetEntityName(E entity) => entity.Name;

        protected virtual bool IsSpawnConditionMet(E entity) => true;

        private void SpawnView(E entity)
        {
            if (!this.IsSpawnConditionMet(entity))
                return;

            string name = this.GetEntityName(entity);
            EntityViewAbstract<E> view = _viewPool.Rent(name);
            view.transform.SetParent(_viewport);
            view.Show(entity);

            _activeViews.Add(entity, view);
            this.OnViewAdded?.Invoke(entity, view);
        }

        private void UnspawnView(E entity)
        {
            if (!_activeViews.Remove(entity, out EntityViewAbstract<E> view))
                return;

            view.Hide();
            this.OnViewRemoved?.Invoke(entity, view);

            string name = this.GetEntityName(entity);
            _viewPool.Return(name, view);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class WorldView<E> : MonoBehaviour where E : IEntity<E>
    {
        public event Action<E, AbstractView<E>> OnViewAdded;
      
        public event Action<E, AbstractView<E>> OnViewRemoved; 

        private readonly Dictionary<E, AbstractView<E>> _activeViews = new();

        [SerializeField]
        private Transform _viewport;

        [SerializeField]
        private ViewPool<E> _viewPool;

        private IWorld<E> _world;

        public AbstractView<E> GetView(E entity) => _activeViews[entity];

        public Dictionary<E, AbstractView<E>> GetAllViews() => new(_activeViews);

        public void Show(IWorld<E> world)
        {
            this.Hide();

            _world = world;
            
            if (_world == null) 
                return;
            
            _world.OnAdded += this.SpawnView;
            _world.OnDeleted += this.UnspawnView;

            foreach (E entity in _world.GetAll())
                this.SpawnView(entity);
        }

        public void Hide()
        {
            if (_world == null)
                return;

            _world.OnAdded -= this.SpawnView;
            _world.OnDeleted -= this.UnspawnView;

            foreach (E entity in _world.GetAll())
                this.UnspawnView(entity);
        }

        protected virtual string GetEntityName(E entity) => entity.Name;

        protected virtual bool IsSpawnConditionMet(E entity) => true;

        private void SpawnView(E entity)
        {
            if (!this.IsSpawnConditionMet(entity))
                return;

            string name = this.GetEntityName(entity);
            AbstractView<E> view = _viewPool.Rent(name);
            view.transform.SetParent(_viewport);
            view.Show(entity);
            
            _activeViews.Add(entity, view);
            this.OnViewAdded?.Invoke(entity, view);
        }

        private void UnspawnView(E entity)
        {
            if (!_activeViews.Remove(entity, out AbstractView<E> view))
                return;

            view.Hide();
            this.OnViewRemoved?.Invoke(entity, view);

            string name = this.GetEntityName(entity);
            _viewPool.Return(name, view);
        }
    }
}
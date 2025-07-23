using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public class EntityWorldView : EntityWorldView<IEntity>
    {
    }

    public abstract class EntityWorldView<E> : MonoBehaviour where E : IEntity
    {
        public event Action<E, EntityViewBase<E>> OnViewAdded;
        public event Action<E, EntityViewBase<E>> OnViewRemoved;

        private readonly Dictionary<E, EntityViewBase<E>> _activeViews = new();

        [SerializeField]
        private Transform _viewport;

        [SerializeField]
        private EntityViewPool<E> _viewPool;

        private IEntityWorld<E> _world;

        public EntityViewBase<E> GetView(E entity) => _activeViews[entity];

        public void Show(IEntityWorld<E> world)
        {
            this.Hide();

            _world = world;

            if (_world == null)
                return;

            _world.OnAdded += this.SpawnView;
            _world.OnRemoved += this.UnspawnView;

            foreach (E entity in _world.GetAll())
                this.SpawnView(entity);
        }

        public void Hide()
        {
            if (_world == null)
                return;

            _world.OnAdded -= this.SpawnView;
            _world.OnRemoved -= this.UnspawnView;

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
            EntityViewBase<E> view = _viewPool.Rent(name);
            view.transform.SetParent(_viewport);
            view.Show(entity);

            _activeViews.Add(entity, view);
            this.OnViewAdded?.Invoke(entity, view);
        }

        private void UnspawnView(E entity)
        {
            if (!_activeViews.Remove(entity, out EntityViewBase<E> view))
                return;

            view.Hide();
            this.OnViewRemoved?.Invoke(entity, view);

            string name = this.GetEntityName(entity);
            _viewPool.Return(name, view);
        }
    }
}
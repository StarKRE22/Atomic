using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity World View")]
    [DisallowMultipleComponent]
    public class EntityWorldView : MonoBehaviour
    {
        public event Action<IEntity, ViewBase> OnViewAdded;
        public event Action<IEntity, ViewBase> OnViewRemoved; 

        private readonly Dictionary<IEntity, ViewBase> _activeViews = new();

        [SerializeField]
        private Transform _viewport;

        [SerializeField]
        private EntityViewPool _viewPool;

        private IEntityWorld _world;

        public ViewBase GetView(IEntity entity) => _activeViews[entity];

        public Dictionary<IEntity, ViewBase> GetAllViews() => new(_activeViews);

        public void Show(IEntityWorld world)
        {
            this.Hide();

            _world = world;
            
            if (_world == null) 
                return;
            
            _world.OnAdded += this.SpawnView;
            _world.OnDeleted += this.UnspawnView;

            foreach (IEntity entity in _world.GetAll())
                this.SpawnView(entity);
        }

        public void Hide()
        {
            if (_world == null)
                return;

            _world.OnAdded -= this.SpawnView;
            _world.OnDeleted -= this.UnspawnView;

            foreach (IEntity entity in _world.GetAll())
                this.UnspawnView(entity);
        }

        protected virtual string GetEntityName(IEntity entity) => entity.Name;

        protected virtual bool IsSpawnConditionMet(IEntity entity) => true;

        private void SpawnView(IEntity entity)
        {
            if (!this.IsSpawnConditionMet(entity))
                return;

            string name = this.GetEntityName(entity);
            ViewBase view = _viewPool.Rent(name);
            view.transform.SetParent(_viewport);
            view.Show(entity);
            
            _activeViews.Add(entity, view);
            this.OnViewAdded?.Invoke(entity, view);
        }

        private void UnspawnView(IEntity entity)
        {
            if (!_activeViews.Remove(entity, out ViewBase view))
                return;

            view.Hide();
            this.OnViewRemoved?.Invoke(entity, view);

            string name = this.GetEntityName(entity);
            _viewPool.Return(name, view);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public class SceneEntityPool : IEntityPool
    {
        private readonly SceneEntity _prefab;
        private readonly Transform _container;
        private readonly Queue<SceneEntity> _queue = new();

        public SceneEntityPool(SceneEntity prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SceneEntity entity = SceneEntity.Create(_prefab, _container);
                this.OnCreate(entity);
                _queue.Enqueue(entity);
            }
        }

        public IEntity Rent()
        {
            if (!_queue.TryDequeue(out SceneEntity entity))
            {
                entity = SceneEntity.Create(_prefab, _container);
                this.OnCreate(entity);
            }

            this.OnRent(entity);
            return entity;
        }

        public void Return(IEntity entity)
        {
            if (SceneEntity.TryCast(entity, out SceneEntity sceneEntity) && !_queue.Contains(sceneEntity))
            {
                this.OnReturn(sceneEntity);
                _queue.Enqueue(sceneEntity);
            }
        }

        public void Clear()
        {
            foreach (SceneEntity entity in _queue)
            {
                this.OnDestroy(entity);
                GameObject.Destroy(entity);
            }

            _queue.Clear();
        }

        protected virtual void OnCreate(SceneEntity entity)
        {
            entity.gameObject.SetActive(false);
        }

        protected virtual void OnDestroy(SceneEntity entity)
        {
        }

        protected virtual void OnRent(SceneEntity entity)
        {
            entity.gameObject.SetActive(true);
        }

        protected virtual void OnReturn(SceneEntity entity)
        {
            entity.gameObject.SetActive(false);
            entity.transform.SetParent(_container);
        }
    }
}
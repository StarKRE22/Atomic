using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public class ScenePool<E> : IPool<E> where E : SceneEntity<E>
    {
        private readonly E _prefab;
        private readonly Transform _container;
        private readonly Stack<E> _stack = new();

        public ScenePool(E prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                E entity = SceneEntity<E>.Create(_prefab, _container);
                this.OnCreate(entity);
                _stack.Push(entity);
            }
        }

        public E Rent()
        {
            if (!_stack.TryPop(out E entity))
            {
                entity = SceneEntity<E>.Create(_prefab, _container);
                this.OnCreate(entity);
            }

            this.OnRent(entity);
            return entity;
        }

        public void Return(E entity)
        {
            if (!_stack.Contains(entity))
            {
                this.OnReturn(entity);
                _stack.Push(entity);
            }
        }

        public void Clear()
        {
            foreach (E entity in _stack)
            {
                this.OnDestroy(entity);
                GameObject.Destroy(entity);
            }

            _stack.Clear();
        }

        protected virtual void OnCreate(E entity) => 
            entity.gameObject.SetActive(false);

        protected virtual void OnDestroy(E entity)
        {
        }

        protected virtual void OnRent(E entity) => 
            entity.gameObject.SetActive(true);

        protected virtual void OnReturn(E entity)
        {
            entity.gameObject.SetActive(false);
            entity.transform.SetParent(_container);
        }
    }
}
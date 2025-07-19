using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class ScenePool<E> : MonoBehaviour, IPool<E> where E : SceneEntity<E>
    {
        [SerializeField]
        private E _prefab;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private int _initialCount;
        
        private readonly Stack<E> _stack = new();

        private void Awake()
        {
            this.Init(_initialCount);
        }

        public void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                E entity = SceneEntity<E>.Create(_prefab, _container);
                this.OnSpawn(entity);
                _stack.Push(entity);
            }
        }

        public E Rent()
        {
            if (!_stack.TryPop(out E entity))
            {
                entity = SceneEntity<E>.Create(_prefab, _container);
                this.OnSpawn(entity);
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
                this.OnUnspawn(entity);
                Destroy(entity);
            }

            _stack.Clear();
        }

        protected virtual void OnSpawn(E entity) => 
            entity.gameObject.SetActive(false);

        protected virtual void OnUnspawn(E entity)
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
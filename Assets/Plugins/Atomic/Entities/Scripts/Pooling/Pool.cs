using System.Collections.Generic;

namespace Atomic.Entities
{
    public class Pool<E> : IPool<E> where E : IEntity<E>
    {
        private readonly Stack<E> _stack = new();
        private readonly IFactory<E> _factory;

        public Pool(IFactory<E> factory) => _factory = factory;

        public void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                E entity = _factory.Create();
                this.OnCreate(entity);
                _stack.Push(entity);
            }
        }

        public void Clear()
        {
            foreach (E entity in _stack) 
                this.OnDestroy(entity);

            _stack.Clear();
        }

        public E Rent()
        {
            if (!_stack.TryPop(out E entity))
            {
                entity = _factory.Create();
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

        protected virtual void OnCreate(E entity)
        {
        }
        
        protected virtual void OnDestroy(E entity)
        {
        }

        protected virtual void OnRent(E entity)
        {
        }

        protected virtual void OnReturn(E entity)
        {
        }
    }
}
using System.Collections.Generic;

namespace Atomic.Entities
{
    public class Pool<E> : IPool<E> where E : IEntity<E>
    {
        private readonly Queue<E> _queue = new();
        private readonly IFactory<E> _factory;

        public Pool(IFactory<E> factory) => _factory = factory;

        public void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                E entity = _factory.Create();
                this.OnCreate(entity);
                _queue.Enqueue(entity);
            }
        }

        public void Clear()
        {
            foreach (E entity in _queue) 
                this.OnDestroy(entity);

            _queue.Clear();
        }

        public E Rent()
        {
            if (!_queue.TryDequeue(out E entity))
            {
                entity = _factory.Create();
                this.OnCreate(entity);
            }

            this.OnRent(entity);
            return entity;
        }

        public void Return(E entity)
        {
            if (!_queue.Contains(entity))
            {
                this.OnReturn(entity);
                _queue.Enqueue(entity);
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
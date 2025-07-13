using System.Collections.Generic;

namespace Atomic.Entities
{
    public class EntityPool : IEntityPool
    {
        private readonly Queue<IEntity> _queue = new();
        private readonly IEntityFactory _prototype;

        public EntityPool(IEntityFactory prototype)
        {
            _prototype = prototype;
        }

        public void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                IEntity entity = _prototype.Create();
                this.OnCreate(entity);
                _queue.Enqueue(entity);
            }
        }

        public void Clear()
        {
            foreach (IEntity entity in _queue) 
                this.OnDestroy(entity);

            _queue.Clear();
        }

        public IEntity Rent()
        {
            if (!_queue.TryDequeue(out IEntity entity))
            {
                entity = _prototype.Create();
                this.OnCreate(entity);
            }

            this.OnRent(entity);
            return entity;
        }

        public void Return(IEntity entity)
        {
            if (!_queue.Contains(entity))
            {
                this.OnReturn(entity);
                _queue.Enqueue(entity);
            }
        }

        protected virtual void OnCreate(IEntity entity)
        {
        }
        
        protected virtual void OnDestroy(IEntity entity)
        {
        }

        protected virtual void OnRent(IEntity entity)
        {
        }

        protected virtual void OnReturn(IEntity entity)
        {
        }
    }
}
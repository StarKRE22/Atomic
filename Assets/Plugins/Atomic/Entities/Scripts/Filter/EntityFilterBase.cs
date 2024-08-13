using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /**
     * Experimental.
     */
    public abstract class EntityFilterBase : IEntityFilter
    {
        public event Action<IEntity> OnEntityAdded;
        public event Action<IEntity> OnEntityDeleted;

        public List<IEntity> Entities => new(this.entities);

        private readonly HashSet<IEntity> entities = new();
        private readonly Dictionary<object, object> subscriptions = new();

        private IEntityWorld world;
        
        public int CopyTo(IEntity[] results)
        {
            this.entities.CopyTo(results);
            return this.entities.Count;
        }

        public void CopyTo(List<IEntity> results)
        {
            results.Clear();
            results.AddRange(this.entities);
        }

        public bool HasEntity(IEntity entity)
        {
            return this.entities.Contains(entity);
        }

        public void Initialize(IEntityWorld world)
        {
            this.world = world;
            this.world.OnEntityAdded += this.Subscribe;
            this.world.OnEntityDeleted += this.Unsubscribe;

            IReadOnlyList<IEntity> entities = this.world.Entities;
            for (int i = 0, count = entities.Count; i < count; i++)
            {
                IEntity entity = entities[i];
                this.Subscribe(entity);
            }
        }

        public void Dispose()
        {
            IReadOnlyList<IEntity> entities = this.world.Entities;
            for (int i = 0, count = entities.Count; i < count; i++)
            {
                IEntity entity = entities[i];
                this.Unsubscribe(entity);
            }

            this.world.OnEntityAdded -= this.Subscribe;
            this.world.OnEntityDeleted -= this.Unsubscribe;
            this.world = null;
        }
        
        public IEnumerator<IEntity> GetEnumerator()
        {
            return this.entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        private void Subscribe(IEntity entity)
        {
            this.OnSubscribe(entity);

            if (this.Matches(entity) && this.entities.Add(entity))
            {
                this.OnEntityAdded?.Invoke(entity);
            }
        }

        private void Unsubscribe(IEntity entity)
        {
            this.OnUnsubscribe(entity);

            if (this.entities.Remove(entity))
            {
                this.OnEntityDeleted?.Invoke(entity);
            }
        }

        protected abstract bool Matches(IEntity entity);
        
        protected virtual void OnSubscribe(IEntity entity)
        {
            entity.OnValueAdded += this.OnValueAdded;
            entity.OnValueDeleted += this.OnValueDeleted;
            entity.OnValueChanged += this.OnValueChanged;

            entity.OnTagAdded += this.OnTagAdded;
            entity.OnTagDeleted += this.OnTagDeleted;
        }
        
        protected virtual void OnUnsubscribe(IEntity entity)
        {
            entity.OnValueAdded -= this.OnValueAdded;
            entity.OnValueDeleted -= this.OnValueDeleted;
            entity.OnValueChanged -= this.OnValueChanged;

            entity.OnTagAdded -= this.OnTagAdded;
            entity.OnTagDeleted -= this.OnTagDeleted;
        }
        
        protected virtual void OnTagDeleted(IEntity entity, int tag) => this.Synchronize(entity);
        protected virtual void OnTagAdded(IEntity entity, int _) => this.Synchronize(entity);
        
        protected virtual void OnValueDeleted(IEntity entity, int key, object value) => this.Synchronize(entity);
        protected virtual void OnValueAdded(IEntity entity, int key, object value) => this.Synchronize(entity);
        protected virtual void OnValueChanged(IEntity entity, int key, object value) => this.Synchronize(entity);

        protected bool AddSubscription(object key, object subscription)
        {
            return this.subscriptions.TryAdd(key, subscription);
        }

        protected T RemoveSubscription<T>(object key)
        {
            if (this.subscriptions.Remove(key, out object value))
            {
                return (T) value;
            }

            return default;
        }

        protected void Synchronize(IEntity entity)
        {
            bool matches = this.Matches(entity);

            if (!matches && this.entities.Remove(entity))
            {
                this.OnEntityDeleted?.Invoke(entity);
            }
            else if (matches && this.entities.Add(entity))
            {
                this.OnEntityAdded?.Invoke(entity);
            }
        }
    }
}
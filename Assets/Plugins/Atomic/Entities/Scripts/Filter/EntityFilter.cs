using System;
using System.Collections;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public class EntityFilter : IEntityFilter
    {
        public event Action<IEntity> OnAdded;
        public event Action<IEntity> OnDeleted;

        public int Count => entities.Count;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly HashSet<IEntity> entities = new();
        private readonly IEntityWorld world;

        private readonly Predicate<IEntity> predicate;
        private readonly IEntityFilter.ITrigger[] triggers;

        public EntityFilter(
            in IEntityWorld world,
            in Predicate<IEntity> predicate,
            params IEntityFilter.ITrigger[] triggers
        )
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this.world = world ?? throw new ArgumentNullException(nameof(world));
            this.triggers = triggers;
            this.Initialize();
        }

        private void Initialize()
        {
            this.world.OnAdded += this.Subscribe;
            this.world.OnDeleted += this.Unsubscribe;

            foreach (IEntity entity in this.world.All)
                this.Subscribe(entity);
        }

        public void Dispose()
        {
            foreach (IEntity entity in this.world.All)
                this.Unsubscribe(entity);

            this.world.OnAdded -= this.Subscribe;
            this.world.OnDeleted -= this.Unsubscribe;
        }

        public IEntity[] GetAll()
        {
            var result = new IEntity[this.entities.Count];
            this.entities.CopyTo(result);
            return result;
        }

        public int GetAll(IEntity[] results)
        {
            this.entities.CopyTo(results);
            return this.entities.Count;
        }

        public void CopyTo(ICollection<IEntity> results)
        {
            results.Clear();

            foreach (IEntity entity in entities)
                results.Add(entity);
        }


        public bool Has(IEntity entity)
        {
            return this.entities.Contains(entity);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return this.entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Subscribe(IEntity entity)
        {
            entity.OnTagAdded += this.OnTagAdded;
            entity.OnTagDeleted += this.OnTagDeleted;

            entity.OnValueAdded += this.OnValueAdded;
            entity.OnValueDeleted += this.OnValueDeleted;
            entity.OnValueChanged += this.OnValueChanged;

            foreach (var trigger in triggers)
                trigger.Subscribe(entity, this.Synchronize);

            if (this.predicate(entity) && this.entities.Add(entity))
                this.OnAdded?.Invoke(entity);
        }

        private void Unsubscribe(IEntity entity)
        {
            entity.OnTagAdded -= this.OnTagAdded;
            entity.OnTagDeleted -= this.OnTagDeleted;

            entity.OnValueAdded -= this.OnValueAdded;
            entity.OnValueDeleted -= this.OnValueDeleted;
            entity.OnValueChanged -= this.OnValueChanged;

            foreach (var trigger in triggers)
                trigger.Unsubscribe(entity, this.Synchronize);

            if (this.entities.Remove(entity))
                this.OnDeleted?.Invoke(entity);
        }

        private void OnTagDeleted(IEntity entity, int tag) => this.Synchronize(entity);
        private void OnTagAdded(IEntity entity, int _) => this.Synchronize(entity);

        private void OnValueDeleted(IEntity entity, int key) => this.Synchronize(entity);
        private void OnValueAdded(IEntity entity, int key) => this.Synchronize(entity);
        private void OnValueChanged(IEntity entity, int key) => this.Synchronize(entity);

        private void Synchronize(IEntity entity)
        {
            bool matches = this.predicate(entity);

            if (!matches && this.entities.Remove(entity)) this.OnDeleted?.Invoke(entity);
            else if (matches && this.entities.Add(entity)) this.OnAdded?.Invoke(entity);
        }
    }
}
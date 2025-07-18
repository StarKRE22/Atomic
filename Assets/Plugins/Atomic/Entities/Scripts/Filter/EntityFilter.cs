using System;
using System.Collections;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a dynamic filter over a set of entities in the world, based on a predicate and optional triggers.
    /// Automatically tracks entities that satisfy the predicate and invokes events on changes.
    /// </summary>
    public class EntityFilter : IEntityFilter
    {
        /// <inheritdoc />
        public event Action<IEntity> OnAdded;

        /// <inheritdoc />
        public event Action<IEntity> OnDeleted;

        /// <inheritdoc />
        public int Count => entities.Count;

        /// <summary>
        /// The set of entities currently matching the filter.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly HashSet<IEntity> entities = new();

        private readonly IEntityWorld world;
        private readonly Predicate<IEntity> predicate;
        private readonly IEntityFilter.ITrigger[] triggers;

        /// <summary>
        /// Creates a new entity filter that watches a world for entities matching a predicate and responds to trigger changes.
        /// </summary>
        /// <param name="world">The world to observe.</param>
        /// <param name="predicate">The predicate used to filter entities.</param>
        /// <param name="triggers">Optional triggers that can invalidate entities or cause re-evaluation.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="world"/> or <paramref name="predicate"/> is null.</exception>
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

        /// <summary>
        /// Initializes the filter by subscribing to existing entities and world events.
        /// </summary>
        private void Initialize()
        {
            this.world.OnAdded += this.Subscribe;
            this.world.OnDeleted += this.Unsubscribe;

            foreach (IEntity entity in this.world)
                this.Subscribe(entity);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (IEntity entity in this.world)
                this.Unsubscribe(entity);

            this.world.OnAdded -= this.Subscribe;
            this.world.OnDeleted -= this.Unsubscribe;
        }

        /// <inheritdoc />
        public IEntity[] GetAll()
        {
            IEntity[] result = new IEntity[this.entities.Count];
            this.entities.CopyTo(result);
            return result;
        }

        /// <inheritdoc />
        public int GetAll(IEntity[] results)
        {
            this.entities.CopyTo(results);
            return this.entities.Count;
        }

        /// <inheritdoc />
        public void CopyTo(ICollection<IEntity> results)
        {
            results.Clear();
            
            foreach (IEntity entity in this.entities)
                results.Add(entity);
        }

        /// <inheritdoc />
        public bool Has(IEntity entity) => this.entities.Contains(entity);

        /// <inheritdoc />
        public IEnumerator<IEntity> GetEnumerator() => this.entities.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        
        private void Subscribe(IEntity entity)
        {
            entity.OnTagAdded += this.OnTagAdded;
            entity.OnTagDeleted += this.OnTagDeleted;

            entity.OnValueAdded += this.OnValueAdded;
            entity.OnValueDeleted += this.OnValueDeleted;
            entity.OnValueChanged += this.OnValueChanged;

            for (int i = 0, count = this.triggers.Length; i < count; i++)
            {
                IEntityFilter.ITrigger trigger = this.triggers[i];
                trigger.Subscribe(entity, this.Synchronize);
            }

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

            for (int i = 0, count = this.triggers.Length; i < count; i++)
            {
                IEntityFilter.ITrigger trigger = this.triggers[i];
                trigger.Unsubscribe(entity, this.Synchronize);
            }

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
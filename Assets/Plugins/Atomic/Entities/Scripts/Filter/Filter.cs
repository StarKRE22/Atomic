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
    public class Filter<E> : IFilter<E> where E : IEntity<E> 
    {
        /// <inheritdoc />
        public event Action<E> OnAdded;

        /// <inheritdoc />
        public event Action<E> OnDeleted;

        /// <inheritdoc />
        public int Count => entities.Count;

        /// <summary>
        /// The set of entities currently matching the filter.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly HashSet<E> entities = new();

        private readonly IWorld<E> world;
        private readonly Predicate<E> predicate;
        private readonly IFilter<E>.ITrigger[] triggers;

        /// <summary>
        /// Creates a new entity filter that watches a world for entities matching a predicate and responds to trigger changes.
        /// </summary>
        /// <param name="world">The world to observe.</param>
        /// <param name="predicate">The predicate used to filter entities.</param>
        /// <param name="triggers">Optional triggers that can invalidate entities or cause re-evaluation.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="world"/> or <paramref name="predicate"/> is null.</exception>
        public Filter(
            in IWorld<E> world,
            in Predicate<E> predicate,
            params IFilter<E>.ITrigger[] triggers
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

            foreach (E entity in this.world)
                this.Subscribe(entity);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (E entity in this.world)
                this.Unsubscribe(entity);

            this.world.OnAdded -= this.Subscribe;
            this.world.OnDeleted -= this.Unsubscribe;
        }

        /// <inheritdoc />
        public E[] GetAll()
        {
            E[] result = new E[this.entities.Count];
            this.entities.CopyTo(result);
            return result;
        }

        /// <inheritdoc />
        public int GetAll(E[] results)
        {
            this.entities.CopyTo(results);
            return this.entities.Count;
        }

        /// <inheritdoc />
        public void CopyTo(ICollection<E> results)
        {
            results.Clear();
            
            foreach (E entity in this.entities)
                results.Add(entity);
        }

        /// <inheritdoc />
        public bool Has(E entity) => this.entities.Contains(entity);

        /// <inheritdoc />
        public IEnumerator<E> GetEnumerator() => this.entities.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        
        private void Subscribe(E entity)
        {
            entity.OnTagAdded += this.OnTagAdded;
            entity.OnTagDeleted += this.OnTagDeleted;

            entity.OnValueAdded += this.OnValueAdded;
            entity.OnValueDeleted += this.OnValueDeleted;
            entity.OnValueChanged += this.OnValueChanged;

            for (int i = 0, count = this.triggers.Length; i < count; i++)
            {
                IFilter<E>.ITrigger trigger = this.triggers[i];
                trigger.Subscribe(entity, this.Synchronize);
            }

            if (this.predicate(entity) && this.entities.Add(entity))
                this.OnAdded?.Invoke(entity);
        }

        private void Unsubscribe(E entity)
        {
            entity.OnTagAdded -= this.OnTagAdded;
            entity.OnTagDeleted -= this.OnTagDeleted;

            entity.OnValueAdded -= this.OnValueAdded;
            entity.OnValueDeleted -= this.OnValueDeleted;
            entity.OnValueChanged -= this.OnValueChanged;

            for (int i = 0, count = this.triggers.Length; i < count; i++)
            {
                IFilter<E>.ITrigger trigger = this.triggers[i];
                trigger.Unsubscribe(entity, this.Synchronize);
            }

            if (this.entities.Remove(entity))
                this.OnDeleted?.Invoke(entity);
        }

        private void OnTagDeleted(E entity, int tag) => this.Synchronize(entity);
        private void OnTagAdded(E entity, int _) => this.Synchronize(entity);

        private void OnValueDeleted(E entity, int key) => this.Synchronize(entity);
        private void OnValueAdded(E entity, int key) => this.Synchronize(entity);
        private void OnValueChanged(E entity, int key) => this.Synchronize(entity);

        private void Synchronize(E entity)
        {
            bool matches = this.predicate(entity);
            if (!matches && this.entities.Remove(entity)) this.OnDeleted?.Invoke(entity);
            else if (matches && this.entities.Add(entity)) this.OnAdded?.Invoke(entity);
        }
    }
}
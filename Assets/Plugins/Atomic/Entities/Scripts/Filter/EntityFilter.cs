using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="EntityFilter{E}"/> for working with <see cref="IEntity"/> collections.
    /// </summary>
    /// <remarks>
    /// This is a convenience wrapper that avoids specifying a type parameter when filtering general entities.
    /// </remarks>
    public class EntityFilter : EntityFilter<IEntity>, IReadOnlyEntityCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFilter"/> class.
        /// </summary>
        /// <param name="source">The source collection of entities to observe.</param>
        /// <param name="predicate">The predicate that determines inclusion in the filter.</param>
        /// <param name="triggers">Optional triggers for dynamic change tracking.</param>
        public EntityFilter(
            IReadOnlyEntityCollection<IEntity> source,
            Predicate<IEntity> predicate,
            params IEntityTrigger<IEntity>[] triggers)
            : base(source, predicate, triggers)
        {
        }
    }

    /// <summary>
    /// Represents a dynamic, observable filtered view over an existing <see cref="IReadOnlyEntityCollection{E}"/>.
    /// Entities are included based on a predicate and tracked using optional triggers.
    /// </summary>
    /// <typeparam name="E">The type of entity being filtered. Must implement <see cref="IEntity"/>.</typeparam>
    public class EntityFilter<E> : IReadOnlyEntityCollection<E>, IDisposable where E : IEntity
    {
        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<E> OnAdded;

        /// <inheritdoc/>
        public event Action<E> OnRemoved;

        /// <inheritdoc/>
        public int Count => this.state.Count;

        private readonly EntityCollection<E> state;

        private readonly IReadOnlyEntityCollection<E> source;
        private readonly Predicate<E> predicate;
        private readonly IEntityTrigger<E>[] triggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFilter{E}"/> class.
        /// </summary>
        /// <param name="source">The source entity collection to observe.</param>
        /// <param name="predicate">The predicate used to determine filter inclusion.</param>
        /// <param name="triggers">Optional triggers used to re-evaluate entities when their state changes.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public EntityFilter(
            IReadOnlyEntityCollection<E> source,
            Predicate<E> predicate,
            params IEntityTrigger<E>[] triggers
        )
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this.state = new EntityCollection<E>();
            this.triggers = triggers;

            for (int i = 0, count = triggers.Length; i < count; i++)
                triggers[i].SetAction(this.Synchronize);

            this.source.OnAdded += this.Observe;
            this.source.OnRemoved += this.Unobserve;

            foreach (E entity in this.source)
                this.Observe(entity);
        }

        /// <summary>
        /// Releases all subscriptions and clears internal state.
        /// </summary>
        public void Dispose()
        {
            foreach (E entity in this.source)
                this.Unobserve(entity);

            this.source.OnAdded -= this.Observe;
            this.source.OnRemoved -= this.Unobserve;
        }

        /// <inheritdoc/>
        public void CopyTo(ICollection<E> results) => this.state.CopyTo(results);

        /// <inheritdoc/>
        public void CopyTo(E[] array, int arrayIndex) => this.state.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public bool Contains(E entity) => this.state.Contains(entity);

        public EntityCollection<E>.Enumerator GetEnumerator() => this.state.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator<E> IEnumerable<E>.GetEnumerator() => this.state.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void Observe(E entity)
        {
            for (int i = 0, count = this.triggers.Length; i < count; i++)
                this.triggers[i].Track(entity);

            Debug.Log($"PASS PREDICATE {predicate(entity)} && CONTAINS {state.Contains(entity)}");
            if (this.predicate(entity) && this.state.Add(entity))
            {
                Debug.Log($"ADD TO FILTER {entity}");
                this.OnStateChanged?.Invoke();
                this.OnAdded?.Invoke(entity);
            }
        }

        private void Unobserve(E entity)
        {
            for (int i = 0, count = this.triggers.Length; i < count; i++)
                this.triggers[i].Untrack(entity);

            if (this.state.Remove(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnRemoved?.Invoke(entity);
            }
        }

        internal void Synchronize(E entity)
        {
            bool matches = this.predicate(entity);

            if (!matches && this.state.Remove(entity))
            {
                Debug.Log($"SYNC REMOVE FROM FILTER {entity}");
                this.OnStateChanged?.Invoke();
                this.OnRemoved?.Invoke(entity);
            }
            else if (matches && this.state.Add(entity))
            {
                Debug.Log($"SYNC ADD TO FILTER {entity}");
                
                this.OnStateChanged?.Invoke();
                this.OnAdded?.Invoke(entity);
            }
        }
    }
}
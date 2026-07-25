using System;
using System.Collections;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a dynamic, type-safe view (filter) over an existing entity collection,
    /// selecting entities of type <typeparamref name="T"/> from a source of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="T">The derived entity type to include in the filter.</typeparam>
    /// <typeparam name="E">The base entity type that the source collection exposes.</typeparam>
    /// <remarks>
    /// <para>
    /// The <see cref="DerivedEntityFilter{E,B}"/> observes a source collection of entities
    /// and maintains a live subset of entities that match the given <paramref name="predicate"/>.
    /// </para>
    /// <para>
    /// The filter automatically synchronizes its internal state whenever entities are
    /// added to or removed from the source, or when tracked entities trigger state changes
    /// via <see cref="IEntityTrigger{TEntity}"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="IReadOnlyEntityCollection{TEntity}"/>
    /// <seealso cref="IEntityTrigger{TEntity}"/>
    /// <seealso cref="EntityCollection{TEntity}"/>
    public class DerivedEntityFilter<T, E> : IReadOnlyEntityCollection<T>, IDisposable
        where E : IEntity
        where T : E
    {
        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<T> OnAdded;

        /// <inheritdoc/>
        public event Action<T> OnRemoved;

        /// <inheritdoc/>
        public int Count => this.state.Count;

#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        private readonly EntityCollection<T> state;
        private readonly IReadOnlyEntityCollection<E> source;
        private readonly Predicate<T> predicate;
        private readonly IEntityTrigger<T>[] triggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFilter{E}"/> class.
        /// </summary>
        /// <param name="source">The source entity collection to observe.</param>
        /// <param name="predicate">The predicate used to determine filter inclusion.</param>
        /// <param name="triggers">Optional triggers used to re-evaluate entities when their state changes.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public DerivedEntityFilter(
            IReadOnlyEntityCollection<E> source,
            Predicate<T> predicate,
            params IEntityTrigger<T>[] triggers
        )
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this.state = new EntityCollection<T>();
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
            foreach (T entity in this.source)
                this.Unobserve(entity);

            this.source.OnAdded -= this.Observe;
            this.source.OnRemoved -= this.Unobserve;
        }

        /// <inheritdoc/>
        public void CopyTo(ICollection<T> results) => this.state.CopyTo(results);

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex) => this.state.CopyTo(array, arrayIndex);

        public T this[int index] => state[index];

        public bool TryGetAt(int index, out T entity) => state.TryGetAt(index, out entity);

        /// <inheritdoc/>
        public bool Contains(T entity) => this.state.Contains(entity);

        public EntityCollection<T>.Enumerator GetEnumerator() => this.state.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.state.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void Observe(E baseEntity)
        {
            if (baseEntity is not T entity)
                return;
            
            for (int i = 0, count = this.triggers.Length; i < count; i++)
                this.triggers[i].Track(entity);

            if (this.predicate(entity) && this.state.Add(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnAdded?.Invoke(entity);
            }
        }

        private void Unobserve(E baseEntity)
        {
            if (baseEntity is not T entity)
                return;

            for (int i = 0, count = this.triggers.Length; i < count; i++)
                this.triggers[i].Untrack(entity);

            if (this.state.Remove(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnRemoved?.Invoke(entity);
            }
        }

        internal void Synchronize(T entity)
        {
            bool matches = this.predicate(entity);

            if (!matches && this.state.Remove(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnRemoved?.Invoke(entity);
            }
            else if (matches && this.state.Add(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnAdded?.Invoke(entity);
            }
        }
    }
}
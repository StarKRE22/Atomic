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
    /// selecting entities of type <typeparamref name="E"/> from a source of type <typeparamref name="B"/>.
    /// </summary>
    /// <typeparam name="E">The derived entity type to include in the filter.</typeparam>
    /// <typeparam name="B">The base entity type that the source collection exposes.</typeparam>
    /// <remarks>
    /// <para>
    /// The <see cref="TypedEntityFilter{E, B}"/> observes a source collection of entities
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
    public class TypedEntityFilter<E, B> : IReadOnlyEntityCollection<E>, IDisposable
        where B : IEntity
        where E : B
    {
        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<E> OnAdded;

        /// <inheritdoc/>
        public event Action<E> OnRemoved;

        /// <inheritdoc/>
        public int Count => this.state.Count;

#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        private readonly EntityCollection<E> state;
        private readonly IReadOnlyEntityCollection<B> source;
        private readonly Predicate<E> predicate;
        private readonly IEntityTrigger<E>[] triggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFilter{E}"/> class.
        /// </summary>
        /// <param name="source">The source entity collection to observe.</param>
        /// <param name="predicate">The predicate used to determine filter inclusion.</param>
        /// <param name="triggers">Optional triggers used to re-evaluate entities when their state changes.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public TypedEntityFilter(
            IReadOnlyEntityCollection<B> source,
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

        private void Observe(B baseEntity)
        {
            if (baseEntity is not E entity)
                return;
            
            for (int i = 0, count = this.triggers.Length; i < count; i++)
                this.triggers[i].Track(entity);

            if (this.predicate(entity) && this.state.Add(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnAdded?.Invoke(entity);
            }
        }

        private void Unobserve(B baseEntity)
        {
            if (baseEntity is not E entity)
                return;

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
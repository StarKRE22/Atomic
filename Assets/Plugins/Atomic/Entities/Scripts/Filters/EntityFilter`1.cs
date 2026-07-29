using System;
using System.Collections;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
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

#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
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

        public E this[int index] => state[index];

        public bool TryGetAt(int index, out E entity) => state.TryGetAt(index, out entity);

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

            if (this.predicate(entity) && this.state.Add(entity))
            {
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
                this.OnStateChanged?.Invoke();
                this.OnRemoved?.Invoke(entity);
            }
            else if (matches && this.state.Add(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnAdded?.Invoke(entity);
            }
        }
        
        public static Builder StartBuild() => new();

        /// <summary>
        /// Fluent builder for creating <see cref="EntityFilter{E}"/> instances.
        /// </summary>
        public struct Builder
        {
            private IReadOnlyEntityCollection<E> _source;
            private Predicate<E> _predicate;
            private List<IEntityTrigger<E>> _triggers;

            /// <summary>
            /// Assigns a source collection.
            /// </summary>
            public Builder WithSource(IReadOnlyEntityCollection<E> source)
            {
                _source = source ?? throw new ArgumentNullException(nameof(source));
                return this;
            }

            /// <summary>
            /// Assigns a filter predicate.
            /// </summary>
            public Builder WithPredicate(Predicate<E> predicate)
            {
                _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
                return this;
            }

            /// <summary>
            /// Adds a trigger.
            /// </summary>
            public Builder WithTrigger(IEntityTrigger<E> trigger)
            {
                if (trigger == null) throw new ArgumentNullException(nameof(trigger));

                _triggers ??= new List<IEntityTrigger<E>>();
                _triggers.Add(trigger);
                return this;
            }

            /// <summary>
            /// Adds multiple triggers.
            /// </summary>
            public Builder WithTriggers(params IEntityTrigger<E>[] triggers)
            {
                if (triggers == null) throw new ArgumentNullException(nameof(triggers));

                _triggers ??= new List<IEntityTrigger<E>>();
                _triggers.AddRange(triggers);
                return this;
            }

            /// <summary>
            /// Builds and returns the configured <see cref="EntityFilter{E}"/>.
            /// </summary>
            public EntityFilter<E> Build()
            {
                if (_source == null) throw new InvalidOperationException("Source must be provided.");
                if (_predicate == null) throw new InvalidOperationException("Predicate must be provided.");

                return new EntityFilter<E>(
                    _source,
                    _predicate,
                    _triggers != null ? _triggers.ToArray() : Array.Empty<IEntityTrigger<E>>()
                );
            }
        }
    }
}
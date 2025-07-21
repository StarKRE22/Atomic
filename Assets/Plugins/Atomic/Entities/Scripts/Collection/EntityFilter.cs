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
        /// <summary>
        /// Provides a mechanism for observing entity-level changes that may affect filter membership.
        /// Implementations of this interface allow the filter to stay in sync with changing entity state.
        /// </summary>
        public interface ITrigger
        {
            /// <summary>
            /// Delegate used to notify the filter that an entity's state may require re-evaluation.
            /// </summary>
            /// <param name="entity">The entity whose state changed.</param>
            public delegate void SyncAction(E entity);

            /// <summary>
            /// Subscribes to entity-specific events that might influence the filter.
            /// </summary>
            /// <param name="entity">The entity to observe.</param>
            /// <param name="syncAction">Callback to invoke when a relevant change occurs.</param>
            void Subscribe(E entity, SyncAction syncAction);

            /// <summary>
            /// Unsubscribes from previously tracked changes on the given entity.
            /// </summary>
            /// <param name="entity">The entity to stop observing.</param>
            /// <param name="syncAction">Callback that was previously registered.</param>
            void Unsubscribe(E entity, SyncAction syncAction);
        }

        /// <inheritdoc/>
        public event Action<E> OnAdded;

        /// <inheritdoc/>
        public event Action<E> OnDeleted;

        /// <inheritdoc/>
        public int Count => entities.Count;

        /// <summary>
        /// The internal set of currently matching entities.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        //TODO: NATIVE HASHSET
        private readonly HashSet<E> entities = new();

        private readonly IReadOnlyEntityCollection<E> collection;
        private readonly Predicate<E> predicate;
        private readonly ITrigger[] triggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFilter{E}"/> class.
        /// </summary>
        /// <param name="collection">The source entity collection to observe.</param>
        /// <param name="predicate">The predicate used to determine filter inclusion.</param>
        /// <param name="triggers">Optional triggers used to re-evaluate entities when their state changes.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> or <paramref name="predicate"/> is null.</exception>
        public EntityFilter(
            IReadOnlyEntityCollection<E> collection,
            Predicate<E> predicate,
            params ITrigger[] triggers
        )
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this.triggers = triggers;
            this.Initialize();
        }

        private void Initialize()
        {
            this.collection.OnAdded += this.Subscribe;
            this.collection.OnDeleted += this.Unsubscribe;

            foreach (E entity in this.collection)
                this.Subscribe(entity);
        }

        /// <summary>
        /// Releases all subscriptions and clears internal state.
        /// </summary>
        public void Dispose()
        {
            foreach (E entity in this.collection)
                this.Unsubscribe(entity);

            this.collection.OnAdded -= this.Subscribe;
            this.collection.OnDeleted -= this.Unsubscribe;
        }

        /// <inheritdoc/>
        public E[] GetAll()
        {
            E[] result = new E[this.entities.Count];
            this.entities.CopyTo(result);
            return result;
        }

        /// <inheritdoc/>
        public int GetAll(E[] results)
        {
            this.entities.CopyTo(results);
            return this.entities.Count;
        }

        /// <inheritdoc/>
        public void CopyTo(ICollection<E> results)
        {
            results.Clear();

            foreach (E entity in this.entities)
                results.Add(entity);
        }

        /// <inheritdoc/>
        public bool Has(E entity) => this.entities.Contains(entity);

        /// <inheritdoc/>
        public IEnumerator<E> GetEnumerator() => this.entities.GetEnumerator();

        /// <inheritdoc/>
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
                ITrigger trigger = this.triggers[i];
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
                ITrigger trigger = this.triggers[i];
                trigger.Unsubscribe(entity, this.Synchronize);
            }

            if (this.entities.Remove(entity))
                this.OnDeleted?.Invoke(entity);
        }

        private void OnTagDeleted(IEntity entity, int tag) => this.Synchronize((E) entity);
        private void OnTagAdded(IEntity entity, int _) => this.Synchronize((E) entity);

        private void OnValueDeleted(IEntity entity, int key) => this.Synchronize((E) entity);
        private void OnValueAdded(IEntity entity, int key) => this.Synchronize((E) entity);
        private void OnValueChanged(IEntity entity, int key) => this.Synchronize((E) entity);

        private void Synchronize(E entity)
        {
            bool matches = this.predicate(entity);

            if (!matches && this.entities.Remove(entity))
                this.OnDeleted?.Invoke(entity);
            else if (matches && this.entities.Add(entity))
                this.OnAdded?.Invoke(entity);
        }
    }

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
        /// <param name="collection">The source collection of entities to observe.</param>
        /// <param name="predicate">The predicate that determines inclusion in the filter.</param>
        /// <param name="triggers">Optional triggers for dynamic change tracking.</param>
        public EntityFilter(
            IReadOnlyEntityCollection<IEntity> collection,
            Predicate<IEntity> predicate,
            params ITrigger[] triggers)
            : base(collection, predicate, triggers)
        {
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;

// ReSharper disable VirtualMemberCallInConstructor
#endif

namespace Atomic.Entities
{
    public abstract class EntityFilterAbstract<E> : IReadOnlyEntityCollection<E>, IDisposable where E : IEntity
    {
        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<E> OnAdded;

        /// <inheritdoc/>
        public event Action<E> OnRemoved;

        /// <inheritdoc/>
        public int Count => this.entities.Count;

        /// <summary>
        /// The internal set of currently matching entities.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        //TODO: NATIVE HASHSET
        private readonly HashSet<E> entities = new();

        private readonly IReadOnlyEntityCollection<E> collection;

        protected EntityFilterAbstract(IReadOnlyEntityCollection<E> collection, Predicate<E> predicate)
        {

            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            this.collection.OnAdded += this.Inspect;
            this.collection.OnRemoved += this.Uninspect;

            foreach (E entity in this.collection)
                this.Inspect(entity);
        }


        /// <summary>
        /// Releases all subscriptions and clears internal state.
        /// </summary>
        public void Dispose()
        {
            foreach (E entity in this.collection)
                this.Uninspect(entity);

            this.collection.OnAdded -= this.Inspect;
            this.collection.OnRemoved -= this.Uninspect;
        }

        /// <inheritdoc/>
        public void CopyTo(ICollection<E> results)
        {
            results.Clear();

            foreach (E entity in this.entities)
                results.Add(entity);
        }

        /// <inheritdoc/>
        public void CopyTo(E[] array, int arrayIndex) => this.entities.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public bool Contains(E entity) => this.entities.Contains(entity);

        /// <inheritdoc/>
        public IEnumerator<E> GetEnumerator() => this.entities.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        
        protected virtual void OnInspect(E entity)
        {
        }

        protected virtual void OnUninspect(E entity)
        {
        }

        private void Inspect(E entity)
        {
            this.OnInspect(entity);
            
            if (this.predicate(entity) && this.entities.Add(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnAdded?.Invoke(entity);
            }
        }

        private void Uninspect(E entity)
        {
            this.OnUninspect(entity);
            
            if (this.entities.Remove(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnRemoved?.Invoke(entity);
            }
        }

        protected void Synchronize(E entity)
        {
            bool matches = this.predicate(entity);

            if (!matches && this.entities.Remove(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnRemoved?.Invoke(entity);
            }
            else if (matches && this.entities.Add(entity))
            {
                this.OnStateChanged?.Invoke();
                this.OnAdded?.Invoke(entity);
            }
        }
    }
}
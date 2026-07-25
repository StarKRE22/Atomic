using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only reactive collection that provides notifications
    /// when items are added, removed, or when the overall state changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public interface IReadOnlyReactiveCollection<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Occurs when the overall state of the collection changes.
        /// This can happen due to bulk operations or significant modifications.
        /// </summary>
        event Action OnStateChanged;

        /// <summary>
        /// Occurs when a new item is added to the collection.
        /// </summary>
        /// <remarks>
        /// Use this event to react to additions without iterating over the collection.
        /// </remarks>
        event Action<T> OnItemAdded;

        /// <summary>
        /// Occurs when an existing item is removed from the collection.
        /// </summary>
        /// <remarks>
        /// Use this event to react to removals without iterating over the collection.
        /// </remarks>
        event Action<T> OnItemRemoved;

        /// <summary><para>Determines whether the current collection contains a specific value.</para></summary>
        /// <param name="item">The object to locate in the current collection.</param>
        bool Contains(T item);

        /// <summary><para>Copies the elements of the current collection to a <see cref="T:System.Array" />, starting at the specified index.</para></summary>
        /// <param name="array">A one-dimensional, zero-based <see cref="T:System.Array" /> that is the destination of the elements copied from the current instance.</param>
        void CopyTo(T[] array, int arrayIndex);

        #region Subscriptions

        public readonly struct StateChangedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveCollection<T> collection;
            private readonly Action handler;

            public StateChangedSubscription(IReadOnlyReactiveCollection<T> collection, Action handler)
            {
                this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.collection.OnStateChanged += this.handler;
            }

            public void Dispose()
            {
                if (this.collection != null)
                    this.collection.OnStateChanged -= this.handler;
            }
        }

        public readonly struct ItemAddedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveCollection<T> collection;
            private readonly Action<T> handler;

            public ItemAddedSubscription(IReadOnlyReactiveCollection<T> collection, Action<T> handler)
            {
                this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.collection.OnItemAdded += this.handler;
            }

            public void Dispose()
            {
                if (this.collection != null)
                    this.collection.OnItemAdded -= this.handler;
            }
        }

        public readonly struct ItemRemovedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveCollection<T> collection;
            private readonly Action<T> handler;

            public ItemRemovedSubscription(IReadOnlyReactiveCollection<T> collection, Action<T> handler)
            {
                this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.collection.OnItemRemoved += this.handler;
            }

            public void Dispose()
            {
                if (this.collection != null)
                    this.collection.OnItemRemoved -= this.handler;
            }
        }

        #endregion
    }
}
// ReSharper disable PossibleInterfaceMemberAmbiguity
using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only reactive list that notifies subscribers when its contents change.
    /// Includes events for inserts, deletions, modifications, and state changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public interface IReadOnlyReactiveList<T> : IReadOnlyReactiveArray<T>, IReadOnlyReactiveCollection<T>
    {
        /// <summary>
        /// Event triggered when the overall state of the set changes.
        /// </summary>
        new event Action OnStateChanged;

        ///<inheritdoc cref="IReadOnlyReactiveArray{T}"/> 
        int IReadOnlyReactiveArray<T>.Length => this.Count;

        /// <summary>
        /// Event triggered when a new item is inserted at a specific index.
        /// </summary>
        new event Action<int, T> OnItemAdded;

        /// <summary>
        /// Event triggered when an item is deleted from a specific index.
        /// </summary>
        new event Action<int, T> OnItemRemoved;

        #region Subscriptions

        public new readonly struct StateChangedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveList<T> collection;
            private readonly Action handler;

            public StateChangedSubscription(IReadOnlyReactiveList<T> collection, Action handler)
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
        
        public new readonly struct ItemAddedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveList<T> list;
            private readonly Action<int, T> handler;

            public ItemAddedSubscription(IReadOnlyReactiveList<T> list, Action<int, T> handler)
            {
                this.list = list ?? throw new ArgumentNullException(nameof(list));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.list.OnItemAdded += this.handler;
            }

            public void Dispose()
            {
                if (this.list != null)
                    this.list.OnItemAdded -= this.handler;
            }
        }

        public new readonly struct ItemRemovedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveList<T> list;
            private readonly Action<int, T> handler;

            public ItemRemovedSubscription(IReadOnlyReactiveList<T> list, Action<int, T> handler)
            {
                this.list = list ?? throw new ArgumentNullException(nameof(list));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.list.OnItemRemoved += this.handler;
            }

            public void Dispose()
            {
                if (this.list != null)
                    this.list.OnItemRemoved -= this.handler;
            }
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only reactive key-value dictionary that provides notifications
    /// when items are added, removed, updated, or when the overall state changes.
    /// </summary>
    /// <typeparam name="K">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="V">The type of values in the dictionary.</typeparam>
    public interface IReadOnlyReactiveDictionary<K, V> :
        IReadOnlyDictionary<K, V>,
        IReadOnlyReactiveCollection<KeyValuePair<K, V>>
    {
        /// <summary>
        /// Occurs when a new key-value pair is added to the dictionary.
        /// </summary>
        /// <remarks>
        /// Use this event to react to newly inserted items.
        /// </remarks>
        new event Action<K, V> OnItemAdded;

        /// <summary>
        /// Occurs when a key-value pair is removed from the dictionary.
        /// </summary>
        /// <remarks>
        /// Use this event to react to deleted items.
        /// </remarks>
        new event Action<K, V> OnItemRemoved;

        /// <summary>
        /// Event triggered when an existing key's value is changed.
        /// </summary>
        event Action<K, V> OnItemChanged;

        #region Subscriptions

        public new readonly struct StateChangedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveDictionary<K, V> dictionary;
            private readonly Action handler;

            public StateChangedSubscription(IReadOnlyReactiveDictionary<K, V> dictionary, Action handler)
            {
                this.dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.dictionary.OnStateChanged += this.handler;
            }

            public void Dispose()
            {
                if (this.dictionary != null)
                    this.dictionary.OnStateChanged -= this.handler;
            }
        }

        public new readonly struct ItemAddedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveDictionary<K, V> dictionary;
            private readonly Action<K, V> handler;

            public ItemAddedSubscription(IReadOnlyReactiveDictionary<K, V> dictionary, Action<K, V> handler)
            {
                this.dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.dictionary.OnItemAdded += this.handler;
            }

            public void Dispose()
            {
                if (this.dictionary != null)
                    this.dictionary.OnItemAdded -= this.handler;
            }
        }

        public new readonly struct ItemRemovedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveDictionary<K, V> dictionary;
            private readonly Action<K, V> handler;

            public ItemRemovedSubscription(IReadOnlyReactiveDictionary<K, V> dictionary, Action<K, V> handler)
            {
                this.dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.dictionary.OnItemRemoved += this.handler;
            }

            public void Dispose()
            {
                if (this.dictionary != null)
                    this.dictionary.OnItemRemoved -= this.handler;
            }
        }

        public readonly struct ItemChangedSubscription : IDisposable
        {
            private readonly IReadOnlyReactiveDictionary<K, V> dictionary;
            private readonly Action<K, V> handler;

            public ItemChangedSubscription(IReadOnlyReactiveDictionary<K, V> dictionary, Action<K, V> handler)
            {
                this.dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
                this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

                this.dictionary.OnItemChanged += this.handler;
            }

            public void Dispose()
            {
                if (this.dictionary != null)
                    this.dictionary.OnItemChanged -= this.handler;
            }
        }

        #endregion
    }
}
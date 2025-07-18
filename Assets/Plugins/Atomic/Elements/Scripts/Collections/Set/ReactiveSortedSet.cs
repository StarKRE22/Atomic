using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive sorted set that notifies subscribers about changes such as additions,
    /// removals, and state modifications. Supports Unity serialization.
    /// </summary>
    /// <typeparam name="T">The type of elements in the set.</typeparam>
    [Serializable]
    public class ReactiveSortedSet<T> : IReactiveSet<T>,
#if UNITY_5_3_OR_NEWER
        ISerializationCallbackReceiver
#endif
    {
        /// <inheritdoc/>
        public event StateChangedHandler OnStateChanged;

        /// <summary>
        /// Event triggered when an item is added to the set.
        /// </summary>
        public event AddItemHandler<T> OnItemAdded;

        /// <summary>
        /// Event triggered when an item is removed from the set.
        /// </summary>
        public event RemoveItemHandler<T> OnItemRemoved;

        /// <summary>
        /// Event triggered specifically when the set is cleared.
        /// </summary>
        public event ClearHandler OnCleared;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => this.set.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((ICollection<T>) this.set).IsReadOnly;

        private SortedSet<T> set;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private List<T> items;

        /// <summary>
        /// Initializes a new empty <see cref="ReactiveSortedSet{T}"/>.
        /// </summary>
        public ReactiveSortedSet() =>
            this.set = new SortedSet<T>();

        /// <summary>
        /// Initializes the set with the specified elements.
        /// </summary>
        /// <param name="elements">Initial items to add to the set.</param>
        public ReactiveSortedSet(params T[] elements) =>
            this.set = new SortedSet<T>(elements);

        /// <summary>
        /// Initializes the set with a custom comparer and elements.
        /// </summary>
        /// <param name="elements">Initial items to add to the set.</param>
        /// <param name="comparer">Custom comparer for sorting.</param>
        public ReactiveSortedSet(IEnumerable<T> elements, IComparer<T> comparer) =>
            this.set = new SortedSet<T>(elements, comparer);

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => this.set.GetEnumerator();

        /// <inheritdoc/>
        public bool Add(T item)
        {
            if (this.set.Add(item))
            {
                this.OnStateChanged?.Invoke();
                this.OnItemAdded?.Invoke(item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes multiple items from the set.
        /// </summary>
        /// <param name="items">The items to remove.</param>
        public void RemoveAll(IEnumerable<T> items)
        {
            foreach (T item in items)
                this.Remove(item);
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            if (item != null && this.set.Remove(item))
            {
                this.OnStateChanged?.Invoke();
                this.OnItemRemoved?.Invoke(item);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool Contains(T item) => item != null && this.set.Contains(item);

        /// <inheritdoc/>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (this.set.Count > 0)
            {
                this.set.ExceptWith(other);
                this.OnStateChanged?.Invoke();
            }
        }

        /// <inheritdoc/>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (this.set.Count > 0)
            {
                this.set.IntersectWith(other);
                this.OnStateChanged?.Invoke();
            }
        }

        /// <inheritdoc/>
        public bool IsProperSubsetOf(IEnumerable<T> other) => this.set.IsProperSubsetOf(other);

        /// <inheritdoc/>
        public bool IsProperSupersetOf(IEnumerable<T> other) => this.set.IsProperSupersetOf(other);

        /// <inheritdoc/>
        public bool IsSubsetOf(IEnumerable<T> other) => this.set.IsSubsetOf(other);

        /// <inheritdoc/>
        public bool IsSupersetOf(IEnumerable<T> other) => this.set.IsSupersetOf(other);

        /// <inheritdoc/>
        public bool Overlaps(IEnumerable<T> other) => this.set.Overlaps(other);

        /// <inheritdoc/>
        public bool SetEquals(IEnumerable<T> other) => this.set.SetEquals(other);

        /// <inheritdoc/>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            this.set.SymmetricExceptWith(other);
            this.OnStateChanged?.Invoke();
        }

        /// <inheritdoc/>
        public void UnionWith(IEnumerable<T> other)
        {
            this.set.UnionWith(other);
            this.OnStateChanged?.Invoke();
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (this.set.Count > 0)
            {
                this.set.Clear();
                this.OnStateChanged?.Invoke();
                this.OnCleared?.Invoke();
            }
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex) => this.set.CopyTo(array, arrayIndex);

        /// <summary>
        /// Copies the elements of the set to a new array.
        /// </summary>
        /// <param name="array">The target array to fill.</param>
        public void CopyTo(T[] array) => this.set.CopyTo(array);

        /// <summary>
        /// Replaces the entire set with a single new item.
        /// </summary>
        /// <param name="other">The item to use as the new content of the set.</param>
        public void ReplaceTo(T other)
        {
            this.set.Clear();
            this.set.Add(other);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Replaces the entire set with the specified items.
        /// </summary>
        /// <param name="other">The items to add.</param>
        public void ReplaceTo(params T[] other)
        {
            this.set.Clear();
            this.set.UnionWith(other);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Replaces the entire set with the specified items.
        /// </summary>
        /// <param name="other">The items to add.</param>
        public void ReplaceTo(IEnumerable<T> other)
        {
            this.set.Clear();
            this.set.UnionWith(other);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Returns whether the set is empty.
        /// </summary>
        public bool IsEmpty() => this.set.Count == 0;

        /// <summary>
        /// Returns whether the set is not empty.
        /// </summary>
        public bool IsNotEmpty() => this.set.Count > 0;

        /// <inheritdoc/>
        void ICollection<T>.Add(T item)
        {
            if (item != null)
                this.Add(item);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Restores the internal <see cref="SortedSet{T}"/> from the serialized list after deserialization.
        /// </summary>
        public void OnAfterDeserialize()
        {
            this.set = new SortedSet<T>(this.items);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Prepares the internal data for Unity serialization by storing the current items in a list.
        /// </summary>
        public void OnBeforeSerialize() =>
            this.items = new List<T>(this.set);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_5_3_OR_NEWER
using UnityEngine;

// ReSharper disable NotResolvedInText
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A fixed-size reactive array that emits events when elements change.
    /// Provides indexed access and supports enumeration.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    [Serializable]
    public class ReactiveArray<T> : IReactiveArray<T>, IDisposable
    {
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T[] items;

        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer.GetDefault<T>();

        /// <inheritdoc/>
        public event Action<int, T> OnItemChanged;

        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public int Length => this.items.Length;

        /// <summary>
        /// Creates a new reactive array with the specified capacity.
        /// </summary>
        /// <param name="capacity">The size of the internal array. Must be non-negative.</param>
        public ReactiveArray(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        /// <summary>
        /// Creates a reactive array initialized with the given elements.
        /// </summary>
        /// <param name="elements">Elements to initialize the array with.</param>
        public ReactiveArray(params T[] elements) => this.items = elements;

        /// <summary>
        /// Creates a reactive array initialized with the given elements.
        /// </summary>
        /// <param name="elements">Elements to initialize the array with.</param>
        public ReactiveArray(IEnumerable<T> elements) => this.items = elements.ToArray();

        /// <inheritdoc cref="IReactiveArray{T}.this" />
        public T this[int index]
        {
            get { return this.items[index]; }
            set
            {
                ref T item = ref this.items[index];
                if (s_comparer.Equals(item, value))
                    return;

                item = value;
                this.OnItemChanged?.Invoke(index, value);
                this.OnStateChanged?.Invoke();
            }
        }

        /// <summary>
        /// Resets all elements in the array to their default values.
        /// </summary>
        /// <remarks>
        /// - Triggers <see cref="OnItemChanged"/> only for elements that were not already default.
        /// - Triggers <see cref="OnStateChanged"/> once at the end, regardless of changes.
        /// </remarks>
        /// <example>
        /// Clearing a reactive array of integers:
        /// <code>
        /// var array = new ReactiveArray&lt;int&gt;(1, 2, 3);
        /// array.Clear(); // All elements set to 0, OnItemChanged fired for all.
        /// </code>
        /// </example>
        public void Clear()
        {
            int length = this.items.Length;
            if (length == 0)
                return;

            for (int i = 0; i < length; i++)
            {
                ref T item = ref this.items[i];
                if (s_comparer.Equals(item, default))
                    continue;

                item = default;
                this.OnItemChanged?.Invoke(i, default);
            }

            this.OnStateChanged?.Invoke();
        }

        /// <summary><para>Determines whether the current collection contains a specific value.</para></summary>
        /// <param name="item">The object to locate in the current collection.</param>
        public bool Contains(T item)
        {
            if (item != null)
                for (int i = 0, count = this.items.Length; i < count; i++)
                    if (s_comparer.Equals(this.items[i], item))
                        return true;

            return false;
        }

        /// <summary>
        /// Copies all elements from this reactive array to the specified destination array,
        /// starting at the given index in the destination array.
        /// </summary>
        /// <param name="array">The destination array to copy elements into.</param>
        /// <param name="arrayIndex">The zero-based index in the destination array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="array"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="arrayIndex"/> is negative.</exception>
        /// <exception cref="ArgumentException">Thrown if the destination array does not have enough space to hold all elements starting at <paramref name="arrayIndex"/>.</exception>
        /// <example>
        /// <code>
        /// var array = new ReactiveArray&lt;int&gt;(1, 2, 3, 4, 5);
        /// int[] target = new int[5];
        /// array.CopyTo(target, 0); // target = [1, 2, 3, 4, 5]
        /// </code>
        /// </example>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Array index cannot be negative.");
            if (array.Length - arrayIndex < this.items.Length)
                throw new ArgumentException("The destination array is too small to contain all elements.",
                    nameof(array));

            Array.Copy(this.items, 0, array, arrayIndex, this.items.Length);
        }

        /// <summary>
        /// Copies a range of elements from this reactive array to a destination array.
        /// </summary>
        /// <param name="sourceIndex">The zero-based index in this array at which copying begins.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The zero-based index in the destination array at which storing begins.</param>
        /// <param name="length">The number of elements to copy.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="destination"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If indices or length are invalid.</exception>
        /// <exception cref="ArgumentException">If the destination array is too small to contain the copied elements.</exception>
        /// <example>
        /// <code>
        /// var array = new ReactiveArray&lt;int&gt;(1, 2, 3, 4, 5);
        /// int[] target = new int[5];
        /// array.Copy(1, target, 0, 3); // target = [2, 3, 4, 0, 0]
        /// </code>
        /// </example>
        public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (sourceIndex < 0 || destinationIndex < 0 || length < 0)
                throw new ArgumentOutOfRangeException("Indices and length must be non-negative.");
            if (sourceIndex + length > this.items.Length)
                throw new ArgumentOutOfRangeException(nameof(length), "Source range exceeds array length.");
            if (destinationIndex + length > destination.Length)
                throw new ArgumentException("Destination array is too small.");

            Array.Copy(this.items, sourceIndex, destination, destinationIndex, length);
        }

        /// <summary>
        /// Updates the contents of the reactive array with values from the specified <paramref name="newItems"/> collection.
        /// </summary>
        /// <remarks>
        /// This method works as follows:
        /// <list type="bullet">
        /// <item>Existing elements that differ from the new values are updated, triggering <see cref="OnItemChanged"/>.</item>
        /// <item>If there are more new elements than the current array length, an <see cref="ArgumentException"/> is thrown.</item>
        /// <item>If there are fewer new elements than the current array length, the remaining elements are cleared (defaulted) and <see cref="OnItemDeleted"/> is triggered for them.</item>
        /// <item>After the method completes, <see cref="OnStateChanged"/> is always invoked.</item>
        /// </list>
        /// </remarks>
        /// <param name="newItems">The collection of new items to populate the array with.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="newItems"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if the number of items in <paramref name="newItems"/> does not match the array length.</exception>
        public void Populate(IEnumerable<T> newItems)
        {
            if (newItems == null)
                throw new ArgumentNullException(nameof(newItems));

            using var enumerator = newItems.GetEnumerator();
            int index = 0;

            // Update existing elements
            while (index < this.items.Length && enumerator.MoveNext())
            {
                T newValue = enumerator.Current;
                ref T current = ref this.items[index];

                if (!s_comparer.Equals(current, newValue))
                {
                    current = newValue;
                    this.OnItemChanged?.Invoke(index, newValue);
                }

                index++;
            }

            // If there are still items in newItems but array is full — throw
            if (enumerator.MoveNext())
                throw new ArgumentException("Item count does not match array length.", nameof(newItems));

            // Clear remaining elements if newItems has fewer elements
            for (int i = index; i < this.items.Length; i++)
            {
                T removedItem = this.items[i];
                this.items[i] = default;
                this.OnItemChanged?.Invoke(i, removedItem);
            }

            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Fills the array with the specified value.
        /// </summary>
        /// <param name="value">The value to set for all elements.</param>
        /// <remarks>
        /// - Triggers <see cref="OnItemChanged"/> for each element that changes.
        /// - Triggers <see cref="OnStateChanged"/> once at the end.
        /// </remarks>
        /// <example>
        /// Filling a reactive array of integers:
        /// <code>
        /// var array = new ReactiveArray&lt;int&gt;(3);
        /// array.Fill(42); // All elements set to 42, events triggered
        /// </code>
        /// </example>
        public void Fill(T value)
        {
            for (int i = 0, length = this.items.Length; i < length; i++)
            {
                ref T current = ref this.items[i];
                if (!s_comparer.Equals(current, value))
                {
                    current = value;
                    this.OnItemChanged?.Invoke(i, value);
                }
            }

            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Resizes the array to the specified new size.
        /// </summary>
        /// <param name="newSize">The new length of the array. Must be non-negative.</param>
        /// <remarks>
        /// - If the new size is greater than the current size, new elements are initialized with default(T).  
        /// - If the new size is smaller, excess elements are discarded.  
        /// - Triggers <see cref="OnItemChanged"/> for all changed or new elements.  
        /// - Triggers <see cref="OnStateChanged"/> once at the end.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="newSize"/> is negative.</exception>
        public void Resize(int newSize)
        {
            if (newSize < 0)
                throw new ArgumentOutOfRangeException(nameof(newSize));

            if (newSize == this.items.Length)
                return;

            T[] newItems = new T[newSize];
            int minLength = Math.Min(newSize, this.items.Length);

            for (int i = 0; i < minLength; i++)
                newItems[i] = this.items[i];

            this.items = newItems;

            for (int i = minLength; i < newSize; i++)
                this.OnItemChanged?.Invoke(i, default);

            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Returns a struct-based enumerator for the array.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        /// <summary>
        /// Disposes this array and clears all event subscriptions.
        /// </summary>
        public void Dispose()
        {
            this.OnItemChanged = null;
            this.OnStateChanged = null;
        }

        /// <summary>
        /// A lightweight enumerator for <see cref="ReactiveArray{T}"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReactiveArray<T> _array;
            private int _index;
            private T _current;

            /// <inheritdoc/>
            public T Current => _current;

            object IEnumerator.Current => _current;

            internal Enumerator(ReactiveArray<T> array)
            {
                _array = array;
                _index = -1;
                _current = default;
            }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                int next = _index + 1;
                if (next >= _array.items.Length)
                    return false;

                _index = next;
                _current = _array.items[_index];
                return true;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                _index = -1;
                _current = default;
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                // Nothing to dispose.
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
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

        private static readonly IEqualityComparer<T> s_equalityComparer = EqualityComparer.GetDefault<T>();

        /// <inheritdoc/>
        public event ChangeItemHandler<T> OnItemChanged;

        /// <inheritdoc/>
        public event StateChangedHandler OnStateChanged;

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

        /// <inheritdoc cref="IReactiveArray{T}.this" />
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.items.Length)
                    throw new IndexOutOfRangeException($"Index {index} is out of bounds.");

                return this.items[index];
            }
            set
            {
                if (index < 0 || index >= this.items.Length)
                    throw new IndexOutOfRangeException($"Index {index} is out of bounds.");
                
                ref T current = ref this.items[index];

                if (s_equalityComparer.Equals(current, value))
                    return;

                current = value;
                this.OnStateChanged?.Invoke();
                this.OnItemChanged?.Invoke(index, value);
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
                if (s_equalityComparer.Equals(item, default)) 
                    continue;
                
                item = default;
                this.OnItemChanged?.Invoke(i, default);
            }

            this.OnStateChanged?.Invoke();
        }
        
        /// <summary>
        /// Replaces all elements in the array with values from the provided sequence.
        /// Fires <see cref="OnItemChanged"/> for each changed item,
        /// and <see cref="OnStateChanged"/> once at the end.
        /// </summary>
        /// <param name="newItems">The new values to assign. Must match the array length.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="newItems"/> is null.</exception>
        /// <exception cref="ArgumentException">If <paramref name="newItems"/> has a different number of items than the array.</exception>
        public void SetAll(IEnumerable<T> newItems)
        {
            if (newItems == null)
                throw new ArgumentNullException(nameof(newItems));

            using var enumerator = newItems.GetEnumerator();
            int index = 0;

            while (index < this.items.Length && enumerator.MoveNext())
            {
                T newValue = enumerator.Current;
                ref T current = ref this.items[index];

                if (!s_equalityComparer.Equals(current, newValue))
                {
                    current = newValue;
                    this.OnItemChanged?.Invoke(index, newValue);
                }

                index++;
            }

            if (index != this.items.Length || enumerator.MoveNext())
                throw new ArgumentException("Item count does not match array length.", nameof(newItems));

            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Returns a struct-based enumerator for the array.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

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

            public Enumerator(ReactiveArray<T> array)
            {
                _array = array;
                _index = -1;
                _current = default;
            }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                if (_index + 1 == _array.Length)
                    return false;

                _current = _array[++_index];
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

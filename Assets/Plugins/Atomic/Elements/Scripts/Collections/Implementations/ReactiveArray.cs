using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField]
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
            get => this.items[index];
            set
            {
                ref T current = ref this.items[index];

                if (s_equalityComparer.Equals(current, value))
                    return;

                current = value;
                this.OnStateChanged?.Invoke();
                this.OnItemChanged?.Invoke(index, value);
            }
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
            InternalUtils.Dispose(ref this.OnItemChanged);
            InternalUtils.Dispose(ref this.OnStateChanged);
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

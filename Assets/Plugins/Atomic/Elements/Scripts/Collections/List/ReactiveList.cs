using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive, resizable list that triggers events when items are added, removed,
    /// changed, or when the state changes as a whole. Supports manual memory pooling for temporary buffers.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [Serializable]
    public class ReactiveList<T> : IReactiveList<T>, IDisposable
    {
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T[] items;

        private static readonly IEqualityComparer<T> s_equalityComparer = EqualityComparer.GetDefault<T>();
        private static readonly ArrayPool<T> s_arrayPool = ArrayPool<T>.Shared;

        /// <inheritdoc/>
        public event StateChangedHandler OnStateChanged;

        /// <inheritdoc/>
        public event ChangeItemHandler<T> OnItemChanged;

        /// <inheritdoc/>
        public event InsertItemHandler<T> OnItemInserted;

        /// <inheritdoc/>
        public event DeleteItemHandler<T> OnItemDeleted;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => this.count;

        /// <summary>
        /// Gets the current internal array capacity.
        /// </summary>
        public int Capacity => items.Length;

        private int count;

        /// <summary>
        /// Initializes an empty list with the given initial capacity.
        /// </summary>
        /// <param name="capacity">Initial number of allocated elements.</param>
        public ReactiveList(int capacity = 0)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
            this.count = 0;
        }

        /// <summary>
        /// Initializes the list with the given items.
        /// </summary>
        public ReactiveList(params T[] items)
        {
            this.items = items;
            this.count = this.items.Length;
        }

        /// <summary>
        /// Initializes the list with a copy of the given enumerable.
        /// </summary>
        public ReactiveList(IEnumerable<T> items)
        {
            this.items = items.ToArray();
            this.count = this.items.Length;
        }

        /// <inheritdoc cref="IList{T}.this" />
        public T this[int index]
        {
            get => this.items[index];
            set => this.Set(index, value);
        }

        /// <inheritdoc/>
        public void Add(T item)
        {
            int index = this.count;
            if (index == this.items.Length)
                this.IncreaseCapacity();

            this.items[index] = item;
            this.count++;

            this.OnItemInserted?.Invoke(index, item);
            this.OnStateChanged?.Invoke();
        }

        /// <inheritdoc/>
        public void Clear()
        {
            int count = this.count;
            if (count == 0)
                return;

            this.count = 0;

            T[] buffer = s_arrayPool.Rent(count);
            Array.Copy(this.items, buffer, count);

            try
            {
                for (int i = 0; i < count; i++)
                    this.OnItemDeleted?.Invoke(i, buffer[i]);
            }
            finally
            {
                s_arrayPool.Return(buffer);
            }

            this.OnStateChanged?.Invoke();
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            for (int i = 0; i < this.count; i++)
                if (s_equalityComparer.Equals(this.items[i], item))
                    return true;

            return false;
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (!s_equalityComparer.Equals(this.items[i], item))
                    continue;

                this.count--;

                for (int j = i; j < this.count; j++)
                    this.items[j] = this.items[j + 1];

                this.OnItemDeleted?.Invoke(i, item);
                this.OnStateChanged?.Invoke();
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException($"Index {index} out of range!");

            T item = this.items[index];
            this.count--;

            for (int j = index; j < this.count; j++)
                this.items[j] = this.items[j + 1];

            this.OnItemDeleted?.Invoke(index, item);
            this.OnStateChanged?.Invoke();
        }

        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            for (int i = 0; i < this.count; i++)
                if (s_equalityComparer.Equals(this.items[i], item))
                    return i;

            return -1;
        }

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > this.count)
                throw new IndexOutOfRangeException($"Index {index} out of range!");

            if (this.count == this.items.Length)
                this.IncreaseCapacity();

            for (int i = this.count; i > index; i--)
                this.items[i] = this.items[i - 1];

            this.items[index] = item;
            this.count++;

            this.OnItemInserted?.Invoke(index, item);
            this.OnStateChanged?.Invoke();
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex = 0) =>
            Array.Copy(this.items, 0, array, arrayIndex, this.count);

        public Enumerator GetEnumerator() => new(this);

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Clear();
            this.OnItemChanged = null;
            this.OnStateChanged = null;
        }

        /// <summary>
        /// Increases the internal array capacity (usually doubling the current capacity).
        /// </summary>
        private void IncreaseCapacity()
        {
            int capacity = this.items.Length;
            int newCapacity = capacity == 0 ? 1 : capacity * 2;

            if ((uint) newCapacity > int.MaxValue)
                newCapacity = int.MaxValue;

            Array.Resize(ref this.items, newCapacity);
        }

        /// <summary>
        /// Sets the value at the specified index and triggers events if the value changed.
        /// </summary>
        private void Set(int index, T value)
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException($"Index {index} out of range!");

            if (s_equalityComparer.Equals(this.items[index], value))
                return;

            this.items[index] = value;
            this.OnItemChanged?.Invoke(index, value);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Internal enumerator used for foreach iteration.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReactiveList<T> _list;
            private int _index;
            private T _current;

            public T Current => _current;
            object IEnumerator.Current => _current;

            public Enumerator(ReactiveList<T> list)
            {
                _list = list;
                _index = -1;
                _current = default;
            }

            public bool MoveNext()
            {
                if (++_index >= _list.count)
                    return false;

                _current = _list.items[_index];
                return true;
            }

            public void Reset()
            {
                _index = -1;
                _current = default;
            }

            public void Dispose()
            {
            }
        }
    }
}
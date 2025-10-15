using System;
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
    public partial class ReactiveList<T> : IReactiveList<T>, IDisposable
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer.GetDefault<T>();

        /// <inheritdoc cref="IReactiveList{T}" />
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<int, T> OnItemAdded;

        /// <inheritdoc/>
        public event Action<int, T> OnItemRemoved;

        /// <inheritdoc/>
        public event Action<int, T> OnItemChanged;

        /// <inheritdoc/>
        event Action<T> IReadOnlyReactiveCollection<T>.OnItemAdded
        {
            add => this.onItemAdded += value;
            remove => this.onItemRemoved -= value;
        }

        /// <inheritdoc/>
        event Action<T> IReadOnlyReactiveCollection<T>.OnItemRemoved
        {
            add => this.onItemRemoved += value;
            remove => this.onItemRemoved -= value;
        }

        private event Action<T> onItemAdded;

        private event Action<T> onItemRemoved;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => this.count;

        /// <summary>
        /// Gets the current internal array capacity.
        /// </summary>
        public int Capacity => this.items.Length;

        private T[] items;
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
            this.items = items ?? Array.Empty<T>();
            this.count = this.items.Length;
        }

        /// <summary>
        /// Initializes the list with a copy of the given enumerable.
        /// </summary>
        public ReactiveList(IEnumerable<T> items)
        {
            this.items = items == null ? Array.Empty<T>() : items.ToArray();
            this.count = this.items.Length;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Clear();

            this.onItemAdded = null;
            this.onItemRemoved = null;
            this.OnItemAdded = null;
            this.OnItemRemoved = null;
            this.OnItemChanged = null;
            this.OnStateChanged = null;
        }

        /// <inheritdoc cref="IList{T}.this" />
        public T this[int index]
        {
            get
            {
                return index < 0 || index >= this.count
                    ? throw new IndexOutOfRangeException($"Index {index} out of range!")
                    : this.items[index];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (index < 0 || index >= this.count)
                    throw new IndexOutOfRangeException($"Index {index} out of range!");

                if (s_comparer.Equals(this.items[index], value))
                    return;

                this.items[index] = value;
                this.OnItemChanged?.Invoke(index, value);
                this.OnStateChanged?.Invoke();
            }
        }

        /// <inheritdoc/>
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            int index = this.count;
            if (index == this.items.Length)
            {
                //Resize array
                int newCapacity = this.items.Length == 0 ? 1 : this.items.Length * 2;
                if (newCapacity < 0)
                    newCapacity = int.MaxValue;

                Array.Resize(ref this.items, newCapacity);
            }

            this.items[index] = item;
            this.count++;

            this.OnItemAdded?.Invoke(index, item);
            this.onItemAdded?.Invoke(item);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Adds a range of items to the end of the list efficiently.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="items"/> is <c>null</c>.</exception>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (items is ICollection<T> collection)
            {
                //Ensure capacity
                int requiredCapacity = this.count + collection.Count;
                if (requiredCapacity > this.items.Length)
                {
                    int newCapacity = Math.Max(this.items.Length * 2, requiredCapacity);
                    Array.Resize(ref this.items, newCapacity);
                }
            }

            int initialCount = this.count;

            foreach (T item in items)
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                if (this.count == this.items.Length)
                {
                    // Expand array size
                    int newCapacity = this.items.Length == 0 ? 1 : this.items.Length * 2;
                    if (newCapacity < 0)
                        newCapacity = int.MaxValue;

                    Array.Resize(ref this.items, newCapacity);
                }

                this.items[this.count++] = item;
                this.OnItemAdded?.Invoke(this.count - 1, item);
                this.onItemAdded?.Invoke(item);
            }

            if (this.count > initialCount)
                this.OnStateChanged?.Invoke();
        }

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (index < 0 || index > this.count)
                throw new IndexOutOfRangeException($"Index {index} out of range!");

            if (this.count == this.items.Length)
            {
                // Expand array size
                int newCapacity = this.items.Length == 0 ? 1 : this.items.Length * 2;
                if (newCapacity < 0)
                    newCapacity = int.MaxValue;

                Array.Resize(ref this.items, newCapacity);
            }

            if (index < this.count)
                Array.Copy(this.items, index, this.items, index + 1, this.count - index);

            this.items[index] = item;
            this.count++;

            this.OnItemAdded?.Invoke(index, item);
            this.onItemAdded?.Invoke(item);
            this.OnStateChanged?.Invoke();
        }

        /// <summary><para>Determines whether the current collection contains a specific value.</para></summary>
        /// <param name="item">The object to locate in the current collection.</param>
        public bool Contains(T item)
        {
            if (item != null)
                for (int i = 0; i < this.count; i++)
                    if (s_comparer.Equals(this.items[i], item))
                        return true;

            return false;
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            if (item != null)
            {
                for (int i = 0; i < this.count; i++)
                {
                    if (!s_comparer.Equals(this.items[i], item))
                        continue;

                    this.count--;

                    if (i < this.count)
                        Array.Copy(this.items, i + 1, this.items, i, this.count - i);

                    this.OnItemRemoved?.Invoke(i, item);
                    this.onItemRemoved?.Invoke(item);
                    this.OnStateChanged?.Invoke();
                    return true;
                }
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

            if (index < this.count)
                Array.Copy(this.items, index + 1, this.items, index, this.count - index);

            this.OnItemRemoved?.Invoke(index, item);
            this.onItemRemoved?.Invoke(item);
            this.OnStateChanged?.Invoke();
        }

        public void Clear()
        {
            int count = this.count;
            if (count == 0)
                return;

            this.count = 0;
            this.OnStateChanged?.Invoke();

            for (int i = 0; i < count; i++)
            {
                T item = this.items[i];
                this.OnItemRemoved?.Invoke(i, item);
                this.onItemRemoved?.Invoke(item);
            }
        }

        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            if (item != null)
                for (int i = 0; i < this.count; i++)
                    if (s_comparer.Equals(this.items[i], item))
                        return i;

            return -1;
        }

        /// <inheritdoc cref="IReadOnlyReactiveList{T}" />
        public void CopyTo(T[] array, int arrayIndex = 0) =>
            Array.Copy(this.items, 0, array, arrayIndex, this.count);

        /// <inheritdoc cref="IReadOnlyReactiveList{T}" />
        public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length) =>
            Array.Copy(this.items, sourceIndex, destination, destinationIndex, length);

        /// <summary>
        /// Updates the contents of the list with the values from the specified <paramref name="newItems"/> collection.
        /// </summary>
        /// <remarks>
        /// The method works as follows:
        /// <list type="bullet">
        /// <item>Existing elements that differ from the new values are updated, triggering <see cref="OnItemChanged"/>.</item>
        /// <item>If there are more new elements than the current list, the additional elements are added, triggering <see cref="OnItemAdded"/>.</item>
        /// <item>If there are fewer new elements than the current list, the excess elements are removed, triggering <see cref="OnItemRemoved"/>.</item>
        /// <item>After the method completes, <see cref="OnStateChanged"/> is always invoked.</item>
        /// </list>
        /// </remarks>
        /// <param name="newItems">The collection of new items to populate the list with.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="newItems"/> is <c>null</c>.</exception>
        public void Populate(IEnumerable<T> newItems)
        {
            if (newItems == null)
                throw new ArgumentNullException(nameof(newItems));

            using var enumerator = newItems.GetEnumerator();
            int index = 0;

            while (index < this.count && enumerator.MoveNext())
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

            while (enumerator.MoveNext())
                this.Add(enumerator.Current);

            while (index < this.count)
            {
                T removedItem = this.items[index];
                this.count--;
                for (int j = index; j < this.count; j++)
                    this.items[j] = this.items[j + 1];

                this.OnItemRemoved?.Invoke(index, removedItem);
                this.onItemRemoved?.Invoke(removedItem);
            }

            this.OnStateChanged?.Invoke();
        }

        public Enumerator GetEnumerator() => new(this);

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

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

            internal Enumerator(ReactiveList<T> list)
            {
                _list = list;
                _index = -1;
                _current = default;
            }

            public bool MoveNext()
            {
                int next = _index + 1;
                if (next >= _list.count)
                    return false;

                _index = next;
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
    
#if UNITY_5_3_OR_NEWER
    public partial class ReactiveList<T> : ISerializationCallbackReceiver
    {
        [SerializeField]
        internal T[] serializedItems;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (this.serializedItems != null) 
                this.Populate(this.serializedItems);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.serializedItems = this.ToArray();
        }
    }
#endif
}
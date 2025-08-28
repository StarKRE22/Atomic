using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a singly linked list of elements of type <typeparamref name="T"/>.
    /// Provides methods to add, remove, search and enumerate items.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the list.</typeparam>
    public class LinkedList<T> : IList<T>
    {
        protected const int INITIAL_CAPACITY = 1;
        private const int UNDEFINED_INDEX = -1;
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer.GetDefault<T>();

        private struct Node
        {
            public T item;
            public int next;
        }

        private Node[] _nodes;
        private int _head;
        private int _tail;
        private int _freeIndex;
        private int _count;

        /// <summary>
        /// Gets the number of elements contained in the list.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Always returns <c>false</c>, as the list is not read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Initializes a new, empty instance of the <see cref="LinkedList{T}"/> class
        /// with the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the internal node storage.</param>
        public LinkedList(int capacity = INITIAL_CAPACITY)
        {
            _nodes = new Node[Math.Max(capacity, INITIAL_CAPACITY)];
            _head = UNDEFINED_INDEX;
            _tail = UNDEFINED_INDEX;
            _freeIndex = 0;
            _count = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedList{T}"/> class
        /// containing the specified items.
        /// </summary>
        /// <param name="items">An array of items to initialize the list with.</param>
        public LinkedList(params T[] items) : this(items.Length)
        {
            for (int i = 0, count = items.Length; i < count; i++)
                this.Add(items[i]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedList{T}"/> class
        /// containing elements copied from the specified enumerable.
        /// </summary>
        /// <param name="items">The enumerable collection whose elements are copied to the list.</param>
        public LinkedList(IEnumerable<T> items) : this(items.Count())
        {
            foreach (T item in items)
                this.Add(item);
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.
        /// </exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                int current = _head;
                for (int i = 0; i < index; i++)
                    current = _nodes[current].next;

                return _nodes[current].item;
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                int current = _head;
                for (int i = 0; i < index; i++)
                    current = _nodes[current].next;

                _nodes[current].item = value;
            }
        }

        /// <summary>
        /// Adds an object to the end of the list.
        /// </summary>
        /// <param name="item">The object to add.</param>
        public void Add(T item)
        {
            if (item == null)
                return;

            if (_freeIndex == _nodes.Length)
            {
                int newCapacity = _nodes.Length * 2;
                if (newCapacity < 0) newCapacity = int.MaxValue;
                Array.Resize(ref _nodes, newCapacity);
            }

            _nodes[_freeIndex] = new Node
            {
                item = item,
                next = UNDEFINED_INDEX
            };

            if (_tail != UNDEFINED_INDEX)
                _nodes[_tail].next = _freeIndex;

            _tail = _freeIndex;

            if (_head == UNDEFINED_INDEX)
                _head = _freeIndex;

            _freeIndex++;
            _count++;
        }

        /// <summary>
        /// Inserts an element into the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="index"/> is less than 0 or greater than <see cref="Count"/>.
        /// </exception>
        public void Insert(int index, T item)
        {
            if (item == null)
                return;

            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and Count.");

            if (_freeIndex == _nodes.Length)
            {
                int newCapacity = _nodes.Length * 2;
                if (newCapacity < 0) newCapacity = int.MaxValue;
                Array.Resize(ref _nodes, newCapacity);
            }

            int nodeIndex = _freeIndex;
            _nodes[nodeIndex] = new Node {item = item, next = UNDEFINED_INDEX};
            _freeIndex++;

            if (index == 0) // insert at head
            {
                _nodes[nodeIndex].next = _head;
                _head = nodeIndex;
                if (_tail == UNDEFINED_INDEX)
                    _tail = nodeIndex;
            }
            else
            {
                // find node at UNDEFINED_INDEX
                int prev = _head;
                for (int i = 0; i < index - 1; i++)
                    prev = _nodes[prev].next;

                _nodes[nodeIndex].next = _nodes[prev].next;
                _nodes[prev].next = nodeIndex;

                if (_nodes[nodeIndex].next == UNDEFINED_INDEX) // new tail
                    _tail = nodeIndex;
            }

            _count++;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the list.
        /// </summary>
        /// <param name="item">The object to remove from the list.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully removed; otherwise <c>false</c>.</returns>
        public bool Remove(T item)
        {
            if (_head == UNDEFINED_INDEX || item == null)
                return false;

            int current = _head;
            int prev = UNDEFINED_INDEX;

            while (current != UNDEFINED_INDEX)
            {
                if (s_comparer.Equals(_nodes[current].item, item))
                {
                    if (prev != UNDEFINED_INDEX)
                        _nodes[prev].next = _nodes[current].next;
                    else
                        _head = _nodes[current].next;

                    if (current == _tail)
                        _tail = prev;

                    _nodes[current] = default;
                    _count--;
                    return true;
                }

                prev = current;
                current = _nodes[current].next;
            }

            return false;
        }

        /// <summary>
        /// Returns the zero-based index of the first occurrence of a specific object in the list.
        /// </summary>
        /// <param name="item">The object to locate.</param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item)
        {
            if (item == null) return -1;

            int index = 0;
            int current = _head;
            while (current != UNDEFINED_INDEX)
            {
                if (s_comparer.Equals(_nodes[current].item, item))
                    return index;

                current = _nodes[current].next;
                index++;
            }

            return -1;
        }

        /// <summary>
        /// Removes the element at the specified index of the list.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.
        /// </exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Index must be within the bounds of the collection.");

            if (index == 0) // remove head
            {
                _head = _nodes[_head].next;
                if (_head == UNDEFINED_INDEX) //list is empty
                    _tail = UNDEFINED_INDEX;
            }
            else
            {
                int prev = _head;
                for (int i = 0; i < index - 1; i++)
                    prev = _nodes[prev].next;

                int toRemove = _nodes[prev].next;
                _nodes[prev].next = _nodes[toRemove].next;

                if (_nodes[prev].next == UNDEFINED_INDEX) //removed tail
                    _tail = prev;
            }

            _count--;
        }

        /// <summary>
        /// Determines whether the list contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the list.</param>
        /// <returns><c>true</c> if <paramref name="item"/> is found; otherwise <c>false</c>.</returns>
        public bool Contains(T item)
        {
            if (item != null)
            {
                int current = _head;
                while (current != UNDEFINED_INDEX)
                {
                    if (s_comparer.Equals(_nodes[current].item, item)) return true;
                    current = _nodes[current].next;
                }
            }

            return false;
        }

        /// <summary>
        /// Copies the elements of the list to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the copied elements.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">If the target array is too small.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            
            if (arrayIndex + _count > array.Length)
                throw new ArgumentException("The target array is too small to hold the elements.");

            int current = _head;
            while (current != UNDEFINED_INDEX)
            {
                array[arrayIndex++] = _nodes[current].item;
                current = _nodes[current].next;
            }
        }

        /// <summary>
        /// Removes all elements from the list.
        /// </summary>
        public void Clear()
        {
            _head = UNDEFINED_INDEX;
            _tail = UNDEFINED_INDEX;
            _freeIndex = 0;
            _count = 0;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Enumerates the elements of a <see cref="LinkedList{T}"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            public T Current => _current;

            object IEnumerator.Current => _current;
            
            private readonly LinkedList<T> _list;
            private int _index;
            private T _current;

            internal Enumerator(LinkedList<T> list)
            {
                _list = list;
                _index = list._head;
                _current = default;
            }

            /// <summary>
            /// Advances the enumerator to the next element of the list.
            /// </summary>
            /// <returns><c>true</c> if the enumerator was successfully advanced; <c>false</c> if the end is reached.</returns>
            public bool MoveNext()
            {
                if (_index == UNDEFINED_INDEX)
                    return false;

                ref readonly var node = ref _list._nodes[_index];
                _current = node.item;
                _index = node.next;
                return true;
            }

            /// <summary>
            /// Sets the enumerator to its initial position.
            /// </summary>
            public void Reset()
            {
                _index = _list._head;
                _current = default;
            }

            
            /// <summary>
            /// Performs application-defined tasks associated with freeing resources.
            /// </summary>
            public void Dispose()
            {
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive linked list collection that notifies about changes
    /// to its elements. Supports fast insertions at the head and tail,
    /// and maintains a free-list of removed nodes for efficient reuse.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the list.</typeparam>
    public partial class ReactiveLinkedList<T> : IReactiveList<T>, IDisposable
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer<T>.Default;
      
        private const int UNDEFINED_INDEX = -1;
        
        protected const int INITIAL_CAPACITY = 4;

        /// <summary>
        /// Occurs when the state of the list changes (e.g., add, remove, clear).
        /// </summary>
        public event Action OnStateChanged;

        /// <summary>
        /// Occurs when an existing item is replaced or modified.
        /// </summary>
        public event Action<int, T> OnItemChanged;

        /// <summary>
        /// Occurs when a new item is inserted into the list.
        /// </summary>
        public event Action<int, T> OnItemAdded;
        
        /// <summary>
        /// Occurs when an item is removed from the list.
        /// </summary>
        public event Action<int, T> OnItemRemoved;
        
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
        
        /// <summary>
        /// Gets the number of elements contained in the list.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gets a value indicating whether the list is read-only.
        /// Always returns false.
        /// </summary>
        public bool IsReadOnly => false;

        private struct Node
        {
            public T item;
            public int next;
        }

        private Node[] _nodes;
        private int _head;
        private int _tail;
        private int _count;
        private int _freeList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveLinkedList{T}"/> class
        /// with the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the list.</param>
        public ReactiveLinkedList(int capacity = INITIAL_CAPACITY)
        {
            _nodes = new Node[Math.Max(capacity, INITIAL_CAPACITY)];
            _head = _tail = UNDEFINED_INDEX;
            _count = 0;
            _freeList = UNDEFINED_INDEX;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveLinkedList{T}"/> class
        /// and adds the specified items.
        /// </summary>
        /// <param name="items">The items to add to the list.</param>
        public ReactiveLinkedList(params T[] items) : this(items.Length)
        {
            foreach (T item in items) Add(item);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveLinkedList{T}"/> class
        /// and adds items from an enumerable collection.
        /// </summary>
        /// <param name="items">The collection of items to add.</param>
        public ReactiveLinkedList(IEnumerable<T> items) : this(items.Count())
        {
            foreach (T item in items) Add(item);
        }

        /// <summary>
        /// Releases all resources used by the list and clears its content.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the element at the specified index in the list.
        /// </summary>
        /// <param name="index">The zero-based index of the element.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is invalid.</exception>
        public T this[int index]
        {
            get
            {
                int node = GetNodeIndex(index);
                return _nodes[node].item;
            }
            set
            {
                int node = GetNodeIndex(index);
                if (!s_comparer.Equals(_nodes[node].item, value))
                {
                    _nodes[node].item = value;
                    OnItemChanged?.Invoke(index, value);
                    OnStateChanged?.Invoke();
                }
            }
        }

        /// <summary>
        /// Adds an item to the end of the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            if (item == null) return;

            int nodeIndex = AllocateNode();
            _nodes[nodeIndex] = new Node {item = item, next = UNDEFINED_INDEX};

            if (_tail != UNDEFINED_INDEX)
                _nodes[_tail].next = nodeIndex;

            _tail = nodeIndex;
            if (_head == UNDEFINED_INDEX)
                _head = nodeIndex;

            _count++;
            OnItemAdded?.Invoke(_count - 1, item);
            onItemAdded?.Invoke(item);
            OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Adds multiple items to the end of the list.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="items"/> is null.</exception>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
                Add(item);
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index where the item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is invalid.</exception>
        public void Insert(int index, T item)
        {
            if (item == null) return;
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            int nodeIndex = AllocateNode();
            _nodes[nodeIndex] = new Node {item = item, next = UNDEFINED_INDEX};

            if (index == 0)
            {
                _nodes[nodeIndex].next = _head;
                _head = nodeIndex;
                if (_tail == UNDEFINED_INDEX)
                    _tail = nodeIndex;
            }
            else
            {
                int prev = GetNodeIndex(index - 1);
                _nodes[nodeIndex].next = _nodes[prev].next;
                _nodes[prev].next = nodeIndex;
                if (_nodes[nodeIndex].next == UNDEFINED_INDEX)
                    _tail = nodeIndex;
            }

            _count++;
            OnItemAdded?.Invoke(index, item);
            onItemAdded?.Invoke(item);
            OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Removes the first occurrence of a specific item from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was successfully removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            if (_head == UNDEFINED_INDEX || item == null) return false;

            int current = _head;
            int prev = UNDEFINED_INDEX;
            int idx = 0;

            while (current != UNDEFINED_INDEX)
            {
                if (s_comparer.Equals(_nodes[current].item, item))
                {
                    RemoveNode(current, prev, idx);
                    return true;
                }

                prev = current;
                current = _nodes[current].next;
                idx++;
            }

            return false;
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            int current = GetNodeIndex(index);
            int prev = index == 0 ? UNDEFINED_INDEX : GetNodeIndex(index - 1);
            RemoveNode(current, prev, index);
        }


        /// <summary>
        /// Determines the index of a specific item in the list.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        /// <returns>The index of the item if found; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            if (item == null) return -1;

            int idx = 0;
            int current = _head;
            while (current != UNDEFINED_INDEX)
            {
                if (s_comparer.Equals(_nodes[current].item, item))
                    return idx;

                current = _nodes[current].next;
                idx++;
            }

            return -1;
        }

        /// <summary>
        /// Determines whether the list contains a specific value.
        /// </summary>
        /// <param name="item">The item to locate in the list.</param>
        /// <returns>True if the item exists; otherwise, false.</returns>
        public bool Contains(T item) => IndexOf(item) >= 0;

        /// <summary>
        /// Copies the elements of the list to a specified array, starting at a particular index.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="arrayIndex">The starting index in the destination array.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) 
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (arrayIndex + _count > array.Length) 
                throw new ArgumentException("Array too small");

            int current = _head;
            while (current != UNDEFINED_INDEX)
            {
                array[arrayIndex++] = _nodes[current].item;
                current = _nodes[current].next;
            }
        }

        /// <summary>
        /// Copies a range of elements from the list to a specified array.
        /// </summary>
        /// <param name="sourceIndex">The zero-based index in the list at which copying begins.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The zero-based index in the destination array at which storing begins.</param>
        /// <param name="length">The number of elements to copy.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="destination"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="sourceIndex"/>, <paramref name="destinationIndex"/>, or <paramref name="length"/> is negative.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="sourceIndex"/> + <paramref name="length"/> exceeds the list count,
        /// or if <paramref name="destinationIndex"/> + <paramref name="length"/> exceeds the destination array length.
        /// </exception>
        public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (sourceIndex < 0 || sourceIndex >= _count)
                throw new ArgumentOutOfRangeException(nameof(sourceIndex));
            if (destinationIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(destinationIndex));
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (sourceIndex + length > _count)
                throw new ArgumentException("Source range exceeds list length");
            if (destinationIndex + length > destination.Length)
                throw new ArgumentException("Destination array too small");

            int current = _head;
            int skipped = 0;

            // Skip nodes until reaching sourceIndex
            while (skipped < sourceIndex)
            {
                current = _nodes[current].next;
                skipped++;
            }

            // Copy the requested number of elements
            for (int i = 0; i < length; i++)
            {
                destination[destinationIndex + i] = _nodes[current].item;
                current = _nodes[current].next;
            }
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void Clear()
        {
            int current = _head;
            while (current != UNDEFINED_INDEX)
            {
                ref Node node = ref _nodes[current];
                int next = node.next;
                T removed = node.item;
                
                node.item = default;
                node.next = _freeList;
                
                _freeList = current;
                
                OnItemRemoved?.Invoke(current, removed);
                onItemRemoved?.Invoke(removed);

                current = next;
            }

            _head = _tail = UNDEFINED_INDEX;
            _count = 0;
            OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An enumerator for the list.</returns>
        public Enumerator GetEnumerator() => new(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
      
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Enumerates the elements of a <see cref="ReactiveLinkedList{T}"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReactiveLinkedList<T> _list;
            private int _index;
            private T _current;

            internal Enumerator(ReactiveLinkedList<T> list)
            {
                _list = list;
                _index = list._head;
                _current = default;
            }

            /// <summary>Gets the element at the current position of the enumerator.</summary>
            public T Current => _current;

            object IEnumerator.Current => _current;

            /// <summary>Advances the enumerator to the next element of the list.</summary>
            /// <returns>True if successfully moved to the next element; otherwise, false.</returns>
            public bool MoveNext()
            {
                if (_index == UNDEFINED_INDEX) return false;
                ref Node node = ref _list._nodes[_index];
                _current = node.item;
                _index = node.next;
                return true;
            }

            /// <summary>Resets the enumerator to its initial position.</summary>
            public void Reset()
            {
                _index = _list._head;
                _current = default;
            }

            /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
            public void Dispose()
            {
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int AllocateNode()
        {
            if (_freeList != UNDEFINED_INDEX)
            {
                int nodeIndex = _freeList;
                _freeList = _nodes[nodeIndex].next; // следующий свободный
                return nodeIndex;
            }

            if (_count >= _nodes.Length)
                Array.Resize(ref _nodes, _nodes.Length * 2);

            return _count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetNodeIndex(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            int current = _head;
            for (int i = 0; i < index; i++)
                current = _nodes[current].next;

            return current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RemoveNode(int current, int prev, int index)
        {
            ref Node node = ref _nodes[current];
            
            if (prev != UNDEFINED_INDEX)
                _nodes[prev].next = node.next;
            else
                _head = node.next;

            if (_tail == current)
                _tail = prev;

            T removed = node.item;
        
            node.item = default;
            node.next = _freeList;
            
            _freeList = current;
            _count--;
            
            OnItemRemoved?.Invoke(index, removed);
            onItemRemoved?.Invoke(removed);
            OnStateChanged?.Invoke();
        }
    }

#if UNITY_5_3_OR_NEWER
    [Serializable]
    public partial class ReactiveLinkedList<T> : ISerializationCallbackReceiver
    {
        [SerializeField]
        internal T[] serializedItems;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.serializedItems = this.ToArray();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (this.serializedItems == null)
                return;

            this.Clear();

            for (int i = 0, count = this.serializedItems.Length; i < count; i++)
                this.Add(this.serializedItems[i]);
        }
    }
#endif
}
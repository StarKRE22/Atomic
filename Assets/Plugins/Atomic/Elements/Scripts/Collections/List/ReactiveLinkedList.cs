using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Atomic.Elements
{
    public partial class ReactiveLinkedList<T> : IReactiveList<T>
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer<T>.Default;
        private const int UNDEFINED_INDEX = -1;
        protected const int INITIAL_CAPACITY = 4;

        public event StateChangedHandler OnStateChanged;
        public event ChangeItemHandler<T> OnItemChanged;
        public event InsertItemHandler<T> OnItemInserted;
        public event DeleteItemHandler<T> OnItemDeleted;

        public int Count => _count;
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

        public ReactiveLinkedList(int capacity = INITIAL_CAPACITY)
        {
            _nodes = new Node[Math.Max(capacity, INITIAL_CAPACITY)];
            _head = _tail = UNDEFINED_INDEX;
            _count = 0;
            _freeList = UNDEFINED_INDEX;
        }

        public ReactiveLinkedList(params T[] items) : this(items.Length)
        {
            foreach (var item in items) Add(item);
        }

        public ReactiveLinkedList(IEnumerable<T> items) : this(items.Count())
        {
            foreach (var item in items) Add(item);
        }

        #region Allocation & FreeList

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

        private void FreeNode(int index)
        {
            _nodes[index].item = default;
            _nodes[index].next = _freeList;
            _freeList = index;
        }

        #endregion

        #region Indexer

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

        private int GetNodeIndex(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            int current = _head;
            for (int i = 0; i < index; i++)
                current = _nodes[current].next;

            return current;
        }

        #endregion

        #region Add / Insert

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
            OnItemInserted?.Invoke(_count - 1, item);
            OnStateChanged?.Invoke();
        }

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
            OnItemInserted?.Invoke(index, item);
            OnStateChanged?.Invoke();
        }

        #endregion

        #region Remove

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

        public void RemoveAt(int index)
        {
            int current = GetNodeIndex(index);
            int prev = (index == 0) ? UNDEFINED_INDEX : GetNodeIndex(index - 1);
            RemoveNode(current, prev, index);
        }

        private void RemoveNode(int current, int prev, int index)
        {
            if (prev != UNDEFINED_INDEX)
                _nodes[prev].next = _nodes[current].next;
            else
                _head = _nodes[current].next;

            if (_tail == current)
                _tail = prev;

            T removed = _nodes[current].item;
            FreeNode(current);

            _count--;
            OnItemDeleted?.Invoke(index, removed);
            OnStateChanged?.Invoke();
        }

        #endregion

        #region Search

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

        public bool Contains(T item) => IndexOf(item) >= 0;

        #endregion

        #region Copy / Clear

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (arrayIndex + _count > array.Length)
                throw new ArgumentException("Array too small");

            int current = _head;
            while (current != UNDEFINED_INDEX)
            {
                array[arrayIndex++] = _nodes[current].item;
                current = _nodes[current].next;
            }
        }

        public void Clear()
        {
            int current = _head;
            while (current != UNDEFINED_INDEX)
            {
                int next = _nodes[current].next;
                FreeNode(current);
                current = next;
            }

            _head = _tail = UNDEFINED_INDEX;
            _count = 0;
            OnStateChanged?.Invoke();
        }

        #endregion

        #region Enumerator

        public Enumerator GetEnumerator() => new(this);
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

            public T Current => _current;
            object IEnumerator.Current => _current;

            public bool MoveNext()
            {
                if (_index == UNDEFINED_INDEX) return false;
                ref Node node = ref _list._nodes[_index];
                _current = node.item;
                _index = node.next;
                return true;
            }

            public void Reset()
            {
                _index = _list._head;
                _current = default;
            }

            public void Dispose()
            {
            }
        }

        #endregion
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
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable AssignNullToNotNullAttribute

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    public partial class ReactiveHashSet<T> : IReactiveSet<T>, IDisposable
    {
        private const int UNDEFINED_INDEX = -1;
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer.GetDefault<T>();
        private static readonly ArrayPool<T> s_arrayPool = ArrayPool<T>.Shared;

        private struct Slot
        {
            public T value;
            public int next;
            public bool exists;
        }

        public event StateChangedHandler OnStateChanged;
        public event AddItemHandler<T> OnItemAdded;
        public event RemoveItemHandler<T> OnItemRemoved;

        public int Count => _count;
        public bool IsReadOnly => false;

        private Slot[] _slots;
        private int _count;

        private int[] _buckets;
        private int _freeList;
        private int _lastIndex;

        public ReactiveHashSet() : this(0)
        {
        }

        //+
        public ReactiveHashSet(int capacity)
        {
            this.Initialize(capacity);
        }

        //+
        public ReactiveHashSet(params T[] elements) : this(elements.Length)
        {
            this.UnionWith(elements);
        }

        //+
        public ReactiveHashSet(IReadOnlyCollection<T> elements) : this(elements.Count)
        {
            this.UnionWith(elements);
        }

        //+
        public ReactiveHashSet(IEnumerable<T> elements) : this(elements.Count())
        {
            this.UnionWith(elements);
        }

        //+
        public bool Contains(T item)
        {
            return item != null && this.FindIndex(item, out _);
        }

        //+
        public bool IsEmpty()
        {
            return _count == 0;
        }

        //+
        public bool IsNotEmpty()
        {
            return _count > 0;
        }

        //+
        public bool Add(T item)
        {
            if (item == null)
                return false;

            if (!this.AddInternal(item))
                return false;

            this.OnItemAdded?.Invoke(item);
            this.OnStateChanged?.Invoke();
            return true;
        }

        //+
        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            T[] addedItems = s_arrayPool.Rent(other.Count());
            int addedCount = 0;

            foreach (T item in other)
                if (item != null && this.AddInternal(item))
                    addedItems[addedCount++] = item;

            try
            {
                if (addedCount > 0)
                {
                    for (int i = 0; i < addedCount; i++)
                        this.OnItemAdded?.Invoke(addedItems[i]);

                    this.OnStateChanged?.Invoke();
                }
            }
            finally
            {
                s_arrayPool.Return(addedItems);
            }
        }

        //+
        public bool Remove(T item)
        {
            if (item == null)
                return false;

            if (!this.RemoveInternal(item))
                return false;

            this.OnItemRemoved?.Invoke(item);
            this.OnStateChanged?.Invoke();
            return true;
        }

        //+
        public void Clear()
        {
            if (_count == 0)
                return;

            int removeCount = 0;
            T[] removedItems = s_arrayPool.Rent(_count);

            for (int i = 0; i < _lastIndex; i++)
            {
                _buckets[i] = UNDEFINED_INDEX;

                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                slot.exists = false;
                slot.next = UNDEFINED_INDEX;
                removedItems[removeCount++] = slot.value;
            }

            _count = 0;
            _freeList = UNDEFINED_INDEX;
            _lastIndex = 0;

            try
            {
                for (int i = 0; i < removeCount; i++)
                    this.OnItemRemoved?.Invoke(removedItems[i]);

                this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(removedItems);
            }
        }

        //+
        public void Dispose()
        {
            this.Clear();

            AtomicHelper.Dispose(ref this.OnItemAdded);
            AtomicHelper.Dispose(ref this.OnItemRemoved);
            AtomicHelper.Dispose(ref this.OnStateChanged);
        }

        //+
        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            for (int i = 0; i < _lastIndex; i++)
            {
                Slot slot = _slots[i];
                if (slot.exists)
                    array[arrayIndex++] = slot.value;
            }
        }

        //+
        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        //+
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        //+
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        //+
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_count == 0)
                return;

            T[] removedItems = s_arrayPool.Rent(other.Count());
            int removedCount = 0;

            foreach (T item in other)
                if (item != null && this.RemoveInternal(item))
                    removedItems[removedCount++] = item;

            try
            {
                if (removedCount > 0)
                {
                    for (int i = 0; i < removedCount; i++)
                        this.OnItemRemoved?.Invoke(removedItems[i]);

                    this.OnStateChanged?.Invoke();
                }
            }
            finally
            {
                s_arrayPool.Return(removedItems);
            }
        }

        //+
        public unsafe void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_count == 0)
                return;

            bool* intersectedItems = stackalloc bool[_lastIndex];

            foreach (T item in other)
                if (item != null && this.FindIndex(item, out int index))
                    intersectedItems[index] = true;

            T[] removedItems = s_arrayPool.Rent(other.Count());
            int removedCount = 0;

            for (int i = 0; i < _lastIndex; i++)
            {
                Slot slot = _slots[i];
                if (slot.exists && !intersectedItems[i] && this.RemoveInternal(slot.value))
                    removedItems[removedCount++] = slot.value;
            }

            try
            {
                if (removedCount > 0)
                    for (int i = 0; i < removedCount; i++)
                        this.OnItemRemoved?.Invoke(removedItems[i]);

                this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(removedItems);
            }
        }

        //+
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            T[] addedItems = s_arrayPool.Rent(_lastIndex);
            int addedCount = 0;

            T[] removedItems = s_arrayPool.Rent(_lastIndex);
            int removedCount = 0;

            foreach (T item in other)
            {
                if (item == null)
                    continue;

                if (this.AddInternal(item))
                    addedItems[addedCount++] = item;
                else if (this.RemoveInternal(item))
                    removedItems[removedCount++] = item;
            }

            try
            {
                for (int i = 0; i < addedCount; i++)
                    this.OnItemAdded?.Invoke(addedItems[i]);

                for (int i = 0; i < removedCount; i++)
                    this.OnItemRemoved?.Invoke(removedItems[i]);

                if (addedCount > 0 || removedCount > 0)
                    this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(addedItems);
                s_arrayPool.Return(removedItems);
            }
        }

        //+
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Count() <= _count)
                return false;

            for (int i = 0; i < _lastIndex; i++)
            {
                Slot slot = _slots[i];
                if (!slot.exists)
                    continue;

                if (!AtomicHelper.Contains(other, slot.value, s_comparer))
                    return false;
            }

            return true;
        }

        //+
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Count() < _count)
                return false;

            for (int i = 0; i < _lastIndex; i++)
            {
                Slot slot = _slots[i];
                if (!slot.exists)
                    continue;

                if (!AtomicHelper.Contains(other, slot.value, s_comparer))
                    return false;
            }

            return true;
        }

        //+
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Count() >= _count)
                return false;

            foreach (T o in other)
                if (!this.Contains(o))
                    return false;

            return true;
        }

        //+
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Count() > _count)
                return false;

            foreach (T o in other)
                if (!this.Contains(o))
                    return false;

            return true;
        }

        //+
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_count == 0)
                return false;

            foreach (T o in other)
                if (this.Contains(o))
                    return true;

            return false;
        }

        //+
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_count != other.Count())
                return false;

            foreach (T o in other)
                if (!this.Contains(o))
                    return false;

            return true;
        }

        //+
        public void ReplaceWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            int otherCount = other.Count();
            if (otherCount == 0)
            {
                this.Clear();
                return;
            }

            T[] addedItems = s_arrayPool.Rent(otherCount);
            int addedCount = 0;

            foreach (T item in other)
                if (item != null && this.AddInternal(item))
                    addedItems[addedCount++] = item;

            T[] removedItems = s_arrayPool.Rent(_lastIndex);
            int removedCount = 0;

            for (int i = 0; i < _lastIndex; i++)
            {
                Slot slot = _slots[i];
                if (!slot.exists)
                    continue;

                T item = slot.value;
                if (!AtomicHelper.Contains(other, item, s_comparer))
                    removedItems[removedCount++] = item;
            }

            for (int i = 0; i < removedCount; i++)
                this.RemoveInternal(removedItems[i]);

            try
            {
                for (int i = 0; i < addedCount; i++) 
                    this.OnItemAdded?.Invoke(addedItems[i]);

                for (int i = 0; i < removedCount; i++) 
                    this.OnItemRemoved?.Invoke(removedItems[i]);

                if (addedCount > 0 || removedCount > 0)
                    this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(addedItems);
                s_arrayPool.Return(removedItems);
            }
        }

        private void IncreaseCapacity()
        {
            int size = AtomicHelper.NextPrime(_slots.Length);
            Array.Resize(ref _slots, size);
            Array.Resize(ref _buckets, size);

            for (int i = 0; i < size; i++)
                _buckets[i] = UNDEFINED_INDEX;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                ref int bucket = ref this.Bucket(slot.value);
                slot.next = bucket;
                bucket = i;
            }
        }

        private bool RemoveInternal(T item)
        {
            if (_count == 0)
                return false;

            ref int bucket = ref this.Bucket(item);

            int index = bucket;
            int last = UNDEFINED_INDEX;

            while (index >= 0)
            {
                ref Slot node = ref _slots[index];
                if (s_comparer.Equals(node.value, item))
                {
                    if (last == UNDEFINED_INDEX)
                        bucket = node.next;
                    else
                        _slots[last].next = node.next;

                    node.next = _freeList;
                    node.exists = false;

                    _count--;

                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                    }
                    else
                    {
                        _freeList = index;
                    }

                    return true;
                }

                last = index;
                index = node.next;
            }

            return false;
        }

        private bool AddInternal(T item)
        {
            if (this.FindIndex(item, out int index))
                return false;

            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = _slots[index].next;
            }
            else
            {
                if (_lastIndex == _slots.Length)
                    this.IncreaseCapacity();

                index = _lastIndex;
                _lastIndex++;
            }

            ref int bucket = ref this.Bucket(item);

            _slots[index] = new Slot
            {
                value = item,
                next = bucket,
                exists = true
            };

            bucket = index;

            _count++;
            return true;
        }

        private bool FindIndex(T item, out int index)
        {
            if (_count == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            index = this.Bucket(item);
            while (index >= 0)
            {
                Slot slot = _slots[index];
                if (slot.exists && s_comparer.Equals(slot.value, item))
                    return true;

                index = slot.next;
            }

            return false;
        }

        private ref int Bucket(T item)
        {
            int index = (int) ((uint) item.GetHashCode() % _slots.Length);
            return ref _buckets[index];
        }

        private void Initialize(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            int size = AtomicHelper.NextPrime(capacity);

            _slots = new Slot[size];
            _buckets = new int[size];

            for (int i = 0; i < size; i++)
                _buckets[i] = UNDEFINED_INDEX;

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;
        }

        //+
        private struct Enumerator : IEnumerator<T>
        {
            private readonly ReactiveHashSet<T> _set;
            private int _index;
            private T _current;

            public T Current => _current;
            object IEnumerator.Current => _current;

            public Enumerator(ReactiveHashSet<T> set)
            {
                _set = set;
                _index = 0;
                _current = default;
            }

            public bool MoveNext()
            {
                while (_index < _set._lastIndex)
                {
                    ref Slot slot = ref _set._slots[_index++];
                    if (slot.exists)
                    {
                        _current = slot.value;
                        return true;
                    }
                }

                _current = default;
                return false;
            }

            void IEnumerator.Reset()
            {
                _index = 0;
                _current = default;
            }

            public void Dispose()
            {
                //Do nothing...
            }
        }
    }
}

#region Serialization

namespace Atomic.Elements
{
    [Serializable]
    public partial class ReactiveHashSet<T> : ISerializationCallbackReceiver
    {
#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [SerializeField]
        private List<T> items;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.items = new List<T>(this);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();

            if (this.items != null)
                this.UnionWith(this.items);
        }
    }
}

#endregion
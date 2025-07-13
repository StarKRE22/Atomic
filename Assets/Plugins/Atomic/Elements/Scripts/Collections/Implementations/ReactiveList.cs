using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveList<T> : IReactiveList<T>, IDisposable
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer.GetDefault<T>();
        private static readonly ArrayPool<T> s_arrayPool = ArrayPool<T>.Shared;

        public event StateChangedHandler OnStateChanged;
        public event ChangeItemHandler<T> OnItemChanged;
        public event InsertItemHandler<T> OnItemInserted;
        public event DeleteItemHandler<T> OnItemDeleted;

        public bool IsReadOnly => false;

        public int Count => this.count;
        public int Capacity => items.Length;

        [SerializeField]
        private T[] items;

        private int count;

        public ReactiveList(int capacity = 0)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));
            
            this.items = new T[capacity];
            this.count = 0;
        }

        public ReactiveList(params T[] items)
        {
            this.items = items.ToArray();
            this.count = this.items.Length;
        }

        public ReactiveList(IEnumerable<T> items)
        {
            this.items = items.ToArray();
            this.count = this.items.Length;
        }

        public T this[int index]
        {
            get { return this.items[index]; }
            set { this.Set(index, value); }
        }

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

        public bool Contains(T item)
        {
            for (int i = 0, count = this.items.Length; i < count; i++)
                if (s_comparer.Equals(this.items[i], item))
                    return true;

            return false;
        }

        public bool Remove(T item)
        {
            if (this.count == 0)
                return false;

            for (int i = 0; i < count; i++)
            {
                if (!s_comparer.Equals(this.items[i], item))
                    continue;

                this.count--;

                //Shift left:
                for (int j = i; j < this.count; j++)
                    this.items[j] = this.items[j + 1];

                this.OnItemDeleted?.Invoke(i, item);
                this.OnStateChanged?.Invoke();
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.items.Length)
                throw new IndexOutOfRangeException($"Index {index} out of range!");

            T item = this.items[index];
            this.count--;

            for (int j = index; j < this.count; j++)
                this.items[j] = this.items[j + 1];

            this.OnItemDeleted?.Invoke(index, item);
            this.OnStateChanged?.Invoke();
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.count; i++)
                if (s_comparer.Equals(this.items[i], item))
                    return i;

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > this.items.Length)
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

        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Dispose()
        {
            this.Clear();
            AtomicHelper.Dispose(ref this.OnItemChanged);
            AtomicHelper.Dispose(ref this.OnStateChanged);
        }

        private void IncreaseCapacity()
        {
            int capacity = this.items.Length;
            int newCapacity = capacity == 0 ? 1 : capacity * 2;
            
            if ((uint) newCapacity > int.MaxValue) 
                newCapacity = int.MaxValue;

            Array.Resize(ref this.items, newCapacity);
        }

        private void Set(int index, T value)
        {
            if (index < 0 || index > this.items.Length)
                throw new IndexOutOfRangeException($"Index {index} out of range!");

            if (s_comparer.Equals(this.items[index], value))
                return;

            this.items[index] = value;
            this.OnItemChanged?.Invoke(index, value);
            this.OnStateChanged?.Invoke();
        }

        private struct Enumerator : IEnumerator<T>
        {
            public T Current => _current;

            object IEnumerator.Current => _current;

            private readonly ReactiveList<T> _list;
            private int _index;
            private T _current;

            public Enumerator(ReactiveList<T> list)
            {
                _list = list;
                _index = -1;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_index + 1 == _list.count)
                    return false;

                _current = _list[++_index];
                return true;
            }

            public void Reset()
            {
                _index = -1;
                _current = default;
            }

            public void Dispose()
            {
                //Nothing...
            }
        }
    }
}
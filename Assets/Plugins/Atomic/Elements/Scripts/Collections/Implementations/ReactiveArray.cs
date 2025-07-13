using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    //TODO: СДЕЛАТЬ LOCK НА ПОДПИСКУ
    [Serializable]
    public class ReactiveArray<T> : IReactiveArray<T>, IDisposable
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer.GetDefault<T>();

        public event ChangeItemHandler<T> OnItemChanged;
        public event StateChangedHandler OnStateChanged;

        public int Length => this.items.Length;

        [SerializeField]
        private T[] items;

        public ReactiveArray(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));
            
            this.items = new T[capacity];
        }

        public ReactiveArray(params T[] elements)
        {
            this.items = elements;
        }

        public T this[int index]
        {
            get { return this.items[index]; }
            set
            {
                ref T current = ref this.items[index];
                if (s_comparer.Equals(current, value))
                    return;

                current = value;
                this.OnStateChanged?.Invoke();
                this.OnItemChanged?.Invoke(index, value);
            }
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
            AtomicHelper.Dispose(ref this.OnItemChanged);
            AtomicHelper.Dispose(ref this.OnStateChanged);
        }

        private struct Enumerator : IEnumerator<T>
        {
            public T Current => _current;

            object IEnumerator.Current => _current;

            private readonly ReactiveArray<T> _array;
            private int _index;
            private T _current;

            public Enumerator(ReactiveArray<T> array)
            {
                _array = array;
                _index = -1;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_index + 1 == _array.Length)
                    return false;

                _current = _array[++_index];
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
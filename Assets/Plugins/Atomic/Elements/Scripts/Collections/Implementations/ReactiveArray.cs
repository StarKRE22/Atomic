using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveArray<T> : IReactiveArray<T>
    {
        private static readonly IEqualityComparer<T> equalityComparer = EqualityComparer.GetDefault<T>();

        public event ChangeItemHandler<T> OnItemChanged;
        public event StateChangedHandler OnStateChanged;

        public int Length => this.array.Length;

        [SerializeField]
        private T[] array;
        
        public ReactiveArray(int length)
        {
            this.array = new T[length];
        }

        public ReactiveArray(params T[] elements)
        {
            this.array = elements;
        }

        public T this[in int index]
        {
            get
            {
                return this.array[index];
            }
            set
            {
                ref T current = ref this.array[index];
                if (equalityComparer.Equals(current, value))
                {
                    return;
                }

                current = value;
                this.OnStateChanged?.Invoke();
                this.OnItemChanged?.Invoke(index, value);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0, count = this.array.Length; i < count; i++)
            {
                yield return this.array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
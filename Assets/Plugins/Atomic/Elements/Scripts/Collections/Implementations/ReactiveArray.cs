using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveArray<T>
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

        public T this[int index]
        {
            get
            {
                return this.array[index];
            }
            set
            {
                if (!equalityComparer.Equals(this.array[index], value))
                {
                    this.array[index] = value;
                    this.OnStateChanged?.Invoke();
                    this.OnItemChanged?.Invoke(index, value);
                }
            }
        }
    }
}
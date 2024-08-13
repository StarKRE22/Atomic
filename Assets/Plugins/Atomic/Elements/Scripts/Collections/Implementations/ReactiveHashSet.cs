using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveHashSet<T> : IReactiveSet<T>, ISerializationCallbackReceiver
    {
        public event StateChangedHandler OnStateChanged;
        public event AddItemHandler<T> OnItemAdded;
        public event RemoveItemHandler<T> OnItemRemoved;
        public event ClearHandler OnCleared;

        public int Count => this.set.Count;
        public bool IsReadOnly => false;
        
        [SerializeField]
        private List<T> items;

        private HashSet<T> set;

        public ReactiveHashSet()
        {
            this.set = new HashSet<T>();
        }
        
        public ReactiveHashSet(params T[] elements)
        {
            this.set = new HashSet<T>(elements);
        }

        public ReactiveHashSet(IEnumerable<T> elements)
        {
            this.set = new HashSet<T>(elements);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.set.GetEnumerator();
        }

        public bool IsEmpty()
        {
            return this.set.Count == 0;
        }

        public bool IsNotEmpty()
        {
            return this.set.Count > 0;
        }

        public bool Add(T item)
        {
            if (!this.set.Add(item))
            {
                return false;
            }

            this.OnStateChanged?.Invoke();
            this.OnItemAdded?.Invoke(item);
            return true;

        }

        public bool Remove(T item)
        {
            if (this.set.Remove(item))
            {
                this.OnStateChanged?.Invoke();
                this.OnItemRemoved?.Invoke(item);
                return true;
            }

            return false;
        }

        public bool Contains(T item)
        {
            return this.set.Contains(item);
        }
        
        public void ExceptWith(IEnumerable<T> other)
        {
            if (this.set.Count == 0)
            {
                return;
            }
            
            this.set.ExceptWith(other);
            this.OnStateChanged?.Invoke();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (this.set.Count == 0)
            {
                return;
            }
            
            this.set.IntersectWith(other);
            this.OnStateChanged?.Invoke();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            this.set.SymmetricExceptWith(other);
            this.OnStateChanged?.Invoke();
        }
        
        public void UnionWith(IEnumerable<T> other)
        {
            this.set.UnionWith(other);
            this.OnStateChanged?.Invoke();
        }

        public void Clear()
        {
            if (this.set.Count == 0)
            {
                return;
            }
            
            this.set.Clear();
            this.OnStateChanged?.Invoke();
            this.OnCleared?.Invoke();
        }
        
        public void CopyTo(T[] array)
        {
            this.set.CopyTo(array);
        }

        public void ReplaceWith(T item)
        {
            this.set.Clear();
            this.set.Add(item);
            this.OnStateChanged?.Invoke();
        }

        public void ReplaceWith(params T[] items)
        {
            this.set.Clear();
            this.set.UnionWith(items);
            this.OnStateChanged?.Invoke();
        }

        public void ReplaceWith(IEnumerable<T> items)
        {
            this.set.Clear();
            this.set.UnionWith(items);
            this.OnStateChanged?.Invoke();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.set.CopyTo(array, arrayIndex);
        }
        
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.set.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.set.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.set.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.set.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return this.set.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return this.set.SetEquals(other);
        }

        void ICollection<T>.Add(T item)
        {
            if (item != null) this.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void OnAfterDeserialize()
        {
            this.set = new HashSet<T>(this.items);
            this.OnStateChanged?.Invoke();
        }

        public void OnBeforeSerialize()
        {
            this.items = new List<T>(this.set);
        }
    }
}
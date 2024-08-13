using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveSortedList<K, V> : IReactiveDictionary<K, V>, 
        ISerializationCallbackReceiver where K : IComparable<K>
    {
        private static readonly IEqualityComparer<V> equalityComparer = EqualityComparer.GetDefault<V>();

        public event StateChangedHandler OnStateChanged;
        public event SetItemHandler<K, V> OnItemChanged;
        public event AddItemHandler<K, V> OnItemAdded;
        public event RemoveItemHandler<K, V> OnItemRemoved;
        public event ClearHandler OnCleared;

        [Serializable]
        public struct Pair
        {
            public K key;
            public V value;
        }
        
        [SerializeField]
        private Pair[] pairs;

        private SortedList<K, V> list;
        
        public ICollection<K> Keys => this.list.Keys;
        public ICollection<V> Values => this.list.Values;
        
        public int Count => this.list.Count;
        public bool IsReadOnly => false;

        public ReactiveSortedList()
        {
            this.list = new SortedList<K, V>();
        }

        public ReactiveSortedList(int capacity = 0)
        {
            this.list = new SortedList<K, V>(capacity);
        }
        
        public V this[K key]
        {
            get { return this.list[key]; }
            set { this.Set(key, value); }
        }
        
        public void Set(K key, V value)
        {
            if (!this.list.TryGetValue(key, out V prev))
            {
                this.Add(key, value);
                return;
            }

            if (!equalityComparer.Equals(prev, value))
            {
                this.list[key] = value;
                this.OnStateChanged?.Invoke();
                this.OnItemChanged?.Invoke(key, value);
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int IndexOfKey(K key)
        {
            return this.list.IndexOfKey(key);
        }

        public int IndexOfValue(V value)
        {
            return this.list.IndexOfValue(value);
        }
        
        public void Add(K key, V value)
        {
            this.list.Add(key, value);
            this.OnItemAdded?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
        }
        
        public bool Remove(K key)
        {
            if (!this.list.TryGetValue(key, out V value))
            {
                return false;
            }
            
            this.list.Remove(key);

            this.OnItemRemoved?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
            return true;
        }

        public bool ContainsKey(K key)
        {
            return this.list.ContainsKey(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            return this.list.TryGetValue(key, out value);
        }

        public void Clear()
        {
            if (this.list.Count > 0)
            {
                this.list.Clear();
                this.OnCleared?.Invoke();
                this.OnStateChanged?.Invoke();
            }
        }

        public void Add(KeyValuePair<K, V> item)
        {
            (K key, V value) = item;
            this.Add(key, value);
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return ((ICollection<KeyValuePair<K, V>>) this.list).Contains(item);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0)
        {
            ((ICollection<KeyValuePair<K, V>>) this.list).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            if (((ICollection<KeyValuePair<K, V>>) this.list).Remove(item))
            {
                this.OnStateChanged?.Invoke();
                this.OnItemRemoved?.Invoke(item.Key, item.Value);
                return true;
            }

            return false;
        }

        public void OnAfterDeserialize()
        {
            this.list = new SortedList<K, V>();

            for (int i = 0, count = this.pairs.Length; i < count; i++)
            {
                Pair pair = this.pairs[i];
                this.list[pair.key] = pair.value;
            }
            
            this.OnStateChanged?.Invoke();
        }

        public void OnBeforeSerialize()
        {
            this.pairs = new Pair[this.list.Count];

            int i = 0;
            foreach (var (key, value) in this.list)
            {
                this.pairs[i++] = new Pair
                {
                    key = key,
                    value = value
                };
            }
        }
    }
}
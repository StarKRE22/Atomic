using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveSortedDictionary<K, V> : IReactiveDictionary<K, V>, ISerializationCallbackReceiver
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

        private SortedDictionary<K, V> dictionary;

        public ICollection<K> Keys => this.dictionary.Keys;
        public ICollection<V> Values => this.dictionary.Values;
        public int Count => this.dictionary.Count;
        public bool IsReadOnly => false;

        public ReactiveSortedDictionary()
        {
            this.dictionary = new SortedDictionary<K, V>();
        }

        public V this[K key]
        {
            get => this.dictionary[key];
            set { this.Set(key, value); }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Set(K key, V value)
        {
            if (!this.dictionary.TryGetValue(key, out V prev))
            {
                this.Add(key, value);
                return;
            }

            if (!equalityComparer.Equals(prev, value))
            {
                this.dictionary[key] = value;
                this.OnStateChanged?.Invoke();
                this.OnItemChanged?.Invoke(key, value);
            }
        }

        public void Add(K key, V value)
        {
            this.dictionary.Add(key, value);
            this.OnItemAdded?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
        }

        public bool Remove(K key)
        {
            if (!this.dictionary.TryGetValue(key, out V value))
            {
                return false;
            }
            
            this.dictionary.Remove(key);

            this.OnItemRemoved?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
            return true;
        }

        public bool ContainsKey(K key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        public void Clear()
        {
            if (this.dictionary.Count > 0)
            {
                this.dictionary.Clear();
                this.OnStateChanged?.Invoke();
                this.OnCleared?.Invoke();
            }
        }

        public void Add(KeyValuePair<K, V> item)
        {
            (K key, V value) = item;
            this.Add(key, value);
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return ((ICollection<KeyValuePair<K, V>>) this.dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0)
        {
            ((ICollection<KeyValuePair<K, V>>) this.dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            if (((ICollection<KeyValuePair<K, V>>) this.dictionary).Remove(item))
            {
                this.OnItemRemoved?.Invoke(item.Key, item.Value);
                this.OnStateChanged?.Invoke();
                return true;
            }

            return false;
        }

        public void OnAfterDeserialize()
        {
            this.dictionary = new SortedDictionary<K, V>();

            for (int i = 0, count = this.pairs.Length; i < count; i++)
            {
                Pair pair = this.pairs[i];
                this.dictionary[pair.key] = pair.value;
            }
            
            this.OnStateChanged?.Invoke();
        }

        public void OnBeforeSerialize()
        {
            this.pairs = new Pair[this.dictionary.Count];

            int i = 0;
            foreach (var (key, value) in this.dictionary)
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
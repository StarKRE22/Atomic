// using System;
// using System.Collections;
// using System.Collections.Generic;
//
// #if UNITY_5_3_OR_NEWER
// using UnityEngine;
// #endif
//
// namespace Atomic.Elements
// {
//     /// <summary>
//     /// A reactive, serializable sorted dictionary (using <see cref="SortedList{TKey, TValue}"/>) that
//     /// raises events when items are added, changed, removed, or when the entire state changes.
//     /// </summary>
//     /// <typeparam name="K">The key type. Must implement <see cref="IComparable{K}"/>.</typeparam>
//     /// <typeparam name="V">The value type.</typeparam>
//     [Serializable]
//     public class ReactiveSortedList<K, V> : IReactiveDictionary<K, V>
// #if UNITY_5_3_OR_NEWER
//         , ISerializationCallbackReceiver
// #endif
//         where K : IComparable<K>
//     {
// #if UNITY_5_3_OR_NEWER
//         /// <summary>
//         /// Serializable key-value pair structure used for Unity serialization.
//         /// </summary>
//         [Serializable]
//         internal struct Pair
//         {
//             public K key;
//             public V value;
//         }
//
//         [SerializeField]
//         private Pair[] pairs;
// #endif
//
//         private static readonly IEqualityComparer<V> s_equalityComparer = EqualityComparer.GetDefault<V>();
//
//         /// <inheritdoc/>
//         public event StateChangedHandler OnStateChanged;
//
//         /// <inheritdoc/>
//         public event SetItemHandler<K, V> OnItemChanged;
//
//         /// <inheritdoc/>
//         public event AddItemHandler<K, V> OnItemAdded;
//
//         /// <inheritdoc/>
//         public event RemoveItemHandler<K, V> OnItemRemoved;
//
//         /// <summary>
//         /// Event triggered when the entire collection is cleared.
//         /// </summary>
//         public event ClearHandler OnCleared;
//
//         private SortedList<K, V> list;
//
//         /// <summary>
//         /// Gets a collection containing the keys.
//         /// </summary>
//         public ICollection<K> Keys => this.list.Keys;
//
//         /// <summary>
//         /// Gets a collection containing the values.
//         /// </summary>
//         public ICollection<V> Values => this.list.Values;
//
//         IEnumerable<K> IReadOnlyDictionary<K, V>.Keys => this.Keys;
//         IEnumerable<V> IReadOnlyDictionary<K, V>.Values => this.Values;
//
//         /// <inheritdoc cref="ICollection{T}.Count" />
//         public int Count => this.list.Count;
//
//         /// <inheritdoc/>
//         public bool IsReadOnly => false;
//
//         /// <summary>
//         /// Initializes a new, empty <see cref="ReactiveSortedList{K, V}"/>.
//         /// </summary>
//         public ReactiveSortedList() => this.list = new SortedList<K, V>();
//
//         /// <summary>
//         /// Initializes a new instance with the given initial capacity.
//         /// </summary>
//         /// <param name="capacity">The initial capacity of the list.</param>
//         public ReactiveSortedList(int capacity = 0) => this.list = new SortedList<K, V>(capacity);
//
//         /// <summary>
//         /// Gets or sets the value associated with the specified key.
//         /// Raises events if the value changes.
//         /// </summary>
//         /// <param name="key">The key of the value.</param>
//         public V this[K key]
//         {
//             get => this.list[key];
//             set => this.Set(key, value);
//         }
//
//         /// <summary>
//         /// Sets the value for a key. If the key does not exist, it's added.
//         /// If the value is different, change events are raised.
//         /// </summary>
//         /// <param name="key">The key to set.</param>
//         /// <param name="value">The value to assign.</param>
//         public void Set(K key, V value)
//         {
//             if (!this.list.TryGetValue(key, out V prev))
//             {
//                 this.Add(key, value);
//                 return;
//             }
//
//             if (!s_equalityComparer.Equals(prev, value))
//             {
//                 this.list[key] = value;
//                 this.OnStateChanged?.Invoke();
//                 this.OnItemChanged?.Invoke(key, value);
//             }
//         }
//
//         /// <inheritdoc/>
//         public IEnumerator<KeyValuePair<K, V>> GetEnumerator() => this.list.GetEnumerator();
//
//         /// <inheritdoc/>
//         IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
//
//         /// <summary>
//         /// Returns the index of the specified key.
//         /// </summary>
//         public int IndexOfKey(K key) => this.list.IndexOfKey(key);
//
//         /// <summary>
//         /// Returns the index of the specified value.
//         /// </summary>
//         public int IndexOfValue(V value) => this.list.IndexOfValue(value);
//
//         /// <inheritdoc/>
//         public void Add(K key, V value)
//         {
//             this.list.Add(key, value);
//             this.OnItemAdded?.Invoke(key, value);
//             this.OnStateChanged?.Invoke();
//         }
//
//         /// <inheritdoc/>
//         public bool Remove(K key)
//         {
//             if (!this.list.Remove(key, out V value))
//                 return false;
//
//             this.OnItemRemoved?.Invoke(key, value);
//             this.OnStateChanged?.Invoke();
//             return true;
//         }
//
//         /// <inheritdoc cref="IReactiveDictionary{K,V}.ContainsKey" />
//         public bool ContainsKey(K key) => this.list.ContainsKey(key);
//
//         /// <inheritdoc cref="IReadOnlyDictionary{TKey,TValue}.TryGetValue" />
//         public bool TryGetValue(K key, out V value) => this.list.TryGetValue(key, out value);
//
//         /// <inheritdoc/>
//         public void Clear()
//         {
//             if (this.list.Count > 0)
//             {
//                 this.list.Clear();
//                 this.OnCleared?.Invoke();
//                 this.OnStateChanged?.Invoke();
//             }
//         }
//
//         /// <inheritdoc/>
//         public void Add(KeyValuePair<K, V> item)
//         {
//             this.Add(item.Key, item.Value);
//         }
//
//         /// <inheritdoc/>
//         public bool Contains(KeyValuePair<K, V> item) =>
//             ((ICollection<KeyValuePair<K, V>>) this.list).Contains(item);
//
//         /// <inheritdoc/>
//         public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0) =>
//             ((ICollection<KeyValuePair<K, V>>) this.list).CopyTo(array, arrayIndex);
//
//         /// <inheritdoc/>
//         public bool Remove(KeyValuePair<K, V> item)
//         {
//             if (((ICollection<KeyValuePair<K, V>>) this.list).Remove(item))
//             {
//                 this.OnItemRemoved?.Invoke(item.Key, item.Value);
//                 this.OnStateChanged?.Invoke();
//                 return true;
//             }
//
//             return false;
//         }
//
// #if UNITY_5_3_OR_NEWER
//         /// <summary>
//         /// Reconstructs the internal dictionary from serialized data.
//         /// Called automatically by Unity after deserialization.
//         /// </summary>
//         public void OnAfterDeserialize()
//         {
//             this.list = new SortedList<K, V>();
//
//             for (int i = 0, count = this.pairs.Length; i < count; i++)
//             {
//                 Pair pair = this.pairs[i];
//                 this.list[pair.key] = pair.value;
//             }
//
//             this.OnStateChanged?.Invoke();
//         }
//
//         /// <summary>
//         /// Converts the internal sorted list into a serializable array of key-value pairs.
//         /// Called automatically by Unity before serialization.
//         /// </summary>
//         public void OnBeforeSerialize()
//         {
//             this.pairs = new Pair[this.list.Count];
//
//             int i = 0;
//             foreach (var (key, value) in this.list)
//             {
//                 this.pairs[i++] = new Pair
//                 {
//                     key = key,
//                     value = value
//                 };
//             }
//         }
// #endif
//     }
// }
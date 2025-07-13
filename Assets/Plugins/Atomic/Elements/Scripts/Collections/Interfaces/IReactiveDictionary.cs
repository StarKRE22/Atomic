using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    public interface IReactiveDictionary<K, V> : IDictionary<K, V>, IReadOnlyDictionary<K, V>
    {
        event StateChangedHandler OnStateChanged;
        event SetItemHandler<K, V> OnItemChanged;
        event AddItemHandler<K, V> OnItemAdded;
        event RemoveItemHandler<K, V> OnItemRemoved;
     
        new int Count { get; } 
        new V this[K key] { get; set; }
        new bool ContainsKey(K key);
    }
}
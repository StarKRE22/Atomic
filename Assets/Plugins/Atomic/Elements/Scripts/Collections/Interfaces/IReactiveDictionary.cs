using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IReactiveDictionary<K, V> : IDictionary<K, V>
    {
        event StateChangedHandler OnStateChanged;
        event SetItemHandler<K, V> OnItemChanged;
        event AddItemHandler<K, V> OnItemAdded;
        event RemoveItemHandler<K, V> OnItemRemoved;
        event ClearHandler OnCleared;
    }
}
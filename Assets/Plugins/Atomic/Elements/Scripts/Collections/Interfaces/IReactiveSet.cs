using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IReactiveSet<T> : ISet<T>
    {
        event StateChangedHandler OnStateChanged;
        event AddItemHandler<T> OnItemAdded;
        event RemoveItemHandler<T> OnItemRemoved;
        event ClearHandler OnCleared;

        void CopyTo(T[] array);

        void ReplaceWith(T item);
        void ReplaceWith(params T[] items);
        void ReplaceWith(IEnumerable<T> items);
        
        bool IsEmpty();
        bool IsNotEmpty();
    }
}
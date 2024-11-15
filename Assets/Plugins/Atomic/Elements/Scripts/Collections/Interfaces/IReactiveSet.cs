using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IReactiveSet<T> : ISet<T>
    {
        event StateChangedHandler OnStateChanged;
        event AddItemHandler<T> OnItemAdded;
        event RemoveItemHandler<T> OnItemRemoved;

        void CopyTo(T[] array);

        void ReplaceTo(T other);
        void ReplaceTo(params T[] other);
        void ReplaceTo(IEnumerable<T> other);
        
        bool IsEmpty();
        bool IsNotEmpty();
    }
}
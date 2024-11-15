using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IReactiveList<T> : IList<T>
    {
        event StateChangedHandler OnStateChanged;
        event ChangeItemHandler<T> OnItemUpdated;
        event InsertItemHandler<T> OnItemInserted;
        event DeleteItemHandler<T> OnItemDeleted;
        
        void Update(int index, T value);
        void CopyTo(T[] array);
    }
}
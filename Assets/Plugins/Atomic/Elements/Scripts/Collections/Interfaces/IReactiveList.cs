using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    public interface IReactiveList<T> : IList<T>, IReadOnlyList<T>
    {
        event StateChangedHandler OnStateChanged;
        event ChangeItemHandler<T> OnItemChanged;
        event InsertItemHandler<T> OnItemInserted;
        event DeleteItemHandler<T> OnItemDeleted;
    }
}
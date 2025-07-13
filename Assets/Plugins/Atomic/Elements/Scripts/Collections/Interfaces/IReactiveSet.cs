using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    public interface IReactiveSet<T> : ISet<T>, IReadOnlyCollection<T>
    {
        event StateChangedHandler OnStateChanged;
        event AddItemHandler<T> OnItemAdded;
        event RemoveItemHandler<T> OnItemRemoved;
    }
}
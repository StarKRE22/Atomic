using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive list that notifies subscribers when its contents change.
    /// Includes events for inserts, deletions, modifications, and state changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public interface IReactiveList<T> : IList<T>, IReadOnlyReactiveList<T>, IReactiveCollection<T>
    {
    }
}
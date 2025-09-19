using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive collection that provides notifications
    /// when items are added, removed, or when the overall state changes.
    /// Extends <see cref="IReadOnlyReactiveCollection{T}"/> with full collection access (add, remove, clear).
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public interface IReactiveCollection<T> : IReadOnlyReactiveCollection<T>, ICollection<T>
    {
    }
}

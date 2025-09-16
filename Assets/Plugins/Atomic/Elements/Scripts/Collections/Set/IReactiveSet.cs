using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive set that provides notifications when its contents change.
    /// Includes events for additions, removals, and global state changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the set.</typeparam>
    public interface IReactiveSet<T> : ISet<T>, IReactiveCollection<T>
    {
    }
}
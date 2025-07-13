using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IReactiveArray<T> : IReadOnlyList<T>
    {
        event ChangeItemHandler<T> OnItemChanged;
        
        event StateChangedHandler OnStateChanged;

        int Length { get; }

        T IReadOnlyList<T>.this[int index] => this[index];

        int IReadOnlyCollection<T>.Count => this.Length;

        new T this[int index] { get; set; }
    }
}
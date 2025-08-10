using System;

namespace Atomic.Elements
{
    public interface IStateSource<out T> where T : Enum
    {
        /// <summary>Raised when the state changes.</summary>
        event Action<T> OnStateChanged;
        
        /// <summary>Gets the current internal state.</summary>
        T GetState();
    }
}
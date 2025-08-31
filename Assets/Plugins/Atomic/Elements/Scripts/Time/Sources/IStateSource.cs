using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a source that provides state notifications.
    /// </summary>
    /// <typeparam name="T">Enum type representing the state.</typeparam>
    public interface IStateSource<out T> where T : Enum
    {
        /// <summary>Raised when the state changes.</summary>
        event Action<T> OnStateChanged;

        /// <summary>Gets the current internal state.</summary>
        /// <returns>The current state.</returns>
        T GetState();
    }
}
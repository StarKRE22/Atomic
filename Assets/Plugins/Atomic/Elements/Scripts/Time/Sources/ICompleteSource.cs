using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a source that can complete and notify listeners.
    /// </summary>
    public interface ICompleteSource
    {
        /// <summary>
        /// Invoked when the source has completed.
        /// </summary>
        event Action OnCompleted;

        /// <summary>
        /// Returns whether the source has completed.
        /// </summary>
        /// <returns>True if completed; otherwise false.</returns>
        bool IsCompleted();
    }
}
using System;

namespace Atomic.Elements
{
    public interface ICompleteSource
    {
        /// <summary>
        /// Invoked when the source has completed.
        /// </summary>
        event Action OnCompleted;
        
        /// <summary>
        /// Returns whether the source has completed.
        /// </summary>
        bool IsCompleted();
    }
}
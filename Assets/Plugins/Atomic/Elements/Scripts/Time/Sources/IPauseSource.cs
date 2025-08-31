using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a source that can be paused and resumed.
    /// </summary>
    public interface IPauseSource
    {
        /// <summary>Raised when the source is paused.</summary>
        event Action OnPaused;

        /// <summary>Raised when the source is resumed.</summary>
        event Action OnResumed;

        /// <summary>Returns true if the source is paused.</summary>
        /// <returns>True if paused; otherwise false.</returns>
        bool IsPaused();

        /// <summary>Pauses the source.</summary>
        void Pause();

        /// <summary>Resumes the source.</summary>
        void Resume();
    }
}
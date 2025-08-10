using System;

namespace Atomic.Elements
{
    public interface IPauseSource
    {
        /// <summary>Raised when the countdown is paused.</summary>
        event Action OnPaused;

        /// <summary>Raised when the countdown is resumed after pause.</summary>
        event Action OnResumed;

        /// <summary>Returns true if the countdown is paused.</summary>
        bool IsPaused();

        /// <summary>Pauses the countdown if it is currently playing.</summary>
        void Pause();
        
        /// <summary>Resumes the countdown if it is currently paused.</summary>
        void Resume();
    }
}
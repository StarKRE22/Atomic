using System;

namespace Atomic.Elements
{
    public interface IStartSource
    {
        /// <summary>Raised when the countdown starts.</summary>
        event Action OnStarted;

        /// <summary>Raised when the countdown is stopped manually.</summary>
        event Action OnStopped;
        
        /// <summary>Returns true if the countdown has not started yet.</summary>
        bool IsIdle();

        /// <summary>Returns true if the countdown is running.</summary>
        bool IsStarted();
        
        /// <summary>Starts the countdown from a specific current time.</summary>
        void Start(float currentTime = 0);
        
        /// <summary>Stops the countdown and resets the current time to zero.</summary>
        void Stop();
    }
}
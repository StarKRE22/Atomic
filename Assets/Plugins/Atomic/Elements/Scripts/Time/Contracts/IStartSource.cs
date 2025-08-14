using System;

namespace Atomic.Elements
{
    public interface IStartSource
    {
        /// <summary>Raised when the source starts.</summary>
        event Action OnStarted;

        /// <summary>Raised when the source is stopped manually.</summary>
        event Action OnStopped;
        
        /// <summary>Returns true if the source has not started yet.</summary>
        bool IsIdle();

        /// <summary>Returns true if the source is running.</summary>
        bool IsStarted();
        
        /// <summary>Starts the source from a specific current time.</summary>
        void Start(float time);

        /// <summary>Starts the source from a specific start time.</summary>
        void Start();
        
        /// <summary>Stops the source and resets the current time to zero.</summary>
        void Stop();
    }
}
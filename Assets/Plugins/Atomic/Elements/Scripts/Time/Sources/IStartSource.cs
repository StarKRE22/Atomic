using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a source that can be started, stopped, and notify start/stop events.
    /// </summary>
    public interface IStartSource
    {
        /// <summary>Raised when the source starts.</summary>
        event Action OnStarted;

        /// <summary>Raised when the source stops.</summary>
        event Action OnStopped;

        /// <summary>Returns true if the source is running.</summary>
        bool IsStarted();

        /// <summary>Starts the source from a specific time.</summary>
        /// <param name="time">Time to start from.</param>
        void Start(float time);

        /// <summary>Starts the source from default start time.</summary>
        void Start();

        /// <summary>Stops the source and resets its time.</summary>
        void Stop();
    }
}
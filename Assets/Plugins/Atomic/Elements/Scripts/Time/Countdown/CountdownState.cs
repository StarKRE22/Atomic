namespace Atomic.Elements
{
    /// <summary>
    /// Represents the state of the countdown.
    /// </summary>
    public enum CountdownState
    {
        /// <summary>No activity yet.</summary>
        IDLE = 0,
        /// <summary>Currently running.</summary>
        PLAYING = 1,
        /// <summary>Temporarily paused.</summary>
        PAUSED = 2,
        /// <summary>Finished naturally or by time running out.</summary>
        COMPLETED = 3
    }
}
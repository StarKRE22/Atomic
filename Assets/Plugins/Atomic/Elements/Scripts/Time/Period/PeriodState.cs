namespace Atomic.Elements
{
    /// <summary>
    /// Represents the state of the cycle timer.
    /// </summary>
    public enum PeriodState
    {
        /// <summary>The timer is idle and not running.</summary>
        IDLE = 0,
        /// <summary>The timer is currently running.</summary>
        PLAYING = 1,
        /// <summary>The timer is paused.</summary>
        PAUSED = 2
    }
}
namespace Atomic.Elements
{
    /// <summary>
    /// Represents the current state of a timer.
    /// </summary>
    public enum TimerState
    {
        /// <summary>
        /// The timer is not running and has not been started.
        /// </summary>
        IDLE = 0,

        /// <summary>
        /// The timer is currently counting down.
        /// </summary>
        PLAYING = 1,

        /// <summary>
        /// The timer is paused and can be resumed.
        /// </summary>
        PAUSED = 2,

        /// <summary>
        /// The timer has finished counting down and expired.
        /// </summary>
        COMPLETED = 3
    }
}
namespace Atomic.Elements
{
    /// <summary>
    /// Represents the state of a stopwatch.
    /// </summary>
    public enum StopwatchState
    {
        /// <summary>
        /// The stopwatch is idle and not running.
        /// </summary>
        IDLE = 0,

        /// <summary>
        /// The stopwatch is currently running.
        /// </summary>
        PLAYING = 1,

        /// <summary>
        /// The stopwatch is paused and can be resumed.
        /// </summary>
        PAUSED = 2
    }
}
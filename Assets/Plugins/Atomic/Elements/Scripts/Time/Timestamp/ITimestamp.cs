namespace Atomic.Elements
{
    /// <summary>
    /// Represents a timestamp that can be tracked over time using ticks.
    /// </summary>
    public interface ITimestamp
    {
        /// <summary>
        /// Gets the tick at which the timestamp is considered complete.
        /// </summary>
        int EndTick { get; }

        /// <summary>
        /// Gets the number of ticks remaining until expiration.
        /// </summary>
        int RemainingTicks { get; }

        /// <summary>
        /// Gets the remaining time (in seconds) until expiration.
        /// </summary>
        float RemainingTime { get; }

        /// <summary>
        /// Starts the timestamp from the current time with the specified duration in seconds.
        /// </summary>
        /// <param name="seconds">The duration in seconds.</param>
        void StartFromSeconds(float seconds);

        /// <summary>
        /// Starts the timestamp with the specified number of ticks.
        /// </summary>
        /// <param name="ticks">The duration in ticks.</param>
        void StartFromTicks(int ticks);

        /// <summary>
        /// Stops and resets the timestamp.
        /// </summary>
        void Stop();

        /// <summary>
        /// Returns the progress of the timestamp relative to a given duration.
        /// </summary>
        /// <param name="duration">The full duration in seconds.</param>
        /// <returns>Progress as a value between 0 and 1.</returns>
        float GetProgress(float duration);

        /// <summary>
        /// Indicates whether the timestamp is stopped and has not started.
        /// </summary>
        /// <returns><c>true</c> if idle; otherwise, <c>false</c>.</returns>
        bool IsIdle();

        /// <summary>
        /// Indicates whether the timestamp is currently active and counting.
        /// </summary>
        /// <returns><c>true</c> if playing; otherwise, <c>false</c>.</returns>
        bool IsPlaying();

        /// <summary>
        /// Indicates whether the timestamp has expired.
        /// </summary>
        /// <returns><c>true</c> if expired; otherwise, <c>false</c>.</returns>
        bool IsExpired();
    }
}
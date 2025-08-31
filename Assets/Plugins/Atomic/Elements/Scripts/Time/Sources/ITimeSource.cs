using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a source that tracks current time and notifies changes.
    /// </summary>
    public interface ITimeSource
    {
        /// <summary>Raised when the current time changes.</summary>
        event Action<float> OnTimeChanged;

        /// <summary>Gets the current time.</summary>
        /// <returns>The current time in seconds.</returns>
        float GetTime();

        /// <summary>Sets the current time.</summary>
        /// <param name="time">The new time (0–duration).</param>
        void SetTime(float time);

        /// <summary>Resets the source to initial time.</summary>
        void ResetTime();
    }
}
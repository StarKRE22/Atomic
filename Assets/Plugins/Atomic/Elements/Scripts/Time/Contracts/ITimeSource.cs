using System;

namespace Atomic.Elements
{
    public interface ITimeSource
    {
        /// <summary>
        /// Invoked when the current remaining time changes.
        /// </summary>
        event Action<float> OnTimeChanged;
        
        /// <summary>
        /// Gets the current remaining time on the cooldown.
        /// </summary>
        float GetTime();

        /// <summary>
        /// Sets the current remaining time on the cooldown.
        /// Triggers <see cref="Cooldown.OnCurrentTimeChanged.OnCurrentTimeChanged"/> and <see cref="Cooldown"/>.
        /// </summary>
        /// <param name="time">The new time to set (must be between 0 and duration).</param>
        void SetTime(float time);
    }
}
using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a source that has a total duration and can notify changes.
    /// </summary>
    public interface IDurationSource
    {
        /// <summary>
        /// Invoked when the duration value changes.
        /// </summary>
        event Action<float> OnDurationChanged;

        /// <summary>
        /// Gets the total duration.
        /// </summary>
        /// <returns>The duration in seconds.</returns>
        float GetDuration();

        /// <summary>
        /// Sets the total duration.
        /// </summary>
        /// <param name="duration">The new duration value.</param>
        void SetDuration(float duration);
    }
}
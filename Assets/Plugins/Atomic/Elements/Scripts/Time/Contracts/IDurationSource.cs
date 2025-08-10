using System;

namespace Atomic.Elements
{
    public interface IDurationSource
    {
        /// <summary>
        /// Invoked when the duration value changes.
        /// </summary>
        event Action<float> OnDurationChanged;

        /// <summary>
        /// Gets the total duration of the cooldown.
        /// </summary>
        float GetDuration();

        /// <summary>
        /// Sets the total duration of the cooldown.
        /// Triggers <see cref="Cooldown.OnDurationChanged.OnDurationChanged"/> and <see cref="Cooldown"/>.
        /// </summary>
        /// <param name="duration">The new duration value.</param>
        void SetDuration(float duration);
    }
}
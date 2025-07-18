using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a cooldown timer that tracks remaining time,
    /// provides progress feedback, and raises events on state changes.
    /// </summary>
    public interface ICooldown
    {
        /// <summary>
        /// Invoked when the duration value changes.
        /// </summary>
        event Action<float> OnDurationChanged;

        /// <summary>
        /// Invoked when the current remaining time changes.
        /// </summary>
        event Action<float> OnCurrentTimeChanged;

        /// <summary>
        /// Invoked when the progress (0 to 1) changes.
        /// </summary>
        event Action<float> OnProgressChanged;

        /// <summary>
        /// Invoked when the cooldown has expired (time reaches zero).
        /// </summary>
        event Action OnExpired;

        /// <summary>
        /// Returns whether the cooldown has expired (i.e., current time is zero or less).
        /// </summary>
        bool IsExpired();

        /// <summary>
        /// Gets the progress of the cooldown (from 0 to 1).
        /// </summary>
        float GetProgress();

        /// <summary>
        /// Sets the progress (from 0 to 1), updating the remaining time accordingly.
        /// </summary>
        /// <param name="progress">The new progress value (0–1).</param>
        void SetProgress(float progress);

        /// <summary>
        /// Resets the cooldown to full duration.
        /// </summary>
        void Reset();

        /// <summary>
        /// Updates the cooldown by reducing current time by deltaTime.
        /// Fires <see cref="Cooldown.OnExpired.OnExpired"/> if the timer reaches zero.
        /// </summary>
        /// <param name="deltaTime">The time to subtract from the current time.</param>
        void Tick(float deltaTime);

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

        /// <summary>
        /// Gets the current remaining time on the cooldown.
        /// </summary>
        float GetCurrentTime();

        /// <summary>
        /// Sets the current remaining time on the cooldown.
        /// Triggers <see cref="Cooldown.OnCurrentTimeChanged.OnCurrentTimeChanged"/> and <see cref="Cooldown"/>.
        /// </summary>
        /// <param name="time">The new time to set (must be between 0 and duration).</param>
        void SetCurrentTime(float time);
    }
}
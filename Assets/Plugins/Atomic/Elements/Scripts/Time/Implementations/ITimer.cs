using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Defines a general-purpose timer interface that supports start, pause, resume, stop,
    /// progress tracking, and state change events.
    /// </summary>
    public interface ITimer
    {
        /// <summary>
        /// Invoked when the timer is started.
        /// </summary>
        event Action OnStarted;

        /// <summary>
        /// Invoked when the timer is stopped.
        /// </summary>
        event Action OnStopped;

        /// <summary>
        /// Invoked when the timer is paused.
        /// </summary>
        event Action OnPaused;

        /// <summary>
        /// Invoked when the timer is resumed from pause.
        /// </summary>
        event Action OnResumed;

        /// <summary>
        /// Invoked when the timer reaches its end.
        /// </summary>
        event Action OnExpired;

        /// <summary>
        /// Invoked when the state of the timer changes.
        /// </summary>
        event Action<TimerState> OnStateChanged;

        /// <summary>
        /// Invoked when the current remaining time changes.
        /// </summary>
        event Action<float> OnCurrentTimeChanged;

        /// <summary>
        /// Invoked when the duration changes.
        /// </summary>
        event Action<float> OnDurationChanged;

        /// <summary>
        /// Invoked when the progress value (0 to 1) changes.
        /// </summary>
        event Action<float> OnProgressChanged;

        /// <summary>
        /// Gets the current state of the timer.
        /// </summary>
        TimerState CurrentState { get; }

        /// <summary>
        /// Gets or sets the total duration of the timer.
        /// </summary>
        float Duration { get; set; }

        /// <summary>
        /// Gets or sets the current remaining time of the timer.
        /// </summary>
        float CurrentTime { get; set; }

        /// <summary>
        /// Gets or sets the progress of the timer (from 0 to 1).
        /// </summary>
        float Progress { get; set; }

        /// <summary>
        /// Returns the current state of the timer.
        /// </summary>
        TimerState GetCurrentState();

        /// <summary>
        /// Returns true if the timer is idle.
        /// </summary>
        bool IsIdle();

        /// <summary>
        /// Returns true if the timer is currently playing.
        /// </summary>
        bool IsPlaying();

        /// <summary>
        /// Returns true if the timer is currently paused.
        /// </summary>
        bool IsPaused();

        /// <summary>
        /// Returns true if the timer has expired.
        /// </summary>
        bool IsExpired();

        /// <summary>
        /// Gets the duration of the timer.
        /// </summary>
        float GetDuration();

        /// <summary>
        /// Gets the current remaining time of the timer.
        /// </summary>
        float GetCurrentTime();

        /// <summary>
        /// Forcefully starts the timer from the beginning.
        /// </summary>
        void ForceStart();

        /// <summary>
        /// Forcefully starts the timer from a specific time.
        /// </summary>
        /// <param name="currentTime">The time to start from.</param>
        void ForceStart(float currentTime);

        /// <summary>
        /// Starts the timer from the beginning.
        /// </summary>
        /// <returns>True if the timer was successfully started.</returns>
        void Start();

        /// <summary>
        /// Starts the timer from a specific time.
        /// </summary>
        /// <param name="currentTime">The time to start from.</param>
        /// <returns>True if the timer was successfully started.</returns>
        void Start(float currentTime);

        /// <summary>
        /// Plays or resumes the timer from idle.
        /// </summary>
        /// <returns>True if the timer was successfully started.</returns>
        bool Play();

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        /// <returns>True if the timer was successfully paused.</returns>
        bool Pause();

        /// <summary>
        /// Resumes the timer from pause.
        /// </summary>
        /// <returns>True if the timer was successfully resumed.</returns>
        bool Resume();

        /// <summary>
        /// Stops the timer and resets the current time.
        /// </summary>
        /// <returns>True if the timer was successfully stopped.</returns>
        bool Stop();

        /// <summary>
        /// Updates the timer by advancing it by a delta time value.
        /// </summary>
        /// <param name="deltaTime">The time in seconds to advance the timer.</param>
        void Tick(float deltaTime);

        /// <summary>
        /// Gets the progress of the timer (from 0 to 1).
        /// </summary>
        float GetProgress();

        /// <summary>
        /// Sets the progress of the timer.
        /// </summary>
        /// <param name="progress">A value from 0 to 1 representing the progress.</param>
        void SetProgress(float progress);

        /// <summary>
        /// Sets the duration of the timer.
        /// </summary>
        /// <param name="duration">The new duration value (must be ≥ 0).</param>
        void SetDuration(float duration);

        /// <summary>
        /// Sets the current remaining time of the timer.
        /// </summary>
        /// <param name="time">The time to set (must be ≥ 0).</param>
        void SetCurrentTime(float time);

        /// <summary>
        /// Resets the current time to the full duration.
        /// </summary>
        void ResetTime();
    }
}
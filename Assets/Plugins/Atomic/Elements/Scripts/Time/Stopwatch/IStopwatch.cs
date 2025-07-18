using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Defines a stopwatch interface that can start, pause, resume, stop,
    /// and track elapsed time, along with state change and time update events.
    /// </summary>
    public interface IStopwatch
    {
        /// <summary>
        /// Invoked when the stopwatch is started.
        /// </summary>
        event Action OnStarted;

        /// <summary>
        /// Invoked when the stopwatch is stopped.
        /// </summary>
        event Action OnStopped;

        /// <summary>
        /// Invoked when the stopwatch is paused.
        /// </summary>
        event Action OnPaused;

        /// <summary>
        /// Invoked when the stopwatch is resumed.
        /// </summary>
        event Action OnResumed;

        /// <summary>
        /// Invoked when the elapsed time changes.
        /// </summary>
        event Action<float> OnCurrentTimeChanged;

        /// <summary>
        /// Invoked when the stopwatch state changes.
        /// </summary>
        event Action<StopwatchState> OnStateChanged;

        /// <summary>
        /// Gets the current state of the stopwatch.
        /// </summary>
        StopwatchState CurrentState { get; }

        /// <summary>
        /// Returns the current state of the stopwatch.
        /// </summary>
        /// <returns>The current <see cref="StopwatchState"/>.</returns>
        StopwatchState GetCurrentState();

        /// <summary>
        /// Gets or sets the current elapsed time.
        /// </summary>
        float CurrentTime { get; set; }

        /// <summary>
        /// Returns the current elapsed time.
        /// </summary>
        /// <returns>The elapsed time in seconds.</returns>
        float GetCurrentTime();

        /// <summary>
        /// Returns true if the stopwatch is currently running.
        /// </summary>
        bool IsPlaying();

        /// <summary>
        /// Returns true if the stopwatch is currently paused.
        /// </summary>
        bool IsPaused();

        /// <summary>
        /// Returns true if the stopwatch is idle.
        /// </summary>
        bool IsIdle();

        /// <summary>
        /// Starts the stopwatch and resets time.
        /// </summary>
        /// <returns>True if started successfully; otherwise, false.</returns>
        bool Start();

        /// <summary>
        /// Starts the stopwatch without resetting the time.
        /// </summary>
        /// <returns>True if started successfully; otherwise, false.</returns>
        bool Play();

        /// <summary>
        /// Pauses the stopwatch.
        /// </summary>
        /// <returns>True if paused successfully; otherwise, false.</returns>
        bool Pause();

        /// <summary>
        /// Resumes the stopwatch from paused state.
        /// </summary>
        /// <returns>True if resumed successfully; otherwise, false.</returns>
        bool Resume();

        /// <summary>
        /// Stops the stopwatch and resets time to zero.
        /// </summary>
        /// <returns>True if stopped successfully; otherwise, false.</returns>
        bool Stop();

        /// <summary>
        /// Advances the stopwatch by the specified delta time.
        /// Only works when the stopwatch is running.
        /// </summary>
        /// <param name="deltaTime">The time in seconds to advance.</param>
        void Tick(float deltaTime);

        /// <summary>
        /// Sets the current elapsed time.
        /// </summary>
        /// <param name="time">The time to set (must be ≥ 0).</param>
        void SetCurrentTime(float time);
    }
}
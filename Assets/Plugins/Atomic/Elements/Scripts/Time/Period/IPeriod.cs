using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a looping cycle timer that tracks time progression and emits events on completion of each cycle.
    /// </summary>
    public interface IPeriod
    {
        /// <summary>Raised when the timer starts.</summary>
        event Action OnStarted;

        /// <summary>Raised when the timer stops.</summary>
        event Action OnStopped;

        /// <summary>Raised when the timer is paused.</summary>
        event Action OnPaused;

        /// <summary>Raised when the timer resumes from pause.</summary>
        event Action OnResumed;

        /// <summary>Raised when the cycle completes and starts over.</summary>
        event Action OnCycle;

        /// <summary>Raised when the state of the timer changes.</summary>
        event Action<PeriodState> OnStateChanged;

        /// <summary>Raised when the current time changes.</summary>
        event Action<float> OnTimeChanged;

        /// <summary>Raised when the progress changes.</summary>
        event Action<float> OnProgressChanged;

        /// <summary>Raised when the duration is changed.</summary>
        event Action<float> OnDurationChanged;

        /// <summary>Gets the current state of the cycle timer.</summary>
#if ODIN_INSPECTOR
#endif
        PeriodState CurrentState { get; }

        /// <summary>Gets or sets the total duration of one cycle.</summary>
#if ODIN_INSPECTOR
#endif
        float Duration { get; set; }

        /// <summary>Gets or sets the current time within the current cycle.</summary>
#if ODIN_INSPECTOR
#endif
        float Time { get; set; }

        /// <summary>Gets or sets the progress of the current cycle (from 0 to 1).</summary>
#if ODIN_INSPECTOR
#endif
        float Progress { get; set; }

        /// <summary>Returns the current state of the timer.</summary>
        PeriodState GetState();

        /// <summary>Returns true if the timer is currently playing.</summary>
        bool IsPlaying();

        /// <summary>Returns true if the timer is currently paused.</summary>
        bool IsPaused();

        /// <summary>Returns true if the timer is idle.</summary>
        bool IsIdle();

        /// <summary>Returns the total duration of the cycle.</summary>
        float GetDuration();

        /// <summary>Returns the current time of the cycle.</summary>
        float GetTime();

        /// <summary>Starts the timer from the beginning.</summary>
#if ODIN_INSPECTOR
#endif
        bool Start();

        /// <summary>Starts the timer from a specific current time.</summary>
        /// <param name="currentTime">The time to start the cycle from.</param>
#if ODIN_INSPECTOR
#endif
        bool Start(float currentTime);

        /// <summary>Plays the timer from idle state.</summary>
#if ODIN_INSPECTOR
#endif
        bool Play();

        /// <summary>Pauses the timer.</summary>
#if ODIN_INSPECTOR
#endif
        bool Pause();

        /// <summary>Resumes the timer from pause.</summary>
#if ODIN_INSPECTOR
#endif
        bool Resume();

        /// <summary>Stops the timer and resets current time.</summary>
#if ODIN_INSPECTOR
#endif
        bool Stop();

        /// <summary>Updates the timer with the elapsed delta time.</summary>
        /// <param name="deltaTime">The amount of time to advance the timer by.</param>
#if ODIN_INSPECTOR
#endif
        void Tick(float deltaTime);

        /// <summary>Sets a new duration for the cycle.</summary>
        /// <param name="duration">The new duration.</param>
#if ODIN_INSPECTOR
#endif
        void SetDuration(float duration);

        /// <summary>Sets the current time of the cycle.</summary>
        /// <param name="time">The time to set.</param>
#if ODIN_INSPECTOR
#endif
        void SetTime(float time);

        /// <summary>Gets the progress of the current cycle as a value between 0 and 1.</summary>
        float GetProgress();

        /// <summary>Sets the progress of the current cycle.</summary>
        /// <param name="progress">The progress to set (0 to 1).</param>
        void SetProgress(float progress);
    }
}
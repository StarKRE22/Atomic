using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a countdown timer that supports play, pause, stop, reset
    /// while broadcasting progress and state changes.
    /// </summary>
    public interface ICountdown
    {
        /// <summary>Raised when the countdown starts.</summary>
        event Action OnStarted;

        /// <summary>Raised when the countdown is stopped manually.</summary>
        event Action OnStopped;

        /// <summary>Raised when the countdown is paused.</summary>
        event Action OnPaused;

        /// <summary>Raised when the countdown is resumed after pause.</summary>
        event Action OnResumed;

        /// <summary>Raised when the countdown reaches the end.</summary>
        event Action OnExpired;

        /// <summary>Raised when the state changes.</summary>
        event Action<CountdownState> OnStateChanged;

        /// <summary>Raised when the current time changes.</summary>
        event Action<float> OnTimeChanged;

        /// <summary>Raised when the total duration changes.</summary>
        event Action<float> OnDurationChanged;

        /// <summary>Raised when the progress changes.</summary>
        event Action<float> OnProgressChanged;

        /// <summary>Gets the current state of the countdown.</summary>
#if ODIN_INSPECTOR
#endif
        CountdownState CurrentState { get; }

        /// <summary>Gets or sets the total duration of the countdown.</summary>
#if ODIN_INSPECTOR
#endif
        float Duration { get; set; }

        /// <summary>Gets or sets the current remaining time.</summary>
#if ODIN_INSPECTOR
#endif
        float CurrentTime { get; set; }

        /// <summary>Gets or sets the progress of the countdown (0–1).</summary>
#if ODIN_INSPECTOR
#endif
        float Progress { get; set; }

        /// <summary>Gets the current internal state.</summary>
        CountdownState GetState();

        /// <summary>Returns true if the countdown has not started yet.</summary>
        bool IsIdle();

        /// <summary>Returns true if the countdown is running.</summary>
        bool IsPlaying();

        /// <summary>Returns true if the countdown is paused.</summary>
        bool IsPaused();

        /// <summary>Returns true if the countdown has finished.</summary>
        bool IsExpired();

        /// <summary>Gets the total duration.</summary>
        float GetDuration();

        /// <summary>Gets the current remaining time.</summary>
        float GetTime();

        /// <summary>Starts the countdown from full duration. Stops any current state first.</summary>
#if ODIN_INSPECTOR
#endif
        void ForceStart();

        /// <summary>Starts the countdown from a specific current time. Stops any current state first.</summary>
#if ODIN_INSPECTOR
#endif
        void ForceStart(float currentTime);

        void Start();
        
        void Start(float currentTime);

        /// <summary>Plays the countdown without resetting time.</summary>
#if ODIN_INSPECTOR
#endif
        bool Play();

        /// <summary>Pauses the countdown if it is currently playing.</summary>
#if ODIN_INSPECTOR
#endif
        bool Pause();

        /// <summary>Resumes the countdown if it is currently paused.</summary>
#if ODIN_INSPECTOR
#endif
        bool Resume();

        /// <summary>Stops the countdown and resets the current time to zero.</summary>
#if ODIN_INSPECTOR
#endif
        bool Stop();

        /// <summary>Advances the countdown by deltaTime and triggers completion if needed.</summary>
#if ODIN_INSPECTOR
#endif
        void Tick(float deltaTime);

        /// <summary>Gets the normalized progress (0–1) of the countdown.</summary>
        float GetProgress();

        /// <summary>Sets the current progress of the countdown (0–1).</summary>
#if ODIN_INSPECTOR
#endif
        void SetProgress(float progress);

        /// <summary>Sets the total duration of the countdown.</summary>
#if ODIN_INSPECTOR
#endif
        void SetDuration(float duration);

        /// <summary>Sets the current time remaining in the countdown.</summary>
#if ODIN_INSPECTOR
#endif
        void SetTime(float time);

        /// <summary>Resets the current time to the full duration.</summary>
#if ODIN_INSPECTOR
#endif
        void ResetTime();
    }
}
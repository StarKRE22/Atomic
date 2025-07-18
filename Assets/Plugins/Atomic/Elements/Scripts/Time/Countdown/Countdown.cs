using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a countdown timer that supports play, pause, stop, reset
    /// while broadcasting progress and state changes.
    /// </summary>
    [Serializable]
    public class Countdown : ICountdown
    {
        /// <summary>
        /// Represents the state of the countdown.
        /// </summary>
        public enum State
        {
            /// <summary>No activity yet.</summary>
            IDLE = 0,
            /// <summary>Currently running.</summary>
            PLAYING = 1,
            /// <summary>Temporarily paused.</summary>
            PAUSED = 2,
            /// <summary>Finished naturally or by time running out.</summary>
            EXPIRED = 3
        }

        /// <summary>Raised when the countdown starts.</summary>
        public event Action OnPlaying;

        /// <summary>Raised when the countdown is stopped manually.</summary>
        public event Action OnStopped;

        /// <summary>Raised when the countdown is paused.</summary>
        public event Action OnPaused;

        /// <summary>Raised when the countdown is resumed after pause.</summary>
        public event Action OnResumed;

        /// <summary>Raised when the countdown reaches the end.</summary>
        public event Action OnExpired;

        /// <summary>Raised when the state changes.</summary>
        public event Action<State> OnStateChanged;

        /// <summary>Raised when the current time changes.</summary>
        public event Action<float> OnCurrentTimeChanged;

        /// <summary>Raised when the total duration changes.</summary>
        public event Action<float> OnDurationChanged;

        /// <summary>Raised when the progress changes.</summary>
        public event Action<float> OnProgressChanged;

        /// <summary>Gets the current state of the countdown.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public State CurrentState => this.currentState;

        /// <summary>Gets or sets the total duration of the countdown.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Duration
        {
            get => this.duration;
            set => this.SetDuration(value);
        }

        /// <summary>Gets or sets the current remaining time.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float CurrentTime
        {
            get => this.currentTime;
            set => this.SetCurrentTime(value);
        }

        /// <summary>Gets or sets the progress of the countdown (0–1).</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Progress
        {
            get => this.GetProgress();
            set => this.SetProgress(value);
        }

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [SerializeField]
        private float duration;

        private float currentTime;
        private State currentState;

        /// <summary>Initializes a new instance of <see cref="Countdown"/>.</summary>
        public Countdown()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Countdown"/> with a specific duration and optional looping.
        /// </summary>
        /// <param name="duration">The countdown duration.</param>
        public Countdown(float duration) => this.duration = duration;

        /// <summary>Gets the current internal state.</summary>
        public State GetCurrentState() => this.currentState;

        /// <summary>Returns true if the countdown has not started yet.</summary>
        public bool IsIdle() => this.currentState == State.IDLE;

        /// <summary>Returns true if the countdown is running.</summary>
        public bool IsPlaying() => this.currentState == State.PLAYING;

        /// <summary>Returns true if the countdown is paused.</summary>
        public bool IsPaused() => this.currentState == State.PAUSED;

        /// <summary>Returns true if the countdown has finished.</summary>
        public bool IsExpired() => this.currentState == State.EXPIRED;

        /// <summary>Gets the total duration.</summary>
        public float GetDuration() => this.duration;

        /// <summary>Gets the current remaining time.</summary>
        public float GetCurrentTime() => this.currentTime;

        /// <summary>Starts the countdown from full duration. Stops any current state first.</summary>
#if ODIN_INSPECTOR
        [Title("Methods")]
        [Button]
#endif
        public void ForceStart()
        {
            this.Stop();
            this.Start();
        }

        /// <summary>Starts the countdown from a specific current time. Stops any current state first.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart(float currentTime)
        {
            this.Stop();
            this.Start(currentTime);
        }

        public bool Start()
        {
            this.ResetTime();
            return this.Play();
        }

        public bool Start(float currentTime)
        {
            this.SetCurrentTime(currentTime);
            return this.Play();
        }

        /// <summary>Plays the countdown without resetting time.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Play()
        {
            if (this.currentState is not (State.IDLE or State.EXPIRED))
                return false;

            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnPlaying?.Invoke();
            return true;
        }

        /// <summary>Pauses the countdown if it is currently playing.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Pause()
        {
            if (this.currentState != State.PLAYING)
                return false;

            this.currentState = State.PAUSED;
            this.OnStateChanged?.Invoke(State.PAUSED);
            this.OnPaused?.Invoke();
            return true;
        }

        /// <summary>Resumes the countdown if it is currently paused.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Resume()
        {
            if (this.currentState != State.PAUSED)
                return false;

            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnResumed?.Invoke();
            return true;
        }

        /// <summary>Stops the countdown and resets the current time to zero.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Stop()
        {
            if (this.currentState == State.IDLE)
                return false;

            this.currentTime = 0;
            this.currentState = State.IDLE;
            this.OnStateChanged?.Invoke(State.IDLE);
            this.OnStopped?.Invoke();
            return true;
        }

        /// <summary>Advances the countdown by deltaTime and triggers completion if needed.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Tick(float deltaTime)
        {
            if (this.currentState != State.PLAYING)
                return;

            this.currentTime = Mathf.Max(0, this.currentTime - deltaTime);
            this.OnCurrentTimeChanged?.Invoke(this.currentTime);

            float progress = 1 - this.currentTime / this.duration;
            this.OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
                this.Complete();
        }

        /// <summary>Completes the countdown.</summary>
        private void Complete()
        {
            this.currentState = State.EXPIRED;
            this.OnStateChanged?.Invoke(State.EXPIRED);
            this.OnExpired?.Invoke();
        }

        /// <summary>Gets the normalized progress (0–1) of the countdown.</summary>
        public float GetProgress()
        {
            return this.currentState switch
            {
                State.PLAYING or State.PAUSED => 1 - this.currentTime / this.duration,
                State.EXPIRED => 1,
                _ => 0
            };
        }

        /// <summary>Sets the current progress of the countdown (0–1).</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            float remainingTime = this.duration * (1 - progress);

            this.currentTime = remainingTime;
            this.OnCurrentTimeChanged?.Invoke(remainingTime);
            this.OnProgressChanged?.Invoke(progress);
        }

        /// <summary>Sets the total duration of the countdown.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetDuration(float duration)
        {
            if (duration < 0)
                throw new Exception($"Duration can't be negative: {duration}!");

            if (Math.Abs(this.duration - duration) > float.Epsilon)
            {
                this.duration = duration;
                this.OnDurationChanged?.Invoke(duration);
            }
        }

        /// <summary>Sets the current time remaining in the countdown.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetCurrentTime(float time)
        {
            if (time < 0)
                throw new Exception($"Time can't be negative: {duration}!");

            float newTime = Mathf.Clamp(time, 0, this.duration);
            if (Math.Abs(newTime - this.currentTime) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnCurrentTimeChanged?.Invoke(newTime);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
        }

        /// <summary>Resets the current time to the full duration.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void ResetTime() => this.SetCurrentTime(this.duration);
    }
}
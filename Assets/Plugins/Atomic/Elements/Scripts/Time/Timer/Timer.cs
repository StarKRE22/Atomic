using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine; 
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a countdown timer that tracks duration, time, progress, and state.
    /// Provides methods for controlling playback and raises events on changes.
    /// </summary>
    [Serializable]
    public class Timer : ITimer
    {
        /// <inheritdoc/>
        public event Action OnStarted;

        /// <inheritdoc/>
        public event Action OnStopped;

        /// <inheritdoc/>
        public event Action OnPaused;

        /// <inheritdoc/>
        public event Action OnResumed;

        /// <inheritdoc/>
        public event Action OnExpired;

        /// <inheritdoc/>
        public event Action<TimerState> OnStateChanged;

        /// <inheritdoc/>
        public event Action<float> OnTimeChanged;

        /// <inheritdoc/>
        public event Action<float> OnDurationChanged;

        /// <inheritdoc/>
        public event Action<float> OnProgressChanged;

        /// <summary>
        /// Gets the current state of the timer.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public TimerState CurrentState => this.currentState;

        /// <summary>
        /// Gets or sets the total duration of the timer.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Duration
        {
            get => this.duration;
            set => this.SetDuration(value);
        }

        /// <summary>
        /// Gets or sets the current remaining time of the timer.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Time
        {
            get => this.currentTime;
            set => this.SetTime(value);
        }

        /// <summary>
        /// Gets or sets the progress of the timer (from 0 to 1).
        /// </summary>
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
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private float duration;

        private float currentTime;
        private TimerState currentState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        public Timer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class with a given duration.
        /// </summary>
        /// <param name="duration">The duration of the timer.</param>
        public Timer(float duration) => this.duration = duration;

        /// <summary>
        /// Gets the current state of the timer.
        /// </summary>
        public TimerState GetState() => this.currentState;

        /// <summary>
        /// Returns true if the timer is idle.
        /// </summary>
        public bool IsIdle() => this.currentState == TimerState.IDLE;

        /// <summary>
        /// Returns true if the timer is currently running.
        /// </summary>
        public bool IsPlaying() => this.currentState == TimerState.PLAYING;

        /// <summary>
        /// Returns true if the timer is paused.
        /// </summary>
        public bool IsPaused() => this.currentState == TimerState.PAUSED;

        /// <summary>
        /// Returns true if the timer has expired.
        /// </summary>
        public bool IsExpired() => this.currentState == TimerState.EXPIRED;

        /// <summary>
        /// Gets the total duration of the timer.
        /// </summary>
        public float GetDuration() => this.duration;

        /// <summary>
        /// Gets the current remaining time of the timer.
        /// </summary>
        public float GetTime() => this.currentTime;

        /// <summary>
        /// Stops and restarts the timer from zero.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart()
        {
            this.Stop();
            this.Start();
        }

        /// <summary>
        /// Stops and restarts the timer from a specific time.
        /// </summary>
        /// <param name="currentTime">The time to start from.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart(float currentTime)
        {
            this.Stop();
            this.Start(currentTime);
        }

        /// <summary>
        /// Starts the timer from zero.
        /// </summary>
        /// <returns>True if started successfully; otherwise false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Start()
        {
            this.ResetTime();
            this.Play();
        }

        /// <summary>
        /// Starts the timer from a specific time.
        /// </summary>
        /// <param name="currentTime">The time to start from.</param>
        /// <returns>True if started successfully; otherwise false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Start(float currentTime)
        {
            this.SetTime(currentTime);
            this.Play();
        }

        /// <summary>
        /// Starts or resumes the timer.
        /// </summary>
        /// <returns>True if the timer was started.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Play()
        {
            if (this.currentState is not (TimerState.IDLE or TimerState.EXPIRED))
                return false;

            this.currentState = TimerState.PLAYING;
            this.OnStateChanged?.Invoke(TimerState.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        /// <returns>True if paused successfully.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Pause()
        {
            if (this.currentState != TimerState.PLAYING)
                return false;

            this.currentState = TimerState.PAUSED;
            this.OnStateChanged?.Invoke(TimerState.PAUSED);
            this.OnPaused?.Invoke();
            return true;
        }

        /// <summary>
        /// Resumes the timer from paused state.
        /// </summary>
        /// <returns>True if resumed successfully.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Resume()
        {
            if (this.currentState != TimerState.PAUSED)
                return false;

            this.currentState = TimerState.PLAYING;
            this.OnStateChanged?.Invoke(TimerState.PLAYING);
            this.OnResumed?.Invoke();
            return true;
        }

        /// <summary>
        /// Stops the timer and resets time.
        /// </summary>
        /// <returns>True if stopped successfully.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Stop()
        {
            if (this.currentState == TimerState.IDLE)
                return false;

            this.currentTime = 0;
            this.currentState = TimerState.IDLE;
            this.OnStateChanged?.Invoke(TimerState.IDLE);
            this.OnStopped?.Invoke();
            return true;
        }

        /// <summary>
        /// Updates the timer with the given delta time.
        /// Triggers expiration if the timer reaches the duration.
        /// </summary>
        /// <param name="deltaTime">The time increment in seconds.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Tick(float deltaTime)
        {
            if (this.currentState != TimerState.PLAYING)
                return;

            this.currentTime = Math.Min(this.duration, this.currentTime + deltaTime);
            this.OnTimeChanged?.Invoke(this.currentTime);

            float progress = this.currentTime / this.duration;
            this.OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
                this.Complete();
        }

        private void Complete()
        {
            this.currentState = TimerState.EXPIRED;
            this.OnStateChanged?.Invoke(TimerState.EXPIRED);
            this.OnExpired?.Invoke();
        }

        /// <summary>
        /// Gets the current progress of the timer as a value from 0 to 1.
        /// </summary>
        public float GetProgress()
        {
            return this.currentState switch
            {
                TimerState.PLAYING or TimerState.PAUSED => this.currentTime / this.duration,
                TimerState.EXPIRED => 1,
                _ => 0
            };
        }

        /// <summary>
        /// Sets the progress of the timer.
        /// </summary>
        /// <param name="progress">The new progress value (0–1).</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetProgress(float progress)
        {
            progress = Math.Clamp(progress, 0, 1);
            this.currentTime = this.duration * progress;
            this.OnTimeChanged?.Invoke(this.currentTime);
            this.OnProgressChanged?.Invoke(progress);
        }

        /// <summary>
        /// Sets the total duration of the timer.
        /// </summary>
        /// <param name="duration">The new duration value.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetDuration(float duration)
        {
            if (duration < 0)
                return;

            if (Math.Abs(this.duration - duration) > float.Epsilon)
            {
                this.duration = duration;
                this.OnDurationChanged?.Invoke(duration);
            }
        }

        /// <summary>
        /// Sets the current remaining time of the timer.
        /// </summary>
        /// <param name="time">The new time to set (clamped to [0, duration]).</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetTime(float time)
        {
            if (time < 0)
                return;

            float newTime = Math.Clamp(time, 0, this.duration);
            if (Math.Abs(newTime - this.currentTime) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnTimeChanged?.Invoke(newTime);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
        }

        /// <summary>
        /// Resets the timer's current time to zero.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void ResetTime() => this.SetTime(0);
    }
}
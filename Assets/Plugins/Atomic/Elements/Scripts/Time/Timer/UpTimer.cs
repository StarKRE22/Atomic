using System;
using System.Runtime.CompilerServices;

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
    public class UpTimer : ITimer
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
        public event Action OnCompleted;

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
        /// Initializes a new instance of the <see cref="UpTimer"/> class.
        /// </summary>
        public UpTimer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpTimer"/> class with a given duration.
        /// </summary>
        /// <param name="duration">The duration of the timer.</param>
        public UpTimer(float duration) => this.duration = duration;

        /// <summary>
        /// Implicitly converts a <see cref="float"/> value to a <see cref="UpTimer"/> instance.
        /// </summary>
        /// <param name="duration">The duration in seconds for the new <see cref="UpTimer"/>.</param>
        /// <returns>A new <see cref="UpTimer"/> initialized with the specified duration.</returns>
        /// <example>
        /// <code>
        /// UpTimer timer = 5f; // creates a count up timer with duration = 5 seconds
        /// </code>
        /// </example>
        public static implicit operator UpTimer(float duration) => new(duration);

        /// <summary>
        /// Implicitly converts an <see cref="int"/> value to a <see cref="UpTimer"/> instance.
        /// </summary>
        /// <param name="duration">The duration in seconds for the new <see cref="UpTimer"/>.</param>
        /// <returns>A new <see cref="UpTimer"/> initialized with the specified duration.</returns>
        /// <example>
        /// <code>
        /// UpTimer timer = 3; // creates a count up timer with duration = 3 seconds
        /// </code>
        /// </example>
        public static implicit operator UpTimer(int duration) => new(duration);
        
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
        public bool IsStarted() => this.currentState == TimerState.PLAYING;

        /// <summary>
        /// Returns true if the timer is paused.
        /// </summary>
        public bool IsPaused() => this.currentState == TimerState.PAUSED;

        /// <summary>
        /// Returns true if the timer has expired.
        /// </summary>
        public bool IsCompleted() => this.currentState == TimerState.COMPLETED;

        /// <summary>
        /// Gets the total duration of the timer.
        /// </summary>
        public float GetDuration() => this.duration;

        /// <summary>
        /// Gets the current remaining time of the timer.
        /// </summary>
        public float GetTime() => this.currentTime;

        /// <summary>
        /// Starts the timer from a zero time.
        /// </summary>
        public void Start() => this.Start(0);

        /// <summary>
        /// Starts the timer from a specific time.
        /// </summary>
        /// <param name="time">The time to start from.</param>
        /// <returns>True if started successfully; otherwise false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Start(float time)
        {
            if (this.currentState is not (TimerState.IDLE or TimerState.COMPLETED))
                return;

            this.SetTime(time);
            this.currentState = TimerState.PLAYING;
            this.OnStateChanged?.Invoke(TimerState.PLAYING);
            this.OnStarted?.Invoke();
        }

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        /// <returns>True if paused successfully.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Pause()
        {
            if (this.currentState != TimerState.PLAYING)
                return;

            this.currentState = TimerState.PAUSED;
            this.OnStateChanged?.Invoke(TimerState.PAUSED);
            this.OnPaused?.Invoke();
        }

        /// <summary>
        /// Resumes the timer from paused state.
        /// </summary>
        /// <returns>True if resumed successfully.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Resume()
        {
            if (this.currentState != TimerState.PAUSED)
                return;

            this.currentState = TimerState.PLAYING;
            this.OnStateChanged?.Invoke(TimerState.PLAYING);
            this.OnResumed?.Invoke();
        }

        /// <summary>
        /// Stops the timer and resets time.
        /// </summary>
        /// <returns>True if stopped successfully.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Stop()
        {
            if (this.currentState == TimerState.IDLE)
                return;

            this.currentTime = 0;
            this.currentState = TimerState.IDLE;
            this.OnStateChanged?.Invoke(TimerState.IDLE);
            this.OnStopped?.Invoke();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Complete()
        {
            this.currentState = TimerState.COMPLETED;
            this.OnStateChanged?.Invoke(TimerState.COMPLETED);
            this.OnCompleted?.Invoke();
        }

        /// <summary>
        /// Gets the current progress of the timer as a value from 0 to 1.
        /// </summary>
        public float GetProgress()
        {
            return this.currentState switch
            {
                TimerState.PLAYING or TimerState.PAUSED => this.currentTime / this.duration,
                TimerState.COMPLETED => 1,
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
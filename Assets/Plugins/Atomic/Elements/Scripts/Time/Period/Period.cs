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
    /// Represents a looping cycle timer that tracks time progression and emits events on completion of each cycle.
    /// </summary>
    [Serializable]
    public class Period : IPeriod
    {
        /// <summary>Raised when the timer starts.</summary>
        public event Action OnStarted;

        /// <summary>Raised when the timer stops.</summary>
        public event Action OnStopped;

        /// <summary>Raised when the timer is paused.</summary>
        public event Action OnPaused;

        /// <summary>Raised when the timer resumes from pause.</summary>
        public event Action OnResumed;

        /// <summary>Raised when the cycle completes and starts over.</summary>
        public event Action OnPeriod;

        /// <summary>Raised when the state of the timer changes.</summary>
        public event Action<PeriodState> OnStateChanged;

        /// <summary>Raised when the current time changes.</summary>
        public event Action<float> OnTimeChanged;

        /// <summary>Raised when the progress changes.</summary>
        public event Action<float> OnProgressChanged;

        /// <summary>Raised when the duration is changed.</summary>
        public event Action<float> OnDurationChanged;

        /// <summary>Gets the current state of the cycle timer.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public PeriodState State => this.state;

        /// <summary>Gets or sets the total duration of one cycle.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Duration
        {
            get => this.duration;
            set => this.SetDuration(value);
        }

        /// <summary>Gets or sets the current time within the current cycle.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Time
        {
            get => this.time;
            set => this.SetTime(value);
        }

        /// <summary>Gets or sets the progress of the current cycle (from 0 to 1).</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Progress
        {
            get => this.GetProgress();
            set => this.SetProgress(value);
        }

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        private float duration;

        private float time;

        private PeriodState state;

        /// <summary>Initializes a new instance of the <see cref="Period"/> class.</summary>
        public Period()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Period"/> class with a specified duration.</summary>
        /// <param name="duration">The duration of each cycle.</param>
        public Period(float duration) => this.duration = duration;

        /// <summary>
        /// Implicitly converts a <see cref="float"/> value to a <see cref="Period"/> instance.
        /// </summary>
        /// <param name="duration">The duration in seconds for the new <see cref="Period"/>.</param>
        /// <returns>A new <see cref="Period"/> initialized with the specified duration.</returns>
        /// <example>
        /// <code>
        /// Period timer = 5f; // creates a Period with duration = 5 seconds
        /// </code>
        /// </example>
        public static implicit operator Period(float duration) => new(duration);

        /// <summary>
        /// Implicitly converts an <see cref="int"/> value to a <see cref="Period"/> instance.
        /// </summary>
        /// <param name="duration">The duration in seconds for the new <see cref="Period"/>.</param>
        /// <returns>A new <see cref="Period"/> initialized with the specified duration.</returns>
        /// <example>
        /// <code>
        /// Period timer = 3; // creates a Period with duration = 3 seconds
        /// </code>
        /// </example>
        public static implicit operator Period(int duration) => new(duration);
        
        /// <summary>Returns the current state of the timer.</summary>
        public PeriodState GetState() => this.state;

        /// <summary>Returns true if the timer is currently playing.</summary>
        public bool IsStarted() => this.state == PeriodState.PLAYING;

        /// <summary>Returns true if the timer is currently paused.</summary>
        public bool IsPaused() => this.state == PeriodState.PAUSED;

        /// <summary>Returns true if the timer is idle.</summary>
        public bool IsIdle() => this.state == PeriodState.IDLE;

        /// <summary>Returns the total duration of the cycle.</summary>
        public float GetDuration() => this.duration;

        /// <summary>Returns the current time of the cycle.</summary>
        public float GetTime() => this.time;

        public void Start() => this.Start(0);
        
        /// <summary>Starts the timer from a specific current time.</summary>
        /// <param name="time">The time to start the cycle from.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Start(float time)
        {
            if (this.state is not PeriodState.IDLE)
                return;

            this.time = Math.Clamp(time, 0, this.duration);
            this.state = PeriodState.PLAYING;
            this.OnStateChanged?.Invoke(PeriodState.PLAYING);
            this.OnStarted?.Invoke();
        }

        /// <summary>Pauses the timer.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Pause()
        {
            if (this.state != PeriodState.PLAYING)
                return;

            this.state = PeriodState.PAUSED;
            this.OnStateChanged?.Invoke(PeriodState.PAUSED);
            this.OnPaused?.Invoke();
        }

        /// <summary>Resumes the timer from pause.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Resume()
        {
            if (this.state != PeriodState.PAUSED)
                return;

            this.state = PeriodState.PLAYING;
            this.OnStateChanged?.Invoke(PeriodState.PLAYING);
            this.OnResumed?.Invoke();
        }

        /// <summary>Stops the timer and resets current time.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Stop()
        {
            if (this.state == PeriodState.IDLE)
                return;

            this.time = 0;
            this.state = PeriodState.IDLE;
            this.OnStateChanged?.Invoke(PeriodState.IDLE);
            this.OnStopped?.Invoke();
        }

        /// <summary>Updates the timer with the elapsed delta time.</summary>
        /// <param name="deltaTime">The amount of time to advance the timer by.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Tick(float deltaTime)
        {
            if (this.state != PeriodState.PLAYING)
                return;

            this.time = Math.Min(this.time + deltaTime, this.duration);
            this.OnTimeChanged?.Invoke(this.time);

            float progress = this.time / this.duration;
            this.OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
                this.Complete();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Complete()
        {
            this.OnPeriod?.Invoke();
            this.SetTime(this.time - this.duration);
        }

        /// <summary>Sets a new duration for the cycle.</summary>
        /// <param name="duration">The new duration.</param>
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

        /// <summary>Sets the current time of the cycle.</summary>
        /// <param name="time">The time to set.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetTime(float time)
        {
            time = Math.Clamp(time, 0, this.duration);
            if (Math.Abs(time - this.time) > float.Epsilon)
            {
                this.time = time;
                this.OnTimeChanged?.Invoke(time);
            }
        }

        /// <summary>Gets the progress of the current cycle as a value between 0 and 1.</summary>
        public float GetProgress()
        {
            return this.state switch
            {
                PeriodState.PLAYING or PeriodState.PAUSED => this.time / this.duration,
                _ => 0
            };
        }

        /// <summary>Sets the progress of the current cycle.</summary>
        /// <param name="progress">The progress to set (0 to 1).</param>
        public void SetProgress(float progress)
        {
            progress = Math.Clamp(progress, 0, 1);
            float newTime = this.duration * progress;
            this.time = newTime;
            this.OnTimeChanged?.Invoke(newTime);
            this.OnProgressChanged?.Invoke(progress);
        }
        
        public void ResetTime() => this.SetTime(0);
    }
}
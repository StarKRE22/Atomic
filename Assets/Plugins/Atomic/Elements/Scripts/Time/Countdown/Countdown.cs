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
    /// Represents a countdown timer that supports play, pause, stop, reset
    /// while broadcasting progress and state changes.
    /// </summary>
    [Serializable]
    public class Countdown : ICountdown
    {
        /// <summary>Raised when the countdown starts.</summary>
        public event Action OnStarted;

        /// <summary>Raised when the countdown is stopped manually.</summary>
        public event Action OnStopped;

        /// <summary>Raised when the countdown is paused.</summary>
        public event Action OnPaused;

        /// <summary>Raised when the countdown is resumed after pause.</summary>
        public event Action OnResumed;

        /// <summary>Raised when the countdown reaches the end.</summary>
        public event Action OnCompleted;

        /// <summary>Raised when the state changes.</summary>
        public event Action<CountdownState> OnStateChanged;

        /// <summary>Raised when the current time changes.</summary>
        public event Action<float> OnTimeChanged;

        /// <summary>Raised when the total duration changes.</summary>
        public event Action<float> OnDurationChanged;

        /// <summary>Raised when the progress changes.</summary>
        public event Action<float> OnProgressChanged;

        /// <summary>Gets the current state of the countdown.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public CountdownState State => this.state;

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
            get => this.time;
            set => this.SetTime(value);
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
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private float duration;

        private float time;

        private CountdownState state;

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
        public CountdownState GetState() => this.state;

        /// <summary>Returns true if the countdown has not started yet.</summary>
        public bool IsIdle() => this.state == CountdownState.IDLE;

        /// <summary>Returns true if the countdown is running.</summary>
        public bool IsStarted() => this.state == CountdownState.PLAYING;

        /// <summary>Returns true if the countdown is paused.</summary>
        public bool IsPaused() => this.state == CountdownState.PAUSED;

        /// <summary>Returns true if the countdown has finished.</summary>
        public bool IsCompleted() => this.state == CountdownState.COMPLETED;

        /// <summary>Gets the total duration.</summary>
        public float GetDuration() => this.duration;

        /// <summary>Gets the current remaining time.</summary>
        public float GetTime() => this.time;

        public void Start() => this.Start(this.duration);

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Start(float time)
        {
            if (this.state is not (CountdownState.IDLE or CountdownState.COMPLETED))
                return;

            this.SetTime(time);
            this.state = CountdownState.PLAYING;
            this.OnStateChanged?.Invoke(CountdownState.PLAYING);
            this.OnStarted?.Invoke();
        }

        /// <summary>Pauses the countdown if it is currently playing.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Pause()
        {
            if (this.state != CountdownState.PLAYING)
                return;

            this.state = CountdownState.PAUSED;
            this.OnStateChanged?.Invoke(CountdownState.PAUSED);
            this.OnPaused?.Invoke();
        }

        /// <summary>Resumes the countdown if it is currently paused.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Resume()
        {
            if (this.state != CountdownState.PAUSED)
                return;

            this.state = CountdownState.PLAYING;
            this.OnStateChanged?.Invoke(CountdownState.PLAYING);
            this.OnResumed?.Invoke();
        }

        /// <summary>Stops the countdown and resets the current time to zero.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Stop()
        {
            if (this.state == CountdownState.IDLE)
                return;

            this.time = 0;
            this.state = CountdownState.IDLE;
            this.OnStateChanged?.Invoke(CountdownState.IDLE);
            this.OnStopped?.Invoke();
        }

        /// <summary>Advances the countdown by deltaTime and triggers completion if needed.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Tick(float deltaTime)
        {
            if (this.state != CountdownState.PLAYING)
                return;

            this.time = Math.Max(0, this.time - deltaTime);
            this.OnTimeChanged?.Invoke(this.time);

            float progress = 1 - this.time / this.duration;
            this.OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
                this.Complete();
        }

        /// <summary>Completes the countdown.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Complete()
        {
            this.state = CountdownState.COMPLETED;
            this.OnStateChanged?.Invoke(CountdownState.COMPLETED);
            this.OnCompleted?.Invoke();
        }

        /// <summary>Gets the normalized progress (0–1) of the countdown.</summary>
        public float GetProgress()
        {
            return this.state switch
            {
                CountdownState.PLAYING or CountdownState.PAUSED => 1 - this.time / this.duration,
                CountdownState.COMPLETED => 1,
                _ => 0
            };
        }

        /// <summary>Sets the current progress of the countdown (0–1).</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetProgress(float progress)
        {
            progress = Math.Clamp(progress, 0, 1);
            float remainingTime = this.duration * (1 - progress);

            this.time = remainingTime;
            this.OnTimeChanged?.Invoke(remainingTime);
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
        public void SetTime(float time)
        {
            time = Math.Clamp(time, 0, this.duration);
            if (Math.Abs(time - this.time) > float.Epsilon)
            {
                this.time = time;
                this.OnTimeChanged?.Invoke(time);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
        }

        /// <summary>Resets the current time to the full duration.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void ResetTime() => this.SetTime(this.duration);
    }
}
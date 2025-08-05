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
        public event Action OnCycle;
        
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
        public PeriodState CurrentState => this.currentState;

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
            get => this.currentTime;
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
        
        private float currentTime;
        
        private PeriodState currentState;

        /// <summary>Initializes a new instance of the <see cref="Period"/> class.</summary>
        public Period()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Period"/> class with a specified duration.</summary>
        /// <param name="duration">The duration of each cycle.</param>
        public Period(float duration) => this.duration = duration;

        /// <summary>Returns the current state of the timer.</summary>
        public PeriodState GetState() => this.currentState;
      
        /// <summary>Returns true if the timer is currently playing.</summary>
        public bool IsPlaying() => this.currentState == PeriodState.PLAYING;
        
        /// <summary>Returns true if the timer is currently paused.</summary>
        public bool IsPaused() => this.currentState == PeriodState.PAUSED;
        
        /// <summary>Returns true if the timer is idle.</summary>
        public bool IsIdle() => this.currentState == PeriodState.IDLE;
        
        /// <summary>Returns the total duration of the cycle.</summary>
        public float GetDuration() => this.duration;
        
        /// <summary>Returns the current time of the cycle.</summary>
        public float GetTime() => this.currentTime;

        /// <summary>Starts the timer from the beginning.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start()
        {
            if (this.currentState is not PeriodState.IDLE)
                return false;

            this.currentTime = 0;
            this.currentState = PeriodState.PLAYING;
            this.OnStateChanged?.Invoke(PeriodState.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }
        
        /// <summary>Starts the timer from a specific current time.</summary>
        /// <param name="currentTime">The time to start the cycle from.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start(float currentTime)
        {
            if (this.currentState is not PeriodState.IDLE)
                return false;

            this.currentTime = Math.Clamp(currentTime, 0, this.duration);
            this.currentState = PeriodState.PLAYING;
            this.OnStateChanged?.Invoke(PeriodState.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }
        
        /// <summary>Plays the timer from idle state.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Play()
        {
            if (this.currentState is not PeriodState.IDLE)
                return false;

            this.currentState = PeriodState.PLAYING;
            this.OnStateChanged?.Invoke(PeriodState.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }
        
        /// <summary>Pauses the timer.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Pause()
        {
            if (this.currentState != PeriodState.PLAYING)
                return false;

            this.currentState = PeriodState.PAUSED;
            this.OnStateChanged?.Invoke(PeriodState.PAUSED);
            this.OnPaused?.Invoke();
            return true;
        }
        
        /// <summary>Resumes the timer from pause.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Resume()
        {
            if (this.currentState != PeriodState.PAUSED)
                return false;

            this.currentState = PeriodState.PLAYING;
            this.OnStateChanged?.Invoke(PeriodState.PLAYING);
            this.OnResumed?.Invoke();
            return true;
        }
        
        /// <summary>Stops the timer and resets current time.</summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Stop()
        {
            if (this.currentState == PeriodState.IDLE)
                return false;

            this.currentTime = 0;
            this.currentState = PeriodState.IDLE;
            this.OnStateChanged?.Invoke(PeriodState.IDLE);
            this.OnStopped?.Invoke();
            return true;
        }

        /// <summary>Updates the timer with the elapsed delta time.</summary>
        /// <param name="deltaTime">The amount of time to advance the timer by.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Tick(float deltaTime)
        {
            if (this.currentState != PeriodState.PLAYING)
                return;

            this.currentTime = Math.Min(this.currentTime + deltaTime, this.duration);
            this.OnTimeChanged?.Invoke(this.currentTime);

            float progress = this.currentTime / this.duration;
            this.OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
                this.CompleteCycle();
        }

        private void CompleteCycle()
        {
            this.SetTime(this.currentTime - this.duration);
            this.OnCycle?.Invoke();
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
            if (time < 0)
                return;

            float newTime = Math.Clamp(time, 0, this.duration);
            if (Math.Abs(newTime - this.duration) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnTimeChanged?.Invoke(newTime);
            }
        }
        
        /// <summary>Gets the progress of the current cycle as a value between 0 and 1.</summary>
        public float GetProgress()
        {
            return this.currentState switch
            {
                PeriodState.PLAYING or PeriodState.PAUSED => this.currentTime / this.duration,
                _ => 0
            };
        }

        /// <summary>Sets the progress of the current cycle.</summary>
        /// <param name="progress">The progress to set (0 to 1).</param>
        public void SetProgress(float progress)
        {
            progress = Math.Clamp(progress, 0, 1);
            float newTime = this.duration * progress;
            this.currentTime = newTime;
            this.OnTimeChanged?.Invoke(newTime);
            this.OnProgressChanged?.Invoke(progress);
        }
    }
}

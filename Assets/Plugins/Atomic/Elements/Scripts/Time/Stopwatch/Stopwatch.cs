using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a stopwatch timer that can start, pause, resume, stop,
    /// and track elapsed time, with events for state and time changes.
    /// </summary>
    [Serializable]
    public class Stopwatch : IStopwatch
    {
        /// <summary>
        /// Invoked when the stopwatch starts.
        /// </summary>
        public event Action OnStarted;
        
        /// <summary>
        /// Invoked when the stopwatch is stopped.
        /// </summary>
        public event Action OnStopped;
        
        /// <summary>
        /// Invoked when the stopwatch is paused.
        /// </summary>
        public event Action OnPaused;
        
        /// <summary>
        /// Invoked when the stopwatch is resumed from pause.
        /// </summary>
        public event Action OnResumed;
        
        /// <summary>
        /// Invoked when the current elapsed time changes.
        /// </summary>
        public event Action<float> OnTimeChanged;
        
        /// <summary>
        /// Invoked when the state of the stopwatch changes.
        /// </summary>
        public event Action<StopwatchState> OnStateChanged;

        /// <summary>
        /// Gets the current state of the stopwatch.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public StopwatchState State => this.state;

        /// <summary>
        /// Gets or sets the current elapsed time of the stopwatch.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Time
        {
            get => this.time;
            set => this.SetTime(value);
        }
        
        private StopwatchState state = StopwatchState.IDLE;
        private float time;

        /// <summary>
        /// Returns the current state of the stopwatch.
        /// </summary>
        public StopwatchState GetState() => this.state;
     
        /// <summary>
        /// Returns true if the stopwatch is currently running.
        /// </summary>
        public bool IsStarted() => this.state == StopwatchState.PLAYING;
    
        /// <summary>
        /// Returns true if the stopwatch is currently paused.
        /// </summary>
        public bool IsPaused() => this.state == StopwatchState.PAUSED;
   
        /// <summary>
        /// Returns true if the stopwatch is idle.
        /// </summary>
        public bool IsIdle() => this.state == StopwatchState.IDLE;
        
        /// <summary>
        /// Gets the current elapsed time.
        /// </summary>
        public float GetTime() => this.time;

        public void Start() => this.Start(0);
        
        /// <summary>
        /// Starts the stopwatch and resets elapsed time to zero.
        /// </summary>
        /// <returns>True if successfully started; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Start(float time)
        {
            if (this.state is not StopwatchState.IDLE)
                return;

            this.time = Math.Max(0, time);
            this.state = StopwatchState.PLAYING;
            this.OnStateChanged?.Invoke(StopwatchState.PLAYING);
            this.OnStarted?.Invoke();
        }

        /// <summary>
        /// Pauses the stopwatch.
        /// </summary>
        /// <returns>True if successfully paused; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Pause()
        {
            if (this.state != StopwatchState.PLAYING)
                return;

            this.state = StopwatchState.PAUSED;
            this.OnStateChanged?.Invoke(StopwatchState.PAUSED);
            this.OnPaused?.Invoke();
        }

        /// <summary>
        /// Resumes the stopwatch from paused state.
        /// </summary>
        /// <returns>True if successfully resumed; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Resume()
        {
            if (this.state != StopwatchState.PAUSED)
                return;

            this.state = StopwatchState.PLAYING;
            this.OnStateChanged?.Invoke(StopwatchState.PLAYING);
            this.OnResumed?.Invoke();
        }

        /// <summary>
        /// Stops the stopwatch and resets elapsed time to zero.
        /// </summary>
        /// <returns>True if successfully stopped; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Stop()
        {
            if (this.state == StopwatchState.IDLE)
                return;

            this.time = 0;
            this.state = StopwatchState.IDLE;
            this.OnStateChanged?.Invoke(StopwatchState.IDLE);
            this.OnStopped?.Invoke();
        }
        
        /// <summary>
        /// Advances the stopwatch by the given delta time.
        /// Only works when in the PLAYING state.
        /// </summary>
        /// <param name="deltaTime">The time to add to the current elapsed time.</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Tick(float deltaTime)
        {
            if (this.state == StopwatchState.PLAYING)
            {
                this.time += deltaTime;
                this.OnTimeChanged?.Invoke(this.time);
            }
        }
        
        /// <summary>
        /// Sets the current elapsed time.
        /// </summary>
        /// <param name="time">The time value to set (must be >= 0).</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetTime(float time)
        {
            time = Math.Max(time, 0);
            if (Math.Abs(this.time - time) > float.Epsilon)
            {
                this.time = time;
                this.OnTimeChanged?.Invoke(time);
            }
        }

        public void ResetTime() => this.SetTime(0);
    }
}
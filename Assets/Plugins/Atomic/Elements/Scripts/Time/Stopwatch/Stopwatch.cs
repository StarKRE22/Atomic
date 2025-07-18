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
        public event Action<float> OnCurrentTimeChanged;
        
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
        public StopwatchState CurrentState => this.currentState;

        /// <summary>
        /// Gets or sets the current elapsed time of the stopwatch.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float CurrentTime
        {
            get => this.currentTime;
            set => this.SetCurrentTime(value);
        }
        
        private StopwatchState currentState = StopwatchState.IDLE;
        private float currentTime;

        /// <summary>
        /// Returns the current state of the stopwatch.
        /// </summary>
        public StopwatchState GetCurrentState() => this.currentState;
     
        /// <summary>
        /// Returns true if the stopwatch is currently running.
        /// </summary>
        public bool IsPlaying() => this.currentState == StopwatchState.PLAYING;
    
        /// <summary>
        /// Returns true if the stopwatch is currently paused.
        /// </summary>
        public bool IsPaused() => this.currentState == StopwatchState.PAUSED;
   
        /// <summary>
        /// Returns true if the stopwatch is idle.
        /// </summary>
        public bool IsIdle() => this.currentState == StopwatchState.IDLE;
        
        /// <summary>
        /// Gets the current elapsed time.
        /// </summary>
        public float GetCurrentTime() => this.currentTime;

        /// <summary>
        /// Starts the stopwatch and resets elapsed time to zero.
        /// </summary>
        /// <returns>True if successfully started; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start()
        {
            if (this.currentState is not StopwatchState.IDLE)
                return false;

            this.currentTime = 0;
            this.currentState = StopwatchState.PLAYING;
            this.OnStateChanged?.Invoke(StopwatchState.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }
        
        /// <summary>
        /// Starts the stopwatch without resetting time (alias for Play).
        /// </summary>
        /// <returns>True if successfully started; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Play()
        {
            if (this.currentState is not StopwatchState.IDLE)
                return false;

            this.currentState = StopwatchState.PLAYING;
            this.OnStateChanged?.Invoke(StopwatchState.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }

        /// <summary>
        /// Pauses the stopwatch.
        /// </summary>
        /// <returns>True if successfully paused; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Pause()
        {
            if (this.currentState != StopwatchState.PLAYING)
                return false;

            this.currentState = StopwatchState.PAUSED;
            this.OnStateChanged?.Invoke(StopwatchState.PAUSED);
            this.OnPaused?.Invoke();
            return true;
        }

        /// <summary>
        /// Resumes the stopwatch from paused state.
        /// </summary>
        /// <returns>True if successfully resumed; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Resume()
        {
            if (this.currentState != StopwatchState.PAUSED)
                return false;

            this.currentState = StopwatchState.PLAYING;
            this.OnStateChanged?.Invoke(StopwatchState.PLAYING);
            this.OnResumed?.Invoke();
            return true;
        }

        /// <summary>
        /// Stops the stopwatch and resets elapsed time to zero.
        /// </summary>
        /// <returns>True if successfully stopped; otherwise, false.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Stop()
        {
            if (this.currentState == StopwatchState.IDLE)
                return false;

            this.currentTime = 0;
            this.currentState = StopwatchState.IDLE;
            this.OnStateChanged?.Invoke(StopwatchState.IDLE);
            this.OnStopped?.Invoke();
            return true;
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
            if (this.currentState == StopwatchState.PLAYING)
            {
                this.currentTime += deltaTime;
                this.OnCurrentTimeChanged?.Invoke(this.currentTime);
            }
        }
        
        /// <summary>
        /// Sets the current elapsed time.
        /// </summary>
        /// <param name="time">The time value to set (must be >= 0).</param>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetCurrentTime(float time)
        {
            float newTime = Math.Max(time, 0);
            if (Math.Abs(currentTime - newTime) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnCurrentTimeChanged?.Invoke(time);
            }
        }
    }
}
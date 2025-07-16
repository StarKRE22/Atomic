using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a countdown timer that supports play, pause, stop, reset, and looping,
    /// while broadcasting progress and state changes.
    /// </summary>
    [Serializable]
    public class Countdown : 
        IStartSource,
        IPauseSource,
        IExpiredSource,
        IProgressSource,
        ITickSource,
        ICurrentTimeSource, 
        IDurationSource
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
            ENDED = 3
        }

        /// <summary>Raised when the countdown starts.</summary>
        public event Action OnStarted;
      
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

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        /// <summary>Gets the current state of the countdown.</summary>
        public State CurrentState => this.currentState;

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        /// <summary>Gets or sets the total duration of the countdown.</summary>
        public float Duration
        {
            get { return this.duration; }
            set { this.SetDuration(value); }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        /// <summary>Gets or sets the current remaining time.</summary>
        public float CurrentTime
        {
            get { return this.currentTime; }
            set { this.SetCurrentTime(value); }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        /// <summary>Gets or sets the progress of the countdown (0–1).</summary>
        public float Progress
        {
            get { return this.GetProgress(); }
            set { this.SetProgress(value); }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        /// <summary>Gets or sets whether the countdown should loop upon completion.</summary>
        public bool Loop
        {
            get { return this.loop; }
            set { this.loop = value; }
        }

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [SerializeField]
        private float duration;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [SerializeField]
        private bool loop;

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
        /// <param name="loop">Whether the countdown should loop.</param>
        public Countdown(float duration, bool loop = false)
        {
            this.duration = duration;
            this.loop = loop;
        }

        /// <summary>Gets the current internal state.</summary>
        public State GetCurrentState() => this.currentState; 
      
        /// <summary>Returns true if the countdown has not started yet.</summary>
        public bool IsIdle() => this.currentState == State.IDLE;
        
        /// <summary>Returns true if the countdown is running.</summary>
        public bool IsPlaying() => this.currentState == State.PLAYING;
        
        /// <summary>Returns true if the countdown is paused.</summary>
        public bool IsPaused() => this.currentState == State.PAUSED;
        
        /// <summary>Returns true if the countdown has finished.</summary>
        public bool IsExpired() => this.currentState == State.ENDED;

        /// <summary>Gets the total duration.</summary>
        public float GetDuration() => this.duration;
        
        /// <summary>Gets the current remaining time.</summary>
        public float GetCurrentTime() => this.currentTime;

#if ODIN_INSPECTOR
        [Title("Methods")]
        [Button]
#endif
        /// <summary>Starts the countdown from full duration. Stops any current state first.</summary>
        public void ForceStart()
        {
            this.Stop();
            this.Start();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Starts the countdown from a specific current time. Stops any current state first.</summary>
        public void ForceStart(float currentTime)
        {
            this.Stop();
            this.Start(currentTime);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Starts the countdown from full duration.</summary>
        public bool Start()
        {
            if (this.currentState is not (State.IDLE or State.ENDED))
                return false;

            this.currentTime = this.duration;
            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }
        
        
#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Starts the countdown from a specific time.</summary>
        public bool Start(float currentTime)
        {
            if (this.currentState is not (State.IDLE or State.ENDED))
                return false;

            this.currentTime = Mathf.Clamp(currentTime, 0, this.duration);
            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }
        
#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Plays the countdown without resetting time.</summary>
        public bool Play()
        {
            if (this.currentState is not (State.IDLE or State.ENDED))
                return false;

            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }


#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Pauses the countdown if it is currently playing.</summary>
        public bool Pause()
        {
            if (this.currentState != State.PLAYING)
                return false;

            this.currentState = State.PAUSED;
            this.OnStateChanged?.Invoke(State.PAUSED);
            this.OnPaused?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Resumes the countdown if it is currently paused.</summary>
        public bool Resume()
        {
            if (this.currentState != State.PAUSED)
                return false;

            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnResumed?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Stops the countdown and resets the current time to zero.</summary>
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

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Advances the countdown by deltaTime and triggers completion if needed.</summary>
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

        /// <summary>Completes the countdown and triggers looping if enabled.</summary>
        private void Complete()
        {
            this.currentState = State.ENDED;
            this.OnStateChanged?.Invoke(State.ENDED);
            this.OnExpired?.Invoke();

            if (this.loop) 
                this.Start();
        }

        /// <summary>Gets the normalized progress (0–1) of the countdown.</summary>
        public float GetProgress()
        {
            return this.currentState switch
            {
                State.PLAYING or State.PAUSED => 1 - this.currentTime / this.duration,
                State.ENDED => 1,
                _ => 0
            };
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Sets the current progress of the countdown (0–1).</summary>
        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            float remainingTime = this.duration * (1 - progress);

            this.currentTime = remainingTime;
            this.OnCurrentTimeChanged?.Invoke(remainingTime);
            this.OnProgressChanged?.Invoke(progress);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Sets the total duration of the countdown.</summary>
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

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Sets the current time remaining in the countdown.</summary>
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

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>Resets the current time to the full duration.</summary>
        public void ResetTime() => this.SetCurrentTime(this.duration);
    }
}
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class Cycle : IPlayable, IProgressable, ITickable, IPausable
    {
        public enum State
        {
            IDLE = 0,
            PLAYING = 1,
            PAUSED = 2
        }

        public event System.Action OnStarted;
        public event System.Action OnStopped;
        public event System.Action OnPaused;
        public event System.Action OnResumed;

        public event System.Action OnCycle;
        public event System.Action<State> OnStateChanged;

        public event System.Action<float> OnCurrentTimeChanged;
        public event System.Action<float> OnProgressChanged;
        public event System.Action<float> OnDurationChanged;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public State CurrentState
        {
            get { return this.currentState; }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Duration
        {
            get { return this.duration; }
            set { this.SetDuration(value); }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float CurrentTime
        {
            get { return this.currentTime; }
            set { this.SetCurrentTime(value); }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Progress
        {
            get { return this.GetProgress(); }
            set { this.SetProgress(value); }
        }
        
        [SerializeField]
        private float duration;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private float currentTime;
        private State currentState;

        public Cycle()
        {
        }

        public Cycle(float duration)
        {
            this.duration = duration;
        }

        public State GetCurrentState() => this.currentState;
        public bool IsPlaying() => this.currentState == State.PLAYING;
        public bool IsPaused() => this.currentState == State.PAUSED;
        public bool IsIdle() => this.currentState == State.IDLE;

        public float GetDuration() => this.duration;
        public float GetCurrentTime() => this.currentTime;

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start()
        {
            if (this.currentState is not State.IDLE)
            {
                return false;
            }

            this.currentTime = 0;
            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start(float currentTime)
        {
            if (this.currentState is not State.IDLE)
            {
                return false;
            }

            this.currentTime = Mathf.Clamp(currentTime, 0, this.duration);
            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnStarted?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Pause()
        {
            if (this.currentState != State.PLAYING)
            {
                return false;
            }

            this.currentState = State.PAUSED;
            this.OnStateChanged?.Invoke(State.PAUSED);
            this.OnPaused?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Resume()
        {
            if (this.currentState != State.PAUSED)
            {
                return false;
            }

            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnResumed?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Stop()
        {
            if (this.currentState == State.IDLE)
            {
                return false;
            }

            this.currentTime = 0;
            this.currentState = State.IDLE;
            this.OnStateChanged?.Invoke(State.IDLE);
            this.OnStopped?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Tick(float deltaTime)
        {
            if (this.currentState != State.PLAYING)
            {
                return;
            }

            this.currentTime = Mathf.Min(this.currentTime + deltaTime, this.duration);
            this.OnCurrentTimeChanged?.Invoke(this.currentTime);

            float progress = this.currentTime / this.duration;
            this.OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
            {
                this.CompleteCycle();
            }
        }

        private void CompleteCycle()
        {
            this.OnCycle?.Invoke();
            this.SetCurrentTime(this.currentTime - this.duration);
        }
        
#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetDuration(float duration)
        {
            if (duration < 0)
            {
                return;
            }

            if (Math.Abs(this.duration - duration) > float.Epsilon)
            {
                this.duration = duration;
                this.OnDurationChanged?.Invoke(duration);
            }
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetCurrentTime(float time)
        {
            if (time < 0)
            {
                return;
            }

            float newTime = Mathf.Clamp(time, 0, this.duration);
            if (Math.Abs(newTime - this.duration) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnCurrentTimeChanged?.Invoke(newTime);
            }
        }

        public float GetProgress()
        {
            return this.currentState switch
            {
                State.PLAYING or State.PAUSED => this.currentTime / this.duration,
                _ => 0
            };
        }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            float newTime = this.duration * progress;
            this.currentTime = newTime;
            this.OnCurrentTimeChanged?.Invoke(newTime);
            this.OnProgressChanged?.Invoke(progress);
        }
    }
}
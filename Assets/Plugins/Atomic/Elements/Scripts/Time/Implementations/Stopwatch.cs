using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class Stopwatch : IPlayable, ITimeable, ITickable, IPausable
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
        
        public event System.Action<float> OnCurrentTimeChanged;
        public event System.Action<State> OnStateChanged;

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
        public float CurrentTime
        {
            get { return this.currentTime; }
            set { this.SetCurrentTime(value); }
        }
        
        private State currentState = State.IDLE;
        private float currentTime;

        public State GetCurrentState() => this.currentState;
        public bool IsPlaying() => this.currentState == State.PLAYING;
        public bool IsPaused() => this.currentState == State.PAUSED;
        public bool IsIdle() => this.currentState == State.IDLE;
        
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
            if (this.currentState == State.PLAYING)
            {
                this.currentTime += deltaTime;
                this.OnCurrentTimeChanged?.Invoke(this.currentTime);
            }
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetCurrentTime(float time)
        {
            float newTime = Mathf.Max(time, 0);

            if (Math.Abs(currentTime - newTime) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnCurrentTimeChanged?.Invoke(time);
            }
        }
    }
}
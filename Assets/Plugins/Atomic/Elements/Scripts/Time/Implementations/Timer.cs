using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class Timer : IPlayable, IPausable, IProgressable, IEndable, ITickable
    {
        public enum State
        {
            IDLE = 0,
            PLAYING = 1,
            PAUSED = 2,
            ENDED = 3
        }
        
        public event System.Action OnStarted;
        public event System.Action OnStopped;
        public event System.Action OnPaused;
        public event System.Action OnResumed;
        public event System.Action OnEnded;

        public event System.Action<State> OnStateChanged;

        public event System.Action<float> OnCurrentTimeChanged;
        public event System.Action<float> OnDurationChanged;
        public event System.Action<float> OnProgressChanged;

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

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
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

        
        public Timer()
        {
        }

        public Timer(float duration, bool loop = false)
        {
            this.duration = duration;
            this.loop = loop;
        }
        
        public State GetCurrentState() => this.currentState;
        public bool IsIdle() => this.currentState == State.IDLE;
        public bool IsPlaying() => this.currentState == State.PLAYING;
        public bool IsPaused() => this.currentState == State.PAUSED;
        public bool IsEnded() => this.currentState == State.ENDED;

        public float GetDuration() => this.duration;
        public float GetCurrentTime() => this.currentTime;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart()
        {
            this.Stop();
            this.Start();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart(float currentTime)
        {
            this.Stop();
            this.Start(currentTime);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start()
        {
            if (this.currentState is not (State.IDLE or State.ENDED))
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
            if (this.currentState is not (State.IDLE or State.ENDED))
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

            this.currentTime = Mathf.Min(this.duration, this.currentTime + deltaTime);
            this.OnCurrentTimeChanged?.Invoke(this.currentTime);

            float progress = this.currentTime / this.duration;
            this.OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
            {
                this.Complete();
            }
        }
        
        private void Complete()
        {
            this.currentState = State.ENDED;
            this.OnStateChanged?.Invoke(State.ENDED);
            this.OnEnded?.Invoke();

            if (this.loop)
            {
                this.Start();
            }
        }
        
        public float GetProgress()
        {
            return this.currentState switch
            {
                State.PLAYING or State.PAUSED => this.currentTime / this.duration,
                State.ENDED => 1,
                _ => 0
            };
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this.currentTime = this.duration * progress;
            this.OnCurrentTimeChanged?.Invoke(this.currentTime);
            this.OnProgressChanged?.Invoke(progress);
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
            if (Math.Abs(newTime - this.currentTime) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnCurrentTimeChanged?.Invoke(newTime);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
        }
    }


    [Serializable]
    public class Timer<T> : IPlayable<T>, IProgressable, IEndable<T>, ITickable, IValue<T>, IPausable
    {
      public enum State
        {
            IDLE = 0,
            PLAYING = 1,
            PAUSED = 2,
            ENDED = 3
        }
        
        public event System.Action<T> OnStarted;
        public event System.Action<T> OnStopped;
        public event System.Action<T> OnEnded;

        public event System.Action OnPaused;
        public event System.Action OnResumed;

        public event System.Action<State> OnStateChanged;

        public event System.Action<float> OnCurrentTimeChanged;
        public event System.Action<float> OnDurationChanged;
        public event System.Action<float> OnProgressChanged;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public T Value
        {
            get { return this.currentValue; }
        }

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

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
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
        private T currentValue;

        public Timer()
        {
        }

        public Timer(float duration, bool loop = false)
        {
            this.duration = duration;
            this.loop = loop;
        }
        
        public State GetCurrentState() => this.currentState;
        public bool IsIdle() => this.currentState == State.IDLE;
        public bool IsPlaying() => this.currentState == State.PLAYING;
        public bool IsPaused() => this.currentState == State.PAUSED;
        public bool IsEnded() => this.currentState == State.ENDED;

        public float GetDuration() => this.duration;
        public float GetCurrentTime() => this.currentTime;
        public T GetCurrentValue() => this.currentValue;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart(T value)
        {
            this.Stop();
            this.Start(value);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart(float currentTime, T value)
        {
            this.Stop();
            this.Start(currentTime, value);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start(T value)
        {
            if (this.currentState is not (State.IDLE or State.ENDED))
            {
                return false;
            }

            this.currentValue = value;
            this.currentTime = 0;
            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnStarted?.Invoke(value);
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start(float currentTime, T value)
        {
            if (this.currentState is not (State.IDLE or State.ENDED))
            {
                return false;
            }

            this.currentValue = value;
            this.currentTime = Mathf.Clamp(currentTime, 0, this.duration);
            this.currentState = State.PLAYING;
            this.OnStateChanged?.Invoke(State.PLAYING);
            this.OnStarted?.Invoke(value);
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

            T value = this.currentValue;

            this.currentValue = default;
            this.currentTime = 0;
            this.currentState = State.IDLE;
            this.OnStateChanged?.Invoke(State.IDLE);
            this.OnStopped?.Invoke(value);
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

            this.currentTime = Mathf.Min(this.duration, this.currentTime + deltaTime);
            this.OnCurrentTimeChanged?.Invoke(this.currentTime);

            float progress = this.currentTime / this.duration;
            this.OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
            {
                this.Complete();
            }
        }
        
        private void Complete()
        {
            T value = this.currentValue;
            this.currentValue = value;

            this.currentState = State.ENDED;
            this.OnStateChanged?.Invoke(State.ENDED);
            this.OnEnded?.Invoke(value);

            if (this.loop)
            {
                this.Start(value);
            }
        }
        
        public float GetProgress()
        {
            return this.currentState switch
            {
                State.PLAYING or State.PAUSED => this.currentTime / this.duration,
                State.ENDED => 1,
                _ => 0
            };
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this.currentTime = this.duration * progress;
            this.OnCurrentTimeChanged?.Invoke(this.currentTime);
            this.OnProgressChanged?.Invoke(progress);
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
            if (Math.Abs(newTime - this.currentTime) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnCurrentTimeChanged?.Invoke(newTime);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
        }
    }
}
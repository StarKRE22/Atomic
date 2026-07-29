using System;

namespace Atomic.Elements
{
    public sealed class ThreadSafeCooldown : ICooldown, MainThreadDispatcher.IFlushable
    {
        public event Action<float> OnDurationChanged;
        public event Action<float> OnTimeChanged;
        public event Action<float> OnProgressChanged;
        public event Action OnCompleted;

        private readonly object _lock = new();

        private float _duration;
        private float _time;

        private bool _durationDirty;
        private bool _timeDirty;
        private bool _progressDirty;
        private bool _completed;

        public ThreadSafeCooldown(float duration) : this(duration, duration)
        {
        }

        public ThreadSafeCooldown(float duration, float current)
        {
            _duration = Math.Max(0, duration);
            _time = Math.Clamp(current, 0, _duration);
        }

        public float Duration
        {
            get
            {
                lock (_lock) return _duration;
            }
            set => SetDuration(value);
        }

        public float CurrentTime
        {
            get
            {
                lock (_lock) return _time;
            }
            set => SetTime(value);
        }

        public float Progress
        {
            get
            {
                lock (_lock) return GetProgressInternal();
            }
            set => SetProgress(value);
        }

        public bool IsCompleted()
        {
            lock (_lock)
                return _time <= 0;
        }

        public bool IsPlaying()
        {
            lock (_lock)
                return _time > 0;
        }

        public void Tick(float deltaTime)
        {
            lock (_lock)
            {
                if (_time <= 0)
                    return;

                float newTime = Math.Max(0, _time - deltaTime);
                if (Math.Abs(newTime - _time) <= float.Epsilon)
                    return;

                _time = newTime;

                _timeDirty = true;
                _progressDirty = true;

                if (_time <= 0)
                    _completed = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

        public void ResetTime()
        {
            SetTime(_duration);
        }

        public float GetDuration()
        {
            return this.Duration;
        }

        public void SetDuration(float duration)
        {
            duration = Math.Max(0, duration);

            lock (_lock)
            {
                if (Math.Abs(_duration - duration) <= float.Epsilon)
                    return;

                _duration = duration;

                if (_time > _duration)
                    _time = _duration;

                _durationDirty = true;
                _progressDirty = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

        public float GetTime()
        {
            return this.CurrentTime;
        }

        public void SetTime(float time)
        {
            if (time < 0)
                throw new ArgumentException($"Time can't be negative: {time}!", nameof(time));

            lock (_lock)
            {
                float newTime = Math.Clamp(time, 0, _duration);

                if (Math.Abs(newTime - _time) <= float.Epsilon)
                    return;

                _time = newTime;

                _timeDirty = true;
                _progressDirty = true;

                if (_time <= 0)
                    _completed = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

        public float GetProgress()
        {
            return this.Progress;
        }

        public void SetProgress(float progress)
        {
            progress = Math.Clamp(progress, 0, 1);

            lock (_lock)
            {
                float newTime = _duration * progress;

                if (Math.Abs(newTime - _time) <= float.Epsilon)
                    return;

                _time = newTime;

                _timeDirty = true;
                _progressDirty = true;

                if (_time <= 0)
                    _completed = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

        private float GetProgressInternal()
        {
            return _duration <= 0 ? 0 : _time / _duration;
        }

        void MainThreadDispatcher.IFlushable.Flush()
        {
            float duration, time, progress;
            bool durationDirty, timeDirty, progressDirty, completed;

            Action<float> durationHandler;
            Action<float> timeHandler;
            Action<float> progressHandler;
            Action completedHandler;

            lock (_lock)
            {
                duration = _duration;
                time = _time;
                progress = GetProgressInternal();

                durationDirty = _durationDirty;
                timeDirty = _timeDirty;
                progressDirty = _progressDirty;
                completed = _completed;

                _durationDirty = false;
                _timeDirty = false;
                _progressDirty = false;
                _completed = false;

                durationHandler = OnDurationChanged;
                timeHandler = OnTimeChanged;
                progressHandler = OnProgressChanged;
                completedHandler = OnCompleted;
            }

            if (durationDirty)
                durationHandler?.Invoke(duration);

            if (timeDirty)
                timeHandler?.Invoke(time);

            if (progressDirty)
                progressHandler?.Invoke(progress);

            if (completed)
                completedHandler?.Invoke();
        }

        public override string ToString()
        {
            lock (_lock)
                return $"{nameof(_duration)}: {_duration}, {nameof(_time)}: {_time}";
        }
    }
}
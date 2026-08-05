using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a thread-safe cooldown timer whose change notifications
    /// are dispatched on the main thread.
    /// </summary>
    public sealed class ThreadSafeCooldown : ICooldown, MainThreadDispatcher.IFlushable
    {
         /// <summary>
        /// Occurs when the cooldown duration changes.
        /// </summary>
        public event Action<float> OnDurationChanged;

        /// <summary>
        /// Occurs when the remaining time changes.
        /// </summary>
        public event Action<float> OnTimeChanged;

        /// <summary>
        /// Occurs when the cooldown progress changes.
        /// </summary>
        public event Action<float> OnProgressChanged;

         /// <summary>
        /// Occurs when the cooldown reaches zero.
        /// </summary>
        public event Action OnCompleted;

        private readonly object _lock = new();

        private float _duration;
        private float _time;

        private bool _durationDirty;
        private bool _timeDirty;
        private bool _progressDirty;
        private bool _completed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeCooldown"/> class
        /// with the specified duration. The current time is initialized to the duration.
        /// </summary>
        /// <param name="duration">The cooldown duration.</param>
        public ThreadSafeCooldown(float duration) : this(duration, duration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeCooldown"/> class.
        /// </summary>
        /// <param name="duration">The cooldown duration.</param>
        /// <param name="current">The initial remaining time.</param>
        public ThreadSafeCooldown(float duration, float current)
        {
            _duration = Math.Max(0, duration);
            _time = Math.Clamp(current, 0, _duration);
        }

        /// <summary>
        /// Gets or sets the cooldown duration.
        /// </summary>
        public float Duration
        {
            get
            {
                lock (_lock) return _duration;
            }
            set => SetDuration(value);
        }

        /// <summary>
        /// Gets or sets the remaining cooldown time.
        /// </summary>
        public float CurrentTime
        {
            get
            {
                lock (_lock) return _time;
            }
            set => SetTime(value);
        }

        /// <summary>
        /// Gets or sets the normalized cooldown progress.
        /// </summary>
        /// <value>
        /// A value in the range [0, 1], where 0 represents a completed cooldown
        /// and 1 represents a full cooldown.
        /// </value>
        public float Progress
        {
            get
            {
                lock (_lock) return GetProgressInternal();
            }
            set => SetProgress(value);
        }

        /// <summary>
        /// Determines whether the cooldown has completed.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the remaining time is zero; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public bool IsCompleted()
        {
            lock (_lock)
                return _time <= 0;
        }

        /// <summary>
        /// Determines whether the cooldown is currently active.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the remaining time is greater than zero;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsPlaying()
        {
            lock (_lock)
                return _time > 0;
        }

        /// <summary>
        /// Advances the cooldown by the specified amount of time.
        /// </summary>
        /// <param name="deltaTime">The elapsed time.</param>
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

        /// <summary>
        /// Resets the remaining time to the current duration.
        /// </summary>
        public void ResetTime()
        {
            SetTime(_duration);
        }

        /// <summary>
        /// Returns the cooldown duration.
        /// </summary>
        /// <returns>The cooldown duration.</returns>
        public float GetDuration()
        {
            return this.Duration;
        }

        /// <summary>
        /// Sets the cooldown duration.
        /// </summary>
        /// <param name="duration">The new cooldown duration.</param>
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

        /// <summary>
        /// Returns the remaining cooldown time.
        /// </summary>
        /// <returns>The remaining cooldown time.</returns>
        public float GetTime()
        {
            return this.CurrentTime;
        }

        /// <summary>
        /// Sets the remaining cooldown time.
        /// </summary>
        /// <param name="time">The new remaining time.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="time"/> is negative.
        /// </exception>
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

        /// <summary>
        /// Returns the normalized cooldown progress.
        /// </summary>
        /// <returns>
        /// A value in the range [0, 1].
        /// </returns>
        public float GetProgress()
        {
            return this.Progress;
        }

        /// <summary>
        /// Sets the cooldown progress.
        /// </summary>
        /// <param name="progress">
        /// The normalized progress. Values outside the range [0, 1] are clamped.
        /// </param>
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

        /// <summary>
        /// Returns a string representation of the cooldown.
        /// </summary>
        /// <returns>
        /// A string containing the duration and remaining time.
        /// </returns>
        public override string ToString()
        {
            lock (_lock)
                return $"{nameof(_duration)}: {_duration}, {nameof(_time)}: {_time}";
        }
    }
}

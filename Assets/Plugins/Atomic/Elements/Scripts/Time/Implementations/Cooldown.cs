using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a cooldown timer that tracks remaining time,
    /// provides progress feedback, and raises events on state changes.
    /// </summary>
    [Serializable]
    public sealed class Cooldown : IExpiredSource, IProgressSource
    {
        /// <summary>
        /// Invoked when the duration value changes.
        /// </summary>
        public event Action<float> OnDurationChanged;

        /// <summary>
        /// Invoked when the current remaining time changes.
        /// </summary>
        public event Action<float> OnCurrentTimeChanged;

        /// <summary>
        /// Invoked when the progress (0 to 1) changes.
        /// </summary>
        public event Action<float> OnProgressChanged;

        /// <summary>
        /// Invoked when the cooldown has expired (time reaches zero).
        /// </summary>
        public event Action OnExpired;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private float _duration;

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private float _current;

        /// <summary>
        /// Initializes a new cooldown with a given duration. Starts at full time.
        /// </summary>
        /// <param name="duration">The total duration of the cooldown.</param>
        public Cooldown(float duration)
        {
            _duration = duration;
            _current = duration;
        }

        /// <summary>
        /// Initializes a new cooldown with a given duration and current remaining time.
        /// </summary>
        /// <param name="duration">The total duration of the cooldown.</param>
        /// <param name="current">The current remaining time.</param>
        public Cooldown(float duration, float current)
        {
            _duration = duration;
            _current = current;
        }

        /// <summary>
        /// Returns whether the cooldown has expired (i.e., current time is zero or less).
        /// </summary>
        public bool IsExpired() => _current <= 0;

        /// <summary>
        /// Gets the progress of the cooldown (from 0 to 1).
        /// </summary>
        public float GetProgress() => _current / _duration;

        /// <summary>
        /// Sets the progress (from 0 to 1), updating the remaining time accordingly.
        /// </summary>
        /// <param name="progress">The new progress value (0–1).</param>
        public void SetProgress(float progress)
        {
            if (progress < 0)
                throw new ArgumentOutOfRangeException(nameof(progress));

            progress = Mathf.Clamp01(progress);
            float remainingTime = _duration * progress;

            _current = remainingTime;
            this.OnCurrentTimeChanged?.Invoke(remainingTime);
            this.OnProgressChanged?.Invoke(progress);
        }

        /// <summary>
        /// Resets the cooldown to full duration.
        /// </summary>
        public void Reset()
        {
            if (Math.Abs(_duration - _current) <= float.Epsilon)
                return;

            _current = _duration;
            this.OnCurrentTimeChanged?.Invoke(_current);
            this.OnProgressChanged?.Invoke(this.GetProgress());
        }

        /// <summary>
        /// Updates the cooldown by reducing current time by deltaTime.
        /// Fires <see cref="OnExpired"/> if the timer reaches zero.
        /// </summary>
        /// <param name="deltaTime">The time to subtract from the current time.</param>
        public void Tick(float deltaTime)
        {
            if (_current == 0)
                return;

            _current = Mathf.Max(0, _current - deltaTime);

            this.OnCurrentTimeChanged?.Invoke(_current);
            this.OnProgressChanged?.Invoke(this.GetProgress());

            if (_current <= 0)
                this.OnExpired?.Invoke();
        }

        /// <summary>
        /// Returns a string representation of the cooldown's duration and current time.
        /// </summary>
        public override string ToString() => $"{nameof(_duration)}: {_duration}, {nameof(_current)}: {_current}";

        /// <summary>
        /// Gets the total duration of the cooldown.
        /// </summary>
        public float GetDuration() => _duration;

        /// <summary>
        /// Sets the total duration of the cooldown.
        /// Triggers <see cref="OnDurationChanged"/> and <see cref="OnProgressChanged"/>.
        /// </summary>
        /// <param name="duration">The new duration value.</param>
        public void SetDuration(float duration)
        {
            if (duration < 0)
                throw new ArgumentException($"Duration can't be negative: {duration}!", nameof(duration));

            if (Math.Abs(_duration - duration) > float.Epsilon)
            {
                _duration = duration;
                this.OnDurationChanged?.Invoke(duration);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
        }

        /// <summary>
        /// Gets the current remaining time on the cooldown.
        /// </summary>
        public float GetCurrentTime() => _current;

        /// <summary>
        /// Sets the current remaining time on the cooldown.
        /// Triggers <see cref="OnCurrentTimeChanged"/> and <see cref="OnProgressChanged"/>.
        /// </summary>
        /// <param name="time">The new time to set (must be between 0 and duration).</param>
        public void SetCurrentTime(float time)
        {
            if (time < 0)
                throw new ArgumentException($"Time can't be negative: {time}!", nameof(time));

            float newTime = Mathf.Clamp(time, 0, _duration);
            if (Math.Abs(newTime - _current) <= float.Epsilon)
                return;

            _current = newTime;
            this.OnCurrentTimeChanged?.Invoke(newTime);
            this.OnProgressChanged?.Invoke(this.GetProgress());
        }
    }
}
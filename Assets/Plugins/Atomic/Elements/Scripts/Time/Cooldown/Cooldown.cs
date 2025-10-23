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
    /// Represents a cooldown timer that tracks remaining time,
    /// provides progress feedback, and raises events on state changes.
    /// </summary>
    [Serializable]
    public class Cooldown : ICooldown
    {
        /// <summary>
        /// Invoked when the duration value changes.
        /// </summary>
        public event Action<float> OnDurationChanged;

        /// <summary>
        /// Invoked when the current remaining time changes.
        /// </summary>
        public event Action<float> OnTimeChanged;

        /// <summary>
        /// Invoked when the progress (0 to 1) changes.
        /// </summary>
        public event Action<float> OnProgressChanged;

        /// <summary>
        /// Invoked when the cooldown has expired (time reaches zero).
        /// </summary>
        public event Action OnCompleted;
        
        /// <summary>Gets or sets the total duration of the cooldown.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Duration
        {
            get => _duration;
            set => this.SetDuration(value);
        }

        /// <summary>Gets or sets the current remaining time.</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float CurrentTime
        {
            get => _time;
            set => this.SetTime(value);
        }
        
        /// <summary>Gets or sets the progress of the cooldown (0–1).</summary>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Progress
        {
            get => this.GetProgress();
            set => this.SetProgress(value);
        }
        
#if UNITY_5_3_OR_NEWER
        [Min(float.Epsilon)]
        [SerializeField]
#endif
        private float _duration;

#if UNITY_5_3_OR_NEWER
        [Min(0)]
        [SerializeField]
#endif
        private float _time;

        /// <summary>
        /// Initializes a new cooldown with a given duration. Starts at full time.
        /// </summary>
        /// <param name="duration">The total duration of the cooldown.</param>
        public Cooldown(float duration) : this(duration, duration)
        {
        }

        /// <summary>
        /// Initializes a new cooldown with a given duration and current remaining time.
        /// </summary>
        /// <param name="duration">The total duration of the cooldown.</param>
        /// <param name="current">The current remaining time.</param>
        public Cooldown(float duration, float current)
        {
            _duration = Math.Max(0, duration);
            _time = Math.Min(current, _duration);
        }
        
        /// <summary>
        /// Implicitly converts a <see cref="float"/> value to a <see cref="Cooldown"/> instance.
        /// </summary>
        /// <param name="duration">The duration in seconds for the new <see cref="Cooldown"/>.</param>
        /// <returns>A new <see cref="Cooldown"/> initialized with the specified duration.</returns>
        /// <example>
        /// <code>
        /// Cooldown cooldown = 5f; // creates a Cooldown with duration = 5 seconds
        /// </code>
        /// </example>
        public static implicit operator Cooldown(float duration) => new(duration);

        /// <summary>
        /// Implicitly converts an <see cref="int"/> value to a <see cref="Cooldown"/> instance.
        /// </summary>
        /// <param name="duration">The duration in seconds for the new <see cref="Cooldown"/>.</param>
        /// <returns>A new <see cref="Cooldown"/> initialized with the specified duration.</returns>
        /// <example>
        /// <code>
        /// Cooldown cooldown = 3; // creates a Cooldown with duration = 3 seconds
        /// </code>
        /// </example>
        public static implicit operator Cooldown(int duration) => new(duration);
        
        /// <summary>
        /// Returns whether the cooldown has expired (i.e., current time is zero or less).
        /// </summary>
        public bool IsCompleted() => _time <= 0;

        /// <summary>
        /// Gets the progress of the cooldown (from 0 to 1).
        /// </summary>
        public float GetProgress() => _time / _duration;

        /// <summary>
        /// Sets the progress (from 0 to 1), updating the remaining time accordingly.
        /// </summary>
        /// <param name="progress">The new progress value (0–1).</param>
        public void SetProgress(float progress)
        {
            progress = Math.Clamp(progress, 0, 1);
            float time = _duration * progress;

            _time = time;
            this.OnTimeChanged?.Invoke(time);
            this.OnProgressChanged?.Invoke(progress);
        }

        /// <summary>
        /// Resets the cooldown to full duration.
        /// </summary>
        public void ResetTime() => this.SetTime(_duration);

        /// <summary>
        /// Updates the cooldown by reducing current time by deltaTime.
        /// Fires <see cref="OnCompleted"/> if the timer reaches zero.
        /// </summary>
        /// <param name="deltaTime">The time to subtract from the current time.</param>
        public void Tick(float deltaTime)
        {
            if (_time == 0)
                return;

            _time = Math.Max(0, _time - deltaTime);

            this.OnTimeChanged?.Invoke(_time);
            this.OnProgressChanged?.Invoke(this.GetProgress());

            if (_time <= 0)
                this.OnCompleted?.Invoke();
        }
        
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
            duration = Math.Max(0, duration);
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
        public float GetTime() => _time;

        /// <summary>
        /// Sets the current remaining time on the cooldown.
        /// Triggers <see cref="OnTimeChanged"/> and <see cref="OnProgressChanged"/>.
        /// </summary>
        /// <param name="time">The new time to set (must be between 0 and duration).</param>
        public void SetTime(float time)
        {
            if (time < 0)
                throw new ArgumentException($"Time can't be negative: {time}!", nameof(time));

            float newTime = Math.Clamp(time, 0, _duration);
            if (Math.Abs(newTime - _time) <= float.Epsilon)
                return;

            _time = newTime;
            this.OnTimeChanged?.Invoke(newTime);
            this.OnProgressChanged?.Invoke(this.GetProgress());
        }
        
        /// <summary>
        /// Returns a string representation of the cooldown's duration and current time.
        /// </summary>
        public override string ToString() => $"{nameof(_duration)}: {_duration}, {nameof(_time)}: {_time}";
    }
}
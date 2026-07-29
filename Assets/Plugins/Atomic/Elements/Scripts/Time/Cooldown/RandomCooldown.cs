using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a cooldown timer with a random duration between a specified minimum and maximum.
    /// </summary>
    [Serializable]
    public class RandomCooldown : ICooldown
    {
        /// <summary>
        /// Occurs when the cooldown has completed.
        /// </summary>
        public event Action OnCompleted
        {
            add => _cooldown.OnCompleted += value;
            remove => _cooldown.OnCompleted -= value;
        }

        /// <summary>
        /// Occurs when the progress of the cooldown changes.
        /// </summary>
        public event Action<float> OnProgressChanged
        {
            add => _cooldown.OnProgressChanged += value;
            remove => _cooldown.OnProgressChanged -= value;
        }

        /// <summary>
        /// Occurs when the duration of the cooldown changes.
        /// </summary>
        public event Action<float> OnDurationChanged
        {
            add => _cooldown.OnDurationChanged += value;
            remove => _cooldown.OnDurationChanged -= value;
        }

        /// <summary>
        /// Occurs when the current time of the cooldown changes.
        /// </summary>
        public event Action<float> OnTimeChanged
        {
            add => _cooldown.OnTimeChanged += value;
            remove => _cooldown.OnTimeChanged -= value;
        }

        /// <summary>
        /// The minimum duration of the cooldown.
        /// </summary>
        [Min(float.Epsilon)]
        [SerializeField]
        private float _minDuration;

        /// <summary>
        /// The maximum duration of the cooldown.
        /// </summary>
        [Min(float.Epsilon)]
        [SerializeField]
        private float _maxDuration;

        private readonly Cooldown _cooldown;
        private readonly IRandomizer _randomizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomCooldown"/> class with specified min and max durations.
        /// </summary>
        /// <param name="minDuration">The minimum duration of the cooldown.</param>
        /// <param name="maxDuration">The maximum duration of the cooldown.</param>
        public RandomCooldown(float minDuration, float maxDuration, IRandomizer randomizer)
        {
            _minDuration = minDuration;
            _maxDuration = maxDuration;
            _randomizer = randomizer;
            _cooldown = new Cooldown(_randomizer.Range(minDuration, maxDuration));
        }

        /// <summary>
        /// Gets the current duration of the cooldown.
        /// </summary>
        public float GetDuration() => _cooldown.GetDuration();

        /// <summary>
        /// Sets a new duration for the cooldown.
        /// </summary>
        public void SetDuration(float duration) => _cooldown.SetDuration(duration);

        /// <summary>
        /// Gets the current elapsed time of the cooldown.
        /// </summary>
        public float GetTime() => _cooldown.GetTime();

        /// <summary>
        /// Sets the elapsed time of the cooldown.
        /// </summary>
        public void SetTime(float time) => _cooldown.SetTime(time);

        /// <summary>
        /// Resets the cooldown to a new random duration between min and max.
        /// </summary>
        public void ResetTime() => _cooldown.SetDuration(_randomizer.Range(_minDuration, _maxDuration));

        /// <summary>
        /// Gets the progress of the cooldown as a value between 0 and 1.
        /// </summary>
        public float GetProgress() => _cooldown.GetProgress();

        /// <summary>
        /// Sets the progress of the cooldown.
        /// </summary>
        public void SetProgress(float progress) => _cooldown.SetProgress(progress);

        /// <summary>
        /// Returns whether the cooldown has completed.
        /// </summary>
        public bool IsCompleted() => _cooldown.IsCompleted();

        /// <summary>
        /// Updates the cooldown by a specified delta time.
        /// </summary>
        /// <param name="deltaTime">The amount of time to advance the cooldown.</param>
        public void Tick(float deltaTime) => _cooldown.Tick(deltaTime);

        public bool IsPlaying() => _cooldown.IsPlaying();
        
        public static Builder StartBuild() => new();

        public struct Builder
        {
            private float _minDuration;
            private float _maxDuration;
            private IRandomizer _randomizer;

            public Builder WithMinDuration(float minDuration)
            {
                if (minDuration <= 0f)
                    throw new ArgumentOutOfRangeException(nameof(minDuration));

                _minDuration = minDuration;
                return this;
            }

            public Builder WithMaxDuration(float maxDuration)
            {
                if (maxDuration <= 0f)
                    throw new ArgumentOutOfRangeException(nameof(maxDuration));

                _maxDuration = maxDuration;
                return this;
            }

            public Builder WithDuration(float minDuration, float maxDuration)
            {
                if (minDuration <= 0f)
                    throw new ArgumentOutOfRangeException(nameof(minDuration));

                if (maxDuration <= 0f)
                    throw new ArgumentOutOfRangeException(nameof(maxDuration));

                if (maxDuration < minDuration)
                    throw new ArgumentException("maxDuration must be >= minDuration");

                _minDuration = minDuration;
                _maxDuration = maxDuration;
                return this;
            }

            public Builder WithRandomizer(IRandomizer randomizer)
            {
                _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
                return this;
            }

            public RandomCooldown Build()
            {
                if (_randomizer == null)
                    throw new InvalidOperationException("Randomizer must be provided.");

                if (_minDuration <= 0f)
                    throw new InvalidOperationException("MinDuration must be > 0.");

                if (_maxDuration <= 0f)
                    throw new InvalidOperationException("MaxDuration must be > 0.");

                if (_maxDuration < _minDuration)
                    throw new InvalidOperationException("MaxDuration must be >= MinDuration.");

                return new RandomCooldown(_minDuration, _maxDuration, _randomizer);
            }
        }
    }
}

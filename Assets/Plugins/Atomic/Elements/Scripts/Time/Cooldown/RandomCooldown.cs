#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class RandomCooldown : ICooldown
    {
        public event Action OnCompleted
        {
            add => _cooldown.OnCompleted += value;
            remove => _cooldown.OnCompleted -= value;
        }

        public event Action<float> OnProgressChanged
        {
            add => _cooldown.OnProgressChanged += value;
            remove => _cooldown.OnProgressChanged -= value;
        }

        public event Action<float> OnDurationChanged
        {
            add => _cooldown.OnDurationChanged += value;
            remove => _cooldown.OnDurationChanged -= value;
        }

        public event Action<float> OnTimeChanged
        {
            add => _cooldown.OnTimeChanged += value;
            remove => _cooldown.OnTimeChanged -= value;
        }
        
        [Min(float.Epsilon)]
        [SerializeField]
        private float _minDuration;

        [Min(float.Epsilon)]
        [SerializeField]
        private float _maxDuration;

        private readonly Cooldown _cooldown;

        public RandomCooldown(float minDuration, float maxDuration)
        {
            _minDuration = minDuration;
            _maxDuration = maxDuration;
            _cooldown = new Cooldown(Random.Range(minDuration, maxDuration));
        }
        
        public float GetDuration() => _cooldown.GetDuration();

        public void SetDuration(float duration) => _cooldown.SetDuration(duration);
        
        public float GetTime() => _cooldown.GetTime();

        public void SetTime(float time) => _cooldown.SetTime(time);

        public void ResetTime() => _cooldown.SetDuration(Random.Range(_minDuration, _maxDuration));

        public float GetProgress() => _cooldown.GetProgress();

        public void SetProgress(float progress) => _cooldown.SetProgress(progress);
        
        public bool IsCompleted() => _cooldown.IsCompleted();

        public void Tick(float deltaTime) => _cooldown.Tick(deltaTime);
    }
}
#endif
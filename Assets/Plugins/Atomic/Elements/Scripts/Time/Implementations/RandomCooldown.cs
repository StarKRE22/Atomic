using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class RandomCooldown
    {
        [SerializeField]
        private float _minDuration;

        [SerializeField]
        private float _maxDuration;

        [SerializeField]
        private float _current;

        public RandomCooldown(float minDuration, float maxDuration)
        {
            _minDuration = minDuration;
            _maxDuration = maxDuration;
            this.Reset();
        }

        public bool IsExpired()
        {
            return _current <= 0;
        }

        public void Reset()
        {
            _current = Random.Range(_minDuration, _maxDuration);
        }

        public void Tick(float deltaTime)
        {
            _current = Mathf.Max(0, _current - deltaTime);
        }
    }
}
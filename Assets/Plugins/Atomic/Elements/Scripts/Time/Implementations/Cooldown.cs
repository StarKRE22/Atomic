using System;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class Cooldown
    {
        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _current;

        public Cooldown(float duration)
        {
            _duration = duration;
            _current = duration;
        }

        public Cooldown(float duration, float current)
        {
            _duration = duration;
            _current = current;
        }

        public bool IsExpired()
        {
            return _current <= 0;
        }

        public float GetProgress()
        {
            return _current / _duration;
        }

        public void Reset()
        {
            _current = _duration;
        }

        public void Tick(float deltaTime)
        {
            _current = Mathf.Max(0, _current - deltaTime);
        }
        
        public override string ToString()
        {
            return $"{nameof(_duration)}: {_duration}, {nameof(_current)}: {_current}";
        }
    }
}
using System;

namespace Atomic.Elements
{
    // Decorator
    public class DurationTimestamp : IDurationTimestamp
    {
        public event Action<float> OnDurationChanged;

        public int EndTick => _timestamp.EndTick;

        public int RemainingTicks => _timestamp.RemainingTicks;

        public float RemainingSeconds => _timestamp.RemainingSeconds;
        
        private float _duration;

        private readonly ITimestamp _timestamp;
        
        public DurationTimestamp(ITimestamp timestamp, float durationSeconds)
        {
            _timestamp = timestamp;
            this.SetDuration(durationSeconds);
        }

        public void StartFromDuration() => _timestamp.StartFromSeconds(_duration);

        public float GetProgress() => _timestamp.GetProgress(_duration);

        public float GetDuration() => _duration;

        public void SetDuration(float duration)
        {
            if (duration < 0)
                throw new Exception($"Duration can't be negative: {duration}!");

            if (Math.Abs(_duration - duration) > float.Epsilon)
            {
                _duration = duration;
                this.OnDurationChanged?.Invoke(duration);
            }
        }
        
        public void StartFromSeconds(float seconds) => _timestamp.StartFromSeconds(seconds);

        public void StartFromTicks(int ticks) => _timestamp.StartFromTicks(ticks);

        public void ResetEndTick() => _timestamp.ResetEndTick();

        public float GetProgress(float duration) => _timestamp.GetProgress(duration);

        public bool IsIdle() => _timestamp.IsIdle();

        public bool IsPlaying() => _timestamp.IsPlaying();

        public bool IsExpired() => _timestamp.IsExpired();
    }
}
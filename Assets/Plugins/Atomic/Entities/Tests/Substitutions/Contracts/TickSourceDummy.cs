using System;

namespace Atomic.Entities
{
    public class TickSourceDummy : ITickLifecycle
    {
        public event Action<float> OnTicked;
        public event Action<float> OnFixedTicked;
        public event Action<float> OnLateTicked;

        public int UpdateCount;
        public int FixedCount;
        public int LateCount;
        
        public void Tick(float dt)
        {
            UpdateCount++;
            this.OnTicked?.Invoke(dt);
        }

        public void FixedTick(float dt)
        {
            FixedCount++;
            this.OnFixedTicked?.Invoke(dt);
        }

        public void LateTick(float dt)
        {
            LateCount++;
            this.OnLateTicked?.Invoke(dt);
        }
    }
}
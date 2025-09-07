using System;

namespace Atomic.Entities
{
    public class UpdateSourceDummy : IUpdateSource
    {
        public event Action<float> OnUpdated;
        public event Action<float> OnFixedUpdated;
        public event Action<float> OnLateUpdated;

        public int UpdateCount;
        public int FixedCount;
        public int LateCount;
        
        public void Tick(float dt)
        {
            UpdateCount++;
            this.OnUpdated?.Invoke(dt);
        }

        public void FixedTick(float dt)
        {
            FixedCount++;
            this.OnFixedUpdated?.Invoke(dt);
        }

        public void LateTick(float dt)
        {
            LateCount++;
            this.OnLateUpdated?.Invoke(dt);
        }
    }
}
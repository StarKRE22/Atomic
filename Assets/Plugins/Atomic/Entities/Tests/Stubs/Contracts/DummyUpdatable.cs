using System;

namespace Atomic.Entities
{
    public class DummyUpdatable : IUpdatable
    {
        public event Action<float> OnUpdated;
        public event Action<float> OnFixedUpdated;
        public event Action<float> OnLateUpdated;

        public int UpdateCount;
        public int FixedCount;
        public int LateCount;
        
        public void OnUpdate(float dt)
        {
            UpdateCount++;
            this.OnUpdated?.Invoke(dt);
        }

        public void OnFixedUpdate(float dt)
        {
            FixedCount++;
            this.OnFixedUpdated?.Invoke(dt);
        }

        public void OnLateUpdate(float dt)
        {
            LateCount++;
            this.OnLateUpdated?.Invoke(dt);
        }
    }
}
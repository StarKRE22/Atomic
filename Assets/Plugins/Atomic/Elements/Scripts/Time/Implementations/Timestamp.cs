namespace Atomic.Elements
{
    //TODO: TDD
    public sealed class Timestamp : ITimestamp
    {
        public int TargetTick { get; }
        public float CurrentDuration { get; }
        public bool Start()
        {
            throw new System.NotImplementedException();
        }

        public bool StartFromSeconds(float seconds)
        {
            throw new System.NotImplementedException();
        }

        public bool StartFromTicks(int ticks)
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public bool IsIdle()
        {
            throw new System.NotImplementedException();
        }

        public bool IsPlaying()
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotPlaying()
        {
            throw new System.NotImplementedException();
        }

        public bool IsExpired()
        {
            throw new System.NotImplementedException();
        }

        public float GetProgress()
        {
            throw new System.NotImplementedException();
        }

        public float GetRemainingTime()
        {
            throw new System.NotImplementedException();
        }

        public int GetRemainingTicks()
        {
            throw new System.NotImplementedException();
        }
    }
}
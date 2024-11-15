namespace Atomic.Elements
{
    public interface ITimestamp
    {
        int TargetTick { get; }
        float CurrentDuration { get; }

        bool Start();
        bool StartFromSeconds(float seconds);
        bool StartFromTicks(int ticks);

        void Stop();
        
        bool IsIdle();
        bool IsPlaying();
        bool IsNotPlaying();
        bool IsExpired();
        
        float GetProgress();
        float GetRemainingTime();
        int GetRemainingTicks();
    }
}
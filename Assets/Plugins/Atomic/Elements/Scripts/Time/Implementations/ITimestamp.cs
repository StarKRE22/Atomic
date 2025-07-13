namespace Atomic.Elements
{
    public interface ITimestamp
    {
        int EndTick { get; }

        int RemainingTicks { get; }
        float RemainingTime { get; }
        
        void StartFromSeconds(float seconds);
        void StartFromTicks(int ticks);
        void Stop();

        float GetProgress(float duration);
        
        bool IsIdle();
        bool IsPlaying();
        bool IsExpired();
    }
}
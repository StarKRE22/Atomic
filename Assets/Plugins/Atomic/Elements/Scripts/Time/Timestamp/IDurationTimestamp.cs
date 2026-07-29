namespace Atomic.Elements
{
    public interface IDurationTimestamp : ITimestamp, IDurationSource
    {
        void StartFromDuration();

        float GetProgress();
    }
}
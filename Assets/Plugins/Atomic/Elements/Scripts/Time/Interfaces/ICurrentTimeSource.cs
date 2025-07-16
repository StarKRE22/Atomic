namespace Atomic.Elements
{
    public interface ICurrentTimeSource
    {
        event System.Action<float> OnCurrentTimeChanged;
        
        float GetCurrentTime();
        
        void SetCurrentTime(float time);
    }
}
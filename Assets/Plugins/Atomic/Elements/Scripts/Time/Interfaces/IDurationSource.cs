namespace Atomic.Elements
{
    public interface IDurationSource
    {
        event System.Action<float> OnDurationChanged; 

        float GetDuration();
        
        void SetDuration(float duration);
    }
}
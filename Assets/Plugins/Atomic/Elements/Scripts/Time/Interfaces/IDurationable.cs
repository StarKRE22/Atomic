namespace Atomic.Elements
{
    public interface IDurationable
    {
        event System.Action<float> OnDurationChanged; 

        float GetDuration();
        void SetDuration(float duration);
    }
}
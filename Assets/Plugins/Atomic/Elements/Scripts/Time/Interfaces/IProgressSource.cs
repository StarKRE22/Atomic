namespace Atomic.Elements
{
    public interface IProgressSource : ICurrentTimeSource, IDurationSource
    {
        event System.Action<float> OnProgressChanged; 

        float GetProgress();
        
        void SetProgress(float progress);
    }
}
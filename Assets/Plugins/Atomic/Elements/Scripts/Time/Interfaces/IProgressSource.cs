namespace Atomic.Elements
{
    public interface IProgressSource 
    {
        event System.Action<float> OnProgressChanged; 

        float GetProgress();
        
        void SetProgress(float progress);
    }
}
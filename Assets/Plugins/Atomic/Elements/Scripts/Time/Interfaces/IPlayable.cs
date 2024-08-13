namespace Atomic.Elements
{
    public interface IPlayable
    {
        event System.Action OnStarted;
        event System.Action OnStopped;

        bool IsPlaying();
        bool Start();
        bool Stop();
    }
    
    public interface IPlayable<T>
    {
        event System.Action<T> OnStarted;
        event System.Action<T> OnStopped;

        bool IsPlaying();
        bool Start(T value);
        bool Stop();
    }
}
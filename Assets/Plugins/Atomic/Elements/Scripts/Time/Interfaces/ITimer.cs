namespace Atomic.Elements
{
    public interface IPlayable
    {
        bool IsPlaying();
        bool Play();
    }
    
    public interface ITimer : 
        IStartable,
        IStoppable,
        IPlayable,
        IPausable,
        IResumable,
        IProgressable,
        IEndable,
        ITickable
    {
    }
}
namespace Atomic.Elements
{
    public interface ITimer : 
        IStartSource,
        IStopSource,
        IPlaySource,
        IPauseSource,
        IResumeSource,
        IProgressSource,
        IExpiredSource,
        ITickSource,
        ICurrentTimeSource, 
        IDurationSource
    {
    }
    
    public interface ITimer<T> : 
        IStartSource<T>,
        IStopSource<T>,
        IProgressSource, 
        IExpiredSource<T>,
        ITickSource, 
        IValue<T>,
        IPauseSource, 
        IResumeSource,
        ICurrentTimeSource, 
        IDurationSource
    {
    }
}
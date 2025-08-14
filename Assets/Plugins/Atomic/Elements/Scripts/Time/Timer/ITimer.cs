namespace Atomic.Elements
{
    /// <summary>
    /// Defines a general-purpose timer interface that supports start, pause, resume, stop,
    /// progress tracking, and state change events.
    /// </summary>
    public interface ITimer :
        IStartSource,
        IPauseSource,
        ICompleteSource,
        IStateSource<TimerState>,
        ITimeSource,
        IDurationSource,
        IProgressSource,
        ITickSource
    {
    }
}
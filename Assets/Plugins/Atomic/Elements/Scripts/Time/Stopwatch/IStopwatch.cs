namespace Atomic.Elements
{
    /// <summary>
    /// Defines a stopwatch interface that can start, pause, resume, stop,
    /// and track elapsed time, along with state change and time update events.
    /// </summary>
    public interface IStopwatch :
        IStartSource,
        IPauseSource,
        ITimeSource,
        IStateSource<StopwatchState>,
        ITickSource
    {
    }
}
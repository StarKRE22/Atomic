namespace Atomic.Elements
{
    /// <summary>
    /// Represents a countdown timer that supports play, pause, stop, reset
    /// while broadcasting progress and state changes.
    /// </summary>
    public interface ICountdown :
        IDurationSource,
        ITimeSource,
        IProgressSource,
        IExpiredSource,
        IResetSource,
        IStartSource,
        IPauseSource,
        ITickSource,
        IStateSource<CountdownState>
    {
    }
}
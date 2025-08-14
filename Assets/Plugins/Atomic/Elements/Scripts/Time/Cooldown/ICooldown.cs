namespace Atomic.Elements
{
    /// <summary>
    /// Represents a cooldown timer that tracks remaining time,
    /// provides progress feedback, and raises events on state changes.
    /// </summary>
    public interface ICooldown :
        IDurationSource,
        ITimeSource,
        IProgressSource,
        ICompleteSource,
        ITickSource
    {
    }
}
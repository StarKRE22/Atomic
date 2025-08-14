using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a looping cycle timer that tracks time progression and emits events on completion of each cycle.
    /// </summary>
    public interface IPeriod :
        IStartSource,
        IPauseSource,
        IStateSource<PeriodState>,
        ITimeSource,
        IProgressSource,
        IDurationSource,
        ITickSource
    {
        /// <summary>Raised when the period completes and starts over.</summary>
        event Action OnPeriod;
    }
}
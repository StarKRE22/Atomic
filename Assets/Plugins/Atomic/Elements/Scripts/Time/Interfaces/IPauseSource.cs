using System;

namespace Atomic.Elements
{
    public interface IPauseSource
    {
        event Action OnPaused;

        bool IsPaused();
        
        bool Pause();
    }
}
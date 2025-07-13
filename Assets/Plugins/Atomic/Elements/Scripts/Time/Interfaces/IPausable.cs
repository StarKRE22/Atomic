using System;

namespace Atomic.Elements
{
    public interface IPausable
    {
        event Action OnPaused;

        bool IsPaused();
        bool Pause();
    }
}
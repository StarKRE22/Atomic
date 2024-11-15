using System;

namespace Atomic.Elements
{
    public interface IResumable
    {
        event Action OnResumed;
        bool Resume();
    }
}
using System;

namespace Atomic.Elements
{
    public interface IResumeSource
    {
        event Action OnResumed;
        
        bool Resume();
    }
}
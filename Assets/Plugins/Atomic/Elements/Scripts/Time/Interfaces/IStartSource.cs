using System;

namespace Atomic.Elements
{
    public interface IStartSource
    {
        event Action OnStarted;
        
        bool Start();
    }

    public interface IStartSource<T>
    {
        event Action<T> OnStarted;
        
        bool Start(T value);
    }
}
using System;

namespace Atomic.Elements
{
    public interface IStoppable
    {
        event Action OnStopped;
        bool Stop();
    }
    
    public interface IStoppable<out T>
    {
        event Action<T> OnStopped;
        bool Stop();
    }
}
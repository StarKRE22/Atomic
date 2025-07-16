using System;

namespace Atomic.Elements
{
    public interface IStopSource
    {
        event Action OnStopped;
        
        bool Stop();
    }
    
    public interface IStopSource<out T>
    {
        event Action<T> OnStopped;
       
        bool Stop();
    }
}
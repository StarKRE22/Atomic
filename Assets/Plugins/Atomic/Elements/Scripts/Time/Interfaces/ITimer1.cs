namespace Atomic.Elements
{
    public interface ITimer<T> : 
        IStartable<T>,
        IStoppable<T>,
        IProgressable, 
        IEndable<T>,
        ITickable, 
        IValue<T>,
        IPausable, 
        IResumable
    {
        
    }
}
namespace Atomic.Elements
{
    public interface IEndable
    {
        event System.Action OnEnded;
        bool IsEnded();
    }

    public interface IEndable<out T>
    {
        event System.Action<T> OnEnded;
        bool IsEnded();
    }
}
namespace Atomic.Elements
{
    public interface IExpiredSource
    {
        event System.Action OnExpired;
        
        bool IsExpired();
    }

    public interface IExpiredSource<out T>
    {
        event System.Action<T> OnExpired;
        
        bool IsExpired();
    }
}
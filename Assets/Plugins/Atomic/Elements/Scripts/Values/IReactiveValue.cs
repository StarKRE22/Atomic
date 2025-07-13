namespace Atomic.Elements
{
    public interface IReactiveValue<out T> : IValue<T>, IReactive<T>
    {
    }
}
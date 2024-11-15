namespace Atomic.Elements
{
    public interface IPredicate : IFunction<bool>
    {
    }

    public interface IPredicate<in T> : IFunction<T, bool>
    {
    }

    public interface IPredicate<in T1, in T2> : IFunction<T1, T2, bool>
    {
    }
}
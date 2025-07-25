namespace Atomic.Elements
{
    public class BasicPredicate : BasicFunction<bool>, IPredicate
    {
    }

    public class BasicPredicate<T> : BasicFunction<T, bool>, IPredicate<T>
    {
    }

    public class BasicPredicate<T1, T2> : BasicFunction<T1, T2, bool>, IPredicate<T1, T2>
    {
    }
}
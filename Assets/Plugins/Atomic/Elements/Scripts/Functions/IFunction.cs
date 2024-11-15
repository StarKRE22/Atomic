namespace Atomic.Elements
{
    public interface IFunction<out R>
    {
        R Invoke();
    }

    public interface IFunction<in T, out R>
    {
        R Invoke(T args);
    }
    
    public interface IFunction<in T1, in T2, out R>
    {
        R Invoke(T1 args1, T2 args2);
    }
}
namespace Atomic.Elements
{
    public interface IRequest : IAction
    {
        bool Required { get; }

        bool Consume();
    }

    public interface IRequest<T> : IAction<T>
    {
        bool Required { get; }
        T Arg { get; }
        
        bool TryGet(out T arg);
        bool Consume(out T arg);
    }

    public interface IRequest<T1, T2> : IAction<T1, T2>
    {
        bool Required { get; }

        T1 Arg1 { get; }
        T2 Arg2 { get; }

        bool TryGet(out T1 args1, out T2 args2);
        bool Consume(out T1 args1, out T2 args2);
    }

    public interface IRequest<T1, T2, T3> : IAction<T1, T2, T3>
    {
        bool Required { get; }

        T1 Arg1 { get; }
        T2 Arg2 { get; }
        T3 Arg3 { get; }

        bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);
        bool Consume(out T1 args1, out T2 args2, out T3 args3);
    }
}
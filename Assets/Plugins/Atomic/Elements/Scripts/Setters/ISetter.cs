namespace Atomic.Elements
{
    public interface ISetter<in T> : IAction<T>
    {
        T Value { set; }

        void IAction<T>.Invoke(T arg) => this.Value = arg;
    }
}
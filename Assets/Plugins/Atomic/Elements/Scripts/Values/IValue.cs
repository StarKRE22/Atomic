namespace Atomic.Elements
{
    public interface IValue<out T> : IFunction<T>
    {
        T Value { get; }

        T IFunction<T>.Invoke() => this.Value;
    }
}
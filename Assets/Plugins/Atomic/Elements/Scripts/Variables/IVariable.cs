namespace Atomic.Elements
{
    public interface IVariable<T> : IValue<T>, ISetter<T>
    {
        new T Value { get; set; }

        T IValue<T>.Value
        {
            get { return this.Value; }
        }

        T ISetter<T>.Value
        {
            set => this.Value = value;
        }
    }
}
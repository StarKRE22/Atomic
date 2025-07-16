namespace Atomic.Elements
{
    public interface IReactiveVariable<T> : IVariable<T>, IReactiveValue<T>
    {
        event System.Action<T> OnValueChanged;
    }
}
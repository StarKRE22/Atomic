namespace Atomic.Contexts
{
    public interface IContextDisable : IContextSystem
    {
        void Disable(IContext context);
    }

    public interface IContextDisable<in T> : IContextDisable where T : IContext
    {
        void IContextDisable.Disable(IContext context) => this.Disable((T) context);
        void Disable(T context);
    }
}
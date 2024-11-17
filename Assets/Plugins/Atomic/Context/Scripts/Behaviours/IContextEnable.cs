namespace Atomic.Contexts
{
    public interface IContextEnable : IContextSystem
    {
        void Enable(IContext context);
    }

    public interface IContextEnable<in T> : IContextEnable where T : IContext
    {
        void IContextEnable.Enable(IContext context) => this.Enable((T) context);
        void Enable(T context);
    }
}
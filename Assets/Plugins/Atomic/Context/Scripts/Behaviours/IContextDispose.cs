namespace Atomic.Contexts
{
    public interface IContextDispose : IContextSystem
    {
        void Dispose(IContext context);
    }
    
    public interface IContextDispose<in T> : IContextDispose where T : IContext
    {
        void IContextDispose.Dispose(IContext context) => this.Dispose((T) context);
        void Dispose(T context);
    }
}
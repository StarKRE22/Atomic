namespace Atomic.Contexts
{
    public interface IContextInit : IContextSystem
    {
        void Init(IContext context);
    }

    public interface IContextInit<in T> : IContextInit where T : IContext
    {
        void Init(T context);
        void IContextInit.Init(IContext context) => this.Init((T) context);
    }
}
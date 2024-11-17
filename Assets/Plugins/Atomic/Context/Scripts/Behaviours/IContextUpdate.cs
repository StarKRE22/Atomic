namespace Atomic.Contexts
{
    public interface IContextUpdate : IContextSystem
    {
        void OnUpdate(IContext context, float deltaTime);
    }

    public interface IContextUpdate<in T> : IContextUpdate where T : IContext
    {
        void OnUpdate(T context, float deltaTime);
        void IContextUpdate.OnUpdate(IContext context, float deltaTime) => this.OnUpdate((T) context, deltaTime);
    }
}
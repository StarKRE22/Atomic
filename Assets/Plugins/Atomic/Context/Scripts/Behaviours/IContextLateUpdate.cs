namespace Atomic.Contexts
{
    public interface IContextLateUpdate : IContextSystem
    {
        void OnLateUpdate(IContext context, float deltaTime);
    }
    
    public interface IContextLateUpdate<in T> : IContextLateUpdate where T : IContext
    {
        void OnLateUpdate(T context, float deltaTime);
        void IContextLateUpdate.OnLateUpdate(IContext context, float deltaTime) => 
            this.OnLateUpdate((T) context, deltaTime);
    }
}
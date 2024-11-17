namespace Atomic.Contexts
{
    public interface IContextFixedUpdate : IContextSystem
    {
        void OnFixedUpdate(IContext context, float deltaTime);
    }

    public interface IContextFixedUpdate<in T> : IContextFixedUpdate where T : IContext
    {
        void OnFixedUpdate(T context, float deltaTime);
        
        void IContextFixedUpdate.OnFixedUpdate(IContext context, float deltaTime) => 
            this.OnFixedUpdate((T) context, deltaTime);
    }
}
namespace Atomic.Entities
{
    public interface IUpdate : IBehaviour
    {
        void OnUpdate(in IEntity entity, in float deltaTime);
    }

    public interface IUpdate<in T> : IUpdate where T : IEntity
    {
        void IUpdate.OnUpdate(in IEntity entity, in float deltaTime) =>
            this.OnUpdate((T) entity, in deltaTime);
        
        void OnUpdate(T context, in float deltaTime);
    }
}
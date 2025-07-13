namespace Atomic.Entities
{
    public interface IFixedUpdate : IBehaviour
    {
        void OnFixedUpdate(in IEntity entity, in float deltaTime);
    }

    public interface IFixedUpdate<in T> : IFixedUpdate where T : IEntity
    {
        void IFixedUpdate.OnFixedUpdate(in IEntity entity, in float deltaTime) =>
            this.OnFixedUpdate((T) entity, in deltaTime);
        
        void OnFixedUpdate(T entity, in float deltaTime);
    }
}
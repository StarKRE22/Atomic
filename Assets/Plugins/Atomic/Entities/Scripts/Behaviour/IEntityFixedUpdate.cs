namespace Atomic.Entities
{
    public interface IEntityFixedUpdate : IEntityBehaviour
    {
        void OnFixedUpdate(IEntity entity, float deltaTime);
    }

    public interface IEntityFixedUpdate<in T> : IEntityFixedUpdate where T : IEntity
    {
        void IEntityFixedUpdate.OnFixedUpdate(IEntity entity, float deltaTime) =>
            this.OnFixedUpdate((T) entity, deltaTime);
        
        void OnFixedUpdate(T entity, float deltaTime);
    }
}
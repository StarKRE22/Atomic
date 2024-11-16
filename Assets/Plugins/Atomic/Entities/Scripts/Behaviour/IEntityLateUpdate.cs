namespace Atomic.Entities
{
    public interface IEntityLateUpdate : IEntityBehaviour
    {
        void OnLateUpdate(IEntity entity, float deltaTime);
    }
    
    public interface IEntityLateUpdate<in T> : IEntityLateUpdate where T : IEntity
    {
        void IEntityLateUpdate.OnLateUpdate(IEntity entity, float deltaTime) => 
            this.OnLateUpdate((T) entity, deltaTime);
        void OnLateUpdate(T entity, float deltaTime);
    }
}
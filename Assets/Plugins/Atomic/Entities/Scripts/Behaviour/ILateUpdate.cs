namespace Atomic.Entities
{
    public interface ILateUpdate : IBehaviour
    {
        void OnLateUpdate(in IEntity entity, in float deltaTime);
    }
    
    public interface ILateUpdate<in T> : ILateUpdate where T : IEntity
    {
        void ILateUpdate.OnLateUpdate(in IEntity entity, in float deltaTime) => 
            this.OnLateUpdate((T) entity, in deltaTime);
        
        void OnLateUpdate(T entity, in float deltaTime);
    }
}
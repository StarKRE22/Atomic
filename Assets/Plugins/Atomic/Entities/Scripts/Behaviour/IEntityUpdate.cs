namespace Atomic.Entities
{
    public interface IEntityUpdate : IEntityBehaviour
    {
        void OnUpdate(IEntity entity, float deltaTime);
    }

    public interface IEntityUpdate<in T> : IEntityUpdate where T : IEntity
    {
        void IEntityUpdate.OnUpdate(IEntity entity, float deltaTime) => this.OnUpdate((T) entity, deltaTime);
        void OnUpdate(T entity, float deltaTime);
    }
}
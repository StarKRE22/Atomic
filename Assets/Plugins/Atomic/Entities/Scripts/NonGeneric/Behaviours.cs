namespace Atomic.Entities.Plugins.Atomic.Entities.Scripts.NonGeneric
{
    public sealed class EntityBehaviours
    {
        
    }
    
    public interface IEntityBehaviour : IBehaviour<IEntity>
    {
    }
    
    public interface IEntityInit : IInit<IEntity>
    {
        void Init(IEntity entity);
    }
    
    public interface IEntityEnable<E> : IBehaviour<E> where E : IEntity<E>
    {
        void Enable(IEntity<E> entity);
    }
    
    public interface IUpdate<E> : IBehaviour<E> where E : IEntity<E>
    {
        void OnUpdate(IEntity<E> entity, float deltaTime);
    }
    
    public interface IFixedUpdate<E> : IBehaviour<E> where E : IEntity<E>
    {
        void OnFixedUpdate(IEntity<E> entity, float deltaTime);
    }
    
    public interface ILateUpdate<E> : IBehaviour<E> where E : IEntity<E>
    {
        void OnLateUpdate(IEntity<E> entity, float deltaTime);
    }
    
    public interface IDisable<E> : IBehaviour<E> where E : IEntity<E>
    {
        void Disable(IEntity<E> entity);
    }
    
    public interface IDispose<E> where E : IEntity<E>
    {
        void Dispose(IEntity<E> entity);
    }
    
    public interface IGizmos<E> : IBehaviour<E> where E : IEntity<E>
    {
        void OnGizmosDraw(IEntity<E> entity);
    }
}
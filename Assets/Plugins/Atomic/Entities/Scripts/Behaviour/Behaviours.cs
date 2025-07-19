namespace Atomic.Entities
{
    #region Generic

    public interface IBehaviour<T> where T : IEntity<T>
    {
    }

    public interface IInit<E> : IBehaviour<E> where E : IEntity<E>
    {
        void Init(IEntity<E> entity);
    }

    public interface IEnable<E> : IBehaviour<E> where E : IEntity<E>
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

    #endregion

    #region Base

    public interface IEntityBehaviour : IBehaviour<IEntity>
    {
    }

    public interface IEntityInit : IInit<IEntity>
    {
        void Init(IEntity<E> entity);
    }

    public interface IEnable<E> : IBehaviour<E> where E : IEntity<E>
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

    #endregion
}
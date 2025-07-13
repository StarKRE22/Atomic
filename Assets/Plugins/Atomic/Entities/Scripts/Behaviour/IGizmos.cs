namespace Atomic.Entities
{
    public interface IGizmos : IBehaviour
    {
        void OnGizmosDraw(in IEntity entity);
    }

    public interface IGizmos<in T> : IGizmos where T : IEntity
    {
        void IGizmos.OnGizmosDraw(in IEntity entity) => this.OnGizmosDraw((T) entity);
        void OnGizmosDraw(T entity);
    }
}
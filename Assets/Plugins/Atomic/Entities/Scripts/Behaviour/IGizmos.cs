namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that allows drawing gizmos for an entity during the editor or debug rendering phase.
    /// </summary>
    public interface IGizmos : IBehaviour
    {
        /// <summary>
        /// Called to draw gizmos for the specified entity.
        /// This is typically invoked during the Unity editor's OnDrawGizmos phase.
        /// </summary>
        /// <param name="entity">The entity for which gizmos should be drawn.</param>
        void OnGizmosDraw(IEntity entity);
    }

    /// <summary>
    /// Generic version of <see cref="IGizmos"/> that provides strongly-typed gizmo drawing
    /// for a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="T">The specific type of entity this behavior applies to.</typeparam>
    public interface IGizmos<in T> : IGizmos where T : IEntity
    {
        /// <inheritdoc/>
        void IGizmos.OnGizmosDraw(IEntity entity) => this.OnGizmosDraw((T) entity);

        /// <summary>
        /// Called to draw gizmos for the specified entity.
        /// </summary>
        /// <param name="entity">The strongly-typed entity instance.</param>
        void OnGizmosDraw(T entity);
    }
}
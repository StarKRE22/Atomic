#if UNITY_5_3_OR_NEWER
namespace Atomic.Entities
{
    /// <summary>
    /// Defines a behavior that allows drawing gizmos for an <see cref="IEntity"/> during the editor or debug rendering phase.
    /// </summary>
    /// <remarks>
    /// This method is automatically called by <c>SceneEntity.OnDrawGizmos()</c> or <c>SceneEntity.OnDrawGizmosSelected()</c>
    /// in the Unity Editor, allowing you to visualize entity data in the scene view.
    /// </remarks>
    public interface IEntityGizmos : IEntityBehaviour
    {
        /// <summary>
        /// Called to draw gizmos for the specified entity.
        /// </summary>
        /// <param name="entity">The entity for which gizmos should be drawn.</param>
        void DrawGizmos(IEntity entity);
    }

    /// <summary>
    /// Provides a strongly-typed version of <see cref="IEntityGizmos"/> for drawing gizmos
    /// related to a specific <see cref="IEntity"/> type.
    /// </summary>
    /// <typeparam name="E">The concrete entity type this behavior is associated with.</typeparam>
    /// <remarks>
    /// This method is automatically invoked by <c>SceneEntity.OnDrawGizmos()</c> or <c>SceneEntity.OnDrawGizmosSelected()</c>
    /// if the entity implements this behavior and is currently visible in the editor.
    /// </remarks>
    public interface IEntityGizmos<in E> : IEntityGizmos where E : IEntity
    {
        /// <summary>
        /// Called to draw gizmos for the specified strongly-typed entity.
        /// </summary>
        /// <param name="entity">The entity instance of type <typeparamref name="E"/>.</param>
        void DrawGizmos(E entity);

        void IEntityGizmos.DrawGizmos(IEntity entity) => this.DrawGizmos((E) entity);
    }
}
#endif
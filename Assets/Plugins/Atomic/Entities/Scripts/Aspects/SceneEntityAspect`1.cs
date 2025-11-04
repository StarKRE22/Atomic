#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a scene-based entity aspect that can be applied or discarded on a specific entity type.
    /// </summary>
    /// <typeparam name="E">The type of entity (<see cref="IEntity"/>) this aspect can be applied to.</typeparam>
    /// <remarks>
    /// Inherit from this class to create reusable MonoBehaviour components that encapsulate
    /// logic to apply and discard behaviors or properties on entities during runtime.
    /// Attach this component to a GameObject in your scene to use it.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Aspects/SceneEntityAspect%601.md")]
    public abstract class SceneEntityAspect<E> : MonoBehaviour, IEntityAspect<E> where E : IEntity
    {
        /// <summary>
        /// Applies this aspect to the specified entity.
        /// </summary>
        /// <param name="entity">The entity to which the aspect should be applied.</param>
        public abstract void Apply(E entity);

        /// <summary>
        /// Discards this aspect from the specified entity, reversing any changes made by <see cref="Apply"/>.
        /// </summary>
        /// <param name="entity">The entity from which the aspect should be removed.</param>
        public abstract void Discard(E entity);
    }
}
#endif
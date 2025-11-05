#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for Unity scene bakers that convert <see cref="GameObject"/>s 
    /// into native <see cref="Entity"/> instances.
    /// </summary>
    /// <remarks>
    /// This class provides a scene-based workflow for converting authored GameObjects 
    /// into runtime <see cref="Entity"/> representations. 
    /// It extends <see cref="SceneEntityBaker{E}"/> with a non-generic implementation for <see cref="IEntity"/>.
    /// 
    /// The <see cref="Create"/> method is sealed and automatically constructs an <see cref="Entity"/> 
    /// using the predefined initialization capacities. 
    /// Derived classes must implement <see cref="Install"/> to define how the entity is configured 
    /// (e.g., adding tags, values, or behaviors).
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Baking/SceneEntityBaker.md")]
    public abstract class SceneEntityBaker : SceneEntityBaker<IEntity>
    {
        // <summary>
        /// Creates a new <see cref="Entity"/> using the predefined initialization values,
        /// then applies custom logic via the <see cref="Install"/> method.
        /// </summary>
        /// <returns>
        /// A fully initialized <see cref="IEntity"/> instance created from the associated <see cref="GameObject"/>.
        /// </returns>
        /// <remarks>
        /// This method is <c>sealed</c> to ensure consistent entity creation logic.  
        /// The customization point for entity composition is <see cref="Install"/>.
        /// </remarks>

        protected sealed override IEntity Create()
        {
            var entity = new Entity(
                this.name,
                this.initialTagCapacity,
                this.initialValueCapacity,
                this.initialBehaviourCapacity
            );
            this.Install(entity);
            return entity;
        }

        /// <summary>
        /// Applies custom setup logic to the created <see cref="IEntity"/> instance.
        /// </summary>
        /// <param name="entity">
        /// The newly created entity to configure. 
        /// This method is called immediately after entity construction in <see cref="Create"/>.
        /// </param>
        /// <remarks>
        /// Override this method in derived classes to define how scene data and MonoBehaviour components 
        /// are baked into the entity (for example, adding values, tags, or behaviours).
        /// </remarks>
        protected abstract void Install(IEntity entity);
    }
}
#endif
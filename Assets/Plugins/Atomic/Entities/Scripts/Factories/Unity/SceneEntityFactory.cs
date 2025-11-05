#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for Unity-based factories that create and configure <see cref="Entity"/> instances.
    /// </summary>
    /// <remarks>
    /// This class is designed for use in scene-based workflows where entities need to be created at runtime
    /// from serialized MonoBehaviours (e.g. for prototyping, design-time composition, or runtime baking).
    /// It also defines an <see cref="Install"/> method that allows injecting custom configuration logic,
    /// such as adding tags, values, or behaviors after the entity has been created.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Factories/SceneEntityFactory.md")]
    public abstract class SceneEntityFactory : SceneEntityFactory<IEntity>, IEntityFactory
    {
        /// <summary>
        /// Creates a new <see cref="Entity"/> using the predefined initialization values,
        /// then applies custom logic via the <see cref="Install"/> method.
        /// </summary>
        public sealed override IEntity Create()
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
        /// Called immediately after entity construction inside <see cref="Create"/>.
        /// </param>
        /// <remarks>
        /// Override this method in derived factory classes to define how scene-level data 
        /// and MonoBehaviour state are mapped into the entity.
        /// Typical usage includes adding components, tags, values, and behaviors that describe 
        /// the baked object’s functionality.
        /// </remarks>
        protected abstract void Install(IEntity entity);
    }
}
#endif
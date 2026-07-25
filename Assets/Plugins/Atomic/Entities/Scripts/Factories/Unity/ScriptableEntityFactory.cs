#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for Unity-based factories that create and configure <see cref="Entity"/> instances.
    /// </summary>
    /// <remarks>
    /// In addition to creating the entity with predefined values, this class defines an <see cref="Install"/> method
    /// that allows injecting additional behaviors, components, or configuration into the newly created entity.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Factories/ScriptableEntityFactory.md")]
    public abstract class ScriptableEntityFactory<TArgs> :
        ScriptableEntityFactory<IEntity, TArgs>, IEntityFactory<TArgs>
        where TArgs : IArgs
    {
        [field: SerializeField]
        public virtual string Name { get; private set; }

        protected override IEntity Create(
            int tagCapacity,
            int valueCapacity,
            int behaviourCapacity,
            Entity.Settings settings,
            TArgs args
        )
        {
            Entity entity = new Entity(
                this.Name,
                tagCapacity,
                valueCapacity,
                behaviourCapacity,
                settings
            );

            this.Install(entity);
            return entity;
        }

        /// <summary>
        /// Applies custom setup logic to the newly created <see cref="IEntity"/> instance.
        /// </summary>
        /// <param name="entity">
        /// The entity instance to configure.  
        /// Called immediately after construction within the <see cref="Create"/> method.
        /// </param>
        /// <remarks>
        /// Implement this method in derived classes to define how data from the 
        /// <see cref="ScriptableObject"/> asset should be applied to the entity.
        /// 
        /// This is typically used to:
        /// <list type="bullet">
        /// <item><description>Add tags, values, or behaviors</description></item>
        /// </list>
        /// </remarks>
        protected abstract void Install(IEntity entity);
    }
}
#endif
#if UNITY_5_3_OR_NEWER
namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for Unity-based factories that create and configure <see cref="Entity"/> instances.
    /// </summary>
    /// <remarks>
    /// In addition to creating the entity with predefined values, this class defines an <see cref="Install"/> method
    /// that allows injecting additional behaviors, components, or configuration into the newly created entity.
    /// </remarks>
    public abstract class ScriptableEntityFactory : ScriptableEntityFactory<IEntity>, IEntityFactory
    {
        /// <summary>
        /// Creates a new <see cref="Entity"/> using the initial parameters defined in the factory
        /// and applies additional configuration via the <see cref="Install"/> method.
        /// </summary>
        /// <returns>A fully constructed and configured <see cref="Entity"/>.</returns>
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

        protected abstract void Install(IEntity entity);
    }
}
#endif
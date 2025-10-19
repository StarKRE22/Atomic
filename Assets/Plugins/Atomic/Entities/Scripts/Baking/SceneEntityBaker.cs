namespace Atomic.Entities
{
    public abstract class SceneEntityBaker : SceneEntityBaker<IEntity>
    {
        /// <summary>
        /// Creates a new <see cref="Entity"/> using the predefined initialization values,
        /// then applies custom logic via the <see cref="Install"/> method.
        /// </summary>
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

        protected abstract void Install(IEntity entity);
    }
}
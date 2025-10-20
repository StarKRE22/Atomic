using Atomic.Entities;

namespace RTSGame
{
    public abstract class UnitFactory : ScriptableEntityFactory<IUnitEntity>
    {
        public string Name => this.name;

        public sealed override IUnitEntity Create()
        {
            var entity = new UnitEntity(
                this.Name,
                this.initialTagCapacity,
                this.initialValueCapacity,
                this.initialBehaviourCapacity
            );
            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IUnitEntity entity);
    }
}
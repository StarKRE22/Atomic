using Atomic.Entities;

namespace RTSGame
{
    public abstract class UnitFactory : ScriptableEntityFactory<IUnit>
    {
        public string Name => this.name;

        public sealed override IUnit Create()
        {
            var entity = new Unit(
                this.Name,
                this.initialTagCapacity,
                this.initialValueCapacity,
                this.initialBehaviourCapacity
            );
            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IUnit entity);
    }
}
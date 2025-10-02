using Atomic.Entities;

namespace RTSGame
{
    public abstract class GameEntityFactory : ScriptableEntityFactory<IGameEntity>
    {
        public string Name => this.name;

        public sealed override IGameEntity Create()
        {
            var entity = new GameEntity(
                this.Name,
                this.initialTagCapacity,
                this.initialValueCapacity,
                this.initialBehaviourCapacity
            );
            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IGameEntity entity);
    }
}
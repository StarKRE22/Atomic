using Atomic.Entities;

namespace RTSGame
{
    public abstract class GameEntityFactory : ScriptableEntityFactory<IGameEntity>
    {
        public sealed override IGameEntity Create()
        {
            GameEntity entity = new GameEntity(
                this.name,
                this.initialTagCount,
                this.initialValueCount,
                this.initialBehaviourCount
            );

            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IGameEntity entity);
    }
}
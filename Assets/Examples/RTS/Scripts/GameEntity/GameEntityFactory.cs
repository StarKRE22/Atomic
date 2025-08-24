using Atomic.Entities;

namespace RTSGame
{
    public class GameEntityFactory : ScriptableEntityFactory<IGameEntity>
    {
        public string Name => this.name;
        
        public override IGameEntity Create() => new GameEntity(
            this.Name,
            this.InitialTagCount,
            this.InitialValueCount,
            this.InitialBehaviourCount
        );
    }
}
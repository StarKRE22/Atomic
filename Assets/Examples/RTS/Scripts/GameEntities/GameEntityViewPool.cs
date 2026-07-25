using Atomic.Entities;

namespace RTSGame
{
    public sealed class GameEntityViewPool : EntityViewPool<GameEntityType, IGameEntity, GameEntityView>
    {
        protected override GameEntityType GetKey(GameEntityView view) => view.Type;
    }
}
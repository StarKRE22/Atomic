using Atomic.Entities;

namespace RTSGame
{
    public sealed class GameEntityWorldView : EntityWorldViewSingleton<GameEntityType, IGameEntity, GameEntityView>
    {
        protected override GameEntityType GetKey(IGameEntity entity) => entity.GetEntityType().Value;
    }
}
using Atomic.Entities;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class GameEntityViewPool : EntityViewPool<GameEntityType, IGameEntity, GameEntityView>
    {
        protected override GameEntityType GetKey(GameEntityView view) => view.Type;
    }
}
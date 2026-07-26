using Atomic.Entities;
using Game.Gameplay;

namespace Game.UI
{
    public sealed class GameEntityCollectionView : EntityCollectionView<GameEntityType, IGameEntity, GameEntityView>
    {
        protected override GameEntityType GetKey(IGameEntity entity) => 
            entity.GetValue(GameEntityAPI.EntityType).Value;
    }
}
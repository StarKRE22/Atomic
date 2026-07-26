using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(
        fileName = "GameEntityCatalog",
        menuName = "Game/Gameplay/GameEntities/GameEntityCatalog"
    )]
    public sealed class GameEntityCatalog : ScriptableEntityCatalog<GameEntityType, IGameEntity, GameEntityFactory, Args<IGameContext>>
    {
        protected override GameEntityType GetKey(GameEntityFactory factory) => factory.Type;
    }
}
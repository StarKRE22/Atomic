using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameEntityCatalog",
        menuName = "RTSGame/GameEntities/New GameEntityCatalog"
    )]
    public sealed class GameEntityCatalog : ScriptableEntityCatalog<GameEntityType, IGameEntity, GameEntityFactory, Args<IGameContext>>
    {
        protected override GameEntityType GetKey(GameEntityFactory factory) => factory.Type;
    }
}
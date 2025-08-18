using BeginnerGame;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameEntityCatalog",
        menuName = "RTSGame/New GameEntityCatalog"
    )]
    public sealed class GameEntityCatalog : ScriptableEntityCatalog<string, IGameEntity, GameEntityFactory>
    {
        protected override string GetKey(GameEntityFactory factory) => factory.name;
    }
}
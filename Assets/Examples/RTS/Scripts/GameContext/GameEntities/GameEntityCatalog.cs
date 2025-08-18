using Atomic.Entities;
using BeginnerGame;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameEntityCatalog",
        menuName = "RTSGame/New GameEntityCatalog"
    )]
    public sealed class GameEntityCatalog : ScriptableEntityCatalog<string, IGameEntity>
    {
        protected override string GetKey(ScriptableEntityFactory<IGameEntity> factory) => factory.name;
    }
}
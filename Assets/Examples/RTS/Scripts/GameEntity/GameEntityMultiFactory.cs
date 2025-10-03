using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameEntityMultiFactory",
        menuName = "RTSGame/GameEntities/New GameEntityMultiFactory"
    )]
    public sealed class GameEntityMultiFactory : ScriptableMultiEntityFactory<string, IGameEntity, GameEntityFactory>
    {
        protected override string GetKey(GameEntityFactory factory) => factory.Name;
    }
}
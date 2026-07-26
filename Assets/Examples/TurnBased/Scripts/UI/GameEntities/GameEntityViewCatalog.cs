using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(
        fileName = "GameEntityViewCatalog",
        menuName = "Game/UI/GameEntityViewCatalog"
    )]
    public sealed class GameEntityViewCatalog : EntityViewCatalog<IGameEntity, GameEntityView>
    {
    }
}
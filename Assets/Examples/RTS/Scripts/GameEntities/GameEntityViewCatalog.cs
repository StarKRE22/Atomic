using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "GameEntityViewCatalog",
        menuName = "RTSGame/Units/New GameEntityViewCatalog"
    )]
    public sealed class GameEntityViewCatalog : EntityViewCatalog<IGameEntity, GameEntityView>
    {
    }
}
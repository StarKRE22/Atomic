using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [RequireComponent(typeof(GameEntityView))]
    public abstract class GameEntityBaker : EntityBakerOptimized<GameEntityType, IGameEntity, GameEntityView, Args<IGameContext>>
    {
    }
}
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [RequireComponent(typeof(GameEntityView))]
    public abstract class GameEntityBaker : MonoEntityBakerOptimized<GameEntityType, IGameEntity, GameEntityView, Args<IGameContext>>
    {
    }
}
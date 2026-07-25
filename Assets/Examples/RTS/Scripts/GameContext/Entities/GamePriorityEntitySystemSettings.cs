using System;
using Atomic.Entities;

namespace RTSGame
{
    [Serializable]
    public sealed class GamePriorityEntitySystemSettings : PriorityEntitySystem<IGameEntity>.Settings
    {
    }
}

using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [EntityAPI]
    public static partial class PlayerContextAPI
    {
        public static readonly ValueKey<IPlayerContext, IGameEntity> Character = new(nameof(Character));
        public static readonly ValueKey<IPlayerContext, IValue<TeamType>> TeamType = new(nameof(TeamType));
        public static readonly ValueKey<IPlayerContext, InputMap> InputMap = new(nameof(InputMap));
        public static readonly ValueKey<IPlayerContext, Camera> Camera = new(nameof(Camera));
    }
}

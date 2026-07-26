using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public readonly struct PushedEventArgs
    {
        public readonly IGameEntity target;
        public readonly Vector2Int startPosition;
        public readonly Vector2Int endPosition;
        public readonly Vector2Int direction;

        public PushedEventArgs(
            IGameEntity target,
            Vector2Int startPosition,
            Vector2Int endPosition,
            Vector2Int direction)
        {
            this.target = target;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.direction = direction;
        }
    }
}
using UnityEngine;

namespace Game.Gameplay
{
    public readonly struct PushOutEventArgs
    {
        public readonly IGameEntity target;
        public readonly Vector2Int position;
        public readonly Vector2Int direction;

        public PushOutEventArgs(IGameEntity target, Vector2Int position, Vector2Int direction)
        {
            this.target = target;
            this.position = position;
            this.direction = direction;
        }
    }
}
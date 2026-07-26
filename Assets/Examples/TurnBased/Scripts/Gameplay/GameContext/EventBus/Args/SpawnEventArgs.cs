using UnityEngine;

namespace Game.Gameplay
{
    public readonly struct SpawnEventArgs
    {
        public readonly IGameEntity entity;
        public readonly Vector2Int position;

        public SpawnEventArgs(IGameEntity entity, Vector2Int position)
        {
            this.entity = entity;
            this.position = position;
        }
    }
}
using UnityEngine;

namespace Game.Gameplay
{
    public struct MoveEventArgs
    {
        public IGameEntity entity;
        public Vector2Int prevPosition;
        public Vector2Int newPosition;

        public MoveEventArgs(IGameEntity entity, Vector2Int prevPosition, Vector2Int newPosition)
        {
            this.entity = entity;
            this.prevPosition = prevPosition;
            this.newPosition = newPosition;
        }
    }
}
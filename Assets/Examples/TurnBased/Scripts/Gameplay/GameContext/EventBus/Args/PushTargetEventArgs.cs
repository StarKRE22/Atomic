using UnityEngine;

namespace Game.Gameplay
{
    public readonly struct PushTargetEventArgs
    {
        public readonly IGameEntity source;
        public readonly IGameEntity target;
        public readonly Vector2Int sourcePosition;
        public readonly Vector2Int targetPosition;
        public readonly Vector2Int pushPosition;

        public PushTargetEventArgs(
            IGameEntity source,
            IGameEntity target,
            Vector2Int sourcePosition,
            Vector2Int targetPosition,
            Vector2Int pushPosition
        )
        {
            this.source = source;
            this.target = target;
            this.sourcePosition = sourcePosition;
            this.targetPosition = targetPosition;
            this.pushPosition = pushPosition;
        }
    }
}